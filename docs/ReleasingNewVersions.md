To create a new release: 

* Update the .csproj version
* Add version notes to `CHANGELOG.md`
* Pack
* Upload to Nuget
* Mark release on GitHub


## To Pack

* Increment version number
* Run tests: `dotnet test --configuration Release`
* Build: `dotnet build --configuration Release`
* Pack: `dotnet pack --configuration Release`
* Upload to nuget