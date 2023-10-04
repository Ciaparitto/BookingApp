using BookingApp.Models;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace BookingApp.Sieve
{
	public class AppSieveProcessor : SieveProcessor
	{
		public AppSieveProcessor(IOptions<SieveOptions> options) : base(options)
		{

		}
		protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
		{
			mapper.Property<Offer>(e => e.price)
			.CanSort()
			.CanFilter();

			mapper.Property<Offer>(e => e.title)
			.CanSort()
			.CanFilter();

			mapper.Property<Offer>(e => e.NumberOfRooms)
			.CanSort()
			.CanFilter();

			mapper.Property<Offer>(e => e.City)
			.CanSort()
			.CanFilter();

			mapper.Property<Offer>(e => e.TypeOfFlat)
			.CanSort()
			.CanFilter();

			return mapper;
		}
	}
}
