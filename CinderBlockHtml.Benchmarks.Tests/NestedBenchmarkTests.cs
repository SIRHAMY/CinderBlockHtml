using CinderBlockHtml.Benchmarks;
using Xunit;

namespace CinderBlockHtml.Benchmarks.Tests;

public class NestedBenchmarkTests
{
    private readonly NestedBenchmark _benchmark = new();

    [Fact]
    public void CinderBlockHtml_Contains100NestedDivs()
    {
        var html = _benchmark.CinderBlockHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<div"));
        Assert.Equal(100, CountOccurrences(html, "</div>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Equal(100, CountOccurrences(html, "<p>"));
        Assert.Equal(100, CountOccurrences(html, "</p>"));
        Assert.Contains("<title>Nested Test Items</title>", html);
        Assert.Contains("<h1>Nested Test Items</h1>", html);
        
        // Check for proper nesting by verifying class attributes
        for (int i = 0; i < 10; i++) // Check first 10 items
        {
            Assert.Contains($"class=\"item-{i}\"", html);
        }
    }

    [Fact]
    public void RawStringHtml_Contains100NestedDivs()
    {
        var html = _benchmark.RawStringHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<div"));
        Assert.Equal(100, CountOccurrences(html, "</div>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Equal(100, CountOccurrences(html, "<p>"));
        Assert.Equal(100, CountOccurrences(html, "</p>"));
        Assert.Contains("<title>Nested Test Items</title>", html);
        Assert.Contains("<h1>Nested Test Items</h1>", html);
        
        // Check for proper nesting by verifying class attributes
        for (int i = 0; i < 10; i++) // Check first 10 items
        {
            Assert.Contains($"class=\"item-{i}\"", html);
        }
    }

    [Fact]
    public void HtmlTagsHtml_Contains100NestedDivs()
    {
        var html = _benchmark.HtmlTagsHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<div"));
        Assert.Equal(100, CountOccurrences(html, "</div>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Equal(100, CountOccurrences(html, "<p>"));
        Assert.Equal(100, CountOccurrences(html, "</p>"));
        Assert.Contains("<title>Nested Test Items</title>", html);
        Assert.Contains("<h1>Nested Test Items</h1>", html);
        
        // Check for proper nesting by verifying class attributes
        for (int i = 0; i < 10; i++) // Check first 10 items
        {
            Assert.Contains($"class=\"item-{i}\"", html);
        }
    }

    [Fact]
    public void ScribanHtml_Contains100NestedDivs()
    {
        var html = _benchmark.ScribanHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<div"));
        Assert.Equal(100, CountOccurrences(html, "</div>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Equal(100, CountOccurrences(html, "<p>"));
        Assert.Equal(100, CountOccurrences(html, "</p>"));
        Assert.Contains("<title>Nested Test Items</title>", html);
        Assert.Contains("<h1>Nested Test Items</h1>", html);
        
        // Check for proper nesting by verifying class attributes
        for (int i = 0; i < 10; i++) // Check first 10 items
        {
            Assert.Contains($"class=\"item-{i}\"", html);
        }
    }

    [Fact]
    public void RazorLightHtml_Contains100NestedDivs()
    {
        var html = _benchmark.RazorLightHtml();
        
        Assert.Equal(100, CountOccurrences(html, "<div"));
        Assert.Equal(100, CountOccurrences(html, "</div>"));
        Assert.Equal(100, CountOccurrences(html, "ID: "));
        Assert.Equal(100, CountOccurrences(html, "Number: "));
        Assert.Equal(100, CountOccurrences(html, "<p>"));
        Assert.Equal(100, CountOccurrences(html, "</p>"));
        Assert.Contains("<title>Nested Test Items</title>", html);
        Assert.Contains("<h1>Nested Test Items</h1>", html);
        
        // Check for proper nesting by verifying class attributes
        for (int i = 0; i < 10; i++) // Check first 10 items
        {
            Assert.Contains($"class=\"item-{i}\"", html);
        }
    }

    [Fact]
    public void AllMethods_ProduceEquivalentStructure()
    {
        var cinderBlockHtml = _benchmark.CinderBlockHtml();
        var rawStringHtml = _benchmark.RawStringHtml();
        var htmlTagsHtml = _benchmark.HtmlTagsHtml();
        var scribanHtml = _benchmark.ScribanHtml();
        var razorLightHtml = _benchmark.RazorLightHtml();
        
        // All should have same number of nested divs
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<div"), CountOccurrences(rawStringHtml, "<div"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<div"), CountOccurrences(htmlTagsHtml, "<div"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<div"), CountOccurrences(scribanHtml, "<div"));
        Assert.Equal(CountOccurrences(cinderBlockHtml, "<div"), CountOccurrences(razorLightHtml, "<div"));
        
        // All should have same basic structure
        string[] allHtml = [cinderBlockHtml, rawStringHtml, htmlTagsHtml, scribanHtml, razorLightHtml];
        foreach (var html in allHtml)
        {
            Assert.Contains("<html>", html);
            Assert.Contains("</html>", html);
            Assert.Contains("<head>", html);
            Assert.Contains("</head>", html);
            Assert.Contains("<title>Nested Test Items</title>", html);
            Assert.Contains("<body>", html);
            Assert.Contains("</body>", html);
            Assert.Contains("<h1>Nested Test Items</h1>", html);
        }
        
        // All should have the same nested structure (first few items)
        foreach (var html in allHtml)
        {
            Assert.Contains("class=\"item-0\"", html);
            Assert.Contains("class=\"item-1\"", html);
            Assert.Contains("class=\"item-2\"", html);
        }
    }

    [Fact]
    public void AllMethods_ProperNestingStructure()
    {
        var cinderBlockHtml = _benchmark.CinderBlockHtml();
        var rawStringHtml = _benchmark.RawStringHtml();
        var htmlTagsHtml = _benchmark.HtmlTagsHtml();
        var scribanHtml = _benchmark.ScribanHtml();
        var razorLightHtml = _benchmark.RazorLightHtml();
        
        string[] allHtml = [cinderBlockHtml, rawStringHtml, htmlTagsHtml, scribanHtml, razorLightHtml];
        
        foreach (var html in allHtml)
        {
            // Verify that item-0 appears before item-1, etc. (proper nesting order)
            var item0Index = html.IndexOf("item-0");
            var item1Index = html.IndexOf("item-1");
            var item2Index = html.IndexOf("item-2");
            
            Assert.True(item0Index < item1Index, "item-0 should appear before item-1");
            Assert.True(item1Index < item2Index, "item-1 should appear before item-2");
            
            // Each div should have exactly one paragraph
            Assert.Equal(100, CountOccurrences(html, "<p>"));
            Assert.Equal(100, CountOccurrences(html, "</p>"));
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