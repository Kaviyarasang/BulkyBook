using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Book.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]

        public string Title { get; set; }

        public string Description { get; set; }
        [Required]

        public string ISBN { get; set; }
        [Required]

        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]

        public double ListPrice { get; set; }

        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]

        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]

        public double Price100 { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int CoverTypeID { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }

    }
}
