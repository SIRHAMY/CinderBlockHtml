# Overview

CinderBlockHtml is an HTML DSL for building HTML with composable blocks.

It is a port of Pim Brouwer's [Falco.Markup](https://github.com/falcoframework/Falco.Markup) to native C#.

## Purpose

Provide C# with a more ergonomic HTML DSL. Falco.Markup gets a lot right and is usable in C# projects as F#.

However many are reluctant to bridge the gap so here porting a C# version that some may find easier to read in native C# projects.

## To Pack

* Increment version number
* Run tests: `dotnet test --configuration Release`
* Build: `dotnet build --configuration Release`
* Pack: `dotnet pack --configuration Release`
* Upload to nuget