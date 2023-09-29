using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models
{
	public class Offer
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string title { get; set; }
		[Required]
		public string description { get; set; }
		[Required]
		public DateTime? AddDate { get; set; } = DateTime.Now;
		[Required]
		public float price { get; set; }
		[Required]
		public int NumberOfRooms {get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string TypeOfFlat { get; set; }
		
	}
}
