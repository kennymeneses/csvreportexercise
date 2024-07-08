using csvreportexercise.application.Commons.V1.Enums;
using csvreportexercise.application.Handlers.V1.Interfaces;
using csvreportexercise.application.Models.V1;
using csvreportexercise.application.Requests.V1;
using csvreportexercise.application.Responses.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace csvreportexercise.application.Handlers.V1;

public sealed class CsvReportHandler(IOpenLibraryService openLibraryService, ICacheService cache) : ICsvReportHandler
{
    public async Task<DownloadCsvReportResponse> GetCsvReport(IsbnInfoFilteredRequest request, CancellationToken cancellationToken)
    {
        List<BookInfo> bookInfoList = new List<BookInfo>();

        foreach (var pair in request.IsbnDict)
        {
            OpenLibraryBookInfo? fullBookInfo= await openLibraryService.GetBookInfo(pair.Key);

            if (fullBookInfo is not null)
            {
                BookInfo bookInfo = new BookInfo()
                {
                    RowNumber = pair.Value,
                    Isbn = pair.Key,
                    Type = DataRetrievalType.Server,
                    Title = fullBookInfo.Details.Title,
                    Subtitle = fullBookInfo.Details.Subtitle,
                    AuthorName = fullBookInfo.Details.Authors[0].Name,
                    NumberPages = fullBookInfo.Details.NumberOfPages,
                    PublishDate = fullBookInfo.Details.PublishDate
                };
                
                bookInfoList.Add(bookInfo);
                cache.StoreBookInfo(bookInfo);
            }
        }

        foreach (BookInfo bookStored in request.BooksStored)
        {
            bookInfoList.Add(bookStored);
        }
        
        using (var memoryStream = new MemoryStream())
        {
            var workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Full Report");
            IRow headerRow = sheet.CreateRow(2);
            SetTittles(headerRow, workbook, sheet);
            DrawnBooksInfo(bookInfoList, workbook, sheet);
            
            workbook.Write(memoryStream);
            
            return new DownloadCsvReportResponse()
            {
                FileName = "FullOpenLibraryReport.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Bytes = memoryStream.ToArray()
            };
        }
    }

    private void DrawnBooksInfo(List<BookInfo> bookInfoList, XSSFWorkbook woorkbook, ISheet excelSheet)
    {
        int rowNum = 3;
        
        foreach (BookInfo bookInfo in bookInfoList)
        {
            IRow rowIn = excelSheet.CreateRow(rowNum);

            ICell rowNumberCell = rowIn.CreateCell(1);
            rowNumberCell.CellStyle = GetTableCellStyle(woorkbook);
            rowNumberCell.SetCellValue(bookInfo.RowNumber);
            
            ICell typeCell = rowIn.CreateCell(2);
            typeCell.CellStyle = GetTableCellStyle(woorkbook);
            typeCell.SetCellValue(bookInfo.Type.ToString());
            CellRangeAddress mergedType = new CellRangeAddress(rowNum, rowNum, 2, 3);
            excelSheet.AddMergedRegion(mergedType);
            
            ICell isbnCell = rowIn.CreateCell(4);
            isbnCell.CellStyle = GetTableCellStyle(woorkbook);
            isbnCell.SetCellValue(bookInfo.Isbn);
            CellRangeAddress mergedIsbn = new CellRangeAddress(rowNum, rowNum, 4, 5);
            excelSheet.AddMergedRegion(mergedIsbn);
            
            ICell titleCell = rowIn.CreateCell(6);
            titleCell.CellStyle = GetTableCellStyle(woorkbook);
            titleCell.SetCellValue((bookInfo.Title));
            CellRangeAddress mergedTitle = new CellRangeAddress(rowNum, rowNum, 6, 8);
            excelSheet.AddMergedRegion(mergedTitle);
            
            ICell subTitleCell = rowIn.CreateCell(9);
            subTitleCell.CellStyle = GetTableCellStyle(woorkbook);
            subTitleCell.SetCellValue(bookInfo.Subtitle);
            CellRangeAddress mergedSubTitle = new CellRangeAddress(rowNum, rowNum, 9, 11);
            excelSheet.AddMergedRegion(mergedSubTitle);
            
            ICell authorCell = rowIn.CreateCell(12);
            authorCell.CellStyle = GetTableCellStyle(woorkbook);
            authorCell.SetCellValue(bookInfo.AuthorName);
            CellRangeAddress mergedAuthor = new CellRangeAddress(rowNum, rowNum, 12, 14);
            excelSheet.AddMergedRegion(mergedAuthor);
            
            ICell numberPagesCell = rowIn.CreateCell(15);
            numberPagesCell.CellStyle = GetTableCellStyle(woorkbook);
            numberPagesCell.SetCellValue(bookInfo.NumberPages);
            CellRangeAddress mergedNumberPages = new CellRangeAddress(rowNum, rowNum, 15, 16);
            excelSheet.AddMergedRegion(mergedNumberPages);
            
            ICell publishDateCell = rowIn.CreateCell(17);
            publishDateCell.CellStyle = GetTableCellStyle(woorkbook);
            publishDateCell.SetCellValue(bookInfo.PublishDate);
            CellRangeAddress mergedPublishDate = new CellRangeAddress(rowNum, rowNum, 17, 18);
            excelSheet.AddMergedRegion(mergedPublishDate);
            
            rowNum++;
        }
    }
    
