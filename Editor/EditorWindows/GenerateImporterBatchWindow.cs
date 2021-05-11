using ExcelImporter.Editor.CodeGenerators;
using ExcelImporter.Editor.ExcelProcessing;
using ExcelImporter.Editor.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ExcelImporter.Editor.EditorWindows
{
    public class GenerateImporterBatchWindow : EditorWindow
    {
        private ExcelWorkbook[] _workbooks;

        private bool PrefixAssetNames { get; set; }

        public static void OpenFor(IEnumerable<string> filePaths)
        {
            try
            {
                var window = CreateInstance<GenerateImporterBatchWindow>();
                var workbooks = filePaths
                    .Select(ExcelWorkbookFactory.Create)
                    .ToArray();

                window.Initialize(workbooks);
                window.Show();
            }
            catch (Exception e)
            {
                EditorUtils.Error(e, $"Failed to initialize generator for {filePaths.Count()} files");
            }
        }

        private void Initialize(ExcelWorkbook[] workbooks)
        {
            _workbooks = workbooks;
            this.titleContent = new GUIContent($"Generate {_workbooks.Length} Importers");

            PrefixAssetNames = Settings.PrefixAssetNamesByDefault;

            Show();
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 200;

            GUILayout.Label($"Workbook Settings for {_workbooks.Length} workbook(s)", EditorStyles.boldLabel);

            GUILayout.Space(25);
            GUILayout.Label("Checking this, will prefix all import assets with the workbook name", EditorStyles.boldLabel);
            PrefixAssetNames = EditorGUILayout.Toggle("Prefix Asset Names", PrefixAssetNames);

            GUILayout.Space(50);
            if (GUILayout.Button("Generate Importer"))
            {
                SetupWorkbooks();
                ExcelProcessor.Process(_workbooks);
                Close();
            }
        }

        private void SetupWorkbooks()
        {
            foreach (var workbook in _workbooks)
            {
                workbook.WorkbookSettings.PrefixAssetNames = PrefixAssetNames;
            }
        }
    }
}
