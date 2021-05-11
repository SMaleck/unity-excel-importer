using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace ExcelImporter.Tests.Editor.Mocks
{
    public class CellMock : ICell
    {
        public int ColumnIndex { get; }
        public int RowIndex { get; }
        public ISheet Sheet { get; }
        public IRow Row { get; }
        public CellType CellType { get; set; }
        public CellType CachedFormulaResultType { get; }
        public string CellFormula { get; set; }
        public double NumericCellValue { get; set; }
        public DateTime DateCellValue { get; set; }
        public IRichTextString RichStringCellValue { get; set; }
        public byte ErrorCellValue { get; set; }
        public string StringCellValue { get; set; }
        public bool BooleanCellValue { get; set; }
        public ICellStyle CellStyle { get; set; }
        public IComment CellComment { get; set; }
        public IHyperlink Hyperlink { get; set; }
        public CellRangeAddress ArrayFormulaRange { get; }
        public bool IsPartOfArrayFormulaGroup { get; }
        public bool IsMergedCell { get; }

        public void SetCellType(CellType cellType)
        {
            throw new NotImplementedException();
        }

        public void SetCellValue(double value)
        {
            throw new NotImplementedException();
        }

        public void SetCellErrorValue(byte value)
        {
            throw new NotImplementedException();
        }

        public void SetCellValue(DateTime value)
        {
            throw new NotImplementedException();
        }

        public void SetCellValue(IRichTextString value)
        {
            throw new NotImplementedException();
        }

        public void SetCellValue(string value)
        {
            throw new NotImplementedException();
        }

        public ICell CopyCellTo(int targetIndex)
        {
            throw new NotImplementedException();
        }

        public void SetCellFormula(string formula)
        {
            throw new NotImplementedException();
        }

        public void SetCellValue(bool value)
        {
            throw new NotImplementedException();
        }

        public void SetAsActiveCell()
        {
            throw new NotImplementedException();
        }

        public void RemoveCellComment()
        {
            throw new NotImplementedException();
        }
    }
}
