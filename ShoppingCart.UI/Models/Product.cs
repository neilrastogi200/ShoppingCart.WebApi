using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShoppingCart.UI.Validation.Interfaces;

namespace ShoppingCart.UI.Models
{
    public class Product : IEntity
    {
        [Required]
        public int ProductId { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public int Quantity { set; get; }

        public decimal Price { set; get; }
    }
}