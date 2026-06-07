using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Suppier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
