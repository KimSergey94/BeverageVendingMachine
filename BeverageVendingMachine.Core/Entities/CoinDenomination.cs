using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double Value { get; set; }

        /// <summary>
        /// True if blocked by admin
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
