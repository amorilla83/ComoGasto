using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Expenses.API.Models
{
    public class AddStoreModel
    {
        [Required (ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name has max lenght of 100")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Logo is required")]
        public IFormFile Logo { get; set; }
    }

    public class EditStoreModel
    {
        [MaxLength(100, ErrorMessage = "Name has max lenght of 100")]
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
