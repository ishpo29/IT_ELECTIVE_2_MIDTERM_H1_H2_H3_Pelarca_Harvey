using Microsoft.AspNetCore.Mvc;
using POS.Repositories;

namespace POS.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // US-06 AC1: list of all completed transactions
        public IActionResult Index()
        {
            var transactions = _transactionRepository.GetAll();
            return View(transactions);
        }

        // US-06 AC2: details of a specific sale
        public IActionResult Details(int id)
        {
            var transaction = _transactionRepository.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }
    }
}
