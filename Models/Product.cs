using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Product
{
    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal Price { get; set; }

    public int Discount { get; set; }

    public int StockQartity { get; set; }

    public string? Descriotion { get; set; }

    public string? Imagepath { get; set; }

    public int? CategoryId { get; set; }

    public int? SuppieId { get; set; }

    public int? ManufactureId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacture { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Suppier? Suppie { get; set; }
}
