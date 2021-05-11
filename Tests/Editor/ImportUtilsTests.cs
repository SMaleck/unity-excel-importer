using System;
using ExcelImporter.Editor.Utility;
using NUnit.Framework;

namespace ExcelImporter.Tests.Editor
{
    public class ImportUtilsTests
    {
        private static readonly string[] _filePaths1 =
        {
            "/Path/To/Excel.xlsx"
        };

        private static readonly string[] _filePaths2 = 
        {
            "/Path/To/Excel.xlsx",
            "/Path/To/Excel2.xlsx",
            "/Path/To/Excel3"
        };

        private static readonly string[] _filePaths3 = { };

        [Test]
        public void ShouldFilterExcelFiles_1()
        {
            var result = ImportUtils.TryFilterExcelFiles(_filePaths1, out var filePaths);
            
            Assert.True(result);
            Assert.NotNull(filePaths);
            Assert.AreEqual(1, filePaths.Length);
        }

        [Test]
        public void ShouldFilterExcelFiles_2()
        {
            var result = ImportUtils.TryFilterExcelFiles(_filePaths2, out var filePaths);

            Assert.True(result);
            Assert.NotNull(filePaths);
            Assert.AreEqual(2, filePaths.Length);
        }

        [Test]
        public void ShouldFilterExcelFiles_3()
        {
            var result = ImportUtils.TryFilterExcelFiles(_filePaths3, out var filePaths);

            Assert.False(result);
            Assert.NotNull(filePaths);
            Assert.AreEqual(0, filePaths.Length);
        }

        [Test]
        [TestCase("/Path/To/Excel.xlsx", true)]
        [TestCase(@"\Path\To\Excel.xlsx", true)]
        [TestCase("Excel.xlsx", true)]
        [TestCase("/Path/To/Excel.bogus", false)]
        [TestCase("/Path/To/Excel.", false)]
        [TestCase("/Path/To/Excel", false)]
        [TestCase("Excel.bogus", false)]
        [TestCase("Excel.", false)]
        [TestCase("Excel", false)]
        public void ShouldRecognizeExcelFiles(string filePath, bool expected)
        {
            var actual = ImportUtils.IsExcelFile(filePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("/Path/To/Excel.xlsx", "ExcelWorkbookImporter")]
        [TestCase(@"\Path\To\Excel.xlsx", "ExcelWorkbookImporter")]
        [TestCase("Excel.xlsx", "ExcelWorkbookImporter")]
        public void ShouldGenerateWorkbookName(string filePath, string expected)
        {
            var actual = ImportUtils.GenerateImporterNameFromFilePath(filePath);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("/Path/To/Excel.bogus")]
        [TestCase("/Path/To/Excel.")]
        [TestCase("/Path/To/Excel")]
        [TestCase("Excel.bogus")]
        [TestCase("Excel.")]
        [TestCase("Excel")]
        public void GenerateWorkbookNameThrowsOnNonExcelFiles(string filePath)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                ImportUtils.GenerateImporterNameFromFilePath(filePath);
            });
        }
    }
}
