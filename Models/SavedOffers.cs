using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models
{
	public class SavedOffers
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[ForeignKey("Offerid")]
		public int OfferId { get; set; }
		public Offer Offer { get; set; }
		[Required]
		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public UserModel User { get; set; }
	}
}
