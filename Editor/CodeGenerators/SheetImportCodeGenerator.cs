using ExcelImporter.Editor.CodeTemplates;
using ExcelImporter.Editor.ExcelProcessing;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelImporter.Editor.CodeGenerators
{
    public static class SheetImportCodeGenerator
    {
        public static void Generate(ExcelSheet sheet)
        {
            var data = new Dictionary<string, string>();

            var className = sheet.Name.ToSheetClassName();

            data.Add(TemplateKeys.NAMESPACE, Settings.SheetNamespace);
            data.Add(TemplateKeys.CLASS_NAME, className);
            data.Add(TemplateKeys.FIELDS, GenerateFields(sheet));

            Templates.Write(
                Templates.SheetImportTemplate,
                GetFilePath(className),
                data);
        }

        private static string GetFilePath(string name)
        {
            return Path.Combine(Settings.SheetCodePath, $"{name}.cs");
        }

        private static string GenerateFields(ExcelSheet sheet)
        {
            var sb = new StringBuilder();

            foreach (var col in sheet.Columns)
            {
                var type = col.Type.ToReplaceSafe();
                sb.AppendLine($"            public {type} {col.Name};");
            }

            return sb.ToString();
        }
    }
}
