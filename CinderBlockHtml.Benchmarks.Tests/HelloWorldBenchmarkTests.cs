using CinderBlockHtml.Benchmarks;
using Xunit;

namespace CinderBlockHtml.Benchmarks.Tests;

public class HelloWorldBenchmarkTests
{
    private readonly HelloWorldBenchmark _benchmark = new();

    [Fact]
    public void CinderBlockHtml_ContainsExpectedStringCounts()
    {
        var html = _benchmark.CinderBlockHtml();
        
        Assert.Equal(2, CountOccurrences(html, "Hello, World!"));
        Assert.Equal(1, CountOccurrences(html, "This is a simple hello world page used for benchmarks."));
        Assert.Equal(1, CountOccurrences(html, "Welcome to our benchmark test!"));
        Assert.Equal(1, CountOccurrences(html, "class=\"container\""));
        Assert.Equal(2, CountOccurrences(html, "<p>"));
        Assert.Equal(2, CountOccurrences(html, "</p>"));
    }

    [Fact]
    public void HtmlTags_ContainsExpectedStrings()
    {
        var html = _benchmark.HtmlTagsHtml();
        
        Assert.Contains("Hello, World!", html);
        Assert.Contains("This is a simple hello world page used for benchmarks.", html);
        Assert.Contains("Welcome to our benchmark test!", html);
        Assert.Contains("class=\"container\"", html);
    }

    [Fact]
    public void HtmlTags_ContainsExpectedHtmlElements()
    {
        var html = _benchmark.HtmlTagsHtml();
        
        Assert.Contains("<html>", html);
        Assert.Contains("</html>", html);
        Assert.Contains("<head>", html);
        Assert.Contains("</head>", html);
        Assert.Contains("<title>", html);
        Assert.Contains("</title>", html);
        Assert.Contains("<body>", html);
        Assert.Contains("</body>", html);
        Assert.Contains("<h1>", html);
        Assert.Contains("</h1>", html);
        Assert.Contains("<p>", html);
        Assert.Contains("</p>", html);
        Assert.Contains("<div", html);
        Assert.Contains("</div>", html);
    }

    [Fact]
    public void HtmlTags_ContainsExpectedStringCounts()
    {
        var html = _benchmark.HtmlTagsHtml();
        
        Assert.Equal(2, CountOccurrences(html, "Hello, World!"));
        Assert.Equal(1, CountOccurrences(html, "This is a simple hello world page used for benchmarks."));
        Assert.Equal(1, CountOccurrences(html, "Welcome to our benchmark test!"));
        Assert.Equal(1, CountOccurrences(html, "class=\"container\""));
        Assert.Equal(2, CountOccurrences(html, "<p>"));
        Assert.Equal(2, CountOccurrences(html, "</p>"));
    }

    [Fact]
    public void ScribanHtml_ContainsExpectedStrings()
    {
        var html = _benchmark.ScribanHtml();
        
        Assert.Contains("Hello, World!", html);
        Assert.Contains("This is a simple hello world page used for benchmarks.", html);
        Assert.Contains("Welcome to our benchmark test!", html);
        Assert.Contains("class=\"container\"", html);
    }

    [Fact]
    public void ScribanHtml_ContainsExpectedHtmlElements()
    {
        var html = _benchmark.ScribanHtml();
        
        Assert.Contains("<html>", html);
        Assert.Contains("</html>", html);
        Assert.Contains("<head>", html);
        Assert.Contains("</head>", html);
        Assert.Contains("<title>", html);
        Assert.Contains("</title>", html);
        Assert.Contains("<body>", html);
        Assert.Contains("</body>", html);
        Assert.Contains("<h1>", html);
        Assert.Contains("</h1>", html);
        Assert.Contains("<p>", html);
        Assert.Contains("</p>", html);
        Assert.Contains("<div", html);
        Assert.Contains("</div>", html);
    }

    [Fact]
    public void ScribanHtml_ContainsExpectedStringCounts()
    {
        var html = _benchmark.ScribanHtml();
        
        Assert.Equal(2, CountOccurrences(html, "Hello, World!"));
        Assert.Equal(1, CountOccurrences(html, "This is a simple hello world page used for benchmarks."));
        Assert.Equal(1, CountOccurrences(html, "Welcome to our benchmark test!"));
        Assert.Equal(1, CountOccurrences(html, "class=\"container\""));
        Assert.Equal(2, CountOccurrences(html, "<p>"));
        Assert.Equal(2, CountOccurrences(html, "</p>"));
    }

