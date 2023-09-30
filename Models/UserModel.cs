using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models
{
	public class UserModel : IdentityUser
	{
		public bool isAdmin { get; set; }
	}
}
