using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForMenswear.Models
{
	public class Order
	{
        public int Id { get; set; }

       
		public int UserSignupId { get; set; }
		public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }

        // Foreign key
       

        // Navigation property
       
    }
}
