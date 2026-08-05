using POS.Models.Entities;

namespace POS.Repositories
{
    // Registered as a Singleton in Program.cs so the same in-memory list
    // is shared across every request (no database, per the spec).
    public class ProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Espresso",                  Price = 3.50m, StockQuantity = 25, ImageFileName = "espresso.jpg" },
                new Product { Id = 2, Name = "Cappuccino",                Price = 4.50m, StockQuantity = 20, ImageFileName = "cappuccino.jpg" },
                new Product { Id = 3, Name = "Caffe Latte",               Price = 4.75m, StockQuantity = 20, ImageFileName = "latte.jpg" },
                new Product { Id = 4, Name = "Americano",                 Price = 3.25m, StockQuantity = 22, ImageFileName = "americano.jpg" },
                new Product { Id = 5, Name = "Iced Caramel Macchiato",    Price = 5.25m, StockQuantity = 15, ImageFileName = "caramel-macchiato.jpg" },
                new Product { Id = 6, Name = "Butter Croissant",          Price = 3.00m, StockQuantity = 12, ImageFileName = "croissant.jpg" },
                new Product { Id = 7, Name = "Blueberry Muffin",          Price = 3.25m, StockQuantity = 0,  ImageFileName = "blueberry-muffin.jpg" },
                new Product { Id = 8, Name = "Chocolate Chip Cookie",     Price = 2.50m, StockQuantity = 30, ImageFileName = "choc-chip-cookie.jpg" },
            };
        }

        public List<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void DeductStock(int id, int quantity)
        {
            var product = GetById(id);
            if (product != null)
            {
                product.StockQuantity = Math.Max(0, product.StockQuantity - quantity);
            }
        }
    }
}
