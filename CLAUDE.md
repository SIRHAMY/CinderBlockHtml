# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CinderBlockHtml is a C# DSL (Domain Specific Language) for building HTML with composable building blocks. It's a port of Pim Brouwer's F# Falco.Markup library to C#, focusing on simple, effective, and efficient HTML generation.

## Architecture

### Core Components

The library is structured around four main modules:

1. **XmlNode Types** - Abstract base classes for the HTML document tree:
   - `XmlNode`: Base class for all nodes
   - `TextNode`: Contains string content
   - `ElementNode`: HTML elements with tag, attributes, and children
   - `SelfClosingNode`: Self-closing elements (input, img, br, etc.)

2. **XmlAttribute Types** - Attribute system:
   - `XmlAttribute`: Base class for all attributes
   - `KeyValueAttr`: Standard name-value attributes
   - `BooleanAttr`: Boolean attributes (checked, disabled, etc.)

3. **Static Helper Classes**:
   - `Text`: Creates text nodes with encoding options (Raw, Encoded, Rawf)
   - `Attr`: Creates attributes with intelligent merging (Class, Id, Style, etc.)
   - `Elem`: Creates HTML elements (Div, P, H1, Form, etc.)
   - `Renderer`: Converts node trees to HTML strings

### Key Features

- **Attribute Merging**: Class and style attributes are automatically merged when multiple instances are provided
- **Text Encoding**: `Text.Encoded()` provides XSS protection, `Text.Raw()` for trusted content
- **Type Safety**: All HTML elements and attributes are strongly typed
- **Composability**: Elements can be nested and combined easily

## Common Development Commands

### Build and Test
```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run specific test project
dotnet test CinderBlockHtml.Tests/

# Build with release configuration
dotnet build --configuration Release
```

### Package Management
```bash
# Restore packages
dotnet restore

# Build NuGet package (automatically done on build due to GeneratePackageOnBuild)
dotnet pack
```

### Running Examples
```bash
# Run the example web application
dotnet run --project CinderBlockHtml.Examples/

# Run benchmarks
dotnet run --project CinderBlockHtml.Benchmarks/ --configuration Release
```

### Running Benchmarks with Docker
```bash
# Build and run benchmarks in Docker
docker run $(docker build -f Dockerfile.Benchmarks -q .)
```

## Project Structure

- `CinderBlockHtml/` - Main library project (.NET 8.0)
- `CinderBlockHtml.Tests/` - Unit tests using xUnit (.NET 9.0)
- `CinderBlockHtml.Examples/` - ASP.NET Core web app demonstrating usage
- `CinderBlockHtml.Benchmarks/` - BenchmarkDotNet performance tests
- `CinderBlockHtml.Benchmarks.Tests/` - Tests for benchmark scenarios

## Testing Strategy

The project uses xUnit for testing with comprehensive coverage of:
- Text encoding and raw content handling
- Element creation and nesting
- Attribute merging (especially class and style)
- Self-closing elements
- Boolean attributes
- Complex HTML structure generation

## Development Notes

- Target framework: .NET 8.0 for main library, .NET 9.0 for tests and examples
- Uses C# 12.0 language features and nullable reference types
- Includes XML documentation for all public APIs
- Follows record-based architecture for immutability
- Uses `System.Web.HttpUtility.HtmlEncode` for text encoding

## Benchmarking

The project includes comprehensive benchmarks comparing against other HTML generation libraries:
- Eighty
- HtmlTags  
- RazorLight
- Scriban

Benchmarks cover Hello World, List generation, and Nested element scenarios.