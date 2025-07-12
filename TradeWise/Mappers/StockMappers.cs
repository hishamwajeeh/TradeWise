using TradeWise.Dto.StockDto;
using TradeWise.Models;

namespace TradeWise.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                StockId = stockModel.StockId,
                StockName = stockModel.StockName,
                CompanyName = stockModel.CompanyName,
                StockPrice = stockModel.StockPrice,
                LastDivdend = stockModel.LastDivdend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                StockName = stockDto.StockName,
                CompanyName = stockDto.CompanyName,
                StockPrice = stockDto.StockPrice,
                LastDivdend = stockDto.LastDivdend,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}
