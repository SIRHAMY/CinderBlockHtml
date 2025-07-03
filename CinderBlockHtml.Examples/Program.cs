using CinderBlockHtml;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Home page with navigation
app.MapGet("/", () =>
{
    var html = Elem.Html([Attr.Custom("lang", "en")], [
        Elem.Head([], [
            Elem.Meta([Attr.Custom("charset", "UTF-8")]),
            Elem.Meta([Attr.Name("viewport"), Attr.Content("width=device-width, initial-scale=1.0")]),
            Elem.Title([], [Text.Encoded("CinderBlockHtml Examples")]),
            Elem.Create("style", [], [Text.Raw("""
                body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; }
                .container { max-width: 800px; margin: 0 auto; }
                .nav { margin-bottom: 30px; }
                .nav a { margin-right: 15px; color: #007acc; text-decoration: none; }
                .nav a:hover { text-decoration: underline; }
                .example { background: #f5f5f5; padding: 20px; margin: 20px 0; border-radius: 5px; }
                .product-item { margin: 10px 0; padding: 10px; background: white; border-radius: 3px; }
                .btn { padding: 10px 20px; background: #007acc; color: white; border: none; border-radius: 5px; cursor: pointer; }
                .btn:hover { background: #005a99; }
                """)])
        ]),
        Elem.Body([], [
            Elem.Div([Attr.Class("container")], [
                Elem.H1([], [Text.Encoded("CinderBlockHtml Examples")]),
                Elem.P([], [Text.Encoded("This demonstrates various HTML generation patterns using CinderBlockHtml.")]),
                
                Elem.Div([Attr.Class("nav")], [
                    Elem.A([Attr.Href("/")], [Text.Encoded("Home")]),
                    Elem.A([Attr.Href("/about")], [Text.Encoded("About")]),
                    Elem.A([Attr.Href("/products")], [Text.Encoded("Products")]),
                    Elem.A([Attr.Href("/contact")], [Text.Encoded("Contact")])
                ]),
                
                Elem.Div([Attr.Class("example")], [
                    Elem.H2([], [Text.Encoded("Basic Elements")]),
                    Elem.P([], [Text.Encoded("This page demonstrates basic HTML elements created with CinderBlockHtml.")]),
                    Elem.Ul([], [
                        Elem.Li([], [Text.Encoded("Clean, composable HTML generation")]),
                        Elem.Li([], [Text.Encoded("Type-safe attribute handling")]),
                        Elem.Li([], [Text.Encoded("Automatic text encoding for security")])
                    ])
                ])
            ])
        ])
    ]).RenderToString();
    
    return Results.Content(html, "text/html");
});

// About page with form
app.MapGet("/about", () =>
{
    var html = Elem.Html([Attr.Custom("lang", "en")], [
        Elem.Head([], [
            Elem.Meta([Attr.Custom("charset", "UTF-8")]),
            Elem.Title([], [Text.Encoded("About - CinderBlockHtml Examples")]),
            Elem.Create("style", [], [Text.Raw("""
                body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; }
                .container { max-width: 800px; margin: 0 auto; }
                .nav { margin-bottom: 30px; }
                .nav a { margin-right: 15px; color: #007acc; text-decoration: none; }
                .form-group { margin: 15px 0; }
                .form-group label { display: block; margin-bottom: 5px; font-weight: bold; }
                .form-group input, .form-group textarea { width: 100%; padding: 8px; border: 1px solid #ddd; border-radius: 3px; }
                .btn { padding: 10px 20px; background: #007acc; color: white; border: none; border-radius: 5px; cursor: pointer; }
                """)])
        ]),
        Elem.Body([], [
            Elem.Div([Attr.Class("container")], [
                Elem.H1([], [Text.Encoded("About CinderBlockHtml")]),
                
                Elem.Div([Attr.Class("nav")], [
                    Elem.A([Attr.Href("/")], [Text.Encoded("Home")]),
                    Elem.A([Attr.Href("/about")], [Text.Encoded("About")]),
                    Elem.A([Attr.Href("/products")], [Text.Encoded("Products")]),
                    Elem.A([Attr.Href("/contact")], [Text.Encoded("Contact")])
                ]),
                
                Elem.P([], [
                    Text.Encoded("CinderBlockHtml is a composable HTML DSL for C#. It allows you to build HTML using strongly-typed functions and automatic attribute merging.")
                ]),
                
                Elem.H2([], [Text.Encoded("Contact Form Example")]),
                Elem.Form([Attr.Custom("action", "/contact"), Attr.Custom("method", "POST")], [
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "name")], [Text.Encoded("Name:")]),
                        Elem.Input([Attr.Type("text"), Attr.Id("name"), Attr.Name("name"), Attr.Required()])
                    ]),
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "email")], [Text.Encoded("Email:")]),
                        Elem.Input([Attr.Type("email"), Attr.Id("email"), Attr.Name("email"), Attr.Required()])
                    ]),
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "message")], [Text.Encoded("Message:")]),
                        Elem.Create("textarea", [Attr.Id("message"), Attr.Name("message"), Attr.Custom("rows", "5"), Attr.Required()], [])
                    ]),
                    Elem.Button([Attr.Type("submit"), Attr.Class("btn")], [Text.Encoded("Send Message")])
                ])
            ])
        ])
    ]).RenderToString();
    
    return Results.Content(html, "text/html");
});

