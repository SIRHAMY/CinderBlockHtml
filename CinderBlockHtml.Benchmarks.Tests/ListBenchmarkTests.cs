using CinderBlockHtml.Benchmarks;
using Xunit;

namespace CinderBlockHtml.Benchmarks.Tests;

public class ListBenchmarkTests
{
    private readonly ListBenchmark _benchmark = new();

    [Fact]
    public void CinderBlockHtml_Contains100ListItems()
    {
        var html = _benchmark.CinderBlockHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<li>"));
        Assert.Equal(100, CountOccurrences(html, "</li>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Contains("<title>Test Items</title>", html);
        Assert.Contains("<h1>Test Items</h1>", html);
        Assert.Contains("<ul>", html);
        Assert.Contains("</ul>", html);
    }

    [Fact]
    public void RawStringHtml_Contains100ListItems()
    {
        var html = _benchmark.RawStringHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<li>"));
        Assert.Equal(100, CountOccurrences(html, "</li>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Contains("<title>Test Items</title>", html);
        Assert.Contains("<h1>Test Items</h1>", html);
        Assert.Contains("<ul>", html);
        Assert.Contains("</ul>", html);
    }

    [Fact]
    public void HtmlTagsHtml_Contains100ListItems()
    {
        var html = _benchmark.HtmlTagsHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<li>"));
        Assert.Equal(100, CountOccurrences(html, "</li>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Contains("<title>Test Items</title>", html);
        Assert.Contains("<h1>Test Items</h1>", html);
        Assert.Contains("<ul>", html);
        Assert.Contains("</ul>", html);
    }

    [Fact]
    public void ScribanHtml_Contains100ListItems()
    {
        var html = _benchmark.ScribanHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<li>"));
        Assert.Equal(100, CountOccurrences(html, "</li>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Contains("<title>Test Items</title>", html);
        Assert.Contains("<h1>Test Items</h1>", html);
        Assert.Contains("<ul>", html);
        Assert.Contains("</ul>", html);
    }

    [Fact]
    public void RazorLightHtml_Contains100ListItems()
    {
        var html = _benchmark.RazorLightHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<li>"));
        Assert.Equal(100, CountOccurrences(html, "</li>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Contains("<title>Test Items</title>", html);
        Assert.Contains("<h1>Test Items</h1>", html);
        Assert.Contains("<ul>", html);
        Assert.Contains("</ul>", html);
    }

    [Fact]
    public void AllMethods_ProduceEquivalentStructure()
    {
        var cinderBlockHtml = _benchmark.CinderBlockHtml();
        var rawStringHtml = _benchmark.RawStringHtml();
        var htmlTagsHtml = _benchmark.HtmlTagsHtml();
        var scribanHtml = _benchmark.ScribanHtml();
        var razorLightHtml = _benchmark.RazorLightHtml();
        
        // All should have same number of list items
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<li>"), CountOccurrences(rawStringHtml, "<li>"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<li>"), CountOccurrences(htmlTagsHtml, "<li>"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<li>"), CountOccurrences(scribanHtml, "<li>"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<li>"), CountOccurrences(razorLightHtml, "<li>"));
        
        // All should have same basic structure
        string[] allHtml = [cinderBlockHtml, rawStringHtml, htmlTagsHtml, scribanHtml, razorLightHtml];
        foreach (var html in allHtml)
        {
            Assert.Contains("<html>", html);
            Assert.Contains("</html>", html);
            Assert.Contains("<head>", html);
            Assert.Contains("</head>", html);
            Assert.Contains("<title>Test Items</title>", html);
            Assert.Contains("<body>", html);
            Assert.Contains("</body>", html);
            Assert.Contains("<h1>Test Items</h1>", html);
            Assert.Contains("<ul>", html);
            Assert.Contains("</ul>", html);
        }
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
}