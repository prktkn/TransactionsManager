using System.Collections.Generic;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public interface IExcelHelper
    {
         public byte[] GetExcelFileContent(List<Transaction> transactions);
    }
}
