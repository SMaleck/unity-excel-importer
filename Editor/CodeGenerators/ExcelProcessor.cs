using System;
using System.Collections.Generic;
using System.Linq;
using ExcelImporter.Editor.ExcelProcessing;
using ExcelImporter.Editor.Utility;
using UnityEditor;

namespace ExcelImporter.Editor.CodeGenerators
{
    internal static class ExcelProcessor
    {
        public static void Process(IEnumerable<string> excelPaths)
        {
            foreach (var path in excelPaths)
            {
                Process(path);
            }
        }

        public static void Process(string excelPath)
        {
            try
            {
                EditorUtils.Progress("Loading Excel", 0.0f);
                var excelWorkbook = ExcelWorkbookFactory.Create(excelPath);
                Process(excelWorkbook);
            }
            catch (Exception e)
            {
                OnProcessingError(e);
            }
        }

        public static void Process(ExcelWorkbook workbook)
        {
            Process(new[] { workbook });
        }

        public static void Process(IEnumerable<ExcelWorkbook> workbooks)
        {
            try
            {
                workbooks = workbooks.ToArray();

                var progress = 0f;
                var progressChunk = 1f / workbooks.Count();

                foreach (var workbook in workbooks)
                {
                    progress += progressChunk;
                    EditorUtils.Progress($"Generating Code for {workbook.Name}", progress);

                    CodeGenerator.Generate(workbook);
                }

                OnProcessingComplete();
                UnityEngine.Debug.Log($"Successfully processed {workbooks.Count()} workbooks");
            }
            catch (Exception e)
            {
                OnProcessingError(e);
            }
        }

        private static void OnProcessingComplete()
        {
            EditorUtils.ProgressClear();
            AssetDatabase.Refresh();
        }

        private static void OnProcessingError(Exception e)
        {
            EditorUtils.ErrorProcessingExcelFailed(e);
            EditorUtils.ProgressClear();
        }
    }
}
