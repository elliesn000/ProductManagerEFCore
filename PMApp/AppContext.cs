using Microsoft.EntityFrameworkCore;
using System;
using DbClasses;
public class AppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PMApp;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
}

