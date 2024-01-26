using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Excel
{
    public class ExcelReader
    {
        public List<string> ReadFirstColumn(string filePath)
        {
            var values = new List<string>();

            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                foreach (Row r in sheetData.Elements<Row>())
                {
                    Cell cell = r.GetFirstChild<Cell>();
                    if (cell != null)
                    {
                        values.Add(cell.InnerText);
                    }
                }
            }

            return values;
        }
    }
}
