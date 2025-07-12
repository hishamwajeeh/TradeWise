using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TradeWise.Data;
using TradeWise.Dto.StockDto;
using TradeWise.Helpers;
using TradeWise.Interfaces;
using TradeWise.Models;

namespace TradeWise.Repositories
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                throw new KeyNotFoundException($"Stock with ID {id} not found.");
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if(!string.IsNullOrEmpty(query.StockName))
            {
                stocks = stocks.Where(s => s.StockName.Contains(query.StockName) || 
                                            s.CompanyName.Contains(query.StockName));
            }

            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("Stock Name", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.StockName) : stocks.OrderBy(s => s.StockName);
                }
                else
                {
                    stocks = stocks.OrderBy(s => EF.Property<object>(s, query.SortBy));
                }
            }

            // Pagination
            var skip = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skip).Take(query.PageSize).ToListAsync();   
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.Comments)
                .ThenInclude(c => c.AppUser) // ← THIS IS CRUCIAL
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StockId == id);
        }

        public async Task<Stock?> GetStockByNameAsync(string name)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.StockName.ToLower() == name.ToLower());
        }

        public Task<bool> StockExistsAsync(int id)
        {
            return _context.Stocks.AnyAsync(s => s.StockId == id);
        }

        public async Task<Stock> UpdateStockAsync(int id, UpdateStockRequestDto updateStockDto)
        {
            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock == null)
            {
                throw new KeyNotFoundException($"Stock with ID {id} not found.");
            }

            existingStock.StockName = updateStockDto.StockName;
            existingStock.CompanyName = updateStockDto.CompanyName;
            existingStock.StockPrice = updateStockDto.StockPrice;
            existingStock.LastDivdend = updateStockDto.LastDivdend;
            existingStock.Industry = updateStockDto.Industry;
            existingStock.MarketCap = updateStockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}
