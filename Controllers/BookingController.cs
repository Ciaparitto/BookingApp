using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
	public class BookingController : Controller
	{
		public readonly OfferService _OfferService;
		public readonly AppDbContext _Context;
		public BookingController(OfferService OfferService,AppDbContext context) 
		{
			_OfferService = OfferService;	
			_Context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Offer(Offer offer)
		{
			return View(offer);
		}
		[HttpGet]
		public IActionResult SearchOffer()
		{
			var OfferList = _OfferService.GetAllOffer();
			return View(OfferList);
		}
		[HttpPost]
		public IActionResult SearchCurrentOffer(string KeyWords,int RoomsNumber,string City,int MaxPrice,int MinPrice,string TypeOfFlat)
		{
			var offersWithKeyWord = _Context.OfferList.Where(x => x.title.Contains(KeyWords)).ToList();
			var GoodOffers = new List<Offer>();
			foreach (var offer in offersWithKeyWord)
			{
				if
					(
					(offer.price >= MinPrice && offer.price <= MaxPrice)&&
					offer.NumberOfRooms == RoomsNumber &&
					offer.City == City &&
					offer.TypeOfFlat == TypeOfFlat
					)
				{
					GoodOffers.Add(offer);
				}
			}
			return View(GoodOffers);
		}
		[HttpGet]
		public IActionResult CurrentOffer(int OfferId) 
		{
			var Offer = _Context.OfferList.FirstOrDefault(x => x.Id == OfferId);
			return View(Offer);
		}
		[HttpGet]
		public IActionResult AddOffer()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddOffer(Offer offer)
		{
			_OfferService.AddOffer(offer);
			return View();
		}
		[HttpPost]
		public IActionResult DeleteOffer(int OfferId)
		{
			_OfferService.RemoveOffer(OfferId);
			return RedirectToAction("SearchOffer", "Booking");
		}
	}
}
