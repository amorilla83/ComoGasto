using System;
namespace Expenses.API.Models
{
	public class AddProductDetailsModel
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int? BrandId { get; set; }
		public int? FormatId { get; set; }
	}
}

