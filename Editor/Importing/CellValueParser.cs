using ExcelImporter.Editor.ExcelProcessing;
using NPOI.SS.UserModel;
using System;
using System.Globalization;

namespace ExcelImporter.Editor.Importing
{
    public static class CellValueParser
    {
        public static object GetCellValue(ICell cell, ColumnValueType cellType)
        {
            object cellValue = null;

            if (cell == null || cell.CellType == CellType.Blank)
            {
                throw new ArgumentException("Cell cannot be NULL or EMPTY");
            }

            switch (cellType)
            {
                case ColumnValueType.String:
                    cellValue = cell.StringCellValue;
                    break;

                case ColumnValueType.StringTrimmed:
                    cellValue = cell.StringCellValue.Trim();
                    break;

                case ColumnValueType.Int:
                    cellValue = (int)cell.NumericCellValue;
                    break;

                case ColumnValueType.Long:
                    cellValue = (long)cell.NumericCellValue;
                    break;

                case ColumnValueType.Float:
                    cellValue = (float)ParseNumericValue(cell);
                    break;

                case ColumnValueType.Double:
                    cellValue = ParseNumericValue(cell);
                    break;

                case ColumnValueType.Bool:
                    cellValue = cell.BooleanCellValue;
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"No operation defined for field type {cellType}");
            }

            if (cellValue == null)
            {
                throw new ArgumentException("Field cannot be NULL");
            }

            return cellValue;
        }

        private static double ParseNumericValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;

                case CellType.String:
                    return Double.Parse(
                        cell.StringCellValue,
                        NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture);

                case CellType.Unknown:
                case CellType.Formula:
                case CellType.Blank:
                case CellType.Boolean:
                case CellType.Error:
                default:
                    throw new ArgumentOutOfRangeException($"Cannot handle CellType {cell.CellType}");
            }
        }
    }
}
