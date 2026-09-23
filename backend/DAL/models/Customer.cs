using System;
using System.Collections.Generic;

namespace DAL.models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string PrivateName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string City { get; set; } = null!;

    public string AddressStreet { get; set; } = null!;

    public int HouseNumber { get; set; }

    public string? BuildEntry { get; set; }

    public string? Apartment { get; set; }

    public string? BuildFloor { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
