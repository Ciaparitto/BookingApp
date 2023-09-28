using Microsoft.AspNetCore.Identity;

namespace BookingApp.Models
{
	public class UserModel : IdentityUser
	{
		public bool isAdmin { get; set; }
	}
}
