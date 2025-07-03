# Overview

Simple Benchmark against various things.

# Benchmark Results

## Hello World

Render a simple hello world html page using const strings to simulate a small amount of dynamism.

Results from: 2025.07.03 

| Method          | Mean          | Error       | StdDev      | Gen0   | Gen1   | Allocated |
|---------------- |--------------:|------------:|------------:|-------:|-------:|----------:|
| CinderBlockHtml | 1,308.2109 ns |  24.3699 ns |  34.1632 ns | 0.3643 | 0.0019 |    5720 B |
| RawStringHtml   |     0.1589 ns |   0.0414 ns |   0.0387 ns |      - |      - |         - |
| HtmlTagsHtml    | 1,672.4620 ns |  33.3215 ns |  31.1690 ns | 0.4921 | 0.0057 |    7728 B |
| ScribanHtml     | 7,458.7887 ns | 148.4774 ns | 316.4174 ns | 2.1362 | 0.1831 |   33978 B |
| RazorLightHtml  | 3,493.0972 ns |  68.0552 ns | 115.5629 ns | 0.2899 |      - |    4679 B |

## ListBenchmark

Render a list of 100 items to html to simulate a long dynamic html page.

Results from: 2025.07.03

| Method          | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| CinderBlockHtml | 17.190 us | 0.3241 us | 0.3468 us | 6.0425 | 0.7324 |  92.78 KB |
| RawStringHtml   |  5.063 us | 0.0994 us | 0.1221 us | 3.1052 | 0.3815 |  47.59 KB |
| HtmlTagsHtml    | 22.755 us | 0.4451 us | 0.6929 us | 7.6599 | 1.8921 | 117.68 KB |
| ScribanHtml     | 49.876 us | 0.9848 us | 2.0987 us | 7.3242 | 1.2207 | 115.15 KB |
| RazorLightHtml  | 20.309 us | 0.3925 us | 0.3672 us | 4.5166 | 0.4883 |  69.53 KB |