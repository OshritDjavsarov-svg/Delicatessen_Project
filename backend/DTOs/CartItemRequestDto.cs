using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CartItemRequestDto
    {
        [Required]
        public int CustomerId { get; set; } // חובה: מי המשתמש שהוסיף?
        [Required]
        public int ProductId { get; set; } // חובה: איזה מוצר הוסיף?

        [Required, Range(1, 100)]
        public int Quantity { get; set; } = 1; // כמה הוסיף?
    }
}
