using Microsoft.EntityFrameworkCore;
using TradeWise.Data;
using TradeWise.Interfaces;
using TradeWise.Models;

namespace TradeWise.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public Task DeleteCommentAsync(int id)
        {
            var comment = _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return Task.CompletedTask; // Or throw an exception if preferred
            }
            _context.Comments.Remove(comment.Result);
            return _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(i => i.CommentId == id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}
