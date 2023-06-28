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

    /// <summary>
    /// Extensions to work with coin collection
    /// </summary>
    public static class CoinsCollectionExtensions
    {
        /// <summary>
        /// Adds coin collection to the passed coin collection as a parameter
        /// </summary>
        /// <param name="coinsCollectionFrom">Coin collection to get data from</param>
        /// <param name="coinsCollectionTo">Coin collection to put data into</param>
        /// <returns>Passed coin collection with the added data</returns>
        public static CoinsCollection AddToCoinCollection(this CoinsCollection coinsCollectionFrom, CoinsCollection coinsCollectionTo)
        {
            coinsCollectionFrom.CoinDenominationsQuantity.ToList().ForEach(x => {
                if (coinsCollectionTo.CoinDenominationsQuantity.ContainsKey(x.Key))
                    coinsCollectionTo.CoinDenominationsQuantity[x.Key] += x.Value;
                else coinsCollectionTo.CoinDenominationsQuantity.Add(x.Key, x.Value); 
            });
            return coinsCollectionTo;
        }


        /// <summary>
        /// Adds coin collection in a view of Dictionary of coin denominations and their quantity in the Dictionary method is called from
        /// </summary>
        /// <param name="coinDenominationsQuantityTo">Dictionary of coin denominations and their quantity that the passed Dictionary data will be added to</param>
        /// <param name="coinDenominationsQuantityFrom">Dictionary of coin denominations and their quantity that will be added to the Dictionary calling this method</param>
        /// <returns>The supplemented dictionary that called this extension method</returns>
        public static Dictionary<decimal, int> AddCoinCollection(this Dictionary<decimal, int> coinDenominationsQuantityTo, Dictionary<decimal, int> coinDenominationsQuantityFrom)
        {
            coinDenominationsQuantityFrom.ToList().ForEach(x => {
                if (coinDenominationsQuantityTo.ContainsKey(x.Key))
                    coinDenominationsQuantityTo[x.Key] += x.Value;
                else coinDenominationsQuantityTo.Add(x.Key, x.Value);
            });
            return coinDenominationsQuantityTo;
        }
    }

}
