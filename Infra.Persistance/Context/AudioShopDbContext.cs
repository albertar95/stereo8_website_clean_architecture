using AutoMapper.Execution;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infra.Persistance.Context
{
    public class AudioShopDbContext : DbContext
    {
        //define dbsets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Type> Types { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Domain.File> Files { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<ShipPrice> ShipPrices { get; set; }
        public virtual DbSet<User> Users { get; set; }
        //override onconfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("CONNECTION_STRING"
            , o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)); // split query join
            dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        //override onmodelcreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //primary key
            modelBuilder.Entity<Category>().HasKey(x => x.Id).HasName("PK_Categories");
            modelBuilder.Entity<Domain.Type>().HasKey(x => x.Id).HasName("PK_Types");
            modelBuilder.Entity<Brand>().HasKey(x => x.Id).HasName("PK_Brands");
            modelBuilder.Entity<Product>().HasKey(x => x.Id).HasName("PK_Products");
            modelBuilder.Entity<Domain.File>().HasKey(x => x.Id).HasName("PK_Files");
            modelBuilder.Entity<Cart>().HasKey(x => x.Id).HasName("PK_Carts");
            modelBuilder.Entity<Comment>().HasKey(x => x.Id).HasName("PK_Comments");
            modelBuilder.Entity<Favorite>().HasKey(x => x.Id).HasName("PK_Favorites");
            modelBuilder.Entity<Order>().HasKey(x => x.Id).HasName("PK_Orders");
            modelBuilder.Entity<OrderDetail>().HasKey(x => x.Id).HasName("PK_OrderDetails");
            modelBuilder.Entity<Setting>().HasKey(x => x.Id).HasName("PK_Settings");
            modelBuilder.Entity<Ship>().HasKey(x => x.Id).HasName("PK_Ships");
            modelBuilder.Entity<ShipPrice>().HasKey(x => x.Id).HasName("PK_ShipPrices");
            modelBuilder.Entity<User>().HasKey(x => x.Id).HasName("PK_Users");
            //default value
            modelBuilder.Entity<Category>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Domain.Type>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Brand>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Product>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Domain.File>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Cart>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Comment>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Favorite>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Order>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<OrderDetail>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Setting>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Ship>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            //relationships : base and reference types and foreign key and cascade delete
            modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasMany(x => x.Types).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>().HasMany(x => x.Brands).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Brand>().HasMany(x => x.Products).WithOne(q => q.Brand).HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Domain.Type>().HasMany(x => x.Products).WithOne(q => q.Type).HasForeignKey(p => p.TypeId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>().HasMany(x => x.Carts).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>().HasMany(x => x.Favorites).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>().HasMany(x => x.Comments).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>().HasMany(x => x.OrderDetails).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasMany(x => x.Ships).WithOne(q => q.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne(q => q.Order).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(x => x.Carts).WithOne(q => q.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(x => x.Favorites).WithOne(q => q.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(x => x.Comments).WithOne(q => q.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(x => x.Orders).WithOne(q => q.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(x => x.Products).WithOne(q => q.User).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
            //indexes
            modelBuilder.Entity<Product>().HasIndex(x => new { x.ProductName }, "IX_ProductName");
            //auto include (eager loading)
            modelBuilder.Entity<Product>().Navigation(x => x.Category).AutoInclude();
            modelBuilder.Entity<Product>().Navigation(x => x.Type).AutoInclude();
            modelBuilder.Entity<Product>().Navigation(x => x.Brand).AutoInclude();
            //modelBuilder.Entity<Category>().Navigation(x => x.Brands).AutoInclude();
            //modelBuilder.Entity<Category>().Navigation(x => x.Types).AutoInclude();
            //decimal type declaration
            modelBuilder.Entity<Order>().Property(p => p.MelliCode).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Order>().Property(p => p.TotalPrice).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Order>().Property(p => p.ZipCode).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Product>().Property(p => p.PriceWithoutOff).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Ship>().Property(p => p.ShipPrice).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<Ship>().Property(p => p.ZipCode).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<User>().Property(p => p.ZipCode).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<ShipPrice>().Property(p => p.InnerState).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<ShipPrice>().Property(p => p.NeighborState).HasColumnType("decimal(12,0)");
            modelBuilder.Entity<ShipPrice>().Property(p => p.OtherState).HasColumnType("decimal(12,0)");
        }
    }
}
