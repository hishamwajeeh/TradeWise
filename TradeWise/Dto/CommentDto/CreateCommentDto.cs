using System.ComponentModel.DataAnnotations;

namespace TradeWise.Dto.CommentDto
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Title must be at least 1 character long.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Content must be at least 1 character long.")]
        [MaxLength(100, ErrorMessage = "Content cannot exceed 100 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
