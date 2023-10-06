using Microsoft.AspNetCore.Identity;
using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingApp.Models
{
	public class Offer
	{
       

        [Key]
		public int Id { get; set; }
		[Required]
		[Sieve(CanFilter = true)]
		public string title { get; set; }
	
		[Required]
		public string description { get; set; }
		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public DateTime? AddDate { get; set; } = DateTime.Now;
		[Required]
		[Sieve(CanFilter = true, CanSort = true)]
		public float price { get; set; }
		[Required]
		[Sieve(CanFilter = true)]
		public int NumberOfRooms {get; set; }
		[Required]
		[Sieve(CanFilter = true)]
		public string City { get; set; }
		[Required]
		[Sieve(CanFilter = true)]
		public string TypeOfFlat { get; set; }

		public List<Image>? Images { get; set; }
		public string? CreatorId { get; set; }

    }
}
