using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    public interface IBeverageRepository
    {
        Task<Beverage> GetBeverageByIdAsync(int Id);
        Task<List<Beverage>> GetAllBeveragesAsync();
        Task<Beverage> CreateBeverageAsync(Beverage beverage);
        Task<Beverage> UpdateBeverageAsync(Beverage beverage);
        Task<bool> DeleteBeverageAsync(string beverageId);
    }
}
