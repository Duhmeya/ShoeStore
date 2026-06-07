using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Orderitem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