    [Fact]
    public void RazorLightHtml_ContainsExpectedStrings()
    {
        var html = _benchmark.RazorLightHtml();
        
        Assert.Contains("Hello, World!", html);
        Assert.Contains("This is a simple hello world page used for benchmarks.", html);
        Assert.Contains("Welcome to our benchmark test!", html);
        Assert.Contains("class=\"container\"", html);
    }

    [Fact]
    public void RazorLightHtml_ContainsExpectedHtmlElements()
    {
        var html = _benchmark.RazorLightHtml();
        
        Assert.Contains("<html>", html);
        Assert.Contains("</html>", html);
        Assert.Contains("<head>", html);
        Assert.Contains("</head>", html);
        Assert.Contains("<title>", html);
        Assert.Contains("</title>", html);
        Assert.Contains("<body>", html);
        Assert.Contains("</body>", html);
        Assert.Contains("<h1>", html);
        Assert.Contains("</h1>", html);
        Assert.Contains("<p>", html);
        Assert.Contains("</p>", html);
        Assert.Contains("<div", html);
        Assert.Contains("</div>", html);
    }

    [Fact]
    public void RazorLightHtml_ContainsExpectedStringCounts()
    {
        var html = _benchmark.RazorLightHtml();
        
        Assert.Equal(2, CountOccurrences(html, "Hello, World!"));
        Assert.Equal(1, CountOccurrences(html, "This is a simple hello world page used for benchmarks."));
        Assert.Equal(1, CountOccurrences(html, "Welcome to our benchmark test!"));
        Assert.Equal(1, CountOccurrences(html, "class=\"container\""));
        Assert.Equal(2, CountOccurrences(html, "<p>"));
        Assert.Equal(2, CountOccurrences(html, "</p>"));
    }

    [Fact]
    public void RawStringHtml_ContainsExpectedStringCounts()
    {
        var html = _benchmark.RawStringHtml();
        
        Assert.Equal(2, CountOccurrences(html, "Hello, World!"));
        Assert.Equal(1, CountOccurrences(html, "This is a simple hello world page used for benchmarks."));
        Assert.Equal(1, CountOccurrences(html, "Welcome to our benchmark test!"));
        Assert.Equal(1, CountOccurrences(html, "class=\"container\""));
        Assert.Equal(2, CountOccurrences(html, "<p>"));
        Assert.Equal(2, CountOccurrences(html, "</p>"));
    }

    [Fact]
    public void AllMethods_ProduceEquivalentContent()
    {
        var cinderBlockHtml = _benchmark.CinderBlockHtml();
        var rawStringHtml = _benchmark.RawStringHtml();
        var htmlTagsHtml = _benchmark.HtmlTagsHtml();
        var scribanHtml = _benchmark.ScribanHtml();
        var razorLightHtml = _benchmark.RazorLightHtml();
        
        // All outputs should have the same content when whitespace is normalized
        var normalizedCinderBlock = NormalizeWhitespace(cinderBlockHtml);
        var normalizedRawString = NormalizeWhitespace(rawStringHtml);
        var normalizedHtmlTags = NormalizeWhitespace(htmlTagsHtml);
        var normalizedScriban = NormalizeWhitespace(scribanHtml);
        var normalizedRazorLight = NormalizeWhitespace(razorLightHtml);
        
        Assert.Equal(normalizedCinderBlock, normalizedRawString);
        Assert.Equal(normalizedCinderBlock, normalizedHtmlTags);
        Assert.Equal(normalizedCinderBlock, normalizedScriban);
        Assert.Equal(normalizedCinderBlock, normalizedRazorLight);

    }

    private static int CountOccurrences(string text, string pattern)
    {
        int count = 0;
        int index = 0;
        
        while ((index = text.IndexOf(pattern, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += pattern.Length;
        }
        
        return count;
    }

    private static string NormalizeWhitespace(string html)
    {
        // Remove all whitespace between tags and normalize internal whitespace
        return System.Text.RegularExpressions.Regex.Replace(html.Trim(), @">\s+<", "><")
            .Replace("\n", "")
            .Replace("\r", "")
            .Replace("\t", "")
            .Replace("    ", "")
            .Replace("  ", " ")
            .Trim();
    }
}