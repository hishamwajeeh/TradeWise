using System.ComponentModel.DataAnnotations;

namespace TradeWise.Dto.StockDto
{
    public class UpdateStockRequestDto
    {
        [Required(ErrorMessage = "Stock name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Stock name must be between 2 and 50 characters")]
        public string StockName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 100 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stock price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Stock price must be greater than 0")]
        public decimal StockPrice { get; set; }

        [Required(ErrorMessage = "Last dividend is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Last dividend cannot be negative")]
        public decimal LastDivdend { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        [StringLength(50, ErrorMessage = "Industry cannot exceed 50 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Market capitalization is required")]
        [Range(1, long.MaxValue, ErrorMessage = "Market capitalization must be positive")]
        public long MarketCap { get; set; }
    }
}