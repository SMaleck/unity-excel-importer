using System.IO;
using System.Linq;
using UnityEditor;

namespace ExcelImporter.Editor.Utility
{
    public static class PathUtils
    {
        private static string _localPath;
        public static string LocalPath => string.IsNullOrEmpty(_localPath)
            ? (_localPath = GetLocalPath())
            : _localPath;

        public static char Sep => Path.DirectorySeparatorChar;

        private static string GetLocalPath()
        {
            var assetPath = AssetDatabase
                .FindAssets($"{nameof(ExcelImporterSettings)} t:Script")
                .Select(AssetDatabase.GUIDToAssetPath)
                .First(e => e.EndsWith($"{nameof(ExcelImporterSettings)}.cs"));

            return Path.GetDirectoryName(assetPath);
        }
    }
}
