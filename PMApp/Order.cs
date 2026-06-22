using System;
using System.Collections.Generic;
using System.Text;

namespace DbClasses;

public class Order
{
    public int OrderId { get; set; }
        
    public Product Products { get; set; } = new();
    public User Users { get; set; } = new();

    public int OrderQtt { get; set; }

}
