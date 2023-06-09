using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    public class Coin
    {
        public Coin(CoinDenomination denomination)
        {
            Denomination = denomination;
        }
        public CoinDenomination Denomination { get; }
        public int Amount { get; set; }
    }
}
