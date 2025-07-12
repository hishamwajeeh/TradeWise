using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TradeWise.Extensions;
using TradeWise.Interfaces;
using TradeWise.Models;

namespace TradeWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<ApplicationUser> userManager,
            IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolio()
        {
            try
            {
                var username = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(username);

                if (appUser == null)
                {
                    return NotFound($"User '{username}' not found in database");
                }

                var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);
                return Ok(userPortfolio);
            }
            catch (InvalidOperationException ex)
            {
                // This will catch our "username not found" exception
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStockToPortfolio([FromQuery] string name)
        {
            try
            {
                var username = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(username);

                if (appUser == null)
                    return NotFound("User not found");

                var stock = await _stockRepository.GetStockByNameAsync(name);
                if (stock == null)
                    return NotFound($"Stock '{name}' not found");

                var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);
                if (userPortfolio.Any(s => s.StockName.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    return BadRequest($"Stock '{name}' already in portfolio");

                var portfolioModel = new Portfolio
                {
                    StockId = stock.StockId,
                    AppUserId = appUser.Id
                };

                await _portfolioRepository.CreateAsync(portfolioModel);

                // Return a clean DTO instead of the full Portfolio
                return Ok(new
                {
                    Message = "Stock added to portfolio",
                    Stock = new { stock.StockId, stock.StockName },
                    User = new { appUser.UserName }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio([FromQuery] string name)
        {
            try
            {
                var username = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(username);

                if (appUser == null)
                {
                    return NotFound("User not found");
                }

                var deletedPortfolio = await _portfolioRepository.DeletePortfolioAsync(appUser, name);

                if (deletedPortfolio == null)
                {
                    return BadRequest("Stock not found in your portfolio");
                }

                return Ok($"Stock '{name}' removed from portfolio");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting stock: {ex.Message}");
            }
        }
    }
}
