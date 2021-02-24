using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public class CSVHelper: ICSVHelper
    {
        public List<TransactionTemp> GetTransactions(IFormFile file)
        {
            List<TransactionTemp> transactions;
            var result = new StringBuilder();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TransactionCSV>();
                    transactions = records.ToList().Select(x =>
                           new TransactionTemp()
                           {
                               Id = x.TransactionId,
                               Amount = x.Amount,
                               ClientName = x.ClientName,
                               Status = x.Status,
                               Type = x.Type
                           }
                      ).ToList();
                }
            }
            return transactions;
        }
    }
}
