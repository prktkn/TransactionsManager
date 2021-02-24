using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public class ExcelHelper : IExcelHelper
    {
        public byte[] GetExcelFileContent(List<Transaction> transactions)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Transactions");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "User Name";
                worksheet.Cell(currentRow, 2).Value = "Amount";
                worksheet.Cell(currentRow, 3).Value = "Type";
                foreach (var transaction in transactions)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = transaction.ClientName;
                    worksheet.Cell(currentRow, 2).Value = transaction.Amount;
                    worksheet.Cell(currentRow, 3).Value = transaction.Type;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return content;
                }
            }
        }
    }
}
