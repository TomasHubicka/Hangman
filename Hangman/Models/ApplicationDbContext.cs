using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordUser> WordUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<WordUser>()
                .HasKey(c => new { c.WordId, c.UserId });
            modelBuilder.Entity<IdentityUser<Guid>>().HasData(new IdentityUser<Guid>
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Email = "admin@admin.admin",
                NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                EmailConfirmed = true,
                LockoutEnabled = false,
                UserName = "admin@admin.admin",
                NormalizedUserName = "ADMIN@ADMIN.ADMIN",
                PasswordHash = hasher.HashPassword(null, "Admin_1234"),
                SecurityStamp = string.Empty
            });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 1, Category = 0, guessWord = "prase"});
            modelBuilder.Entity<Word>().HasData(new Word { Id = 2, Category = 0, guessWord = "kočka" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 3, Category = 0, guessWord = "krysa" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 4, Category = 0, guessWord = "straka" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 5, Category = 0, guessWord = "brouk" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 6, Category = 1, guessWord = "auto" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 7, Category = 1, guessWord = "autobus" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 8, Category = 1, guessWord = "motorka" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 9, Category = 1, guessWord = "trolejbus" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 10, Category = 1, guessWord = "tramvaj" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 11, Category = 2, guessWord = "pravítko" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 12, Category = 2, guessWord = "tužka" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 13, Category = 2, guessWord = "propiska" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 14, Category = 2, guessWord = "penál" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 15, Category = 2, guessWord = "guma" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 16, Category = 3, guessWord = "Liberec" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 17, Category = 3, guessWord = "Praha" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 18, Category = 3, guessWord = "Plzeň" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 19, Category = 3, guessWord = "Ostrava" });
            modelBuilder.Entity<Word>().HasData(new Word { Id = 20, Category = 3, guessWord = "Antananarivo" });
        }
    }
}
