using System.ComponentModel.DataAnnotations.Schema;
using TradeWise.Models;
[Table("Comments")]
public class Comment
{
    public int CommentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }

    public string? AppUserId { get; set; }
    public ApplicationUser? AppUser { get; set; }
}