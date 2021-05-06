using ExcelImporter.Editor.Utility;
using System.Collections.Generic;
using System.IO;

namespace ExcelImporter.Editor.CodeTemplates
{
    public static class Templates
    {
        private static readonly string Root = $"{PathUtils.LocalPath}{PathUtils.Sep}CodeTemplates{PathUtils.Sep}";

        public static string CodeGenNoticeTemplate = $"{Root}GeneratedCodeNotice.cs.txt";
        public static string WorkbookImporterTemplate = $"{Root}WorkbookImporterTemplate.cs.txt";
        public static string SheetImportTemplate = $"{Root}SheetImportTemplate.cs.txt";
        public static string ImportStatementTemplate = $"{Root}ImportStatementTemplate.cs.txt";

        public static string Read(string readTemplatePath)
        {
            var template = File.ReadAllText(readTemplatePath);
            var codeGenNotice = File.ReadAllText(CodeGenNoticeTemplate);

            return template.Replace(TemplateKeys.CODEGEN_NOTICE, codeGenNotice);
        }

        public static void Write(
            string readTemplatePath,
            string writeFilePath,
            Dictionary<string, string> data)
        {
            var template = Read(readTemplatePath);

            foreach (var kvp in data)
            {
                template = template.Replace(kvp.Key, kvp.Value);
            }

            var directory = Path.GetDirectoryName(writeFilePath);
            Directory.CreateDirectory(directory);

            File.WriteAllText(writeFilePath, template);
        }
    }
}