    private ICellStyle GetTableCellStyle(XSSFWorkbook workbook)
    {
        IFont font = workbook.CreateFont();
        font.Color = IndexedColors.Black.Index;

        ICellStyle style = workbook.CreateCellStyle();
        style.Alignment = HorizontalAlignment.Center;
        style.ShrinkToFit = true;
        style.SetFont(font);

        return style;
    }

    private void SetTittles(IRow rowIn, XSSFWorkbook woorkbook, ISheet excelSheet)
    {
        IFont font = woorkbook.CreateFont();
        font.Color = IndexedColors.Black.Index;
        font.IsBold = true;
        font.FontHeightInPoints = 15;

        ICellStyle style = woorkbook.CreateCellStyle();
        style.WrapText = true;
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
        
        ICell subTitleBookCell = rowIn.CreateCell(9);
        subTitleBookCell.CellStyle = style;

        ICell authorCell = rowIn.CreateCell(12);
        authorCell.CellStyle = style;

        ICell numberPagesBookCell = rowIn.CreateCell(15);
        numberPagesBookCell.CellStyle = style;

        ICell publishDateCell = rowIn.CreateCell(17);
        publishDateCell.CellStyle = style;
        
        rowNumberCell.SetCellValue("Row Number");
        
        dataRetrievalTypeCell.SetCellValue("Data Retrieval Type");
        CellRangeAddress mergedDataRetrievalCellRegion = new CellRangeAddress(2, 2, 2, 3);
        excelSheet.AddMergedRegion(mergedDataRetrievalCellRegion);
        
        isbnCell.SetCellValue("ISBN");
        CellRangeAddress mergedIsbnCellCellRegion = new CellRangeAddress(2, 2, 4, 5);
        excelSheet.AddMergedRegion(mergedIsbnCellCellRegion);
        
        titleBookCell.SetCellValue("Title");
        CellRangeAddress mergedTitleBookCellCellRegion = new CellRangeAddress(2, 2, 6, 8);
        excelSheet.AddMergedRegion(mergedTitleBookCellCellRegion);
        
        subTitleBookCell.SetCellValue("SubTitle");
        CellRangeAddress mergedSubTitleBookCellRegion = new CellRangeAddress(2, 2, 9, 11);
        excelSheet.AddMergedRegion(mergedSubTitleBookCellRegion);
        
        authorCell.SetCellValue("Author Name(s)");
        CellRangeAddress mergedAuthorBookCellCellRegion = new CellRangeAddress(2, 2, 12, 14);
        excelSheet.AddMergedRegion(mergedAuthorBookCellCellRegion);
        
        numberPagesBookCell.SetCellValue("Number of Pages");
        CellRangeAddress mergedNumberPagesCellRegion = new CellRangeAddress(2, 2, 15, 16);
        excelSheet.AddMergedRegion(mergedNumberPagesCellRegion);
        
        publishDateCell.SetCellValue("Publish Date");
        CellRangeAddress mergedPublishDateCellRegion = new CellRangeAddress(2, 2, 17, 18);
        excelSheet.AddMergedRegion(mergedPublishDateCellRegion);
    }
}