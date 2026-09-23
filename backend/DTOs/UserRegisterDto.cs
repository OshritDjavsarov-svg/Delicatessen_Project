using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserRegisterDto
    {
        // פרטים אישיים
        [Required]
        public string PrivateName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;

        // פרטי זיהוי (הצפנה)
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, MinLength(6)] // ולידציה בסיסית של הסיסמה
        public string Password { get; set; } = null!;

        // פרטי קשר וכתובת
        public string Phone { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressStreet { get; set; } = null!;
        public int HouseNumber { get; set; }
        public string? BuildEntry { get; set; }
        public string? Apartment { get; set; }
        public string? BuildFloor { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
