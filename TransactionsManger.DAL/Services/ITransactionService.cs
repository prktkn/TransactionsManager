using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;

namespace TransactionsManger.DAL.Services
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> Get(TransactionFilter filter);
        public Task<Transaction> GetById(int id);
        public Task Merge(List<TransactionTemp> transactions);
        public Task Update(int id, string status);
        public Task Delete(int id);
    }
}
