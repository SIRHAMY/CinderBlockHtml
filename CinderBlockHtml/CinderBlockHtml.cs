using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinderBlockHtml
{
    /// <summary>
    /// Abstract base class for all XML nodes in the HTML document tree.
    /// </summary>
    public abstract record XmlNode;
    /// <summary>
    /// Represents a text node containing string content.
    /// </summary>
    /// <param name="Content">The text content of the node.</param>
    public record TextNode(string Content) : XmlNode;
    /// <summary>
    /// Represents an HTML element with a tag, attributes, and child nodes.
    /// </summary>
    /// <param name="Tag">The HTML tag name.</param>
    /// <param name="Attributes">The attributes for the element.</param>
    /// <param name="Children">The child nodes contained within the element.</param>
    public record ElementNode(string Tag, XmlAttribute[] Attributes, XmlNode[] Children) : XmlNode;
    /// <summary>
    /// Represents a self-closing HTML element with a tag and attributes.
    /// </summary>
    /// <param name="Tag">The HTML tag name.</param>
    /// <param name="Attributes">The attributes for the element.</param>
    public record SelfClosingNode(string Tag, XmlAttribute[] Attributes) : XmlNode;

    /// <summary>
    /// Abstract base class for all XML attributes.
    /// </summary>
    public abstract record XmlAttribute;
    /// <summary>
    /// Represents a key-value attribute with a name and value.
    /// </summary>
    /// <param name="Key">The attribute name.</param>
    /// <param name="Value">The attribute value.</param>
    public record KeyValueAttr(string Key, string Value) : XmlAttribute;
    /// <summary>
    /// Represents a boolean attribute with only a name (no value).
    /// </summary>
    /// <param name="Key">The attribute name.</param>
    public record BooleanAttr(string Key) : XmlAttribute;

    /// <summary>
    /// Provides methods for creating text nodes with different encoding behaviors.
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Creates a text node with raw, unescaped content.
        /// </summary>
        /// <param name="content">The raw text content.</param>
        /// <returns>A text node containing the unescaped content.</returns>
        public static XmlNode Raw(string content) => new TextNode(content);
        /// <summary>
        /// Creates a text node with HTML-encoded content for safe output.
        /// </summary>
        /// <param name="content">The text content to encode.</param>
        /// <returns>A text node containing the HTML-encoded content.</returns>
        public static XmlNode Encoded(string content) => new TextNode(System.Web.HttpUtility.HtmlEncode(content));
        /// <summary>
        /// Creates a text node with formatted raw content using string.Format.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments to format into the string.</param>
        /// <returns>A text node containing the formatted content.</returns>
        public static XmlNode Rawf(string format, params object[] args) => new TextNode(string.Format(format, args));
    }

    /// <summary>
    /// Provides methods for creating HTML attributes with intelligent merging capabilities.
    /// </summary>
    public static class Attr
    {
        /// <summary>
        /// Creates a CSS class attribute.
        /// </summary>
        /// <param name="value">The CSS class name(s).</param>
        /// <returns>A class attribute.</returns>
        public static XmlAttribute Class(string value) => new KeyValueAttr("class", value);
        /// <summary>
        /// Creates an id attribute.
        /// </summary>
        /// <param name="value">The element id.</param>
        /// <returns>An id attribute.</returns>
        public static XmlAttribute Id(string value) => new KeyValueAttr("id", value);
        /// <summary>
        /// Creates a CSS style attribute.
        /// </summary>
        /// <param name="value">The CSS style declarations.</param>
        /// <returns>A style attribute.</returns>
        public static XmlAttribute Style(string value) => new KeyValueAttr("style", value);
        /// <summary>
        /// Creates an href attribute for links.
        /// </summary>
        /// <param name="value">The URL or anchor reference.</param>
        /// <returns>An href attribute.</returns>
        public static XmlAttribute Href(string value) => new KeyValueAttr("href", value);
        /// <summary>
        /// Creates a src attribute for resources like images and scripts.
        /// </summary>
        /// <param name="value">The resource URL.</param>
        /// <returns>A src attribute.</returns>
        public static XmlAttribute Src(string value) => new KeyValueAttr("src", value);
        /// <summary>
        /// Creates an alt attribute for alternative text.
        /// </summary>
        /// <param name="value">The alternative text.</param>
        /// <returns>An alt attribute.</returns>
        public static XmlAttribute Alt(string value) => new KeyValueAttr("alt", value);
        /// <summary>
        /// Creates a title attribute for tooltips.
        /// </summary>
        /// <param name="value">The tooltip text.</param>
        /// <returns>A title attribute.</returns>
        public static XmlAttribute Title(string value) => new KeyValueAttr("title", value);
        /// <summary>
        /// Creates a type attribute for input elements and other typed elements.
        /// </summary>
        /// <param name="value">The type value.</param>
        /// <returns>A type attribute.</returns>
        public static XmlAttribute Type(string value) => new KeyValueAttr("type", value);
        /// <summary>
        /// Creates a name attribute for form elements.
        /// </summary>
        /// <param name="value">The element name.</param>
        /// <returns>A name attribute.</returns>
        public static XmlAttribute Name(string value) => new KeyValueAttr("name", value);
        /// <summary>
        /// Creates a value attribute for form elements.
        /// </summary>
        /// <param name="value">The element value.</param>
        /// <returns>A value attribute.</returns>
        public static XmlAttribute Value(string value) => new KeyValueAttr("value", value);
        /// <summary>
        /// Creates a placeholder attribute for input elements.
        /// </summary>
        /// <param name="value">The placeholder text.</param>
        /// <returns>A placeholder attribute.</returns>
        public static XmlAttribute Placeholder(string value) => new KeyValueAttr("placeholder", value);
        /// <summary>
        /// Creates an action attribute for form submission.
        /// </summary>
        /// <param name="value">The form action URL.</param>
        /// <returns>An action attribute.</returns>
        public static XmlAttribute Action(string value) => new KeyValueAttr("action", value);
        /// <summary>
        /// Creates a method attribute for form submission.
        /// </summary>
        /// <param name="value">The HTTP method (GET, POST, etc.).</param>
        /// <returns>A method attribute.</returns>
        public static XmlAttribute Method(string value) => new KeyValueAttr("method", value);

        /// <summary>
        /// Creates a required boolean attribute for form elements.
        /// </summary>
        /// <returns>A required attribute.</returns>
        public static XmlAttribute Required() => new BooleanAttr("required");
        /// <summary>
        /// Creates a disabled boolean attribute for form elements.
        /// </summary>
        /// <returns>A disabled attribute.</returns>
        public static XmlAttribute Disabled() => new BooleanAttr("disabled");
        /// <summary>
        /// Creates a checked boolean attribute for checkboxes and radio buttons.
        /// </summary>
        /// <returns>A checked attribute.</returns>
        public static XmlAttribute Checked() => new BooleanAttr("checked");
        /// <summary>
        /// Creates a selected boolean attribute for option elements.
        /// </summary>
        /// <returns>A selected attribute.</returns>
        public static XmlAttribute Selected() => new BooleanAttr("selected");
        /// <summary>
        /// Creates a hidden boolean attribute to hide elements.
        /// </summary>
        /// <returns>A hidden attribute.</returns>
        public static XmlAttribute Hidden() => new BooleanAttr("hidden");

        /// <summary>
        /// Creates an onclick event attribute.
        /// </summary>
        /// <param name="value">The JavaScript code to execute on click.</param>
        /// <returns>An onclick attribute.</returns>
        public static XmlAttribute OnClick(string value) => new KeyValueAttr("onclick", value);
        /// <summary>
        /// Creates an onchange event attribute.
        /// </summary>
        /// <param name="value">The JavaScript code to execute on change.</param>
        /// <returns>An onchange attribute.</returns>
        public static XmlAttribute OnChange(string value) => new KeyValueAttr("onchange", value);
        /// <summary>
        /// Creates an onsubmit event attribute.
        /// </summary>
        /// <param name="value">The JavaScript code to execute on form submission.</param>
        /// <returns>An onsubmit attribute.</returns>
        public static XmlAttribute OnSubmit(string value) => new KeyValueAttr("onsubmit", value);
        /// <summary>
        /// Creates an onload event attribute.
        /// </summary>
        /// <param name="value">The JavaScript code to execute on load.</param>
        /// <returns>An onload attribute.</returns>
        public static XmlAttribute OnLoad(string value) => new KeyValueAttr("onload", value);

        /// <summary>
        /// Creates a custom key-value attribute.
        /// </summary>
        /// <param name="key">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        /// <returns>A custom attribute.</returns>
        public static XmlAttribute Custom(string key, string value) => new KeyValueAttr(key, value);
        /// <summary>
        /// Creates a custom boolean attribute.
        /// </summary>
        /// <param name="key">The attribute name.</param>
        /// <returns>A custom boolean attribute.</returns>
        public static XmlAttribute CustomBool(string key) => new BooleanAttr(key);

        /// <summary>
        /// Creates a content attribute for meta tags.
        /// </summary>
        /// <param name="value">The content value.</param>
        /// <returns>A content attribute.</returns>
        public static XmlAttribute Content(string value) => new KeyValueAttr("content", value);

        /// <summary>
        /// Intelligently merges multiple attribute arrays, combining values for additive attributes like class and style.
        /// </summary>
        /// <param name="attributeLists">The attribute arrays to merge.</param>
        /// <returns>A merged array of attributes.</returns>
        public static XmlAttribute[] Merge(params XmlAttribute[][] attributeLists)
        {
            var allAttrs = attributeLists.SelectMany(x => x).ToList();
            var grouped = allAttrs.GroupBy(attr => attr switch
            {
                KeyValueAttr kv => kv.Key,
                BooleanAttr b => b.Key,
                _ => ""
            });

            var result = new List<XmlAttribute>();

            foreach (var group in grouped)
            {
                var key = group.Key;
                var attrs = group.ToList();

                // For additive attributes like class, style, combine values
                if (key == "class" || key == "style" || key == "accept")
                {
                    var values = attrs.OfType<KeyValueAttr>().Select(a => a.Value.TrimEnd().TrimEnd(';'));
                    var separator = key == "class" ? " " : "; ";
                    result.Add(new KeyValueAttr(key, $"{string.Join(separator, values)}{separator.Trim()}"));
                }
                else
                {
                    // For other attributes, take the last one (override behavior)
                    result.Add(attrs.Last());
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Returns an empty array of attributes.
        /// </summary>
        /// <returns>An empty attribute array.</returns>
        public static XmlAttribute[] Empty() =>
            Array.Empty<XmlAttribute>();

    }

    /// <summary>
    /// Provides methods for creating HTML elements with attributes and child nodes.
    /// </summary>
    public static class Elem
    {
        /// <summary>
        /// Creates an HTML root element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An HTML element node.</returns>
        public static XmlNode Html(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("html", attributes, children);

        /// <summary>
        /// Creates a head element for document metadata.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A head element node.</returns>
        public static XmlNode Head(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("head", attributes, children);

        /// <summary>
        /// Creates a body element for document content.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A body element node.</returns>
        public static XmlNode Body(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("body", attributes, children);

        /// <summary>
        /// Creates a div element for generic content division.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A div element node.</returns>
        public static XmlNode Div(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("div", attributes, children);

        /// <summary>
        /// Creates a paragraph element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A paragraph element node.</returns>
        public static XmlNode P(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("p", attributes, children);

        /// <summary>
        /// Creates a span element for inline content.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A span element node.</returns>
        public static XmlNode Span(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("span", attributes, children);

        /// <summary>
        /// Creates a script element for JavaScript code.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A script element node.</returns>
        public static XmlNode Script(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("script", attributes, children);

        /// <summary>
        /// Creates an anchor element for links.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An anchor element node.</returns>
        public static XmlNode A(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("a", attributes, children);

        /// <summary>
        /// Creates an H1 heading element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H1 element node.</returns>
        public static XmlNode H1(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h1", attributes, children);

        /// <summary>
        /// Creates an H2 heading element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H2 element node.</returns>
        public static XmlNode H2(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h2", attributes, children);

        /// <summary>
        /// Creates an H3 heading element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H3 element node.</returns>
        public static XmlNode H3(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h3", attributes, children);

        /// <summary>
        /// Creates an unordered list element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An unordered list element node.</returns>
        public static XmlNode Ul(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("ul", attributes, children);

        /// <summary>
        /// Creates an ordered list element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An ordered list element node.</returns>
        public static XmlNode Ol(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("ol", attributes, children);

        /// <summary>
        /// Creates a list item element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A list item element node.</returns>
        public static XmlNode Li(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("li", attributes, children);

        /// <summary>
        /// Creates a button element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A button element node.</returns>
        public static XmlNode Button(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("button", attributes, children);

        /// <summary>
        /// Creates a form element.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A form element node.</returns>
        public static XmlNode Form(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("form", attributes, children);

        /// <summary>
        /// Creates a label element for form controls.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A label element node.</returns>
        public static XmlNode Label(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("label", attributes, children);

        /// <summary>
        /// Creates a select element for dropdown lists.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A select element node.</returns>
        public static XmlNode Select(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("select", attributes, children);

        /// <summary>
        /// Creates an option element for select lists.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>An option element node.</returns>
        public static XmlNode Option(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("option", attributes, children);

        /// <summary>
        /// Creates a title element for document titles.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A title element node.</returns>
        public static XmlNode Title(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("title", attributes, children);

        /// <summary>
        /// Creates an input element for form input.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>An input element node.</returns>
        public static XmlNode Input(XmlAttribute[] attributes) =>
            new SelfClosingNode("input", attributes);

        /// <summary>
        /// Creates an img element for images.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>An img element node.</returns>
        public static XmlNode Img(XmlAttribute[] attributes) =>
            new SelfClosingNode("img", attributes);

        /// <summary>
        /// Creates a br element for line breaks.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>A br element node.</returns>
        public static XmlNode Br(XmlAttribute[] attributes) =>
            new SelfClosingNode("br", attributes);

        /// <summary>
        /// Creates an hr element for horizontal rules.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>An hr element node.</returns>
        public static XmlNode Hr(XmlAttribute[] attributes) =>
            new SelfClosingNode("hr", attributes);

        /// <summary>
        /// Creates a meta element for metadata.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>A meta element node.</returns>
        public static XmlNode Meta(XmlAttribute[] attributes) =>
            new SelfClosingNode("meta", attributes);

        /// <summary>
        /// Creates a link element for external resources.
        /// </summary>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>A link element node.</returns>
        public static XmlNode Link(XmlAttribute[] attributes) =>
            new SelfClosingNode("link", attributes);

        /// <summary>
        /// Creates a div element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>A div element node.</returns>
        public static XmlNode Div(XmlNode[] children) => Div(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates a paragraph element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>A paragraph element node.</returns>
        public static XmlNode P(XmlNode[] children) => P(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates a span element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>A span element node.</returns>
        public static XmlNode Span(XmlNode[] children) => Span(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates an H1 heading element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H1 element node.</returns>
        public static XmlNode H1(XmlNode[] children) => H1(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates an H2 heading element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H2 element node.</returns>
        public static XmlNode H2(XmlNode[] children) => H2(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates an H3 heading element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>An H3 element node.</returns>
        public static XmlNode H3(XmlNode[] children) => H3(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates an unordered list element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>An unordered list element node.</returns>
        public static XmlNode Ul(XmlNode[] children) => Ul(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates a list item element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>A list item element node.</returns>
        public static XmlNode Li(XmlNode[] children) => Li(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates a button element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>A button element node.</returns>
        public static XmlNode Button(XmlNode[] children) => Button(Array.Empty<XmlAttribute>(), children);
        /// <summary>
        /// Creates an anchor element with no attributes.
        /// </summary>
        /// <param name="children">The child nodes.</param>
        /// <returns>An anchor element node.</returns>
        public static XmlNode A(XmlNode[] children) => A(Array.Empty<XmlAttribute>(), children);

        /// <summary>
        /// Creates a br element with no attributes.
        /// </summary>
        /// <returns>A br element node.</returns>
        public static XmlNode Br() => Br(Array.Empty<XmlAttribute>());
        /// <summary>
        /// Creates an hr element with no attributes.
        /// </summary>
        /// <returns>An hr element node.</returns>
        public static XmlNode Hr() => Hr(Array.Empty<XmlAttribute>());

        /// <summary>
        /// Creates a custom HTML element with the specified tag, attributes, and children.
        /// </summary>
        /// <param name="tag">The HTML tag name.</param>
        /// <param name="attributes">The attributes for the element.</param>
        /// <param name="children">The child nodes.</param>
        /// <returns>A custom element node.</returns>
        public static XmlNode Create(string tag, XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode(tag, attributes, children);

        /// <summary>
        /// Creates a custom self-closing HTML element with the specified tag and attributes.
        /// </summary>
        /// <param name="tag">The HTML tag name.</param>
        /// <param name="attributes">The attributes for the element.</param>
        /// <returns>A custom self-closing element node.</returns>
        public static XmlNode CreateSelfClosing(string tag, XmlAttribute[] attributes) =>
            new SelfClosingNode(tag, attributes);

        /// <summary>
        /// Returns an empty array of XML nodes.
        /// </summary>
        /// <returns>An empty node array.</returns>
        public static XmlNode[] Empty() =>
            Array.Empty<XmlNode>();
    }

    /// <summary>
    /// Provides methods for rendering XML node trees to HTML strings.
    /// </summary>
    public static class Renderer
    {
        /// <summary>
        /// Renders an XML node tree to an HTML string.
        /// </summary>
        /// <param name="node">The XML node to render.</param>
        /// <returns>The rendered HTML string.</returns>
        public static string RenderToString(this XmlNode node)
        {
            var sb = new StringBuilder();
            RenderNode(node, sb);
            return sb.ToString();
        }

        private static void RenderNode(XmlNode node, StringBuilder sb)
        {
            switch (node)
            {
                case TextNode text:
                    sb.Append(text.Content);
                    break;
                    
                case SelfClosingNode selfClosing:
                    sb.Append('<').Append(selfClosing.Tag);
                    RenderAttributes(selfClosing.Attributes, sb);
                    sb.Append(" />");
                    break;
                    
                case ElementNode element:
                    sb.Append('<').Append(element.Tag);
                    RenderAttributes(element.Attributes, sb);
                    sb.Append('>');
                    
                    foreach (var child in element.Children)
                    {
                        RenderNode(child, sb);
                    }
                    
                    sb.Append("</").Append(element.Tag).Append('>');
                    break;
            }
        }

        private static void RenderAttributes(XmlAttribute[] attributes, StringBuilder sb)
        {
            var mergedAttributes = Attr.Merge(attributes);
            foreach (var attr in mergedAttributes)
            {
                switch (attr)
                {
                    case KeyValueAttr kv:
                        sb.Append(' ').Append(kv.Key).Append("=\"").Append(kv.Value).Append('"');
                        break;

                    case BooleanAttr boolean:
                        sb.Append(' ').Append(boolean.Key);
                        break;
                }
            }
        }
    }
}