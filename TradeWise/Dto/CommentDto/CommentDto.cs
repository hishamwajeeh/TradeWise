using TradeWise.Models;

namespace TradeWise.Dto.CommentDto
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public int? StockId { get; set; }
        //public Stock? Stock { get; set; }
    }
}
