using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ElectroStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroStore.DAL
{
    public class ItemsContext : DbContext
    {
        public DbSet<GetIdByArticles>  GetIdByArticles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<StockOfGoods>  StockOfGoods { get; set; }
        public DbSet<Prices> Prices { get; set; }

        public ItemsContext()
        {
           // Database.EnsureDeleted();
           // Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-94EIKF8P\SQLEXPRESS;Database=ElectroStoreDb1;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailsStruct>()
            .HasKey(p => new { p.Id });
            modelBuilder.Entity<DetailsStruct>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ShippingDateDetails>()
           .HasKey(p => new { p.Id });
            modelBuilder.Entity<ShippingDateDetails>().Property(p => p.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<DetailsStruct>().ToTable("DetailsStruct").HasNoKey();
            // использование Fluent API
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Product>()
            //.Property(f => f.Id)
            //.ValueGeneratedOnAdd();

            //modelBuilder.Entity<Order>()
            //    .HasMany(c => c.ProductsList)
            //    .WithMany(s => s.OrderList)
            //    .UsingEntity(j => j.ToTable("OrdersProducts"));
        }

    }
}
