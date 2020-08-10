using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuyBulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ชื่อหมวดหมู่")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
