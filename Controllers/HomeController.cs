﻿using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _Context;

		public HomeController(AppDbContext Context)
		{
			_Context = Context;
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
	}
}