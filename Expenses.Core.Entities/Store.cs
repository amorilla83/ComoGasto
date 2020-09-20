using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }
    }
}
