using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Offer> OfferList { get; set; }
		public DbSet<Image> ImageList { get; set; }
		public DbSet<Booking> BookingList { get; set; }
		public DbSet<SavedOffers> SavedOfferList { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>()
             .HasOne(i => i.Offer)
             .WithMany(n => n.Images)
             .HasForeignKey(i => i.OfferId);
        }
        }
    }
