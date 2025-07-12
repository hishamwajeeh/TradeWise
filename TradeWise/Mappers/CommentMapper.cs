using TradeWise.Dto.CommentDto;
using TradeWise.Models;

namespace TradeWise.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            if (commentModel == null)
            {
                throw new ArgumentNullException(nameof(commentModel), "Comment model cannot be null");
            }
            return new CommentDto
            {
                CommentId = commentModel.CommentId,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                CreatedBy = commentModel.AppUser?.UserName ?? "deleted_user",
                StockId = commentModel.StockId
            };
        }


        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            if (commentDto == null)
            {
                throw new ArgumentNullException(nameof(commentDto), "Comment model cannot be null");
            }
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
        {
            if (commentDto == null)
            {
                throw new ArgumentNullException(nameof(commentDto), "Comment model cannot be null");
            }
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}
