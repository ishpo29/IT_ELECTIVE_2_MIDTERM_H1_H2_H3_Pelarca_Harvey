using POS.Models.Entities;

namespace POS.Repositories
{
    // Single active cart, since only one cashier uses the app at a time.
    public class ShoppingCartRepository
    {
        private readonly ShoppingCart _cart = new ShoppingCart();

        public ShoppingCart GetCart() => _cart;

        public void AddOrUpdateItem(Product product, int quantity)
        {
            var existing = _cart.Items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            _cart.Items.RemoveAll(i => i.ProductId == productId);
        }

        public void Clear()
        {
            _cart.Items.Clear();
        }
    }
}
