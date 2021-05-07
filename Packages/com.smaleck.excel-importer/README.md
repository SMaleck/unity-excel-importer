# Unity Excel Importer
Allows to import data from Excel to Unity as a ScriptableObject

## Dependencies
This package uses [NPOI v2.1.3.1](https://www.nuget.org/packages/NPOI/2.1.3.1).
This is an outdated and officially deprecated version. However it has no additional dependencies of its own, which makes it much easier to include, as you cannot use NuGet to resolve those in Unity. As this package does not need any advanced NPOI features and just needs it for reading excel files, this version will suffice.