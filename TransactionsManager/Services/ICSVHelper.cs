using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public interface ICSVHelper
    {
        public List<TransactionTemp> GetTransactions(IFormFile file);
    }
}
