using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Repositories
{
    public interface ICoinDenominationRepository
    {
        /// <summary>
        /// Get coin denomination entity by value
        /// </summary>
        /// <param name="value">Value or denomination of the coin</param>
        /// <returns>Returns coin denomination entity with the provided value</returns>
        Task<CoinDenomination> GetCoinDenominationByValue(double value);

        ///// <summary>
        ///// Saves changes to database
        ///// </summary>
        ///// <returns>The task result contains the number of state entries written to the database</returns>
        //Task<int> Complete();
    }
}
