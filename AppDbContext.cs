using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingApp
{
	public class AppDbContext : IdentityDbContext<UserModel>
	{
		public AppDbContext() : base()
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Offer> OfferList { get; set; }
	}
}
