using ExcelImporter.Editor.CodeGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace ExcelImporter.Editor.Utility
{
    public class ImportUtils
    {
        public const string ExcelFileExtension = ".xlsx";

        public static bool TryFilterExcelFilesInSelection(out string[] excelPaths)
        {
            var paths = Selection.objects
                .Select(AssetDatabase.GetAssetPath);

            return TryFilterExcelFiles(paths, out excelPaths);
        }

        public static bool TryFilterExcelFiles(IEnumerable<string> filePaths, out string[] excelPaths)
        {
            excelPaths = filePaths
                .Where(IsExcelFile)
                .ToArray();

            return excelPaths.Any();
        }

        public static bool IsExcelFile(string path)
        {
            return Path.GetExtension(path) == ExcelFileExtension;
        }

        public static Type[] FindImporterTypes()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass &&
                               type.Namespace == Settings.ImporterNamespace &&
                               type.Name.EndsWith(CodeGeneratorExtensions.WorkbookImporterSuffix))
                .Distinct()
                .ToArray();
        }

        public static void RunImporters(Type[] types)
        {
            foreach (var type in types)
            {
                RunImporter(type);
            }
        }

        public static void RunImporter(Type importer)
        {
            var methodInfo = importer.GetMethod("Import");
            if (methodInfo != null)
            {
                methodInfo.Invoke(importer, new object[] { });
            }
        }

        public static string GenerateImporterNameFromFilePath(string filePath)
        {
            if (!IsExcelFile(filePath))
            {
                throw new ArgumentException("Attempted to generate Workbook name for non-excel file");
            }

            var filename = Path.GetFileNameWithoutExtension(filePath);
            return filename.ToWorkbookClassName();
        }
    }
}
