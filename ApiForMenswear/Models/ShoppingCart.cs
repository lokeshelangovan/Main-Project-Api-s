using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForMenswear.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserSignup userSignup { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductManagement productManagement { get; set; }
        public int Quantity { get; set; }
    }
}
