using System.ComponentModel.DataAnnotations.Schema;

namespace TradeWise.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public ApplicationUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}
