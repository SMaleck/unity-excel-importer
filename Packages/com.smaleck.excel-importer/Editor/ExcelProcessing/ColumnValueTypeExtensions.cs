using System;

namespace ExcelImporter.Editor.ExcelProcessing
{
    public static class ColumnValueTypeExtensions
    {
        public static string ToReplaceSafe(this ColumnValueType valueType)
        {
            switch (valueType)
            {
                case ColumnValueType.String:
                case ColumnValueType.StringTrimmed:
                    return "string";

                case ColumnValueType.Int:
                    return "int";

                case ColumnValueType.Long:
                    return "long";

                case ColumnValueType.Float:
                    return "float";

                case ColumnValueType.Double:
                    return "double";

                case ColumnValueType.Bool:
                    return "bool";

                default:
                    throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }
    }
}
