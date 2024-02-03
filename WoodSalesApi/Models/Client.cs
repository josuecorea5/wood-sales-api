using System;
using System.Collections.Generic;

namespace WoodSalesApi.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
