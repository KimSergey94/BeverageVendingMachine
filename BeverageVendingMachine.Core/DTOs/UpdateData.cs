using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.DTOs
{
    /// <summary>
    /// Updated data for terminal interface
    /// </summary>
    public class UpdateData
    {
        public UpdateData(decimal depositedAmount, decimal changeAmount, List<CoinDenomination> coins, List<Product> products) 
        {
            DepositedAmount = depositedAmount;
            ChangeAmount = changeAmount;
            Coins = coins;
            Products = products;
        }

        /// <summary>
        /// Total amount of the deposited coins
        /// </summary>
        public decimal? DepositedAmount { get; set; }

        /// <summary>
        /// Change amount
        /// </summary>
        public decimal? ChangeAmount { get; set; }

        /// <summary>
        /// Coin denomination entity list
        /// </summary>
        public List<CoinDenomination> Coins { get; set; }

        /// <summary>
        /// Storage items list
        /// </summary>
        public List<Product> Products { get; set; }
    }
}
