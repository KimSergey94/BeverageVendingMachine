using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using BeverageVendingMachine.Core.Interfaces.Entities;
using BeverageVendingMachine.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Services
{
    /// <summary>
    /// Service to manage vending machine terminal as an admin
    /// </summary>
    public class TerminalAdminService : ITerminalAdminService
    {
        private readonly ITerminalService _terminalService;
        public TerminalAdminService(ITerminalService terminalService)
        {
            _terminalService = terminalService;
        }

        /// <summary>
        /// Blocks passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to block</param>
        /// <returns>Returns 1 of successful, 0 if the coin is already blocked, -1 if there was error</returns>
        public int BlockCoinDenomination(int coinDenominationId)
        {
            return _terminalService.BlockCoinDenomination(coinDenominationId);
        }

        /// <summary>
        /// Unblock passed coin denomination by Id
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination to unblock</param>
        /// <returns>Returns 1 of successful, 0 if the coin is already blocked, -1 if there was error</returns>
        public int UnblockCoinDenomination(int coinDenominationId)
        {
            return _terminalService.UnblockCoinDenomination(coinDenominationId);
        }


        /// <summary>
        /// Creates new storage item
        /// </summary>
        /// <param name="newStorageItem">new storage item object</param>
        /// <returns>Returns whether the creation of the new storage item was successful</returns>
        public async Task<bool> AddNewStorageItem(StorageItem newStorageItem)
        {
            var result = true;
            //unit of work save operation 
            //etc
            _terminalService.AddNewItemToStorageItems(newStorageItem);
            return result;
        }


        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns whether the update was successful</returns>
        public async Task<bool> UpdateStorageItem(StorageItem storageItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <returns>Returns whether the deletion was successful</returns>
        public async Task<bool> DeleteStorageItem(StorageItem storageItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Imports new items and deletes all current storage items
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        /// <returns>Returns whether the import was successful</returns>
        public async Task<bool> ImportNewStorageItems(List<StorageItem> newStorageItemsList)
        {
            throw new NotImplementedException();
        }
    }
}
