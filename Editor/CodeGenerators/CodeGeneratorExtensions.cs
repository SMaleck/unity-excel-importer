using System.Globalization;

namespace ExcelImporter.Editor.CodeGenerators
{
    internal static class CodeGeneratorExtensions
    {
        public const string WorkbookImporterSuffix = "WorkbookImporter";
        public const string SheetImportSuffix = "Table";

        private static readonly char[] TrimChars = { ' ', '-', '+', ':', ',', ';' };

        public static string ToSanitizedName(this string name)
        {
            return name.Trim(TrimChars);
        }

        public static string ToWorkbookClassName(this string name)
        {
            return $"{name.ToSanitizedName()}{WorkbookImporterSuffix}";
        }

        public static string ToSheetClassName(this string name)
        {
            return $"{name.ToSanitizedName()}{SheetImportSuffix}";
        }

        public static string ToReplaceSafe(this bool value)
        {
            return value.ToString().ToLowerInvariant();
        }

        public static string ToReplaceSafe(this int value)
        {
            return $"{NumberToString(value)}";
        }

        public static string ToReplaceSafe(this float value)
        {
            return $"{NumberToString(value)}f";
        }

        public static string ToReplaceSafe(this double value)
        {
            return $"{NumberToString(value)}d";
        }

        private static string NumberToString(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
