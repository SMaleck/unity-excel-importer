# **Unity Excel Importer**
Allows to import data from Excel to Unity as a ScriptableObject.

This package will inspect an excel and generate code, based on the sheets in the workbook. 
The generated code will then read and import the data from the excel and store it in a `ScriptableObject`, 
which you can then use in your projects runtime. 

# The Moving Parts
> This section explains how the importing works. 
> See **How To Generate an Importer** further below, on how to create importers.

When you generate an excel-importer from an excel file, you will end up with at least two new code files in your project

## `WORKBOOKNAMEWorkbookImporter`
> These **do not** need to be accessible by your game's runtime and 
> it is therefore recommended to keep them in the editor assembly to reduce build-size.

You will have one `WorkbookImporter` for each workbook. This includes the excel's filepath and the settings you chose in the generator window. 
It will read the excel and then export each sheet in it.

## `SHEETNAMEImport`
> These **do need** to be accessible by your game's runtime, as this is the type of the `ScriptableObject` asset

You will have one `SheetImport` for each non-ignored sheet in your workbook. This is a dedicated C# class, representing the sheet.
The fields are generated from the first two rows of the excel. Each sheet must follow the following structure. 
See the example excels in the package's `Runtime` folder.

- ROW 1: Field name
- ROW 2: Field type

**Example Data**
| Id    | Name     | Amount  |
|:-----:|:--------:|:-------:|
| *int* | *string* | *double*|
| 0     | Item0    | 0,1     |
| 1     | Item1    | 0,2     |

**Example C# Class**
```csharp
public class SheetImport : ScriptableObject
{
    [Serializable]
    public class Row
    {
        public int Id;
        public string Name;
        public bool IsActive;
        public double Amount;
    }

    public List<Row> Rows = new List<Row>();
}
```

### *Supported Column Types*
> The usage is **not case-sensitive**, so you can use lower- or upper-case variants of these to your liking

- `String`
- `StringTrimmed` (will additionally trim the string during import)
- `Int`
- `Long`
- `Float`
- `Double`
- `Bool`

# Settings
The ExcelImporter will store its settings as a scriptable object in `Assets/Packages/ExcelImporter/`. 
If it doesn'T exist yet, the asset will be automatically created with default values.

![image][settings_path]

![image][settings_default]

### *Workbook Importer Settings*
Which namespace to use and where to generate `WorkbookImporters`. 
**IMPORTANT:** These **do not** need to be accessible by your game's runtime and 
it is therefore recommended to keep them in the editor assembly to reduce build-size.

### *Sheet Import Settings*
Which namespace to use and where to generate `SheetImports`. 
**IMPORTANT:** These need to be accessible by your game's runtime.

### *Import Asset Path*
Where top store the resulting `ScriptableObject` assets.

### *Misc Settings: Ignored Sheet Prefix*
Any sheet with that prefix will be ignored when importing data.

### *Misc Settings: Ignored Column Prefix*
Any column with that prefix in the header name will be ignored when importing data. 
Only applies to sheets that are not ignored.

### *Misc Settings: Prefix Asset Names by Default*
Checking this will by default check the `PrefixAssetNames` setting in the generator window.

# Tools Menus
![image][tools_menu]

There is a menu under `Tools/Excel Importer` which allows you to perform some global operations. 

### *Open Settings*
Finds and opens the `ExcelImporterSettings` asset in the inspector. This will auto-generate them if they do not exist yet.

### *Reload Settings*
Reloads the settings from the asset. The settings are loaded into a static instance, which might not be discarded properly by Unity in some cases. 
Will also auto-generate them if non-existent.

### *Reset Settings*
This will reset the settings to their default values (see image above). 
Will also auto-generate them if non-existent.

### *Import All*
Will import all excels, for which you already generated importers

### *Import XYZ*
Once you generated some importers, you will be able to import the respective excels from here as well.


# Context Menu
![image][context_menu]

When you right-click an excel, you can use the `Context Menu/Excel Importer` menu items, to perform operations on your selection.

### *Generate Importer*
Will open the generator window, which allows you to generate the importer for the selected excels. 
If you have multiple files selected, this will open **one window per file**, allowing you to edit settings per file.

### *Generate Importers with shared settings*
Will open the generator window, which allows you to generate the importer for the selected excels. 
If you have multiple files selected, this will open **a single window for all files**, allowing you to create multiple importers with the same settings.

### *Import Selected*
Imports the selected excel's data into the `ScriptableObject` asset. 
The importer must have been already generated for this to work.


# How To Generate an Importer
You can create an importer for either one excel or a whole selection. The process is identical either way, so the below description applies also if you selected many files.

### *Step 1:* Select Data
1. Find an excel file in the Unity project browser
1. Right-click it
1. In the context menu, select `Excel Importer/Generate Importer`

### *Step 2:* Generate the Importer
You now see the generator window, with some setting and a button to generate the importer.
You can check `Name` and `Path`, to confirm that you have the excel you wanted.

**Importer Settings**
- `PrefixAssetNames`
    - Checking this will prefix the imported asset name with the workbook's name, so `Sheet1.asset` vs `Workbook1.Sheet1.asset`

Click `Generate Importer` when you are ready.

![image][generator_settings]

### *Step 3:* Using the Importer
> *Step 2* will create at least two new code files in your project. 
> See **The Moving Parts** further above, for details on what they do.

You now have two options on how to import the data.
Either way you will get a `ScriptableObject`  asset in the location you set in the settings.

**Option 1: Context Menu**
1. Right-click the excel
1. In the context menu, select `Excel Importer/Import Selection` 

**Option 2: Tools Menu**
1. In the top menu bar, select `Tools/Excel Importer/Import WORKBOOKNAME`


# Gotchas

## *Moving the Excel*
The generated code includes the excel filepath, so if you move the excel afterwards, the import will fail. 
If you move the excel, you will have to re-generate the importer.

## *Empty Rows*
Ensure you have no empty rows at the end of the excel. Those might cause `NullReferences` during import. 
This can happen, when you just delete the cell content at the end of an excel, instead of deleting the whole row. Excel then tends to keep the row, but with NULL values.
To fix this, simply mark and delete the rows at the bottom of your excel sheet.



[settings_default]: ./excelimporter_docs_settings_default.png "Default Settings Scriptable Object"
[settings_path]: ./excelimporter_docs_settings_path.png "Settings Scriptable Location Object Location"
[tools_menu]: ./excelimporter_docs_toolsmenu.png "Tools Menu"
[context_menu]: ./excelimporter_docs_contextmenu.png "Context Menu"
[generator_settings]: ./excelimporter_docs_generator_settings.png "Generator Settings"