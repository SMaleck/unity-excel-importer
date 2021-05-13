# Unity Excel Importer
Allows to import data from Excel to Unity as a ScriptableObject

## Dependencies
This package uses [NPOI v2.1.3.1](https://www.nuget.org/packages/NPOI/2.1.3.1).
This is an outdated and officially deprecated version. However it has no additional dependencies of its own, which makes it much easier to include, as you cannot use NuGet to resolve those in Unity. As this package does not need any advanced NPOI features and just needs it for reading excel files, this version will suffice.

## Quick Start
> See the releases or the [CHANGELOG](./Packages/com.smaleck.excel-importer/CHANGELOG.md) for the current version.

To use this package in your Unity project, you have to manually add it to you `manifest.json`.

1. Go to `YourProject/Packages/`
2. Open `manifest.json`
3. Add this packages git repository to the dependencies object in the JSON:

### Example:
```json
{
  "dependencies": {
    "com.smaleck.excel-importer": "git://github.com/SMaleck/unity-excel-importer.git#v1.0.0"
  }
}
```

## How To
See the [documentation](./Documentation/ExcelImporter.md) for details.