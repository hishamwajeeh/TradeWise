using TradeWise.Dto.StockDto;
using TradeWise.Helpers;
using TradeWise.Models;

namespace TradeWise.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock?> GetStockByNameAsync(string name);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock> UpdateStockAsync(int id, UpdateStockRequestDto updateStockDto);
        Task<Stock?> DeleteStockAsync(int id);
        Task<bool> StockExistsAsync(int id);
    }
}
