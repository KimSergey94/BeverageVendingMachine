﻿using BeverageVendingMachine.Application.DTOs;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Services
{
    /// <summary>
    /// Interface for a service to imitate terminal (operating system) to operate with the vending machine
    /// </summary>
    public interface ITerminalService
    {
        #region User interface

        /// <summary>
        /// Gets update data from the vending machine terminal
        /// </summary>
        /// <returns>Update data from the vending machine terminal</returns>
        UpdateData GetTerminalUpdateData();

        #endregion


        #region Terminal data

        /// <summary>
        /// Get the instance of storage singleton
        /// </summary>
        /// <returns>Returns the instance of storage singleton</returns>
        IStorageService GetStorageInstance();

        /// <summary>
        /// Gets all existing coins
        /// </summary>
        /// <returns>All existing coins</returns>
        Task<IReadOnlyList<CoinDenomination>> GetCoins();

        /// <summary>
        /// Gets all existing storage items
        /// </summary>
        /// <returns>All existing storage items</returns>
        Task<IReadOnlyList<StorageItem>> GetStorageItems();

        /// <summary>
        /// Calculates change by subtracting selected item cost from the deposited amount
        /// </summary>
        /// <returns>Returns amount to be returned to a vending machine user</returns>
        decimal CalculateChange();

        /// <summary>
        /// Returns the amount of deposited coins for a purchase
        /// </summary>
        /// <returns>the amount of deposited coins for a purchase</returns>
        decimal GetDepositedAmount();

        #endregion

        #region AdminTerminal

        /// <summary>
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already blocked, -1 if there was error</returns>
        int BlockCoinDenomination(int coinDenominationId);

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        /// <returns>Returns 1 if successful, 0 if the coin is already unblocked, -1 if there was error</returns>
        int UnblockCoinDenomination(int coinDenominationId);

        #endregion

        #region User terminal

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination you want to deposit</param>
        /// <returns>Update data for vending machine terminal</returns>
        Task<UpdateData> DepositCoin(int coinDenominationId);

        /// <summary>
        /// Selects item for a puchase
        /// </summary>
        /// <param name="purchaseItemId">Id of the selected purchase item</param>
        /// <returns>Update data for vending machine terminal</returns>
        UpdateData SelectPurchaseItem(int purchaseItemId);

        /// <summary>
        /// Unselects item for a puchase
        /// </summary>
        /// <returns>Update data for vending machine terminal</returns>
        UpdateData UnselectPurchaseItem();

        /// <summary>
        /// Releases purchase item and change
        /// </summary>
        /// <returns>Returns an object with the purchase item and change inside</returns>
        Task<PurchaseResult> ReleasePurchaseItemAndChange();

        /// <summary>
        /// Takes purchase item from inventory
        /// </summary>
        /// <returns>Returns purchase item from inventory</returns>
        Task<StorageItem> TakePurchaseItemFromInventory();

        /// <summary>
        /// Releases change
        /// </summary>
        /// <returns>Returns coins collection for a customer</returns>
        Task<CoinsCollection> ReleaseChange();

        #endregion
    }
}
