﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForMenswear.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order order { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductManagement productManagement { get; set; }
        public int Quantity { get; set;}
        public decimal Price { get; set; }
    }
}
