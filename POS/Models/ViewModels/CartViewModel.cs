using POS.Models.Entities;

namespace POS.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal GrandTotal { get; set; }
    }
}
