using Microsoft.EntityFrameworkCore;
using Movies.Application;
using Movies.Domain;
using System;

namespace Movies.Persistency
{
    public class MoviesDbContext : DbContext, IApplicationDbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(m => m.ID);
            modelBuilder.Entity<Movie>().Property(m => m.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Rating>().HasKey(r => r.ID);
            modelBuilder.Entity<Rating>().Property(r => r.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Review>().HasKey(r => r.ID);
            modelBuilder.Entity<Review>().Property(r => r.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>().HasKey(u => u.ID);
            modelBuilder.Entity<User>().Property(u => u.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Ratings)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieID);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieID);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.User)
                .WithMany(u => u.Movies)
                .HasForeignKey(m => m.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Movies)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<Movie>().Ignore(m => m.AverageRating);

            modelBuilder.Entity<User>().HasData(new User[] {
                new User(){ ID = 1, Name = "Admin1", Role = "admin", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 2, Name = "Admin2", Role = "admin", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 3, Name = "User1", Role = "user", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()},
                new User(){ ID = 4, Name = "User2", Role = "user", ApiKey = Guid.NewGuid().ToString(), ApiSecret = Guid.NewGuid().ToString()}
            });
        }

    }   
}
