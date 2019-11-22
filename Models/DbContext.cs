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
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationDetail> NotificationDetail { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<ConversationDetail> ConversationDetail { get; set; }
        public DbSet<FAQ> FAQ { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;uid=root;pwd=Hanhphuc1;database=vtc_freelancer");
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
                entity.Property(x => x.RegisterDateSeller);
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
                entity.Property(x => x.SkillLevel);
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
                entity.HasKey(x => new { x.SellerId, x.CategoryId });

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
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(x => x.OrderId);
                entity.Property(x => x.WorkStatus);
                entity.Property(x => x.Quantity);
                entity.Property(x => x.OrderTime);
                entity.Property(x => x.UserId);
                entity.Property(x => x.ServiceId);
            });
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(x => x.ConversationId);
                entity.Property(x => x.SellerId);
                entity.Property(x => x.BuyerId);
            });
            modelBuilder.Entity<ConversationDetail>(entity =>
            {
                entity.HasKey(x => x.ConversationDetailId);
                entity.Property(x => x.ConversationId);
                entity.Property(x => x.SenderId);
                entity.Property(x => x.Content);
                entity.Property(x => x.Status);
                entity.Property(x => x.TimeSend);
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(x => x.NotificationId);
                entity.Property(x => x.Content);
                entity.Property(x => x.TimeSend);
                entity.Property(x => x.UserId);

            });
            modelBuilder.Entity<NotificationDetail>(entity =>
            {
                entity.HasKey(x => x.NotificationDetailId);
                entity.Property(x => x.NotificationId);
                entity.Property(x => x.UserId);
                entity.Property(x => x.Status);

            });
            modelBuilder.Entity<Service>(entity =>
          {
              entity.HasKey(x => x.ServiceId);
              entity.Property(x => x.Title);
              entity.Property(x => x.Category);
              entity.Property(x => x.SubCategory);
              entity.Property(x => x.Description);
              entity.Property(x => x.TimeCreateService);
              entity.Property(x => x.Status);
              entity.Property(x => x.SellerId);

          });
            modelBuilder.Entity<Package>(entity =>
           {
               entity.HasKey(x => x.PackageId);
               entity.Property(x => x.Name);
               entity.Property(x => x.Description);
               entity.Property(x => x.Price);
               entity.Property(x => x.NumberRevision);
               entity.Property(x => x.DeliveryTime);
               entity.Property(x => x.ServiceId);


           });
            modelBuilder.Entity<Rating>(entity =>
           {
               entity.HasKey(x => x.RatingId);
               entity.Property(x => x.CountStar);
               entity.Property(x => x.Content);
               entity.Property(x => x.ServiceId);
               entity.Property(x => x.UserId);

           });
            modelBuilder.Entity<Report>(entity =>
           {
               entity.HasKey(x => x.ReportId);
               entity.Property(x => x.TitleReport);
               entity.Property(x => x.ContentReport);
               entity.Property(x => x.TimeCreateReport);
               entity.Property(x => x.Status);
               entity.Property(x => x.ServiceId);
               entity.Property(x => x.UserId);

           });
            modelBuilder.Entity<Tag>(entity =>
           {
               entity.HasKey(x => x.TagId);
               entity.Property(x => x.TagName);
               entity.Property(x => x.ServiceId);


           });
            modelBuilder.Entity<FAQ>(entity =>
           {
               entity.HasKey(x => x.FAQId);
               entity.Property(x => x.Question);
               entity.Property(x => x.Reply);
               entity.Property(x => x.ServiceId);


           });
        }
    }
}