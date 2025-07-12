using TradeWise.Models;

namespace TradeWise.Interfaces
{
    public interface IPortfolioRepository
    {
        Task <List<Stock>> GetUserPortfolioAsync(ApplicationUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolioAsync(ApplicationUser user, string name);
    }
}
