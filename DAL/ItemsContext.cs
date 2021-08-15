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
        public DbSet<Remains> Remains { get; set; }
        public DbSet<Nomenclature> Nomenclatures { get; set; }

        public ItemsContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-94EIKF8P\SQLEXPRESS;Database=ElectroStoreDb1;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailsStruct>()
            .HasKey(p => new { p.Id });
            modelBuilder.Entity<DetailsStruct>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ShippingDateDetails>()
           .HasKey(p => new { p.Id });
            modelBuilder.Entity<ShippingDateDetails>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Remains>().HasKey(p => new { p.Id });
            modelBuilder.Entity<Remains>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Nomenclature>().ToTable("Nomenclature").HasKey(p => new { p.NomenclatureId });
            modelBuilder.Entity<Nomenclature>().Property(p => p.NomenclatureId).UseIdentityColumn(1, 1);

            modelBuilder.Entity<Unit>().ToTable("Unit").HasKey(p => new { p.UnitId });
            modelBuilder.Entity<Unit>().Property(p => p.UnitId).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Nomenclature>()
            //            .HasOne(a => a.unit).WithOne(b => b.Nomenclature)
            //            .HasForeignKey<Unit>(e => e.RefNomenclatureId);
     

            modelBuilder.Entity<Image>().ToTable("Image").HasKey(p => new { p.id });
            modelBuilder.Entity<Image>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Images600>().ToTable("Images600").HasKey(p => new { p.id });
            modelBuilder.Entity<Images600>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Youtube>().ToTable("Youtube").HasKey(p => new { p.id });
            modelBuilder.Entity<Youtube>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Weight>().ToTable("Weight").HasKey(p => new { p.id });
            modelBuilder.Entity<Weight>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Volume>().ToTable("Volume").HasKey(p => new { p.id });
            modelBuilder.Entity<Volume>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Pack>().ToTable("Pack").HasKey(p => new { p.id });
            modelBuilder.Entity<Pack>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ValueId>().ToTable("ValueId").HasKey(p => new { p.ValueIdKey });
            modelBuilder.Entity<ValueId>().Property(p => p.ValueIdKey).ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.Attribute>().ToTable("Attribute").HasKey(p => new { p.id });
            modelBuilder.Entity<Models.Attribute>().Property(p => p.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<RootNomenclature>().ToTable("RootNomenclature").HasKey(p => new { p.id });
            modelBuilder.Entity<RootNomenclature>().Property(p => p.id).ValueGeneratedOnAdd();



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
