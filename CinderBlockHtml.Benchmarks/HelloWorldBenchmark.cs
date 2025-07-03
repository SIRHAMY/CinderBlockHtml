using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CinderBlockHtml;
using HtmlTags;

namespace CinderBlockHtml.Benchmarks;

[MemoryDiagnoser]
[SimpleJob]
public class HelloWorldBenchmark
{
    private const string HelloWorldString = "Hello, World!";
    private const string DescriptionString = "This is a simple hello world page used for benchmarks.";
    private const string WelcomeString = "Welcome to our benchmark test!";

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
        var html = new HtmlTag("html");
        
        var head = new HtmlTag("head");
        var title = new HtmlTag("title").Text(HelloWorldString);
        head.Append(title);
        
        var body = new HtmlTag("body");
        var h1 = new HtmlTag("h1").Text(HelloWorldString);
        var p1 = new HtmlTag("p").Text(DescriptionString);
        
        var div = new HtmlTag("div").AddClass("container");
        var p2 = new HtmlTag("p").Text(WelcomeString);
        div.Append(p2);
        
        body.Append(h1);
        body.Append(p1);
        body.Append(div);
        
        html.Append(head);
        html.Append(body);
        
        return html.ToString();
    }
}