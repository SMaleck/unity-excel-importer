using ExcelImporter.Editor.Constants;
using ExcelImporter.Editor.EditorWindows;
using ExcelImporter.Editor.Utility;
using System.Linq;
using UnityEditor;

namespace ExcelImporter.Editor.EditorMenus
{
    public class ExcelImporterContextMenus
    {
        [MenuItem(MenuConstants.ContextRoot + "/Generate Importers")]
        public static void GenerateImporters()
        {
            if (!ImportUtils.TryFilterExcelFilesInSelection(out var filePaths))
            {
                EditorUtils.ErrorNoExcelSelected();
                return;
            }

            foreach (var filePath in filePaths)
            {
                GenerateImporterWindow.OpenFor(filePath);
            }
        }

        [MenuItem(MenuConstants.ContextRoot + "/Generate Importers with shared settings")]
        public static void GenerateImportersBatch()
        {
            if (!ImportUtils.TryFilterExcelFilesInSelection(out var filePaths))
            {
                EditorUtils.ErrorNoExcelSelected();
                return;
            }

            GenerateImporterBatchWindow.OpenFor(filePaths);
        }

        [MenuItem(MenuConstants.ContextRoot + "/Import Selected")]
        public static void ImportSelected()
        {
            if (!ImportUtils.TryFilterExcelFilesInSelection(out var filePaths))
            {
                return;
            }

            var exporterNames = ExtractExporterNames(filePaths);
            var exporterTypes = ImportUtils.FindImporterTypes()
                .Where(e => exporterNames.Contains(e.Name))
                .ToArray();

            ImportUtils.RunImporters(exporterTypes);
        }

        private static string[] ExtractExporterNames(string[] paths)
        {
            return paths
                .Select(ImportUtils.GenerateImporterNameFromFilePath)
                .ToArray();
        }
    }
}
