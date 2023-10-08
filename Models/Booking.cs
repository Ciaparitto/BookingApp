using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BookingApp.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public DateTime startDate { get; set; }
		[Required]
		public DateTime endDate { get; set; }


		[ForeignKey("offerId")]
		public Offer Offer { get; set; }
		public int offerId { get; set; }


		[ForeignKey("UserId")]
		public UserModel User { get; set; }
		public string UserId { get; set; }

	
	}
}
