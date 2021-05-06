using ExcelImporter.Editor.ExcelProcessing;
using NUnit.Framework;

namespace ExcelImporter.Tests.Editor
{
    public class ColumnValueTypeExtensionsTests
    {
        [Theory]
        public void ToReplaceSafeHandlesAllValues(ColumnValueType value)
        {
            Assert.DoesNotThrow(() =>
            {
                value.ToReplaceSafe();
            });
        }
    }
}
