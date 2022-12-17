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
        //override onconfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) 
        {
            dbContextOptionsBuilder.UseSqlServer("Server=.\\MSSQL2017;Database=AudioShopMSDb;User Id=sa;Password=safa@123;"
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
            //default value
            modelBuilder.Entity<Category>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Domain.Type>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Brand>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Product>().Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
            //relationships : base and reference types and foreign key and cascade delete
            modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasMany(x => x.Types).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasMany(x => x.Brands).WithOne(q => q.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Brand>().HasMany(x => x.Products).WithOne(q => q.Brand).HasForeignKey(p => p.BrandId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Domain.Type>().HasMany(x => x.Products).WithOne(q => q.Type).HasForeignKey(p => p.TypeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(x => x.Carts).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(x => x.Favorites).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(x => x.Comments).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(x => x.OrderDetails).WithOne(q => q.Product).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            //indexes
            modelBuilder.Entity<Product>().HasIndex(x => new { x.ProductName }, "IX_ProductName");
            //auto include (eager loading)
            modelBuilder.Entity<Category>().Navigation(x => x.Brands).AutoInclude();
            modelBuilder.Entity<Category>().Navigation(x => x.Types).AutoInclude();
        }
    }
}
