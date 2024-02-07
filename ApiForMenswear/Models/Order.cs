using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiForMenswear.Models
{
	public class Order
	{
        public int Id { get; set; }
       
		public int UserSignupId { get; set; }
        
        public UserSignup UserSignup { get; set; }
       
		public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }       
       
        public string? ShippingAddress { get; set; }
         public string? OrderStatus { get; set; }
        public string? FullName { get; set; }
        public int CreditCard { get; set; }




    }
}
