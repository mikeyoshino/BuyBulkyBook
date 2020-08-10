using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BuyBulkyBook.Models;

namespace BuyBulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }


        [Required]
        [Range(1, 1000)]
        public double PriceList { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }


        public string ImageUrl { get; set; }

        [ForeignKey("CategoryId")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("CoverTypeId")]
        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
    }
}
