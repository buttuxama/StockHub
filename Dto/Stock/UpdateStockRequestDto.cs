using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long.")]
        public string Symbol { get; set; } = String.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Company name must be at most 100 characters long.")]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDividend { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Industry must be at most 25 characters long.")]
        public string Industry { get; set; } = String.Empty;

        [Required]
        [Range(1, 5000000000)]
        public int MarketCap { get; set; }
    }
}
