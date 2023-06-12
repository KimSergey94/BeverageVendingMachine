using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    /// <summary>
    /// Represents a coin element in storage aggregate
    /// </summary>
    public class Coin
    {
        public Coin(int denomination)
        {
            Denomination = denomination;
        }


        /// <summary>
        /// Coin denomination
        /// </summary>
        public int Denomination { get; }

        /// <summary>
        /// Quantity of coins with set denomination
        /// </summary>
        public int Quantity { get; set; }
    }
}
