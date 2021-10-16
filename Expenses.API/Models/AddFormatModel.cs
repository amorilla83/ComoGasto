using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.API.Models
{
    public class AddFormatModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name has max lenght of 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public int ParentId { get; set; }
    }
}
