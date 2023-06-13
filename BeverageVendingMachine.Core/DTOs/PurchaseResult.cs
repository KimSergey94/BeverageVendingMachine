using BeverageVendingMachine.Core.Entities.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Application.DTOs
{
    /// <summary>
    /// Represents the result of a purchase
    /// </summary>
    public class PurchaseResult
    {
        public PurchaseResult(IStorageItem purchaseItem, SortedDictionary<double, List<CoinDenomination>> change)
        {
            PurchaseItem = purchaseItem;
            Change = change;
        }

        /// <summary>
        /// The inventory item that is being purchased
        /// </summary>
        public IStorageItem PurchaseItem { get; set; }

        /// <summary>
        /// Represents the coins for a change
        /// </summary>
        public SortedDictionary<double, List<CoinDenomination>> Change { get; set; }
    }
}
