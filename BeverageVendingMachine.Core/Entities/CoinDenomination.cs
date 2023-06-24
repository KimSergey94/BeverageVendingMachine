using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.Core.Common;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Coin denomination entity
    /// </summary>
    public class CoinDenomination : BaseEntity
    {
        /// <summary>
        /// Coin denomination value
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Indicates the current quantity of coins with the specified denomination inside vending machine storage
        /// </summary>
        public int StorageQuantity { get; set; }

        /// <summary>
        /// Indicates the deposited quantity of coins with the specified denomination inside vending machine storage
        /// </summary>
        public int DepositedQuantity { get; set; }

        /// <summary>
        /// Indicates whether the coin denomination is blocked by admin
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
