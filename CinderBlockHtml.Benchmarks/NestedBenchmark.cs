using BenchmarkDotNet.Attributes;
using CinderBlockHtml;
using Eighty;
using HtmlTags;
using RazorLight;
using Scriban;
using System.Text;

namespace CinderBlockHtml.Benchmarks;

[MemoryDiagnoser]
[SimpleJob]
public class NestedBenchmark
{
    public const int TEST_ITEM_COUNT = 100;
    private readonly List<TestItem> _testItems;
    
    private static readonly RazorLightEngine RazorEngine = new RazorLightEngineBuilder()
        .UseMemoryCachingProvider()
        .Build();
    
    private static readonly Template ScribanTemplate = Template.Parse(@"
<html>
    <head>
        <title>Nested Test Items</title>
    </head>
    <body>
        <h1>Nested Test Items</h1>
        {{ nested_html }}
    </body>
</html>");
    
    private const string RazorTemplate = @"
<html>
    <head>
        <title>Nested Test Items</title>
    </head>
    <body>
        <h1>Nested Test Items</h1>
        @Raw(Model.NestedHtml)
    </body>
</html>";

    public NestedBenchmark()
    {
        var random = new Random(42); // Fixed seed for reproducible results
        _testItems = [];
        
        for (int i = 0; i < TEST_ITEM_COUNT; i++)
        {
            _testItems.Add(new TestItem(Guid.NewGuid(), random.Next(1, 1000)));
        }
    }

    [Benchmark]
    public string CinderBlockHtml()
    {
        XmlNode BuildNestedDivs(int index)
        {
            if (index >= _testItems.Count)
                return new TextNode(""); // Empty text node
            
            var item = _testItems[index];
            var nextDiv = BuildNestedDivs(index + 1);
            
            // Only include next div if it's not empty
            var children = nextDiv is TextNode textNode && string.IsNullOrEmpty(textNode.Content)
                ? new XmlNode[] { Elem.P([Text.Raw($"ID: {item.Id}, Number: {item.RandomNumber}")]) }
                : new XmlNode[] { Elem.P([Text.Raw($"ID: {item.Id}, Number: {item.RandomNumber}")]), nextDiv };
            
            return Elem.Div([Attr.Class($"item-{index}")], children);
        }

        var html = Elem.Html(Attr.Empty(), [
            Elem.Head(Attr.Empty(), [
                Elem.Title(Attr.Empty(), [
                    Text.Raw("Nested Test Items")
                ])
            ]),
            Elem.Body(Attr.Empty(), [
                Elem.H1([
                    Text.Raw("Nested Test Items")
                ]),
                BuildNestedDivs(0)
            ])
        ]);

        return Renderer.RenderToString(html);
    }

    [Benchmark]
    public string RawStringHtml()
    {
        string BuildNestedDivs(int index)
        {
            if (index >= _testItems.Count)
                return "";
            
            var item = _testItems[index];
            
            return $"""
                <div class="item-{index}">
                    <p>ID: {item.Id}, Number: {item.RandomNumber}</p>
                    {BuildNestedDivs(index + 1)}
                </div>
                """;
        }
        
        var nestedDivs = BuildNestedDivs(0);
        
        return $"""
            <html>
                <head>
                    <title>Nested Test Items</title>
                </head>
                <body>
                    <h1>Nested Test Items</h1>
                    {nestedDivs}    
                </body>
            </html>
            """;
    }

    [Benchmark]
    public string HtmlTagsHtml()
    {
        HtmlTag? BuildNestedDivs(int index)
        {
            if (index >= _testItems.Count)
                return null; // No more items
            
            var item = _testItems[index];
            var div = new HtmlTag("div")
                .AddClass($"item-{index}")
                .Append(new HtmlTag("p").Text($"ID: {item.Id}, Number: {item.RandomNumber}"));
            
            var nextDiv = BuildNestedDivs(index + 1);
            if (nextDiv != null) // Only append if not null
            {
                div.Append(nextDiv);
            }
            
            return div;
        }

        var bodyTag = new HtmlTag("body")
            .Append(new HtmlTag("h1").Text("Nested Test Items"));
        
        var nestedDivs = BuildNestedDivs(0);
        if (nestedDivs != null)
        {
            bodyTag.Append(nestedDivs);
        }

        return new HtmlTag("html")
            .Append(new HtmlTag("head")
                .Append(new HtmlTag("title").Text("Nested Test Items")))
            .Append(bodyTag)
            .ToString();
    }

    [Benchmark]
    public string ScribanHtml()
    {
        string BuildNestedHtml(int index)
        {
            if (index >= _testItems.Count)
                return "";
            
            var item = _testItems[index];
            
            return $"""
                <div class="item-{index}">
                    <p>ID: {item.Id}, Number: {item.RandomNumber}</p>
                    {BuildNestedHtml(index + 1)}
                </div>
                """;
        }
        
        var model = new
        {
            nested_html = BuildNestedHtml(0)
        };
        
        return ScribanTemplate.Render(model);
    }

    [Benchmark]
    public string RazorLightHtml()
    {
        string BuildNestedHtml(int index)
        {
            if (index >= _testItems.Count)
                return "";
            
            var item = _testItems[index];
            
            return $"""
                <div class="item-{index}">
                    <p>ID: {item.Id}, Number: {item.RandomNumber}</p>
                    {BuildNestedHtml(index + 1)}
                </div>
                """;
        }
        
        var model = new
        {
            NestedHtml = BuildNestedHtml(0)
        };
        
        return RazorEngine.CompileRenderStringAsync("nesteditems", RazorTemplate, model).Result;
    }

    [Benchmark]
    public string EightyHtml()
    {
        Html BuildNestedDivs(int index)
        {
            if (index >= _testItems.Count)
                return Html.Empty;
            
            var item = _testItems[index];
            var nextDiv = BuildNestedDivs(index + 1);
            
            return Html.div(@class: $"item-{index}")._(
                Html.p_($"ID: {item.Id}, Number: {item.RandomNumber}"),
                nextDiv
            );
        }

        return Html.html_(
            Html.head_(
                Html.title_("Nested Test Items")
            ),
            Html.body_(
                Html.h1_("Nested Test Items"),
                BuildNestedDivs(0)
            )
        ).ToString();
    }
}