using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
	public class Image
	{
		[Key]
		public int id { get; set; }
		public Byte[] image { get; set; }
		public string ContentType { get; set; }
		public bool ToDelete { get; set; }
		[ForeignKey("Offer")]
		public int OfferId { get; set; }

		public Offer Offer { get; set; }
	}
}
