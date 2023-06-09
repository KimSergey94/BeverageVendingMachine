using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    public class Storage
    {
        public Storage() { }
        public Storage(Dictionary<CoinDenomination, List<Coin>> depositedCoins, List<StorageItem> items, StorageItem selectedItem)
        {
            DepositedCoins = depositedCoins;
            Items = items;
            SelectedItem = selectedItem;
        }
        public int DepositedAmount
        {
            get
            {
                var result = 0;
                foreach (var depositedCoins in DepositedCoins)
                    result += (int)depositedCoins.Key * depositedCoins.Value.Count;
                return result;
            }
        }
        public List<CoinDenomination> BlockedCoinDenomination { get; set; } = new List<CoinDenomination>();
        public Dictionary<CoinDenomination, List<Coin>> DepositedCoins { get; set; }
        public List<StorageItem> Items { get; set; }
        public StorageItem SelectedItem { get; set; }

        public int CalculateChange()
        {
            return DepositedAmount - SelectedItem.Cost;
        }
        public void DepositCoin(Coin coin)
        {
            if (DepositedCoins[coin.Denomination] == null)
                DepositedCoins[coin.Denomination] = new List<Coin>() { coin };
            else DepositedCoins[coin.Denomination].Add(coin);
        }
        public int ReleaseSelectedItem()
        {
            var change = CalculateChange();
            if (change >= 0)
            {
                DepositedCoins = new Dictionary<CoinDenomination, List<Coin>>();
                SelectedItem = null;
                return change;
            }
            else
            {
                throw new Exception("Not enough deposited amount.");
            }
        }
        public void SelectItem(StorageItem selectedItem)
        {
            SelectedItem = selectedItem;
        }
        public void BlockCoinDenomination(CoinDenomination coinDenomination)
        {
            if(BlockedCoinDenomination.FirstOrDefault(coinDenom => coinDenom == coinDenomination) == null)
                BlockedCoinDenomination.Add(coinDenomination);
        }
        public void UnblockCoinDenomination(CoinDenomination coinDenomination)
        {
            coinDenomination = BlockedCoinDenomination.FirstOrDefault(coinDenom => coinDenom == coinDenomination);
            if (coinDenomination != null) BlockedCoinDenomination.Remove(coinDenomination);
        }
    }   
}
