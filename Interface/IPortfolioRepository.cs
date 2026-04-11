using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Interface
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(AppUser user);
        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
    }
}
