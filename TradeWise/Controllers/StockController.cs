using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeWise.Data;
using TradeWise.Dto.StockDto;
using TradeWise.Helpers;
using TradeWise.Interfaces;
using TradeWise.Mappers;

namespace TradeWise.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            //_context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllStocks([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stocks = await _stockRepository.GetAllStocksAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task <IActionResult> GetStockById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task <IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (stockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            var stock = stockDto.ToStockFromCreateDto();
            await _stockRepository.CreateStockAsync(stock);

            return CreatedAtAction(nameof(GetStockById), new { id = stock.StockId }, stock.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task <IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = await _stockRepository.UpdateStockAsync(id, updateStockDto);

            if (updateStockDto == null)
            {
                return BadRequest("Invalid stock data.");
            }
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task <IActionResult> DeleteStock(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
