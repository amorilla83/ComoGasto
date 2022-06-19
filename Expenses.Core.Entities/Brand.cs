using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
