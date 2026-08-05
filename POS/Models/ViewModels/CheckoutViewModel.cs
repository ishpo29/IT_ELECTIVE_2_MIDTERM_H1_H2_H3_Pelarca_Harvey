using POS.Models.DTOs;
using POS.Models.Entities;

namespace POS.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public CheckoutFormDTO CheckoutForm { get; set; } = new CheckoutFormDTO();
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal GrandTotal { get; set; }
    }
}
