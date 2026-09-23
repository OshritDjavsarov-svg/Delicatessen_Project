using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderItemResponseDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!; // מה שמוצג ללקוח
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // המחיר שבו נמכר הפריט (OrderItemPrice מה-DB)
        public decimal LineTotal => Quantity * UnitPrice; // מחיר כמות * יחידה
    }
}
