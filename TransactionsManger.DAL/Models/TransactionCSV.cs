using CsvHelper.Configuration.Attributes;

namespace TransactionsManager.DAL.Models
{
    public class TransactionCSV
    {
        [Name("TransactionId")]
        public int TransactionId { get; set; }
        [Name("Status")]
        public string Status { get; set; }
        [Name("Type")]
        public string Type { get; set; }
        [Name("ClientName")]
        public string ClientName { get; set; }
        [Name("Amount")]
        public string Amount { get; set; }
    }
}