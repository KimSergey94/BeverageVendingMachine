using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.Core.Common;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Entity of coin operation 
    /// </summary>
    public class CoinOperation : BaseEntity
    {
        public CoinOperation()
        {

        }
        public CoinOperation(CoinDenomination coinDenomination, int quantity, bool isDeposited)
        {
            CoinDenomination = coinDenomination;
            Quantity = quantity;
            IsDeposited = isDeposited;
        }

        /// <summary>
        /// Coin denomination
        /// </summary>
        public CoinDenomination CoinDenomination { get; set; }

        /// <summary>
        /// Foreign key to coin denomination
        /// </summary>
        public int CoinDenominationId { get; set; }

        /// <summary>
        /// Indicates the quantity of coins with set coin denomination
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Indicates whether the operation is of deposit type
        /// </summary>
        public bool IsDeposited { get; set; }
    }
}
