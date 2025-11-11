## v0.6.0 - 2025.11.10

Major expansion of HTML element support with 37 new elements and improved developer tooling.

### Added
- Text formatting elements: Strong, Em, Code, Pre, Small, Mark, Sub, Sup, Del, Ins, Abbr
- Additional heading levels: H4, H5, H6
- Semantic HTML5 elements: Article, Section, Aside, Nav, Main, Header, Footer
- Complete table support: Table, Thead, Tbody, Tfoot, Tr, Th, Td
- Description list elements: Dl, Dt, Dd
- Form elements: Fieldset, Legend, Textarea
- Block elements: Blockquote
- Other elements: Canvas, Iframe
- Justfile for convenient development commands (test, bench, bench-docker, pack)
- .gitignore file for BenchmarkDotNet artifacts

### Changed
- Updated README with simplified dotnet pack instructions

### Tests
- Added comprehensive test coverage for all new elements

## v0.3.0 - 2025.07.03

Remove `.Pipe` from examples and usages. Replace with `.RenderToString` as an extension method.