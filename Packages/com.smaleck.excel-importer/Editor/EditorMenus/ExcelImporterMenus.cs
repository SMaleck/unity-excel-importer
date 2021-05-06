using ExcelImporter.Editor.Constants;
using ExcelImporter.Editor.Utility;
using UnityEditor;

namespace ExcelImporter.Editor.EditorMenus
{
    public class ExcelImporterMenus
    {
        [MenuItem(MenuConstants.MenuRoot + "/OpenSettings", priority = MenuConstants.Priority1)]
        public static void OpenSettings()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(Settings.SettingsPath);
        }

        [MenuItem(MenuConstants.MenuRoot + "/Reload Settings", priority = MenuConstants.Priority1)]
        public static void ReloadSettings()
        {
            Settings.Reload();
        }

        [MenuItem(MenuConstants.MenuRoot + "/Reset Settings", priority = MenuConstants.Priority1)]
        public static void ResetSettings()
        {
            Settings.Reset();
        }

        [MenuItem(MenuConstants.MenuRoot + "/Import All", priority = MenuConstants.Priority2)]
        public static void ImportAll()
        {
            var importers = ImportUtils.FindImporterTypes();
            ImportUtils.RunImporters(importers);
        }
    }
}
