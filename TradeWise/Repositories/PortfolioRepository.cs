using Microsoft.EntityFrameworkCore;
using TradeWise.Data;
using TradeWise.Interfaces;
using TradeWise.Models;

namespace TradeWise.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolioAsync(ApplicationUser user, string name)
        {
            var portfolioModel = await _context.Portfolios
                .Include(p => p.Stock) // Ensure Stock is loaded for comparison
                .FirstOrDefaultAsync(x =>
                    x.AppUserId == user.Id &&
                    x.Stock.StockName.ToLower() == name.ToLower());

            if (portfolioModel == null)
            {
                return null; // Stock not found in portfolio
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel; // Return deleted entry
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(ApplicationUser user)
        {
            return await _context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    StockId = stock.Stock.StockId,
                    StockName = stock.Stock.StockName,
                    CompanyName = stock.Stock.CompanyName,
                    StockPrice = stock.Stock.StockPrice,
                    LastDivdend = stock.Stock.LastDivdend,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }
    }
}
