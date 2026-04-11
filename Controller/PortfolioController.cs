using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extension;
using api.Interface;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepository,
            IPortfolioRepository portfolioRepository
        )
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var portfolio = await _portfolioRepository.GetUserPortfolioAsync(user);
            return Ok(portfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var stock = await _stockRepository.GetStockBySymbolAsync(symbol);
            if (stock == null)
            {
                return NotFound("Stock not found");
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(user);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock already in portfolio");
            }

            var portfolioEntry = new Portfolio { UserId = user.Id, StockId = stock.Id };
            await _portfolioRepository.CreatePortfolioAsync(portfolioEntry);
            if (portfolioEntry == null)
            {
                return StatusCode(500, "An error occurred while adding the stock to the portfolio");
            }
            else
            {
                return Ok("Stock added to portfolio");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(user);
            var filteredStock = userPortfolio
                .Where(s => s.Symbol.ToLower() == symbol.ToLower())
                .ToList();
            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolioAsync(user, symbol);
            }
            else
            {
                return NotFound("Stock not found in portfolio");
            }
            return Ok("Stock removed from portfolio");
        }
    }
}
