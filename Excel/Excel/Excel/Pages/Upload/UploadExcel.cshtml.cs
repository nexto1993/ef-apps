using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Excel.Pages.Upload
{
    
    public class UploadExcelModel : PageModel
    {
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public List<Contact> Contacts { get; set; }
        public void OnGet()
        {
         
        }
        public void OnPost() 
        {
            if (FileUpload != null && IsExcelFile(FileUpload))
            {
                Contacts = ReadContacts(FileUpload);
            }
            else
            {
                ModelState.AddModelError("", "Please upload a valid Excel file.");
            }
        }
        private bool IsExcelFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }

        private List<Contact> ReadContacts(IFormFile file)
        {
            var contacts = new List<Contact>();

            using (var stream = file.OpenReadStream())
            using (var document = SpreadsheetDocument.Open(stream, false))
            {
                var workbookPart = document.WorkbookPart;
                SharedStringTablePart sharedStringsPart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                var sharedStrings = sharedStringsPart?.SharedStringTable;

                var sheets = workbookPart.Workbook.Descendants<Sheet>();
                var relationshipId = sheets.First().Id.Value;
                var worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                foreach (var row in sheetData.Elements<Row>())
                {
                    var cells = row.Elements<Cell>().ToList();
                    if (cells.Count >= 2)
                    {
                        var nameCell = cells[0];
                        var mobileNumberCell = cells[1];

                        var name = ReadCellValue(nameCell, sharedStrings);
                        var mobileNumber = ReadCellValue(mobileNumberCell, sharedStrings);

                        contacts.Add(new Contact { Name = name, MobileNumber = mobileNumber });
                    }
                }
            }

            return contacts;
        }

        private string ReadCellValue(Cell cell, SharedStringTable sharedStrings)
        {
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                int ssid = int.Parse(cell.CellValue.InnerText);
                return sharedStrings.ElementAt(ssid).InnerText;
            }
            else
            {
                return cell.CellValue?.InnerText;
            }
        }

    }
}
