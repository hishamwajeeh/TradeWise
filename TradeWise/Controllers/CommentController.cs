using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TradeWise.Data;
using TradeWise.Dto.CommentDto;
using TradeWise.Extensions;
using TradeWise.Interfaces;
using TradeWise.Mappers;
using TradeWise.Models;

namespace TradeWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<ApplicationUser> _userManager; 
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<ApplicationUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllCommentsAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }

        [HttpGet("{CommentId:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int CommentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetCommentByIdAsync(CommentId);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return NotFound($"Stock with ID {stockId} not found.");
            }

            var comment = commentDto.ToCommentFromCreate(stockId);
            comment.AppUserId = appUser?.Id;
            await _commentRepo.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { CommentId = comment.CommentId }, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id,[FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.UpdateCommentAsync(id, updateDto.ToCommentFromUpdate());

            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }
            await _commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
