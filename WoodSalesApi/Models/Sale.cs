using System;
using System.Collections.Generic;

namespace WoodSalesApi.Models;

public partial class Sale
{
    public long Id { get; set; }

    public DateTime DateSale { get; set; }

    public int IdClient { get; set; }

    public decimal? Total { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
