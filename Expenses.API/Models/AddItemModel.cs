using System;
using System.ComponentModel.DataAnnotations;

namespace Expenses.API.Models
{
    public class AddItemModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name has max lenght of 100")]
        public string Name { get; set; }
    }


}
