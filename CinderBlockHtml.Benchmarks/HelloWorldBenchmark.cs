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
        var html = Elem.Html(Attr.Empty(), new[]
        {
            Elem.Head(Attr.Empty(), new[]
            {
                Elem.Title(Attr.Empty(),
                [
                    Text.Raw("Hello World")
                ])
            }),
            Elem.Body(Attr.Empty(), new[]
            {
                Elem.H1(new[]
                {
                    Text.Raw("Hello World!")
                }),
                Elem.P(new[]
                {
                    Text.Raw("This is a simple hello world page generated with CinderBlock.")
                }),
                Elem.Div(new[] { Attr.Class("container") }, new[]
                {
                    Elem.P(new[]
                    {
                        Text.Raw("Welcome to our benchmark test!")
                    })
                })
            })
        });

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