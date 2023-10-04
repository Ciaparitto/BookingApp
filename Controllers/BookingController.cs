﻿using BookingApp.Models;
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
		public IActionResult SearchCurrentOffer(string KeyWords,int RoomsNumber,string City,int MaxPrice,int MinPrice,string TypeOfFlat)
		{
			var offersWithKeyWord = new List<Offer>();
			
			if (KeyWords != null)
			{
				offersWithKeyWord = _Context.OfferList.Where(x => x.title.Contains(KeyWords)).ToList();
			}
			else
			{
				offersWithKeyWord = _Context.OfferList.ToList();
			}
			var GoodOffers = new List<Offer>();
			var wyniki = _Context.OfferList.AsQueryable();
			foreach (var offer in offersWithKeyWord)
			{
				if (RoomsNumber != null)
				{
					wyniki = wyniki.Where(x => x.NumberOfRooms == RoomsNumber);
				}
				if (City != null)
				{
					wyniki = wyniki.Where(x => x.City == City);
				}
				if (MinPrice != null)
				{
					wyniki = wyniki.Where(x => x.price >= MinPrice);
				}
				if (MaxPrice != null)
				{
					wyniki = wyniki.Where(x => x.price <= MaxPrice);
				}
				if (TypeOfFlat != null)
				{
					wyniki = wyniki.Where(x => x.TypeOfFlat == TypeOfFlat);
				}
				GoodOffers = wyniki.ToList();

			}
			return View(GoodOffers);
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
