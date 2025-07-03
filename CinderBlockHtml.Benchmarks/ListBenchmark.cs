using BenchmarkDotNet.Attributes;
using CinderBlockHtml;
using Eighty;
using HtmlTags;
using RazorLight;
using Scriban;
using System.Linq;
using System.Text;

namespace CinderBlockHtml.Benchmarks;

public record TestItem(Guid Id, int RandomNumber);

[MemoryDiagnoser]
[SimpleJob]
public class ListBenchmark
{
    public const int TEST_ITEM_COUNT = 100;
    private readonly List<TestItem> _testItems;
    
    private static readonly RazorLightEngine RazorEngine = new RazorLightEngineBuilder()
        .UseMemoryCachingProvider()
        .Build();
    
    private static readonly Template ScribanTemplate = Template.Parse(@"
<html>
    <head>
        <title>Test Items</title>
    </head>
    <body>
        <h1>Test Items</h1>
        <ul>
        {{~ for item in items ~}}
            <li>ID: {{ item.id }}, Number: {{ item.random_number }}</li>
        {{~ end ~}}
        </ul>
    </body>
</html>");
    
    private const string RazorTemplate = @"
<html>
    <head>
        <title>Test Items</title>
    </head>
    <body>
        <h1>Test Items</h1>
        <ul>
        @foreach(var item in Model.Items)
        {
            <li>ID: @item.Id, Number: @item.RandomNumber</li>
        }
        </ul>
    </body>
</html>";

    public ListBenchmark()
    {
        var random = new Random();
        _testItems = Enumerable.Range(0, TEST_ITEM_COUNT)
            .Select(i => new TestItem(Guid.NewGuid(), random.Next(1, 1000)))
            .ToList();
    }

    [Benchmark]
    public string CinderBlockHtml()
    {
        var listItems = _testItems.Select(item => 
            Elem.Li([
                Text.Raw($"ID: {item.Id}, Number: {item.RandomNumber}")
            ])
        ).ToArray();

        var html = Elem.Html(Attr.Empty(), [
            Elem.Head(Attr.Empty(), [
                Elem.Title(Attr.Empty(), [
                    Text.Raw("Test Items")
                ])
            ]),
            Elem.Body(Attr.Empty(), [
                Elem.H1([
                    Text.Raw("Test Items")
                ]),
                Elem.Ul(listItems)
            ])
        ]);

        return Renderer.RenderToString(html);
    }

    [Benchmark]
    public string RawStringHtml()
    {
        var listItems = _testItems.Select(item => 
            $"            <li>ID: {item.Id}, Number: {item.RandomNumber}</li>"
        ).ToList();
        
        var itemsString = string.Join("\n", listItems);
        
        return $"""
            <html>
                <head>
                    <title>Test Items</title>
                </head>
                <body>
                    <h1>Test Items</h1>
                    <ul>
                        {itemsString}
                    </ul>
                </body>
            </html>
            """;
    }

    [Benchmark]
    public string HtmlTagsHtml()
    {
        var ul = new HtmlTag("ul");
        
        foreach (var item in _testItems)
        {
            ul.Append(new HtmlTag("li").Text($"ID: {item.Id}, Number: {item.RandomNumber}"));
        }

        return new HtmlTag("html")
            .Append(new HtmlTag("head")
                .Append(new HtmlTag("title").Text("Test Items")))
            .Append(new HtmlTag("body")
                .Append(new HtmlTag("h1").Text("Test Items"))
                .Append(ul))
            .ToString();
    }

    [Benchmark]
    public string ScribanHtml()
    {
        var model = new
        {
            items = _testItems.Select(item => new
            {
                id = item.Id,
                random_number = item.RandomNumber
            })
        };
        
        return ScribanTemplate.Render(model);
    }

    [Benchmark]
    public string RazorLightHtml()
    {
        var model = new
        {
            Items = _testItems
        };
        
        return RazorEngine.CompileRenderStringAsync("listitems", RazorTemplate, model).Result;
    }

    [Benchmark]
    public string EightyHtml()
    {
        var listItems = _testItems.Select(item =>
            Html.li_($"ID: {item.Id}, Number: {item.RandomNumber}")
        ).ToArray();

        return Html.html_(
            Html.head_(
                Html.title_("Test Items")
            ),
            Html.body_(
                Html.h1_("Test Items"),
                Html.ul_(listItems)
            )
        ).ToString();
    }
}