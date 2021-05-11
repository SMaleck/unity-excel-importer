using ExcelImporter.Editor.ExcelProcessing;
using ExcelImporter.Editor.Importing;
using ExcelImporter.Tests.Editor.Mocks;
using NPOI.SS.UserModel;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ExcelImporter.Tests.Editor
{
    public class CellValueParserTests
    {
        [Theory]
        public void GetCellValueHandlesAllValues(ColumnValueType valueType)
        {
            Assert.DoesNotThrow(() =>
            {
                var cellType = GetCellType(valueType);
                var cell = CreateCellMock(cellType);

                CellValueParser.GetCellValue(cell, valueType);
            });
        }

        [Test]
        public void GetCellValueHandles_Float_Dot()
        {
            Assert.DoesNotThrow(() =>
            {
                var cell = CreateCellMock(CellType.String, "0.1");
                var value = CellValueParser.GetCellValue(cell, ColumnValueType.Float);

                Assert.True(value is float);
                Assert.AreEqual(0.1f, (float)value);
            });
        }

        [Test]
        public void GetCellValueHandles_Double_Dot()
        {
            Assert.DoesNotThrow(() =>
            {
                var cell = CreateCellMock(CellType.String, "0.1");
                var value = CellValueParser.GetCellValue(cell, ColumnValueType.Double);

                Assert.True(value is double);
                Assert.AreEqual(0.1d, (double)value);
            });
        }

        private ICell CreateCellMock(
            CellType cellType,
            string stringValue = "item",
            double numValue = 0.1d,
            bool boolValue = false)
        {
            return new CellMock
            {
                CellType = cellType,
                StringCellValue = stringValue,
                NumericCellValue = numValue,
                BooleanCellValue = boolValue,
            };
        }

        private CellType GetCellType(ColumnValueType valueType)
        {
            switch (valueType)
            {
                case ColumnValueType.String:
                case ColumnValueType.StringTrimmed:
                    return CellType.String;

                case ColumnValueType.Int:
                case ColumnValueType.Long:
                case ColumnValueType.Float:
                case ColumnValueType.Double:
                    return CellType.Numeric;

                case ColumnValueType.Bool:
                    return CellType.Boolean;

                default:
                    throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }
    }
}
