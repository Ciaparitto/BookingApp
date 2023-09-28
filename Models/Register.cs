using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
	public class Register
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		public string EmailAdress { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
