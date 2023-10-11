using BookingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _Context;
		private readonly UserManager<UserModel> _userManager;
		public HomeController(AppDbContext Context, UserManager<UserModel> userManager)
		{
			_Context = Context;
			_userManager = userManager;	
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult DisplayImage(int imageId)
		{
			var image = _Context.ImageList.FirstOrDefault(i => i.id == imageId);

			if (image == null)
			{
				return NotFound();
			}

			return File(image.image, "image/jpeg");
		}
		public IActionResult UserProfile()
		{
			var USER = _userManager.GetUserAsync(User).Result;
			ViewBag.SavedOffers = _Context.SavedOfferList.Where(x => x.UserId == USER.Id).ToList();
			ViewBag.UserOffers = _Context.OfferList.Where(x => x.CreatorId == USER.Id).ToList();
			SavedOffers saved;
			
			return View(USER);
		}
	}
}