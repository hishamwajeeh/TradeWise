using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TradeWise.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int StockId { get; set; }
        public string StockName { get; set; } = string.Empty; // Name of the stock or symbol
        public string CompanyName { get; set; } = string.Empty;
        public decimal StockPrice { get; set; }
        public decimal LastDivdend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; } //Market Capitalization

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
