using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
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
        public PurchaseResult(StorageItem purchaseItem, CoinsCollection change)
        {
            PurchaseItem = purchaseItem;
            Change = change;
        }

        /// <summary>
        /// The inventory item that is being purchased
        /// </summary>
        public StorageItem PurchaseItem { get; set; }

        /// <summary>
        /// Represents the coins for a change
        /// </summary>
        public CoinsCollection Change { get; set; }
    }
}
