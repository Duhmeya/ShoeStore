using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class PickupPoint
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
