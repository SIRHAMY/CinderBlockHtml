using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CinderBlockHtml;

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
}