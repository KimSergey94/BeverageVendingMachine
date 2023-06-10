using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Entity of coin operation 
    /// </summary>
    public class CoinOperation : BaseEntity
    {
        /// <summary>
        /// Coin denomination
        /// </summary>
        public CoinDenomination CoinDenomination { get; set; }

        /// <summary>
        /// Foreign key to coin denomination
        /// </summary>
        public int CoinDenominationId { get; set; }

        /// <summary>
        /// Indicates whether the operation is of withdrawal type
        /// </summary>
        public bool IsWithdrawal { get; set; }

        /// <summary>
        /// Indicates the quantity of coins with set coin denomination
        /// </summary>
        public int Quantity { get; set; }
    }
}
