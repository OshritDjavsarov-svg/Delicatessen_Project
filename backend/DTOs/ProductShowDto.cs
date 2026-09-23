using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductShowDto
    {
        public int ProductId { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; } = null!;

        public string? ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public string? PriceDescription { get; set; }

        public string? HeatingInstruction { get; set; }

        public string? Allergens { get; set; }

        public string? Ingredients { get; set; }

        public string? NutritionalValues { get; set; }

        public string? FilterProduct { get; set; }

        public string? ImageUrl { get; set; }

        // שדות מטבלת HighQuantity שיומרו למערך
        public int[] HighQuantity { get; set; }

    }
}
