using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Core.Entities
{
    public class Format
    {
        public Format ()
        {
            BrandList = new List<Brand>();
         }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Brand> BrandList { get; set; }
    }
}
