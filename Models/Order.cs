using System;
using System.Collections.Generic;

namespace ShoeStore.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public string Code { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int PickupPointId { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual PickupPoint PickupPoint { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
