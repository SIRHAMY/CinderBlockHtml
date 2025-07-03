using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CinderBlockHtml;
using Eighty;
using HtmlTags;
using RazorLight;
using Scriban;

namespace CinderBlockHtml.Benchmarks;

[MemoryDiagnoser]
[SimpleJob]
public class HelloWorldBenchmark
{
    private const string HelloWorldString = "Hello, World!";
    private const string DescriptionString = "This is a simple hello world page used for benchmarks.";
    private const string WelcomeString = "Welcome to our benchmark test!";
    
    private static readonly Template ScribanTemplate = Template.Parse(@"
<html>
    <head>
        <title>{{ hello_world }}</title>
    </head>
    <body>
        <h1>{{ hello_world }}</h1>
        <p>{{ description }}</p>
        <div class=""container"">
            <p>{{ welcome }}</p>
        </div>
    </body>
</html>");
    
    private static readonly RazorLightEngine RazorEngine = new RazorLightEngineBuilder()
        .UseMemoryCachingProvider()
        .Build();
    
    private const string RazorTemplate = @"
<html>
    <head>
        <title>@Model.HelloWorld</title>
    </head>
    <body>
        <h1>@Model.HelloWorld</h1>
        <p>@Model.Description</p>
        <div class=""container"">
            <p>@Model.Welcome</p>
        </div>
    </body>
</html>";

    [Benchmark]
    public string CinderBlockHtml()
    {
        var html = Elem.Html(Attr.Empty(), [
            Elem.Head(Attr.Empty(), [
                Elem.Title(Attr.Empty(), [
                    Text.Raw(HelloWorldString)
                ])
            ]),
            Elem.Body(Attr.Empty(), [
                Elem.H1([
                    Text.Raw(HelloWorldString)
                ]),
                Elem.P([
                    Text.Raw(DescriptionString)
                ]),
                Elem.Div([Attr.Class("container")], [
                    Elem.P([
                        Text.Raw(WelcomeString)
                    ])
                ])
            ])
        ]);

        return Renderer.RenderToString(html);
    }

    [Benchmark]
    public string RawStringHtml()
    {
        return $"""
            <html>
                <head>
                    <title>{HelloWorldString}</title>
                </head>
                <body>
                    <h1>{HelloWorldString}</h1>
                    <p>{DescriptionString}</p>
                    <div class="container">
                        <p>{WelcomeString}</p>
                    </div>
                </body>
            </html>
            """;
    }

    [Benchmark]
    public string HtmlTagsHtml()
    {
        return new HtmlTag("html")
            .Append(new HtmlTag("head")
                .Append(new HtmlTag("title").Text(HelloWorldString)))
            .Append(new HtmlTag("body")
                .Append(new HtmlTag("h1").Text(HelloWorldString))
                .Append(new HtmlTag("p").Text(DescriptionString))
                .Append(new HtmlTag("div")
                    .AddClass("container")
                    .Append(new HtmlTag("p").Text(WelcomeString))))
            .ToString();
    }

    [Benchmark]
    public string ScribanHtml()
    {
        return ScribanTemplate.Render(new
        {
            hello_world = HelloWorldString,
            description = DescriptionString,
            welcome = WelcomeString
        });
    }
    
    [Benchmark]
    public string RazorLightHtml()
    {
        return RazorEngine.CompileRenderStringAsync("helloworld", RazorTemplate, new
        {
            HelloWorld = HelloWorldString,
            Description = DescriptionString,
            Welcome = WelcomeString
        }).Result;
    }
    
    [Benchmark]
    public string EightyHtml()
    {
        return Html.html_(
            Html.head_(
                Html.title_(HelloWorldString)
            ),
            Html.body_(
                Html.h1_(HelloWorldString),
                Html.p_(DescriptionString),
                Html.div(@class: "container")._(
                    Html.p_(WelcomeString)
                )
            )
        ).ToString();
    }
}