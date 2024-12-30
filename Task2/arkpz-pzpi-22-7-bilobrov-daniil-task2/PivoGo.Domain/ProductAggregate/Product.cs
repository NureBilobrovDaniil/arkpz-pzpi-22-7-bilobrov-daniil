using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivoGo.Domain.ProductAggregate
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int StockQuantity { get; set; }
        //public decimal PriceForAll => StockQuantity * Price;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}
