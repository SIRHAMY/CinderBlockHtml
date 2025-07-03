# Overview

Simple Benchmark against various things.

# Benchmark Results

## Hello World

Render a simple hello world html page using const strings to simulate a small amount of dynamism.

Results from: 2025.07.03 

| Method          | Mean          | Error       | StdDev      | Gen0   | Gen1   | Allocated |
|---------------- |--------------:|------------:|------------:|-------:|-------:|----------:|
| CinderBlockHtml | 1,227.1095 ns |  20.6712 ns |  19.3358 ns | 0.3643 | 0.0019 |    5720 B |
| RawStringHtml   |     0.3551 ns |   0.0539 ns |   0.0504 ns |      - |      - |         - |
| HtmlTagsHtml    | 1,605.5206 ns |  29.2699 ns |  27.3791 ns | 0.4921 | 0.0057 |    7728 B |
| ScribanHtml     | 7,604.5052 ns | 150.2625 ns | 242.6459 ns | 2.1973 | 0.1831 |   35083 B |
| RazorLightHtml  | 3,448.5997 ns |  67.6905 ns | 101.3160 ns | 0.2899 |      - |    4679 B |
| EightyHtml      |   616.2327 ns |   6.6551 ns |   5.8996 ns | 0.0858 |      - |    1352 B |

## ListBenchmark

Render a list of 100 items to html to simulate a long dynamic html page.

Results from: 2025.07.03

| Method          | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| CinderBlockHtml | 16.919 us | 0.3377 us | 0.7622 us | 6.0425 | 0.7324 |  92.78 KB |
| RawStringHtml   |  5.033 us | 0.1007 us | 0.1737 us | 3.1052 | 0.2747 |  47.64 KB |
| HtmlTagsHtml    | 21.675 us | 0.4301 us | 0.6437 us | 7.6599 | 1.8921 | 117.68 KB |
| ScribanHtml     | 47.962 us | 0.9554 us | 2.2521 us | 7.3242 | 0.9766 | 116.53 KB |
| RazorLightHtml  | 19.060 us | 0.2875 us | 0.2689 us | 4.5166 | 0.4883 |  69.58 KB |
| EightyHtml      | 16.646 us | 0.2312 us | 0.2049 us | 2.4719 | 0.1831 |  37.97 KB |

## NestedBenchmark

Render 100 items in nested divs to simulate pages with lots of nested elements, similar to complex pages with components.

Results from: 2025.07.03

| Method          | Mean     | Error    | StdDev   | Gen0    | Gen1   | Allocated  |
|---------------- |---------:|---------:|---------:|--------:|-------:|-----------:|
| CinderBlockHtml | 51.77 us | 0.704 us | 0.659 us | 14.0991 | 2.8076 |  216.75 KB |
| RawStringHtml   | 55.81 us | 1.082 us | 1.111 us | 64.5142 |      - |  988.14 KB |
| HtmlTagsHtml    | 67.02 us | 1.337 us | 2.376 us | 15.8691 | 5.9814 |     244 KB |
| ScribanHtml     | 74.24 us | 1.443 us | 3.619 us | 70.8008 | 5.1270 | 1086.29 KB |
| RazorLightHtml  | 63.25 us | 1.222 us | 1.358 us | 66.7725 | 4.5166 | 1025.32 KB |
| EightyHtml      | 28.67 us | 0.543 us | 0.603 us |  3.7842 | 0.0305 |   58.31 KB |