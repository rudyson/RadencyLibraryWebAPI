using Microsoft.EntityFrameworkCore;
using RadencyLibraryWebAPI.Entities;

namespace RadencyLibraryWebAPI.Models
{
    public class LibraryDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			DotNetEnv.Env.Load();
			var connectionProperties = Environment.GetEnvironmentVariable("MSSQLCONNECTIONSTRING");
			optionsBuilder.UseSqlServer(connectionProperties);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasMany(book => book.Reviews)
				.WithOne(review => review.Book)
				.HasForeignKey(review => review.BookId);

			modelBuilder.Entity<Book>()
				.HasMany(book => book.Ratings)
				.WithOne(rating => rating.Book)
				.HasForeignKey(rating => rating.BookId);
		}
	}
}
