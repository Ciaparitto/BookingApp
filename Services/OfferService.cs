using BookingApp.Models;
using BookingApp.Services.Interfaces;

namespace BookingApp.Services
{
	public class OfferService : IOfferService
	{
		public readonly AppDbContext _Context;
		public OfferService(AppDbContext context)
		{
			_Context = context;
		}
		public Offer GetOfferById(int id)
		{
			var CurrentOffer = _Context.OfferList.Find(id);
			return CurrentOffer;
		}
		public List<Offer> GetAllOffer()
		{
			var OfferList =  _Context.OfferList.ToList();
			return OfferList;
		}
		public int AddOffer(Offer offer)
		{
			_Context.OfferList.Add(offer);
			_Context.SaveChanges();
			return offer.Id;
		}
		public int RemoveOffer(int id)
		{
			var OfferToDelete = _Context.OfferList.Find(id);
			if(OfferToDelete != null)
			{
				_Context.OfferList.Remove(OfferToDelete);
				_Context.SaveChanges();
			}
			
			return OfferToDelete.Id;
		}
	}
}
