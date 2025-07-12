using TradeWise.Models;

namespace TradeWise.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);   
    }
}
