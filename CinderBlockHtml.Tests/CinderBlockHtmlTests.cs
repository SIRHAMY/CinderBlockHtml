using CinderBlockHtml;
using SimpleApi;
using Xunit;

namespace SimpleApi.Tests.Infrastructure;

public class CinderBlockHtmlTests
{
    [Fact]
    public void TextNode_Raw_ShouldNotEscapeHtml()
    {
        var result = Text.Raw("<script>alert('test')</script>").RenderToString();
        
        Assert.Equal("<script>alert('test')</script>", result);
    }

    [Fact]
    public void TextNode_Encoded_ShouldEscapeHtml()
    {
        var result = Text.Encoded("<script>alert('test')</script>").RenderToString();
        
        Assert.Equal("&lt;script&gt;alert(&#39;test&#39;)&lt;/script&gt;", result);
    }

    [Fact]
    public void Elem_Div_WithAttributes_ShouldRenderCorrectly()
    {
        var result = Elem.Div([Attr.Class("container"), Attr.Id("main")], [
            Text.Encoded("Hello World")
        ]).RenderToString();
        
        Assert.Equal("<div class=\"container\" id=\"main\">Hello World</div>", result);
    }

    [Fact]
    public void Attr_Class_ShouldMergeMultipleClasses()
    {
        var result = Elem.Div([Attr.Class("btn"), Attr.Class("primary")], [
            Text.Encoded("Click me")
        ]).RenderToString();
        
        Assert.Equal("<div class=\"btn primary\">Click me</div>", result);
    }

    [Fact]
    public void Elem_SelfClosing_ShouldNotHaveClosingTag()
    {
        var result = Elem.Input([Attr.Type("text"), Attr.Name("username"), Attr.Value("test")]).RenderToString();
        
        Assert.Equal("<input type=\"text\" name=\"username\" value=\"test\" />", result);
    }

    [Fact]
    public void Elem_Nested_ShouldRenderCorrectHierarchy()
    {
        var result = Elem.Html([Attr.Custom("lang", "en")], [
            Elem.Head([], [
                Elem.Title([], [Text.Encoded("Test Page")])
            ]),
            Elem.Body([Attr.Class("main")], [
                Elem.Div([Attr.Id("content")], [
                    Elem.P([], [Text.Encoded("Hello World")])
                ])
            ])
        ]).RenderToString();
        
        var expected = "<html lang=\"en\"><head><title>Test Page</title></head><body class=\"main\"><div id=\"content\"><p>Hello World</p></div></body></html>";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Attr_Style_ShouldMergeMultipleStyles()
    {
        var result = Elem.Div([Attr.Style("color: red;"), Attr.Style("background: blue;")], [
            Text.Encoded("Styled content")
        ]).RenderToString();
        
        Assert.Equal("<div style=\"color: red; background: blue;\">Styled content</div>", result);
    }

    [Fact]
    public void Elem_EmptyChildren_ShouldRenderEmptyElement()
    {
        var result = Elem.Div([Attr.Class("empty")], []).RenderToString();
        
        Assert.Equal("<div class=\"empty\"></div>", result);
    }

    [Fact]
    public void Attr_BooleanAttribute_ShouldRenderCorrectly()
    {
        var result = Elem.Input([Attr.Type("checkbox"), Attr.Checked()]).RenderToString();
        
        Assert.Equal("<input type=\"checkbox\" checked />", result);
    }

    [Fact]
    public void Text_Rawf_ShouldFormatAndNotEscape()
    {
        var result = Text.Rawf("Hello <b>{0}</b>!", "World").RenderToString();
        
        Assert.Equal("Hello <b>World</b>!", result);
    }

    [Fact]
    public void ComplexHtmlStructure_ShouldRenderCorrectly()
    {
        var products = new[] { "Product 1", "Product 2", "Product 3" };
        
        var result = Elem.Html([Attr.Custom("lang", "en")], [
            Elem.Head([], [
                Elem.Title([], [Text.Encoded("Products")])
            ]),
            Elem.Body([], [
                Elem.Div([Attr.Class("container")], [
                    Elem.H1([], [Text.Encoded("Product List")]),
                    Elem.Ul([Attr.Class("product-list")], 
                        products.Select(p => 
                            Elem.Li([Attr.Class("product-item")], [Text.Encoded(p)])
                        ).ToArray()
                    )
                ])
            ])
        ]).RenderToString();
        
        Assert.Contains("<title>Products</title>", result);
        Assert.Contains("<h1>Product List</h1>", result);
        Assert.Contains("<li class=\"product-item\">Product 1</li>", result);
        Assert.Contains("<li class=\"product-item\">Product 2</li>", result);
        Assert.Contains("<li class=\"product-item\">Product 3</li>", result);
    }
}