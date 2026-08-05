namespace POS.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // File name only, e.g. "espresso.jpg" — resolved in views as
        // ~/images/products/{ImageFileName}
        public string ImageFileName { get; set; } = string.Empty;

        public bool IsOutOfStock => StockQuantity <= 0;
    }
}