// Products page with dynamic content
app.MapGet("/products", () =>
{
    var products = new[]
    {
        new { Name = "CinderBlockHtml", Price = "Free", Description = "Composable HTML DSL for C#" },
        new { Name = "ASP.NET Core", Price = "Free", Description = "Cross-platform web framework" },
        new { Name = "Entity Framework", Price = "Free", Description = "Object-relational mapper" }
    };
    
    var html = Elem.Html([Attr.Custom("lang", "en")], [
        Elem.Head([], [
            Elem.Meta([Attr.Custom("charset", "UTF-8")]),
            Elem.Title([], [Text.Encoded("Products - CinderBlockHtml Examples")]),
            Elem.Create("style", [], [Text.Raw("""
                body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; }
                .container { max-width: 800px; margin: 0 auto; }
                .nav { margin-bottom: 30px; }
                .nav a { margin-right: 15px; color: #007acc; text-decoration: none; }
                .product-list { display: grid; gap: 20px; }
                .product-item { background: #f9f9f9; padding: 20px; border-radius: 5px; border: 1px solid #ddd; }
                .product-title { color: #333; margin-bottom: 10px; }
                .product-price { font-weight: bold; color: #007acc; font-size: 1.2em; }
                .product-description { color: #666; margin-top: 10px; }
                """)])
        ]),
        Elem.Body([], [
            Elem.Div([Attr.Class("container")], [
                Elem.H1([], [Text.Encoded("Products")]),
                
                Elem.Div([Attr.Class("nav")], [
                    Elem.A([Attr.Href("/")], [Text.Encoded("Home")]),
                    Elem.A([Attr.Href("/about")], [Text.Encoded("About")]),
                    Elem.A([Attr.Href("/products")], [Text.Encoded("Products")]),
                    Elem.A([Attr.Href("/contact")], [Text.Encoded("Contact")])
                ]),
                
                Elem.P([], [Text.Encoded("Here are some products demonstrating dynamic HTML generation:")]),
                
                Elem.Div([Attr.Class("product-list")], 
                    products.Select(product => 
                        Elem.Div([Attr.Class("product-item")], [
                            Elem.H3([Attr.Class("product-title")], [Text.Encoded(product.Name)]),
                            Elem.Div([Attr.Class("product-price")], [Text.Encoded(product.Price)]),
                            Elem.P([Attr.Class("product-description")], [Text.Encoded(product.Description)])
                        ])
                    ).ToArray()
                )
            ])
        ])
    ]).RenderToString();
    
    return Results.Content(html, "text/html");
});

