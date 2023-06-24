using BeverageVendingMachine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Repositories
{
    /// <summary>
    /// Coin denomination repository interface
    /// </summary>
    public interface ICoinDenominationRepository
    {
        /// <summary>
        /// Get coin denomination entity by value
        /// </summary>
        /// <param name="value">Value or denomination of the coin</param>
        /// <returns>Returns coin denomination entity with the provided value</returns>
        Task<CoinDenomination> GetCoinDenominationByValue(decimal value);
    }
}
