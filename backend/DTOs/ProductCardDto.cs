using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    //יצירת מחלקת כרטיס מוצר לפי הצגת מוצר בתצוגה מינימלית 
    public class ProductCardDto
    {
        public int ProductId { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; } = null!;

        public string? ProductDescription { get; set; }

        public string? FilterProduct { get; set; }

        public decimal ProductPrice { get; set; }

        public string? ImageUrl { get; set; }

    }
}