using BookingApp.Models;

namespace BookingApp.Services.Interfaces
{
	public interface IOfferService
	{
		Offer GetOfferById(int id);
		List<Offer> GetAllOffer();
		int AddOffer(Offer offer);
		int RemoveOffer(int id);
	}
}
