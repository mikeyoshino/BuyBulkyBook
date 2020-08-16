using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuyBulkyBook.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Stage { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public bool IsAuthorizedCompany { get; set; }

    }
}
