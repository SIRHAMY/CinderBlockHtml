# Overview

Simple Benchmark against various things.

# Benchmark Results

## Hello World

Render a simple hello world html page using const strings to simulate a small amount of dynamism.

Results from: 2025.07.03 

| Method          | Mean          | Error       | StdDev      | Gen0   | Gen1   | Allocated |
|---------------- |--------------:|------------:|------------:|-------:|-------:|----------:|
| CinderBlockHtml | 1,205.7198 ns |  23.4702 ns |  37.9000 ns | 0.3643 | 0.0019 |    5720 B |
| RawStringHtml   |     0.2099 ns |   0.0552 ns |   0.0516 ns |      - |      - |         - |
| HtmlTagsHtml    | 1,703.9189 ns |  30.7848 ns |  32.9394 ns | 0.4921 | 0.0057 |    7728 B |
| ScribanHtml     | 7,311.3971 ns | 141.9098 ns | 125.7993 ns | 2.1362 | 0.1831 |   33978 B |
| RazorLightHtml  | 3,344.6396 ns |  57.2531 ns |  85.6937 ns | 0.2899 |      - |    4680 B |
| EightyHtml      |   593.1341 ns |  11.6180 ns |  15.9029 ns | 0.0858 |      - |    1352 B |

## ListBenchmark

Render a list of 100 items to html to simulate a long dynamic html page.

Results from: 2025.07.03

| Method          | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| CinderBlockHtml | 15.586 us | 0.2921 us | 0.3125 us | 6.0425 | 0.7324 |  92.77 KB |
| RawStringHtml   |  4.983 us | 0.0980 us | 0.1007 us | 3.1052 | 0.2823 |  47.64 KB |
| HtmlTagsHtml    | 21.137 us | 0.4193 us | 0.8752 us | 7.6599 | 1.8921 | 117.68 KB |
| ScribanHtml     | 46.780 us | 0.9292 us | 1.7452 us | 7.3242 | 1.2207 | 114.85 KB |
| RazorLightHtml  | 19.145 us | 0.3804 us | 0.7684 us | 4.5166 | 0.4883 |  69.69 KB |
| EightyHtml      | 16.643 us | 0.2204 us | 0.2062 us | 2.4719 | 0.1221 |  37.95 KB |

## NestedBenchmark

Render 100 items in nested divs to simulate pages with lots of nested elements, similar to complex pages with components.

Results from: 2025.07.03

| Method          | Mean     | Error    | StdDev   | Gen0    | Gen1   | Allocated  |
|---------------- |---------:|---------:|---------:|--------:|-------:|-----------:|
| CinderBlockHtml | 52.30 us | 1.006 us | 0.941 us | 14.0991 | 2.9297 |  216.73 KB |
| RawStringHtml   | 53.30 us | 0.872 us | 1.103 us | 64.2700 |      - |  985.66 KB |
| HtmlTagsHtml    | 59.94 us | 1.155 us | 0.965 us | 15.8691 | 6.1035 |  243.98 KB |
| ScribanHtml     | 71.03 us | 1.405 us | 2.347 us | 70.5566 | 5.8594 | 1083.24 KB |
| RazorLightHtml  | 59.52 us | 1.177 us | 2.533 us | 66.6504 | 4.5166 | 1023.01 KB |
| EightyHtml      | 26.15 us | 0.464 us | 0.434 us |  3.7842 | 0.0305 |   58.29 KB |