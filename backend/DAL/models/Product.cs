using System;
using System.Collections.Generic;

namespace DAL.models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

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

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<HighQuantity> HighQuantities { get; set; } = new List<HighQuantity>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
