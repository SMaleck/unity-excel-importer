using ExcelImporter.Editor.ExcelProcessing;

namespace ExcelImporter.Editor.CodeGenerators
{
    internal static class CodeGenerator
    {
        public static void Generate(ExcelWorkbook workbook)
        {
            WorkbookImporterCodeGenerator.Generate(workbook);

            foreach (var sheet in workbook.Sheets.Values)
            {
                SheetImportCodeGenerator.Generate(sheet);
            }
        }
    }
}
