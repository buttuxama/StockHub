using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Stock;
using api.Model;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDividend = stock.LastDividend,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
            };
        }
    }
}
