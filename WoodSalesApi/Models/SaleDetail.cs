using System;
using System.Collections.Generic;

namespace WoodSalesApi.Models;

public partial class SaleDetail
{
    public long Id { get; set; }

    public long IdSale { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
}
