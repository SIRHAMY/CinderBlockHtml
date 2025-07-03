using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CinderBlockHtml;

namespace CinderBlockHtml.Benchmarks;

[MemoryDiagnoser]
[SimpleJob]
public class HelloWorldBenchmark
{
    [Benchmark]
    public string CinderBlockHtml()
    {
        var html = Elem.Html(Attr.Empty(), [
            Elem.Head(Attr.Empty(), [
                Elem.Title(Attr.Empty(), [
                    Text.Raw("Hello World")
                ])
            ]),
            Elem.Body(Attr.Empty(), [
                Elem.H1([
                    Text.Raw("Hello World!")
                ]),
                Elem.P([
                    Text.Raw("This is a simple hello world page generated with CinderBlock.")
                ]),
                Elem.Div([Attr.Class("container")], [
                    Elem.P([
                        Text.Raw("Welcome to our benchmark test!")
                    ])
                ])
            ])
        ]);

        return Renderer.RenderToString(html);
    }

    [Benchmark]
    public string RawStringHtml()
    {
        return """
               <html>
               <head>
               <title>Hello World</title>
               </head>
               <body>
               <h1>Hello World!</h1>
               <p>This is a simple hello world page generated with CinderBlock.</p>
               <div class="container">
               <p>Welcome to our benchmark test!</p>
               </div>
               </body>
               </html>
               """;
    }
}