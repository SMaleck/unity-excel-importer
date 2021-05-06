using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ExcelImporter.Editor.Utility
{
    internal class EditorUtils
    {
        private const string ErrorTitle = "Excel Importer Error";
        private const string Ok = "Ok";

        public static void Error(string message)
        {
            EditorUtility.DisplayDialog(ErrorTitle, message, Ok);
            ProgressClear();
        }

        public static void Error(Exception e, string message = "Critical Error")
        {
            var fullMessage = new StringBuilder()
                .AppendLine(message)
                .AppendLine("See console output for more details")
                .AppendLine("")
                .AppendLine("Error:")
                .AppendLine(e.Message)
                .ToString();

            UnityEngine.Debug.LogError(e);
            Error(fullMessage);
        }

        public static void ErrorNoExcelSelected()
        {
            Error("You did not select any Excel files.");
        }

        public static void ErrorProcessingExcelFailed(Exception e)
        {
            Error(e, "Failed to process excel");
        }

        public static void Progress(string info, float progress)
        {
            EditorUtility.DisplayProgressBar("Excel Importer", info, progress);
        }

        public static void ProgressClear()
        {
            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// Returns TRUE if asset was found and loaded
        /// Returns FALSE if asset had to be created
        /// </summary>
        /// <typeparam name="TAsset"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static bool LoadOrCreateAsset<TAsset>(string filePath, out TAsset asset) where TAsset : ScriptableObject
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (!TryLoadAsset<TAsset>(filePath, out asset))
            {
                asset = ScriptableObject.CreateInstance<TAsset>();
                AssetDatabase.CreateAsset((ScriptableObject)asset, filePath);

                return false;
            }

            return true;
        }

        public static bool TryLoadAsset<TAsset>(string filePath, out TAsset asset) where TAsset : ScriptableObject
        {
            asset = (TAsset)AssetDatabase.LoadAssetAtPath(filePath, typeof(TAsset));
            return asset != null;
        }
    }
}
