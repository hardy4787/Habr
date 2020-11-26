using AuthProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Publication>()
                .HasMany(p => p.Categories)
                .WithMany(p => p.Publications)
                .UsingEntity(j => j.ToTable("CategoryPublication"));

            string password = "kek";
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User { 
                        Id = 1,
                        FirstName = "Bohdan",
                        LastName = "Ivanov", 
                        Username="hardy4787", 
                        PasswordHash=passwordHash, 
                        PasswordSalt= passwordSalt
                    },
                });

            modelBuilder.Entity<Category>().HasData(
                new Category[]
                {
                    new Category { Id = 1, Name = "govno" },
                    new Category { Id = 2, Name = "mo4a" },
                    new Category { Id = 3, Name = "ponos" }
                });

            modelBuilder.Entity<Publication>().HasData(
                new Publication[]
                {
                    new Publication { Id = 1, Title = "Tom", Content = "23", UserId = 1 },
                    new Publication { Id = 2, Title = "Alice", Content = "26", UserId = 1 },
                    new Publication { Id = 3, Title = "Sam", Content = "28", UserId = 1 },
                });

            modelBuilder
                .Entity<Publication>()
                .HasMany(p => p.Categories)
                .WithMany(p => p.Publications)
                .UsingEntity(j => j.HasData(new { CategoriesId = 1, PublicationsId = 1 }));

            //modelBuilder.Entity<CategoryPublication>
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
