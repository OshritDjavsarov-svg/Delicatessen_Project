using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    // העתק של הדאטה ללא קשרי גומלין
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string PrivateName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string City { get; set; } = null!;

        public string AddressStreet { get; set; } = null!;

        public int HouseNumber { get; set; }

        public string? BuildEntry { get; set; }

        public string? Apartment { get; set; }

        public string? BuildFloor { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
