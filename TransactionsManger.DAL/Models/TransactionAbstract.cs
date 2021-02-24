namespace TransactionsManager.DAL.Models
{
    public class TransactionAbstract
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string Amount { get; set; }
    }
}
