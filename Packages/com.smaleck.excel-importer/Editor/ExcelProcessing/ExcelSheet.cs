using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace ExcelImporter.Editor.ExcelProcessing
{
    public class ExcelSheet
    {
        public ISheet Sheet { get; }
        public ExcelColumn[] Columns { get; }

        public string Name => Sheet.SheetName;

        public ExcelSheet(ISheet sheet)
        {
            Sheet = sheet;
            Columns = GetColumns(sheet);
        }

        private ExcelColumn[] GetColumns(ISheet sheet)
        {
            AssertSheetIsValid(sheet);

            var columns = new List<ExcelColumn>();

            var headerRow = sheet.GetRow(0);
            var valueTypeRow = sheet.GetRow(1);

            for (var i = 0; i < headerRow.LastCellNum; i++)
            {
                var headerCell = headerRow.Cells[i];
                var valueTypeCell = valueTypeRow.Cells[i];

                if (IsIgnored(headerCell))
                {
                    continue;
                }

                var column = new ExcelColumn(
                    headerCell.StringCellValue,
                    GetColumnValueType(valueTypeCell));

                columns.Add(column);
            }

            return columns.ToArray();
        }

        private static void AssertSheetIsValid(ISheet sheet)
        {
            if (sheet.GetRow(0) == null)
            {
                throw new ArgumentException("Header Row is not defined");
            }
            if (sheet.GetRow(1) == null)
            {
                throw new ArgumentException("ValueType row is not defined");
            }
            if (sheet.GetRow(2) == null)
            {
                throw new ArgumentException("At least one data row must be defined");
            }
        }

        private static ColumnValueType GetColumnValueType(ICell cell)
        {
            var value = cell.StringCellValue;
            return (ColumnValueType)Enum.Parse(typeof(ColumnValueType), value, true);
        }

        private static bool IsIgnored(ICell cell)
        {
            return cell.StringCellValue.StartsWith(Settings.IgnoredColumnPrefix);
        }
    }
}
