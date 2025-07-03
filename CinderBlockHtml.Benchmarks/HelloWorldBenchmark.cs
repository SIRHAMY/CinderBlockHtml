using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CinderBlockHtml;
using HtmlTags;
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
}