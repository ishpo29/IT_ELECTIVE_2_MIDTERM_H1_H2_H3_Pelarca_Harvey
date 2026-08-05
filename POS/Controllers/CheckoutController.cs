using Microsoft.AspNetCore.Mvc;
using POS.Models.DTOs;
using POS.Models.ViewModels;
using POS.Repositories;

namespace POS.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ShoppingCartRepository _cartRepository;
        private readonly TransactionRepository _transactionRepository;

        public CheckoutController(
            ProductRepository productRepository,
            ShoppingCartRepository cartRepository,
            TransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = _cartRepository.GetCart();

            // AC: cannot proceed to checkout with an empty cart
            if (cart.IsEmpty)
            {
                TempData["ErrorMessage"] = "Your cart is empty. Add some items before checking out.";
                return RedirectToAction("Index", "Cart");
            }

            var viewModel = new CheckoutViewModel
            {
                Items = cart.Items,
                GrandTotal = cart.GrandTotal
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutFormDTO checkoutForm)
        {
            var cart = _cartRepository.GetCart();

            if (cart.IsEmpty)
            {
                TempData["ErrorMessage"] = "Your cart is empty. Add some items before checking out.";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new CheckoutViewModel
                {
                    CheckoutForm = checkoutForm,
                    Items = cart.Items,
                    GrandTotal = cart.GrandTotal
                };
                return View(viewModel);
            }

            // Final stock re-check in case anything changed since items were added
            foreach (var item in cart.Items)
            {
                var product = _productRepository.GetById(item.ProductId);
                if (product == null || item.Quantity > product.StockQuantity)
                {
                    ModelState.AddModelError(string.Empty, $"{item.ProductName} no longer has enough stock to complete this sale.");
                    var viewModel = new CheckoutViewModel
                    {
                        CheckoutForm = checkoutForm,
                        Items = cart.Items,
                        GrandTotal = cart.GrandTotal
                    };
                    return View(viewModel);
                }
            }

            // AC3: create Transaction record
            var transaction = _transactionRepository.Create(
                checkoutForm.CustomerName,
                checkoutForm.CustomerEmail,
                new List<Models.Entities.CartItem>(cart.Items),
                cart.GrandTotal);

            // AC4: deduct purchased quantities from product stock
            foreach (var item in cart.Items)
            {
                _productRepository.DeductStock(item.ProductId, item.Quantity);
            }

            // AC5: clear the active cart
            _cartRepository.Clear();

            return RedirectToAction(nameof(Success), new { id = transaction.TransactionId });
        }

        [HttpGet]
        public IActionResult Success(int id)
        {
            var transaction = _transactionRepository.GetById(id);
            if (transaction == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(transaction);
        }
    }
}
