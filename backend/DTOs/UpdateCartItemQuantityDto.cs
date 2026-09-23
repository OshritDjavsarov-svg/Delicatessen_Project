using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UpdateCartItemQuantityDto
    {
        [Required]
        public int OrderItemId { get; set; } // מזהה הפריט שאותו צריך לעדכן
        [Required, Range(1, 10)]
        public int NewQuantity { get; set; } // הכמות החדשה הרצויה (מעל 0)

        [Required] 
        public int CustomerId { get; set; }
    }
}
