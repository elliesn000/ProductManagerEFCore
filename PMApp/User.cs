using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace DbClasses;
public class User
{   
    public string UserId { get; set; } = "null";
    public decimal UserWallet { get; set; }

    public List<Order> Order = new();
}