using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinderBlockHtml
{
    // Core types - keeping it simple like F#
    public abstract record XmlNode;
    public record TextNode(string Content) : XmlNode;
    public record ElementNode(string Tag, XmlAttribute[] Attributes, XmlNode[] Children) : XmlNode;
    public record SelfClosingNode(string Tag, XmlAttribute[] Attributes) : XmlNode;

    public abstract record XmlAttribute;
    public record KeyValueAttr(string Key, string Value) : XmlAttribute;
    public record BooleanAttr(string Key) : XmlAttribute;

    // Text module - like Falco's Text module
    public static class Text
    {
        public static XmlNode Raw(string content) => new TextNode(content);
        public static XmlNode Encoded(string content) => new TextNode(System.Web.HttpUtility.HtmlEncode(content));
        public static XmlNode Rawf(string format, params object[] args) => new TextNode(string.Format(format, args));
    }

    // Attr module - like Falco's Attr module  
    public static class Attr
    {
        // Common attributes
        public static XmlAttribute Class(string value) => new KeyValueAttr("class", value);
        public static XmlAttribute Id(string value) => new KeyValueAttr("id", value);
        public static XmlAttribute Style(string value) => new KeyValueAttr("style", value);
        public static XmlAttribute Href(string value) => new KeyValueAttr("href", value);
        public static XmlAttribute Src(string value) => new KeyValueAttr("src", value);
        public static XmlAttribute Alt(string value) => new KeyValueAttr("alt", value);
        public static XmlAttribute Title(string value) => new KeyValueAttr("title", value);
        public static XmlAttribute Type(string value) => new KeyValueAttr("type", value);
        public static XmlAttribute Name(string value) => new KeyValueAttr("name", value);
        public static XmlAttribute Value(string value) => new KeyValueAttr("value", value);
        public static XmlAttribute Placeholder(string value) => new KeyValueAttr("placeholder", value);

        // Boolean attributes
        public static XmlAttribute Required() => new BooleanAttr("required");
        public static XmlAttribute Disabled() => new BooleanAttr("disabled");
        public static XmlAttribute Checked() => new BooleanAttr("checked");
        public static XmlAttribute Selected() => new BooleanAttr("selected");
        public static XmlAttribute Hidden() => new BooleanAttr("hidden");

        // Events - prefixed with "on"
        public static XmlAttribute OnClick(string value) => new KeyValueAttr("onclick", value);
        public static XmlAttribute OnChange(string value) => new KeyValueAttr("onchange", value);
        public static XmlAttribute OnSubmit(string value) => new KeyValueAttr("onsubmit", value);
        public static XmlAttribute OnLoad(string value) => new KeyValueAttr("onload", value);

        // Custom attribute helper
        public static XmlAttribute Custom(string key, string value) => new KeyValueAttr(key, value);
        public static XmlAttribute CustomBool(string key) => new BooleanAttr(key);

        // Additional common attributes
        public static XmlAttribute Content(string value) => new KeyValueAttr("content", value);

        // Merge function like Falco - combines attribute lists intelligently
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

        public static XmlAttribute[] Empty() =>
            Array.Empty<XmlAttribute>();

    }

    // Elem module - like Falco's Elem module
    public static class Elem
    {
        // Parent elements (can contain children)
        public static XmlNode Html(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("html", attributes, children);

        public static XmlNode Head(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("head", attributes, children);

        public static XmlNode Body(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("body", attributes, children);

        public static XmlNode Div(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("div", attributes, children);

        public static XmlNode P(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("p", attributes, children);

        public static XmlNode Span(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("span", attributes, children);

        public static XmlNode Script(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("script", attributes, children);

        public static XmlNode A(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("a", attributes, children);

        public static XmlNode H1(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h1", attributes, children);

        public static XmlNode H2(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h2", attributes, children);

        public static XmlNode H3(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("h3", attributes, children);

        public static XmlNode Ul(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("ul", attributes, children);

        public static XmlNode Ol(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("ol", attributes, children);

        public static XmlNode Li(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("li", attributes, children);

        public static XmlNode Button(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("button", attributes, children);

        public static XmlNode Form(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("form", attributes, children);

        public static XmlNode Label(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("label", attributes, children);

        public static XmlNode Select(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("select", attributes, children);

        public static XmlNode Option(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("option", attributes, children);

        public static XmlNode Title(XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode("title", attributes, children);

        // Self-closing elements
        public static XmlNode Input(XmlAttribute[] attributes) =>
            new SelfClosingNode("input", attributes);

        public static XmlNode Img(XmlAttribute[] attributes) =>
            new SelfClosingNode("img", attributes);

        public static XmlNode Br(XmlAttribute[] attributes) =>
            new SelfClosingNode("br", attributes);

        public static XmlNode Hr(XmlAttribute[] attributes) =>
            new SelfClosingNode("hr", attributes);

        public static XmlNode Meta(XmlAttribute[] attributes) =>
            new SelfClosingNode("meta", attributes);

        public static XmlNode Link(XmlAttribute[] attributes) =>
            new SelfClosingNode("link", attributes);

        // Convenience overloads for empty attributes
        public static XmlNode Div(XmlNode[] children) => Div(Array.Empty<XmlAttribute>(), children);
        public static XmlNode P(XmlNode[] children) => P(Array.Empty<XmlAttribute>(), children);
        public static XmlNode Span(XmlNode[] children) => Span(Array.Empty<XmlAttribute>(), children);
        public static XmlNode H1(XmlNode[] children) => H1(Array.Empty<XmlAttribute>(), children);
        public static XmlNode H2(XmlNode[] children) => H2(Array.Empty<XmlAttribute>(), children);
        public static XmlNode H3(XmlNode[] children) => H3(Array.Empty<XmlAttribute>(), children);
        public static XmlNode Ul(XmlNode[] children) => Ul(Array.Empty<XmlAttribute>(), children);
        public static XmlNode Li(XmlNode[] children) => Li(Array.Empty<XmlAttribute>(), children);
        public static XmlNode Button(XmlNode[] children) => Button(Array.Empty<XmlAttribute>(), children);
        public static XmlNode A(XmlNode[] children) => A(Array.Empty<XmlAttribute>(), children);

        // Convenience for self-closing with no attributes
        public static XmlNode Br() => Br(Array.Empty<XmlAttribute>());
        public static XmlNode Hr() => Hr(Array.Empty<XmlAttribute>());

        // Custom element helper
        public static XmlNode Create(string tag, XmlAttribute[] attributes, XmlNode[] children) =>
            new ElementNode(tag, attributes, children);

        public static XmlNode CreateSelfClosing(string tag, XmlAttribute[] attributes) =>
            new SelfClosingNode(tag, attributes);

        public static XmlNode[] Empty() =>
            Array.Empty<XmlNode>();
    }

    // Renderer - converts XmlNode tree to HTML string
    public static class Renderer
    {
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