// Contact page with POST handling
app.MapGet("/contact", () =>
{
    var html = Elem.Html([Attr.Custom("lang", "en")], [
        Elem.Head([], [
            Elem.Meta([Attr.Custom("charset", "UTF-8")]),
            Elem.Title([], [Text.Encoded("Contact - CinderBlockHtml Examples")]),
            Elem.Create("style", [], [Text.Raw("""
                body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; }
                .container { max-width: 800px; margin: 0 auto; }
                .nav { margin-bottom: 30px; }
                .nav a { margin-right: 15px; color: #007acc; text-decoration: none; }
                .form-group { margin: 15px 0; }
                .form-group label { display: block; margin-bottom: 5px; font-weight: bold; }
                .form-group input, .form-group textarea { width: 100%; padding: 8px; border: 1px solid #ddd; border-radius: 3px; }
                .btn { padding: 10px 20px; background: #007acc; color: white; border: none; border-radius: 5px; cursor: pointer; }
                .success { background: #d4edda; color: #155724; padding: 15px; border-radius: 5px; margin: 20px 0; }
                """)])
        ]),
        Elem.Body([], [
            Elem.Div([Attr.Class("container")], [
                Elem.H1([], [Text.Encoded("Contact Us")]),
                
                Elem.Div([Attr.Class("nav")], [
                    Elem.A([Attr.Href("/")], [Text.Encoded("Home")]),
                    Elem.A([Attr.Href("/about")], [Text.Encoded("About")]),
                    Elem.A([Attr.Href("/products")], [Text.Encoded("Products")]),
                    Elem.A([Attr.Href("/contact")], [Text.Encoded("Contact")])
                ]),
                
                Elem.Form([Attr.Custom("action", "/contact"), Attr.Custom("method", "POST")], [
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "name")], [Text.Encoded("Name:")]),
                        Elem.Input([Attr.Type("text"), Attr.Id("name"), Attr.Name("name"), Attr.Required()])
                    ]),
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "email")], [Text.Encoded("Email:")]),
                        Elem.Input([Attr.Type("email"), Attr.Id("email"), Attr.Name("email"), Attr.Required()])
                    ]),
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "subject")], [Text.Encoded("Subject:")]),
                        Elem.Input([Attr.Type("text"), Attr.Id("subject"), Attr.Name("subject"), Attr.Required()])
                    ]),
                    Elem.Div([Attr.Class("form-group")], [
                        Elem.Label([Attr.Custom("for", "message")], [Text.Encoded("Message:")]),
                        Elem.Create("textarea", [Attr.Id("message"), Attr.Name("message"), Attr.Custom("rows", "5"), Attr.Required()], [])
                    ]),
                    Elem.Button([Attr.Type("submit"), Attr.Class("btn")], [Text.Encoded("Send Message")])
                ])
            ])
        ])
    ]).RenderToString();
    
    return Results.Content(html, "text/html");
});

// Handle contact form submission
app.MapPost("/contact", (IFormCollection form) =>
{
    var name = form["name"].ToString();
    var email = form["email"].ToString();
    var subject = form["subject"].ToString();
    var message = form["message"].ToString();
    
    var html = Elem.Html([Attr.Custom("lang", "en")], [
        Elem.Head([], [
            Elem.Meta([Attr.Custom("charset", "UTF-8")]),
            Elem.Title([], [Text.Encoded("Message Sent - CinderBlockHtml Examples")]),
            Elem.Create("style", [], [Text.Raw("""
                body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; }
                .container { max-width: 800px; margin: 0 auto; }
                .nav { margin-bottom: 30px; }
                .nav a { margin-right: 15px; color: #007acc; text-decoration: none; }
                .success { background: #d4edda; color: #155724; padding: 15px; border-radius: 5px; margin: 20px 0; }
                .message-details { background: #f8f9fa; padding: 20px; border-radius: 5px; margin: 20px 0; }
                """)])
        ]),
        Elem.Body([], [
            Elem.Div([Attr.Class("container")], [
                Elem.H1([], [Text.Encoded("Message Sent Successfully!")]),
                
                Elem.Div([Attr.Class("nav")], [
                    Elem.A([Attr.Href("/")], [Text.Encoded("Home")]),
                    Elem.A([Attr.Href("/about")], [Text.Encoded("About")]),
                    Elem.A([Attr.Href("/products")], [Text.Encoded("Products")]),
                    Elem.A([Attr.Href("/contact")], [Text.Encoded("Contact")])
                ]),
                
                Elem.Div([Attr.Class("success")], [
                    Text.Encoded("Thank you for your message! We'll get back to you soon.")
                ]),
                
                Elem.Div([Attr.Class("message-details")], [
                    Elem.H3([], [Text.Encoded("Message Details:")]),
                    Elem.P([], [Elem.Create("strong", [], [Text.Encoded("Name: ")]), Text.Encoded(name)]),
                    Elem.P([], [Elem.Create("strong", [], [Text.Encoded("Email: ")]), Text.Encoded(email)]),
                    Elem.P([], [Elem.Create("strong", [], [Text.Encoded("Subject: ")]), Text.Encoded(subject)]),
                    Elem.P([], [Elem.Create("strong", [], [Text.Encoded("Message: ")]), Text.Encoded(message)])
                ])
            ])
        ])
    ]).RenderToString();
    
    return Results.Content(html, "text/html");
});

app.Run();