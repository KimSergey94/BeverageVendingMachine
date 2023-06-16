using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Infrastructure.Repositories
{
    /// <summary>
    /// Coin denomination repository
    /// </summary>
    public class CoinDenominationRepository : ICoinDenominationRepository
    {
        private readonly BeverageVendingMachineContext _beverageVendingMachineContext;
        public CoinDenominationRepository(BeverageVendingMachineContext beverageVendingMachineContext)
        {
            _beverageVendingMachineContext = beverageVendingMachineContext;
        }


        /// <summary>
        /// Get coin denomination entity by value
        /// </summary>
        /// <param name="value">Value or denomination of the coin</param>
        /// <returns>Returns coin denomination entity with the provided value</returns>
        public async Task<CoinDenomination> GetCoinDenominationByValue(decimal value)
        {
            return await _beverageVendingMachineContext.CoinDenominations.FirstOrDefaultAsync(coinDenomination => coinDenomination.Value == value);
        }
    }
}
