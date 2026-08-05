using POS.Models.Entities;

namespace POS.Repositories
{
    public class TransactionRepository
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private int _nextId = 1;

        public Transaction Create(string customerName, string? customerEmail, List<CartItem> items, decimal total)
        {
            var transaction = new Transaction
            {
                TransactionId = _nextId++,
                Date = DateTime.Now,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                TotalAmount = total,
                PurchasedItems = items
            };

            _transactions.Add(transaction);
            return transaction;
        }

        public List<Transaction> GetAll() => _transactions.OrderByDescending(t => t.Date).ToList();

        public Transaction? GetById(int id) => _transactions.FirstOrDefault(t => t.TransactionId == id);
    }
}
