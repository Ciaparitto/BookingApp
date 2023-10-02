using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
	public class BookingController : Controller
	{
		public readonly IOfferService _OfferService;
		public readonly AppDbContext _Context;
		private readonly UserManager<UserModel> _userManager;
		public BookingController(IOfferService OfferService,AppDbContext context, UserManager<UserModel> userManager) 
		{
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
	}
}
