using System;
using System.Collections.Generic;

namespace DAL.models;

public partial class HighQuantity
{
    public int Hqid { get; set; }

    public int ProductId { get; set; }

    public bool HighSalt { get; set; }

    public bool HighSugar { get; set; }

    public bool HighFat { get; set; }

    public virtual Product Product { get; set; } = null!;
}
