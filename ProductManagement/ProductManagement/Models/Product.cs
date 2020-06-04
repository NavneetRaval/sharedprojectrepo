using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product
    {
        public int? ProductId { get; set; }


        [Required(ErrorMessage = "Product Name field is required.")]
        [StringLength(100, ErrorMessage = "Product field maximum length is 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Price field is required.")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity field is required.")]
        public int Quantity { get; set; }


    }
}