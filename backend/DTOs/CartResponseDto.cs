using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CartResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; } = null!;
        public List<OrderItemResponseDto> Items { get; set; } = new List<OrderItemResponseDto>();
        // נחשב את הסכום הכולל אוטומטית:
        public decimal TotalPrice => Items.Sum(i => i.LineTotal);
    }
}
