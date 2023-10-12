using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using BookingApp.Sieve;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookingApp.Controllers
{
	
	public class BookingController : Controller
	{
		public readonly IOfferService _OfferService;
		private readonly ISieveProcessor _sieveProcessor;
		public readonly AppDbContext _Context;
		private readonly UserManager<UserModel> _userManager;
		public BookingController(IOfferService OfferService,AppDbContext context, UserManager<UserModel> userManager,ISieveProcessor sieveProcessor) 
		{
			_sieveProcessor = sieveProcessor;
			_OfferService = OfferService;	
			_Context = context;
			_userManager = userManager;
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
		public IActionResult SearchOffer([FromQuery] string RoomsNumber, [FromQuery] string MinPrice, [FromQuery] string MaxPrice, [FromQuery] string City, [FromQuery] string TypeOfFlat, [FromQuery] string KeyWords)
		{
            var offers = _Context.OfferList.AsQueryable();
            if (KeyWords != null)
            {
                offers = offers.Where(x => x.title.Contains(KeyWords));
            }


            if (TypeOfFlat != null)
            {
                offers = offers.Where(o => o.TypeOfFlat == TypeOfFlat);
            }
            if (MaxPrice != null)
            {
                offers = offers.Where(o => o.price <= int.Parse(MaxPrice));
            }
            if (MinPrice != null)
            {
                offers = offers.Where(o => o.price >= int.Parse(MinPrice));
            }

            if (City != null)
            {
                offers = offers.Where(o => o.City == City);
            }

            if (RoomsNumber != null)
            {
                offers = offers.Where(o => o.NumberOfRooms == int.Parse(RoomsNumber));
            }
            var imageList = _Context.ImageList.ToList();
			foreach(var offer in offers)
			{
				foreach(var image in imageList)
				{
					if(image.OfferId == offer.Id)
					{
						offer.Images.Add(image);
					}
				}
			}
			return View(offers);
		}
		
		public IActionResult CurrentOffer(int OfferId,bool dateIsFree,bool DateIsChecked, DateTime startDate, DateTime endDate) 
		{
			ViewBag.StartDate = startDate;
			ViewBag.EndDate = endDate;
			ViewBag.DateIsChecked = DateIsChecked;
			ViewBag.IsFree = dateIsFree;
			var Offer = _OfferService.GetOfferById(OfferId);
			Offer.Views += 1;
			_Context.SaveChanges();
			foreach(var image in _Context.ImageList)
			{
				if(image.OfferId == Offer.Id)
				{
					Offer.Images.Add(image);
				}
			}
			return View(Offer);
		}
		[HttpGet]
		[Authorize]
		public IActionResult AddOffer()
		{
			return View();
		}
		[HttpPost]
		[Authorize]
		public IActionResult AddOffer(Offer offer,List<IFormFile> files)
		{
			if(ModelState.IsValid)
			{
                var USER = _userManager.GetUserAsync(User).Result;
                offer.CreatorId = USER.Id;
                var id = _OfferService.AddOffer(offer);
                if (files != null && files.Count > 0)
				{
					foreach (var imageFile in HttpContext.Request.Form.Files)
					{
						using (var memoryStream = new MemoryStream())
						{
							imageFile.CopyTo(memoryStream);

							var image = new Image
							{
								image = memoryStream.ToArray(),
								ContentType = imageFile.ContentType,
								OfferId = offer.Id

							};
							_Context.ImageList.Add(image);
							offer.Images.Add(image);
                           
                        }
					}
				}
				
					_Context.SaveChanges();
					
					return RedirectToAction("index","home");
				}
				return View();
			}
		[HttpPost]
		public IActionResult DeleteOffer(int OfferId)
		{
			_OfferService.RemoveOffer(OfferId);
			return RedirectToAction("SearchOffer", "Booking");
		}
		[HttpGet]
		public IActionResult CheckDate(int OfferId,DateTime startDate,DateTime endDate)
		{
			var bookingList = _Context.BookingList.Where(x => x.offerId == OfferId).ToList();
			
			var dateList = new List<DateTime>();
			var BookingDateList = new List<DateTime>();
			while (startDate <= endDate)
			{
				dateList.Add(startDate);
				startDate = startDate.AddDays(1);
			}
			foreach(var date in bookingList)
			{
				while (date.startDate <= date.endDate)
				{
					BookingDateList.Add(date.startDate);
					date.startDate = date.startDate.AddDays(1);
				}
			}
			foreach (var date in BookingDateList)
			{
				foreach(var dateL in dateList)
				{
					if (date.Day == dateL.Day  && date.Month == dateL.Month && date.Year == dateL.Year)
					{
						return RedirectToAction("currentoffer", "booking", new { OfferId = OfferId, dateIsFree = false, DateIsChecked = true , startDate  = startDate,endDate = endDate});
					}
				}
			}
			return RedirectToAction("currentoffer", "booking", new { OfferId = OfferId, dateIsFree = true,DateIsChecked = true, startDate = startDate, endDate = endDate });
		}
		[HttpPost]
		[Authorize]
		public IActionResult AddBooking(DateTime startDate, DateTime endDate,int OfferId )
		{
			var USER = _userManager.GetUserAsync(User).Result;
			if (ModelState.IsValid)
			{ 
				var booking = new Booking
					{
					startDate = startDate,
					endDate = endDate,
					offerId = OfferId,
					UserId = USER.Id
				
					};
				var Id = _OfferService.AddBooking(booking);
				return RedirectToAction("currentoffer", "booking", new { OfferId = OfferId, dateIsFree = false, DateIsChecked = false });

			}
			return RedirectToAction("currentoffer", "booking", new { OfferId = OfferId, dateIsFree = false, DateIsChecked = false });
		}

		[HttpPost]
		
		public IActionResult SaveOffer(int OfferId)
		{
			
			var USER = _userManager.GetUserAsync(User).Result;
			var SavedOfferList = _Context.SavedOfferList.ToList();
			foreach(var offer in  SavedOfferList)
			{
				if(offer.OfferId == OfferId && offer.UserId == USER.Id)
				{
					return RedirectToAction("currentoffer", "booking", new { OfferId = OfferId });
				}
			}
			var SavedOffer = new SavedOffers{
				UserId = USER.Id,
				OfferId = OfferId
				};
			_Context.SavedOfferList.Add(SavedOffer);
			_Context.SaveChanges();
			return RedirectToAction("currentoffer","booking", new { OfferId = OfferId });
			
		} 
	}
}
