using ExcelImporter.Editor.CodeGenerators;
using ExcelImporter.Editor.ExcelProcessing;
using ExcelImporter.Editor.Utility;
using System;
using UnityEditor;
using UnityEngine;

namespace ExcelImporter.Editor.EditorWindows
{
    public class GenerateImporterWindow : EditorWindow
    {
        public ExcelWorkbook Workbook;

        private bool PrefixAssetNames
        {
            get => Workbook.WorkbookSettings.PrefixAssetNames;
            set => Workbook.WorkbookSettings.PrefixAssetNames = value;
        }

        public static void OpenFor(string filePath)
        {
            try
            {
                var window = CreateInstance<GenerateImporterWindow>();
                var workbook = ExcelWorkbookFactory.Create(filePath);

                window.Initialize(workbook);
                window.Show();
            }
            catch (Exception e)
            {
                EditorUtils.Error(e, $"Failed to initialize generator for file: {filePath}");
            }
        }

        private void Initialize(ExcelWorkbook workbook)
        {
            Workbook = workbook;
            this.titleContent = new GUIContent($"Generate Importer: {Workbook.Name}");
            
            PrefixAssetNames = Settings.PrefixAssetNamesByDefault;

            Show();
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 200;

            GUILayout.Label("Workbook Settings for", EditorStyles.boldLabel);
            GUILayout.Label($"Name: {Workbook.Name}");
            GUILayout.Label($"Path: {Workbook.FilePath}");

            GUILayout.Space(50);
            GUILayout.Label("Checking this, will prefix all import assets with the workbook name", EditorStyles.boldLabel);
            PrefixAssetNames = EditorGUILayout.Toggle("Prefix Asset Names", PrefixAssetNames);

            GUILayout.Space(100);
            if (GUILayout.Button("Generate Importer"))
            {
                ExcelProcessor.Process(Workbook);
                Close();
            }
        }
    }
}
