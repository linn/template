# Template

Linn Template Solution. 

### Usage

Install: `dotnet new -i Linn.Template`

Create new solution: `dotnet new linntemplate -n {namespace} -s {solutionFileName}` 

e.g. `dotnet new linntemplate -n Linn.Events -s Events.sln`

### To publish an updated template:

1) Edit Linn.Template.nuspec to update the version number.
2) Make a new package to upload to Nuget  

     `.\nuget.exe pack .\Linn.Template.nuspec`
3) Push new package to nuget
     
     `dotnet nuget push [new .nupkg file]  --api-key [api key] --source https://www.nuget.org/api/v2/package`
