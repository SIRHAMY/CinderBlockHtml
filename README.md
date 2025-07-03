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

## Running Benchmarks

From root: 

```
docker run $(docker build -f Dockerfile.Benchmarks -q .)
```