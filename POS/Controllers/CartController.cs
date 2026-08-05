using Microsoft.AspNetCore.Mvc;
using POS.Models.DTOs;
using POS.Models.ViewModels;
using POS.Repositories;

namespace POS.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly ShoppingCartRepository _cartRepository;

        public CartController(ProductRepository productRepository, ShoppingCartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        // US-03 / US-04: view the active cart
        public IActionResult Index()
        {
            var cart = _cartRepository.GetCart();
            var viewModel = new CartViewModel
            {
                Items = cart.Items,
                GrandTotal = cart.GrandTotal
            };
            return View(viewModel);
        }


        // US-03 / US-04: view the active cart
        // US-02: add a product from the catalog to the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(AddToCartDTO dto)
        {
            var product = _productRepository.GetById(dto.ProductId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "That product could not be found.";
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = $"Could not add {product.Name} to cart: please enter a valid quantity.";
                return RedirectToAction("Index", "Home");
            }

            // Stock validation: how much of this product is already sitting
            // in the cart, plus what's being requested now, must not exceed stock.
            var cart = _cartRepository.GetCart();
            var alreadyInCart = cart.Items.FirstOrDefault(i => i.ProductId == product.Id)?.Quantity ?? 0;

            if (alreadyInCart + dto.Quantity > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot add {dto.Quantity} x {product.Name} — only {product.StockQuantity - alreadyInCart} left in stock.";
                return RedirectToAction("Index", "Home");
            }

            _cartRepository.AddOrUpdateItem(product, dto.Quantity);
            TempData["SuccessMessage"] = $"Added {dto.Quantity} x {product.Name} to the cart.";
            return RedirectToAction("Index", "Home");
        }

        // US-03: update quantity of an item already in the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(UpdateCartDTO dto)
        {
            var product = _productRepository.GetById(dto.ProductId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "That product could not be found.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please enter a valid quantity.";
                return RedirectToAction(nameof(Index));
            }

            if (dto.Quantity > product.StockQuantity)
            {
                TempData["ErrorMessage"] = $"Cannot set quantity to {dto.Quantity} — only {product.StockQuantity} {product.Name} in stock.";
                return RedirectToAction(nameof(Index));
            }

            _cartRepository.UpdateQuantity(dto.ProductId, dto.Quantity);
            return RedirectToAction(nameof(Index));
        }

        // US-04: remove an item from the cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveItem(int productId)
        {
            _cartRepository.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
