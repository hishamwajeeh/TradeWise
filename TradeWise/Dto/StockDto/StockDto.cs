using TradeWise.Models;

namespace TradeWise.Dto.StockDto
{
    public class StockDto
    {
        public int StockId { get; set; }
        public string StockName { get; set; } = string.Empty; // Name of the stock or symbol
        public string CompanyName { get; set; } = string.Empty;
        public decimal StockPrice { get; set; }
        public decimal LastDivdend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } // Market Capitalization

        public List<TradeWise.Dto.CommentDto.CommentDto> Comments { get; set; } = new List<TradeWise.Dto.CommentDto.CommentDto>();
    }
}
