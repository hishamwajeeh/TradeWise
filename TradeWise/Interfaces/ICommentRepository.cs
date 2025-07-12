using TradeWise.Models;

namespace TradeWise.Interfaces
{
    public interface ICommentRepository
    {
        Task <List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int commentId);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(int id, Comment commentModel);
        Task DeleteCommentAsync(int id);
    }
}
