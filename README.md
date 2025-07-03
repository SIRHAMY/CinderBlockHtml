# Overview

CinderBlockHtml is an HTML DSL for building HTML with composable blocks.

It is a port of Pim Brouwer's [Falco.Markup](https://github.com/falcoframework/Falco.Markup) to native C#.

## Purpose

Provide C# with a more ergonomic HTML DSL. Falco.Markup gets a lot right and is usable in C# projects as F#.

However many are reluctant to bridge the gap so here porting a C# version that some may find easier to read in native C# projects.

Goals: 

* **Simple** - Make it super simple to build HTML in C# (and keep it readable). [Simple Systems Scale](https://hamy.xyz/blog/2024-03_simple-scalable-systems).
* **Effective** - Cover all common usecases or have a path for users to do so
* **Efficient** - Be reasonably fast. But don't sacrifice on the other values to do so.

## Quickstart

The library provides four main modules: 

* `Elem` (HTML elements)
* `Attr` (attributes)
* `Text` (content)
* `Renderer` (convert to HTML)

### Basic Usage

```csharp
using CinderBlockHtml;

// Simple element with text content
var greeting = Elem.Div([Attr.Class("container")], [
    Text.Encoded("Hello World")
]);

// Convert to HTML string
var html = greeting.Pipe(Renderer.RenderToString);
// Output: <div class="container">Hello World</div>
```

### Nested Elements and Attributes

```csharp
var page = Elem.Div([Attr.Class("main"), Attr.Id("content")], [
    Elem.H1([], [Text.Encoded("Welcome")]),
    Elem.P([Attr.Class("description")], [
        Text.Encoded("This is a paragraph with "),
        Elem.Strong([], [Text.Encoded("bold text")])
    ])
]).Pipe(Renderer.RenderToString);
```

### Forms and Input Elements

```csharp
var form = Elem.Form([Attr.Action("/submit"), Attr.Method("POST")], [
    Elem.Input([Attr.Type("text"), Attr.Name("username"), Attr.Placeholder("Enter username")]),
    Elem.Input([Attr.Type("password"), Attr.Name("password")]),
    Elem.Input([Attr.Type("submit"), Attr.Value("Login")])
]).Pipe(Renderer.RenderToString);
```

### Dynamic Content

```csharp
var products = new[] { "Product 1", "Product 2", "Product 3" };

var productList = Elem.Ul([Attr.Class("product-list")], 
    products.Select(p => 
        Elem.Li([Attr.Class("product-item")], [Text.Encoded(p)])
    ).ToArray()
).Pipe(Renderer.RenderToString);
```

### Text Handling

```csharp
// Encoded text (safe from XSS)
var safeText = Text.Encoded("<script>alert('xss')</script>");

// Raw HTML (unescaped)
var rawHtml = Text.Raw("<b>Bold text</b>");

// Formatted text
var formatted = Text.Rawf("Hello <b>{0}</b>!", "World");
```

### Attribute Merging

```csharp
// Multiple classes are automatically merged
var button = Elem.Button([Attr.Class("btn"), Attr.Class("primary")], [
    Text.Encoded("Click me")
]);
// Output: <button class="btn primary">Click me</button>

// Multiple styles are merged
var styledDiv = Elem.Div([Attr.Style("color: red;"), Attr.Style("background: blue;")], [
    Text.Encoded("Styled content")
]);
// Output: <div style="color: red; background: blue;">Styled content</div>
```

## Running Benchmarks

From root: 

```
docker run $(docker build -f Dockerfile.Benchmarks -q .)
```