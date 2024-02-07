﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForMenswear.Models
{
	public class ProductManagement
	{
		public int Id { get; set; }
		public string? Name { get; set; }		
		public string? Description { get; set; }
		public decimal? Price { get; set; }
		public int Quantity { get; set; }            
        public string? ImageBase64 { get; set; }
    }
}
