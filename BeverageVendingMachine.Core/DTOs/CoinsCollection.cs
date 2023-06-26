using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.DTOs
{
    /// <summary>
    /// DTO representing collection of coins with coin denominations and their quantity
    /// </summary>
    public class CoinsCollection
    {
        public CoinsCollection(Dictionary<decimal, int> coinDenominationQuantity) 
        {
            CoinDenominationsQuantity = coinDenominationQuantity;
        }

        /// <summary>
        /// Dictionary of coin denominations and their quantity
        /// </summary>
        public Dictionary<decimal, int> CoinDenominationsQuantity { get; set; } = new Dictionary<decimal, int>();
    }

    public static class CoinsCollectionExtensions
    {
        public static CoinsCollection AddToCoinCollection(this CoinsCollection coinsCollectionFrom, CoinsCollection coinsCollectionTo)
        {
            coinsCollectionFrom.CoinDenominationsQuantity.ToList().ForEach(x => {
                if (coinsCollectionTo.CoinDenominationsQuantity.ContainsKey(x.Key))
                    coinsCollectionTo.CoinDenominationsQuantity[x.Key] += x.Value;
                else coinsCollectionTo.CoinDenominationsQuantity.Add(x.Key, x.Value); 
            });
            return coinsCollectionTo;
        }
    }

}
