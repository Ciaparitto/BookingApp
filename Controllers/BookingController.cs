using BookingApp.Models;
using BookingApp.Services;
using BookingApp.Services.Interfaces;
using BookingApp.Sieve;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
			foreach(var offer in OfferList)
			{
				foreach(var image in _Context.ImageList)
				{
					if(image.OfferId == offer.Id)
					{
						offer.Images.Add(image);
					}
				}
			}
			return View(OfferList);
		}
		[HttpPost]
		public async Task<IActionResult> SearchCurrentOffer([FromBody] SieveModel query, ISieveProcessor sieveProcessor, AppDbContext DbContext)
		{
			var offers = DbContext.OfferList
			.AsQueryable();

			var Goodoffers = await sieveProcessor
			.Apply(query, offers)
			.ToListAsync();

			var totalcount = await sieveProcessor
			.Apply(query, offers, applyPagination: false, applySorting: false)
			.CountAsync();

			var result = new PagedResult<Offer>(Goodoffers, totalcount, query.PageSize.Value, query.Page.Value);
			return View(Goodoffers);
		}
		
		public IActionResult CurrentOffer(int OfferId) 
		{
			var Offer = _Context.OfferList.FirstOrDefault(x => x.Id == OfferId);
			foreach(var image in _Context.ImageList)
			{
				if(image.id == Offer.Id)
				{
					Offer.Images.Add(image);
				}
			}
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
	}
}
