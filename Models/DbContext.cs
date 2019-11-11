using System;
using Microsoft.EntityFrameworkCore;

namespace Vtc_Freelancer.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SellerCategory> SellerCategory { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;uid=admin;pwd=123456;database=vtc-freelancer");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(x => x.UserId);
                entity.Property(x => x.UserName);
                entity.Property(x => x.Password);
                entity.Property(x => x.Email);
                entity.Property(x => x.FullName);
                entity.Property(x => x.Country);
                entity.Property(x => x.Address);
                entity.Property(x => x.UserLevel);
                entity.Property(x => x.RegisterDate);
                entity.Property(x => x.IsSeller);
                entity.Property(x => x.Avatar);
                entity.Property(x => x.Status);
            });
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(x => x.SellerId);
                entity.Property(x => x.SellerPoint);
                entity.Property(x => x.Description);
                entity.Property(x => x.UserId);
            });
            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasKey(x => x.LanguageId);
                entity.Property(x => x.Language);
                entity.Property(x => x.Level);
                entity.Property(x => x.SellerId);
            });
            modelBuilder.Entity<Skills>(entity =>
            {
                entity.HasKey(x => x.SkillId);
                entity.Property(x => x.SkillName);
                entity.Property(x => x.Level);
                entity.Property(x => x.SellerId);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.CategoryId);
                entity.Property(x => x.CategoryName);
                entity.Property(x => x.ParenId);
            });
            modelBuilder.Entity<SellerCategory>(entity =>
            {
                entity.HasKey(x => x.SellerCategoryId);
                entity.Property(x => x.SellerId);
            });
            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(x => x.RequestId);
                entity.Property(x => x.Delivered);
                entity.Property(x => x.Category);
                entity.Property(x => x.SubCategory);
                entity.Property(x => x.Budget);
                entity.Property(x => x.Description);
                entity.Property(x => x.TimeCreate);
                entity.Property(x => x.Status);
                entity.Property(x => x.UserId);
            });
            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(x => x.OfferId);
                entity.Property(x => x.Description);
                entity.Property(x => x.Amount);
                entity.Property(x => x.Revisions);
                entity.Property(x => x.DeliveryTime);
                entity.Property(x => x.SellerId);
                entity.Property(x => x.RequestId);
                entity.Property(x => x.ServiceId);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.OrderId);
                entity.Property(x => x.WorkStatus);
                entity.Property(x => x.Quantity);
                entity.Property(x => x.OrderTime);
                entity.Property(x => x.UserId);
                entity.Property(x => x.SellerId);
            });
        }
    }
}