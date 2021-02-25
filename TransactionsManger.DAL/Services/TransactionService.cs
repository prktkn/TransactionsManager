using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;
using TransactionsManger.DAL.Services;

namespace TransactionsManager.DAL.Services
{
    public class TransactionService : ITransactionService
    {
        private DbContext _dbContext;
        public TransactionService(DbContext dbCoontext)
        {
            _dbContext = dbCoontext;
        }
        
        public async Task<List<Transaction>> Get(TransactionFilter filter)
        {
            var transaction = await _dbContext.Set<Transaction>()
                .Where(x => (string.IsNullOrEmpty(filter.Status) || x.Status.Equals(filter.Status)) &&
                            (string.IsNullOrEmpty(filter.Type) || x.Status.Equals(filter.Type)) &&
                            x.ClientName.Contains(filter.ClientName ?? string.Empty))
                .ToListAsync();

            return transaction;
        }

        public async Task<Transaction> GetById(int id)
        {
            return await _dbContext.Set<Transaction>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(int id)
        {
            var transaction = await _dbContext.Set<Transaction>().FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Set<Transaction>().Remove(transaction);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Update(int id, string status)
        {
            var transaction = await _dbContext.Set<Transaction>().FirstOrDefaultAsync(x => x.Id == id);
            transaction.Status = status;
            _dbContext.Update(transaction);
            await _dbContext.SaveChangesAsync();

        }

        public async Task Merge(List<TransactionTemp> transactions)
        {
            await _dbContext.Set<TransactionTemp>().AddRangeAsync(transactions);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC MergeTransactions;");
        }
    }
}
