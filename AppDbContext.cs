using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApp
{
	public class AppDbContext : IdentityDbContext<UserModel>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-R5C9EQ0\SQLEXPRESS;Initial Catalog=DbContextBooking;Integrated Security=True");
			}
		}
		public AppDbContext() : base()
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Offer> OfferList { get; set; }
		public DbSet<Image> ImageList { get; set; }
	}
}
