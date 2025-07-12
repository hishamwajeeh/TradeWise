using Microsoft.AspNetCore.Identity;

namespace TradeWise.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
