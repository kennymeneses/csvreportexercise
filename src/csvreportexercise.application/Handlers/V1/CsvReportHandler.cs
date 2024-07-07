using csvreportexercise.application.Handlers.V1.Interfaces;
using csvreportexercise.application.Responses.V1;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace csvreportexercise.application.Handlers.V1;

public sealed class CsvReportHandler : ICsvReportHandler
{
    public async Task<DownloadCsvReportResponse> GetCsvReport(CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            var workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Full Report");
            IRow headerRow = sheet.CreateRow(1);
            SetSurveyTittle(headerRow, workbook, sheet);
            
            return new DownloadCsvReportResponse()
            {
                FileName = "FullOpenLibraryReport",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Bytes = memoryStream.ToArray()
            };
        }
    }

    private void SetSurveyTittle(IRow rowIn, XSSFWorkbook woorkbook, ISheet excelSheet)
    {
        IFont font = woorkbook.CreateFont();
        font.Color = IndexedColors.Black.Index;
        font.IsBold = true;
        font.FontHeightInPoints = 15;

        ICellStyle style = woorkbook.CreateCellStyle();
        //style.WrapText = true;
        style.Alignment = HorizontalAlignment.Center;
        style.ShrinkToFit = true;
        style.BorderBottom = BorderStyle.Thin;
        style.BorderLeft = BorderStyle.Thin;
        style.SetFont(font);
        
        ICell rowNumberCell = rowIn.CreateCell(1);
        rowNumberCell.CellStyle = style;

        ICell dataRetrievalTypeCell = rowIn.CreateCell(2);
        dataRetrievalTypeCell.CellStyle = style;

        ICell isbnCell = rowIn.CreateCell(4);
        isbnCell.CellStyle = style;

        ICell titleBookCell = rowIn.CreateCell(6);
        titleBookCell.CellStyle = style;
        
        ICell subTitleBookCell = rowIn.CreateCell(8);
        subTitleBookCell.CellStyle = style;

        ICell authorCell = rowIn.CreateCell(10);
        authorCell.CellStyle = style;

        ICell numberPagesBookCell = rowIn.CreateCell(13);
        numberPagesBookCell.CellStyle = style;

        ICell publishDateCell = rowIn.CreateCell(22);
        publishDateCell.CellStyle = style;
        
        rowNumberCell.SetCellValue("Row Number");
        CellRangeAddress mergedNumberCellRegion = new CellRangeAddress(2, 2, 1, 1);
        excelSheet.AddMergedRegion(mergedNumberCellRegion);
        
        dataRetrievalTypeCell.SetCellValue("Data Retrieval Type");
        CellRangeAddress mergedDataRetrievalCellRegion = new CellRangeAddress(2, 2, 2, 3);
        excelSheet.AddMergedRegion(mergedDataRetrievalCellRegion);
        
        isbnCell.SetCellValue("ISBN");
        CellRangeAddress mergedIsbnCellCellRegion = new CellRangeAddress(2, 2, 4, 5);
        excelSheet.AddMergedRegion(mergedIsbnCellCellRegion);
        
        titleBookCell.SetCellValue("Title");
        CellRangeAddress mergedTitleBookCellCellRegion = new CellRangeAddress(2, 2, 6, 7);
        excelSheet.AddMergedRegion(mergedTitleBookCellCellRegion);
        
        subTitleBookCell.SetCellValue("SubTitle");
        CellRangeAddress mergedSubTitleBookCellRegion = new CellRangeAddress(2, 2, 8, 9);
        excelSheet.AddMergedRegion(mergedSubTitleBookCellRegion);
        
        authorCell.SetCellValue("Author Name(s)");
        CellRangeAddress mergedAuthorBookCellCellRegion = new CellRangeAddress(2, 2, 10, 12);
        excelSheet.AddMergedRegion(mergedAuthorBookCellCellRegion);
        
        numberPagesBookCell.SetCellValue("Number of Pages");
        CellRangeAddress mergedNumberPagesCellRegion = new CellRangeAddress(2, 2, 13, 14);
        excelSheet.AddMergedRegion(mergedNumberPagesCellRegion);
        
        publishDateCell.SetCellValue("Publish Date");
        CellRangeAddress mergedPublishDateCellRegion = new CellRangeAddress(2, 2, 15, 16);
        excelSheet.AddMergedRegion(mergedPublishDateCellRegion);
    }
}