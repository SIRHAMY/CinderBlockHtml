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

    [Fact]
    public void Div_WithVariousAttributes_ShouldRenderAllCorrectly()
    {
        // Test a div with many different attributes to ensure they all work
        var result = Elem.Div([
            Attr.Id("test-div"),
            Attr.Class("container"),
            Attr.Class("active"), // Multiple classes should merge
            Attr.Style("color: red;"),
            Attr.Style("background: white;"), // Multiple styles should merge
            Attr.Title("This is a tooltip"),
            Attr.Custom("data-id", "123"),
            Attr.Custom("data-name", "test"),
            Attr.OnClick("handleClick()"),
            Attr.OnChange("handleChange()"),
            Attr.Hidden()
        ], [
            Text.Encoded("Test Content")
        ]).RenderToString();

        // Check that all attributes are present
        Assert.Contains("id=\"test-div\"", result);
        Assert.Contains("class=\"container active\"", result);
        Assert.Contains("style=\"color: red; background: white;\"", result);
        Assert.Contains("title=\"This is a tooltip\"", result);
        Assert.Contains("data-id=\"123\"", result);
        Assert.Contains("data-name=\"test\"", result);
        Assert.Contains("onclick=\"handleClick()\"", result);
        Assert.Contains("onchange=\"handleChange()\"", result);
        Assert.Contains("hidden", result);
        Assert.Contains(">Test Content</div>", result);

        // Verify it's still a valid div
        Assert.StartsWith("<div ", result);
        Assert.EndsWith(">Test Content</div>", result);
    }

    [Fact]
    public void Elem_Strong_WithAttributes_ShouldRenderCorrectly()
    {
        var result = Elem.Strong([Attr.Class("important"), Attr.Id("warning")], [
            Text.Encoded("Warning!")
        ]).RenderToString();

        Assert.Equal("<strong class=\"important\" id=\"warning\">Warning!</strong>", result);
    }

    [Fact]
    public void Elem_Strong_WithoutAttributes_ShouldRenderCorrectly()
    {
        var result = Elem.Strong([
            Text.Encoded("Bold text")
        ]).RenderToString();

        Assert.Equal("<strong>Bold text</strong>", result);
    }

    [Fact]
    public void Elem_Em_WithAttributes_ShouldRenderCorrectly()
    {
        var result = Elem.Em([Attr.Class("emphasis"), Attr.Id("note")], [
            Text.Encoded("Note this!")
        ]).RenderToString();

        Assert.Equal("<em class=\"emphasis\" id=\"note\">Note this!</em>", result);
    }

    [Fact]
    public void Elem_Em_WithoutAttributes_ShouldRenderCorrectly()
    {
        var result = Elem.Em([
            Text.Encoded("Italic text")
        ]).RenderToString();

        Assert.Equal("<em>Italic text</em>", result);
    }

    [Fact]
    public void Elem_Strong_Nested_ShouldRenderCorrectly()
    {
        var result = Elem.P([], [
            Text.Encoded("This is "),
            Elem.Strong([Text.Encoded("very important")]),
            Text.Encoded(" text.")
        ]).RenderToString();

        Assert.Equal("<p>This is <strong>very important</strong> text.</p>", result);
    }

    [Fact]
    public void Elem_Em_Nested_ShouldRenderCorrectly()
    {
        var result = Elem.P([], [
            Text.Encoded("This is "),
            Elem.Em([Text.Encoded("emphasized")]),
            Text.Encoded(" text.")
        ]).RenderToString();

        Assert.Equal("<p>This is <em>emphasized</em> text.</p>", result);
    }

    [Fact]
    public void Elem_StrongAndEm_Combined_ShouldRenderCorrectly()
    {
        var result = Elem.P([], [
            Text.Encoded("This is "),
            Elem.Strong([
                Elem.Em([Text.Encoded("very strongly emphasized")])
            ]),
            Text.Encoded(" text.")
        ]).RenderToString();

        Assert.Equal("<p>This is <strong><em>very strongly emphasized</em></strong> text.</p>", result);
    }

    [Fact]
    public void Elem_H4_ShouldRenderCorrectly()
    {
        var result = Elem.H4([Text.Encoded("Heading 4")]).RenderToString();
        Assert.Equal("<h4>Heading 4</h4>", result);
    }

    [Fact]
    public void Elem_H5_ShouldRenderCorrectly()
    {
        var result = Elem.H5([Text.Encoded("Heading 5")]).RenderToString();
        Assert.Equal("<h5>Heading 5</h5>", result);
    }

    [Fact]
    public void Elem_H6_ShouldRenderCorrectly()
    {
        var result = Elem.H6([Text.Encoded("Heading 6")]).RenderToString();
        Assert.Equal("<h6>Heading 6</h6>", result);
    }

    [Fact]
    public void Elem_Code_ShouldRenderCorrectly()
    {
        var result = Elem.Code([Text.Encoded("var x = 10;")]).RenderToString();
        Assert.Equal("<code>var x = 10;</code>", result);
    }

    [Fact]
    public void Elem_Pre_ShouldRenderCorrectly()
    {
        var result = Elem.Pre([Text.Raw("function test() {\n  return true;\n}")]).RenderToString();
        Assert.Equal("<pre>function test() {\n  return true;\n}</pre>", result);
    }

    [Fact]
    public void Elem_Blockquote_ShouldRenderCorrectly()
    {
        var result = Elem.Blockquote([Text.Encoded("To be or not to be")]).RenderToString();
        Assert.Equal("<blockquote>To be or not to be</blockquote>", result);
    }

    [Fact]
    public void Elem_Small_ShouldRenderCorrectly()
    {
        var result = Elem.Small([Text.Encoded("Fine print")]).RenderToString();
        Assert.Equal("<small>Fine print</small>", result);
    }

    [Fact]
    public void Elem_Mark_ShouldRenderCorrectly()
    {
        var result = Elem.Mark([Text.Encoded("Highlighted")]).RenderToString();
        Assert.Equal("<mark>Highlighted</mark>", result);
    }

    [Fact]
    public void Elem_Sub_ShouldRenderCorrectly()
    {
        var result = Elem.P([], [
            Text.Encoded("H"),
            Elem.Sub([Text.Encoded("2")]),
            Text.Encoded("O")
        ]).RenderToString();
        Assert.Equal("<p>H<sub>2</sub>O</p>", result);
    }

    [Fact]
    public void Elem_Sup_ShouldRenderCorrectly()
    {
        var result = Elem.P([], [
            Text.Encoded("x"),
            Elem.Sup([Text.Encoded("2")])
        ]).RenderToString();
        Assert.Equal("<p>x<sup>2</sup></p>", result);
    }

    [Fact]
    public void Elem_Del_ShouldRenderCorrectly()
    {
        var result = Elem.Del([Text.Encoded("Deleted text")]).RenderToString();
        Assert.Equal("<del>Deleted text</del>", result);
    }

    [Fact]
    public void Elem_Ins_ShouldRenderCorrectly()
    {
        var result = Elem.Ins([Text.Encoded("Inserted text")]).RenderToString();
        Assert.Equal("<ins>Inserted text</ins>", result);
    }

    [Fact]
    public void Elem_Abbr_ShouldRenderCorrectly()
    {
        var result = Elem.Abbr([Attr.Title("HyperText Markup Language")], [
            Text.Encoded("HTML")
        ]).RenderToString();
        Assert.Equal("<abbr title=\"HyperText Markup Language\">HTML</abbr>", result);
    }

    [Fact]
    public void Elem_Table_CompleteStructure_ShouldRenderCorrectly()
    {
        var result = Elem.Table([Attr.Class("data-table")], [
            Elem.Thead([], [
                Elem.Tr([], [
                    Elem.Th([], [Text.Encoded("Name")]),
                    Elem.Th([], [Text.Encoded("Age")])
                ])
            ]),
            Elem.Tbody([], [
                Elem.Tr([], [
                    Elem.Td([], [Text.Encoded("John")]),
                    Elem.Td([], [Text.Encoded("30")])
                ]),
                Elem.Tr([], [
                    Elem.Td([], [Text.Encoded("Jane")]),
                    Elem.Td([], [Text.Encoded("25")])
                ])
            ])
        ]).RenderToString();

        Assert.Contains("<table class=\"data-table\">", result);
        Assert.Contains("<thead>", result);
        Assert.Contains("<tbody>", result);
        Assert.Contains("<tr>", result);
        Assert.Contains("<th>Name</th>", result);
        Assert.Contains("<td>John</td>", result);
    }

    [Fact]
    public void Elem_DefinitionList_ShouldRenderCorrectly()
    {
        var result = Elem.Dl([], [
            Elem.Dt([], [Text.Encoded("HTML")]),
            Elem.Dd([], [Text.Encoded("HyperText Markup Language")]),
            Elem.Dt([], [Text.Encoded("CSS")]),
            Elem.Dd([], [Text.Encoded("Cascading Style Sheets")])
        ]).RenderToString();

        Assert.Contains("<dl>", result);
        Assert.Contains("<dt>HTML</dt>", result);
        Assert.Contains("<dd>HyperText Markup Language</dd>", result);
        Assert.Contains("<dt>CSS</dt>", result);
        Assert.Contains("<dd>Cascading Style Sheets</dd>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Header_ShouldRenderCorrectly()
    {
        var result = Elem.Header([Attr.Class("site-header")], [
            Elem.H1([], [Text.Encoded("My Website")])
        ]).RenderToString();

        Assert.Equal("<header class=\"site-header\"><h1>My Website</h1></header>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Footer_ShouldRenderCorrectly()
    {
        var result = Elem.Footer([Text.Encoded("© 2025")]).RenderToString();
        Assert.Equal("<footer>&#169; 2025</footer>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Nav_ShouldRenderCorrectly()
    {
        var result = Elem.Nav([], [
            Elem.Ul([], [
                Elem.Li([], [Elem.A([Attr.Href("/")], [Text.Encoded("Home")])]),
                Elem.Li([], [Elem.A([Attr.Href("/about")], [Text.Encoded("About")])])
            ])
        ]).RenderToString();

        Assert.Contains("<nav>", result);
        Assert.Contains("<ul>", result);
        Assert.Contains("<a href=\"/\">Home</a>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Article_ShouldRenderCorrectly()
    {
        var result = Elem.Article([Attr.Class("post")], [
            Elem.H2([], [Text.Encoded("Article Title")]),
            Elem.P([], [Text.Encoded("Article content")])
        ]).RenderToString();

        Assert.Contains("<article class=\"post\">", result);
        Assert.Contains("<h2>Article Title</h2>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Section_ShouldRenderCorrectly()
    {
        var result = Elem.Section([Text.Encoded("Section content")]).RenderToString();
        Assert.Equal("<section>Section content</section>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Main_ShouldRenderCorrectly()
    {
        var result = Elem.Main([Text.Encoded("Main content")]).RenderToString();
        Assert.Equal("<main>Main content</main>", result);
    }

    [Fact]
    public void Elem_SemanticHtml_Aside_ShouldRenderCorrectly()
    {
        var result = Elem.Aside([Text.Encoded("Sidebar content")]).RenderToString();
        Assert.Equal("<aside>Sidebar content</aside>", result);
    }

    [Fact]
    public void Elem_Textarea_ShouldRenderCorrectly()
    {
        var result = Elem.Textarea([Attr.Name("comment"), Attr.Placeholder("Enter comment")], [
            Text.Encoded("Default text")
        ]).RenderToString();

        Assert.Equal("<textarea name=\"comment\" placeholder=\"Enter comment\">Default text</textarea>", result);
    }

    [Fact]
    public void Elem_Fieldset_WithLegend_ShouldRenderCorrectly()
    {
        var result = Elem.Fieldset([], [
            Elem.Legend([], [Text.Encoded("Personal Information")]),
            Elem.Label([], [Text.Encoded("Name:")]),
            Elem.Input([Attr.Type("text"), Attr.Name("name")])
        ]).RenderToString();

        Assert.Contains("<fieldset>", result);
        Assert.Contains("<legend>Personal Information</legend>", result);
        Assert.Contains("<label>Name:</label>", result);
    }

    [Fact]
    public void Elem_Iframe_ShouldRenderCorrectly()
    {
        var result = Elem.Iframe([Attr.Src("https://example.com"), Attr.Title("Example")], []).RenderToString();
        Assert.Equal("<iframe src=\"https://example.com\" title=\"Example\"></iframe>", result);
    }

    [Fact]
    public void Elem_Canvas_ShouldRenderCorrectly()
    {
        var result = Elem.Canvas([Attr.Id("myCanvas"), Attr.Custom("width", "800"), Attr.Custom("height", "600")], [
            Text.Encoded("Your browser does not support canvas")
        ]).RenderToString();

        Assert.Contains("<canvas id=\"myCanvas\" width=\"800\" height=\"600\">", result);
        Assert.Contains("Your browser does not support canvas", result);
        Assert.Contains("</canvas>", result);
    }

    [Fact]
    public void Elem_None_ShouldRenderToNothing()
    {
        var result = Elem.None.RenderToString();

        Assert.Equal("", result);
    }

    [Fact]
    public void Attr_None_ShouldRenderToNothing()
    {
        var result = Elem.Div([Attr.Class("test"), Attr.None], [
            Text.Encoded("Content")
        ]).RenderToString();

        Assert.Equal("<div class=\"test\">Content</div>", result);
    }

    [Fact]
    public void Elem_None_InConditional_ShouldWorkCorrectly()
    {
        var showExtra = false;
        var result = Elem.Div([], [
            Text.Encoded("Hello"),
            showExtra ? Text.Encoded(" World") : Elem.None
        ]).RenderToString();

        Assert.Equal("<div>Hello</div>", result);
    }

    [Fact]
    public void Attr_None_InConditional_ShouldWorkCorrectly()
    {
        var isActive = false;
        var result = Elem.Button([
            Attr.Class("btn"),
            isActive ? Attr.Class("active") : Attr.None
        ], [
            Text.Encoded("Click")
        ]).RenderToString();

        Assert.Equal("<button class=\"btn\">Click</button>", result);
    }

    [Fact]
    public void Elem_None_WithMultipleEmpty_ShouldRenderCorrectly()
    {
        var result = Elem.Div([], [
            Elem.None,
            Text.Encoded("Content"),
            Elem.None,
            Elem.None
        ]).RenderToString();

        Assert.Equal("<div>Content</div>", result);
    }

    [Fact]
    public void Attr_None_WithMultipleEmpty_ShouldRenderCorrectly()
    {
        var result = Elem.Div([
            Attr.None,
            Attr.Class("test"),
            Attr.None,
            Attr.Id("main"),
            Attr.None
        ], [
            Text.Encoded("Content")
        ]).RenderToString();

        Assert.Equal("<div class=\"test\" id=\"main\">Content</div>", result);
    }
}