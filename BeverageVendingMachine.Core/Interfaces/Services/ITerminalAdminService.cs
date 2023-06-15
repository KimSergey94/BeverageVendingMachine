﻿using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Services
{
    /// <summary>
    /// Interface for a service to manage vending machine terminal as ab admin
    /// </summary>
    public interface ITerminalAdminService
    {
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


        /// <summary>
        /// Creates new storage item
        /// </summary>
        /// <param name="newStorageItem">new storage item object</param>
        /// <returns>Returns whether the creation of the new storage item was successful</returns>
        Task<bool> AddNewStorageItem(StorageItem newStorageItem);


        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns whether the update was successful</returns>
        Task<bool> UpdateStorageItem(StorageItem storageItem);

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <returns>Returns whether the deletion was successful</returns>
        Task<bool> DeleteStorageItem(StorageItem storageItem);

        /// <summary>
        /// Imports new items and deletes all current storage items
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        /// <returns>Returns whether the import was successful</returns>
        Task<bool> ImportNewStorageItems(List<StorageItem> newStorageItemsList);
    }
}