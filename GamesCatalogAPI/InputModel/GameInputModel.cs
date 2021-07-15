using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must contain between 3 and 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The publisher name must contain between 3 and 100 characters")]
        public string Publisher { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be a minimum of 1 dollar and a maximum of 1000 dollars")]
        public double Price { get; set; }
    }
}
