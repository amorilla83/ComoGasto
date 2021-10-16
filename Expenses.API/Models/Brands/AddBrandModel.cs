namespace Expenses.API.Models.Brands
{
    public class AddBrandModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ProductId { get; set; }
    }
}
