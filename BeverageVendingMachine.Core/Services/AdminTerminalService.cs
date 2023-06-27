using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
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
    public class AdminTerminalService : IAdminTerminalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITerminalService _terminalService;
        public AdminTerminalService(IUnitOfWork unitOfWork, ITerminalService terminalService)
        {
            _unitOfWork = unitOfWork;
            _terminalService = terminalService;
        }


        /// <summary>
        /// Gets update data from the vending machine terminal
        /// </summary>
        /// <returns>Update data from the vending machine terminal</returns>
        public UpdateData GetUpdateData()
        {
            return _terminalService.GetTerminalUpdateData();
        }

        #region Coins operations

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

        #endregion


        #region Storage operations

        /// <summary>
        /// Creates new storage item
        /// </summary>
        /// <param name="newStorageItem">new storage item object</param>
        /// <returns>Returns whether the creation of the new storage item was successful</returns>
        public async Task<bool> AddNewStorageItem(StorageItem newStorageItem)
        {
            var result = true;
            var addedStorageItem = await _unitOfWork.Repository<StorageItem>().AddAsync(newStorageItem);

            if (addedStorageItem == null) result = false;
            else _terminalService.GetStorageInstance().AddStorageItem(newStorageItem);

            return result;
        }

        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns whether the update was successful</returns>
        public async Task<bool> UpdateStorageItem(StorageItem storageItem)
        {
            var result = true;

            var updatedStorageItemEntity = await _unitOfWork.Repository<StorageItem>().UpdateAsync(storageItem);
            
            //needs to be checked
            if(updatedStorageItemEntity == storageItem) result = false;
            else
            {
                var storageItemUpdateResult = _terminalService.GetStorageInstance().UpdateStorageItem(storageItem);
                if (storageItemUpdateResult != 1) throw new Exception("The storage item has not been updated in the system.");
            }
            return result;
        }

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <returns>Returns whether the deletion was successful</returns>
        public async Task<bool> DeleteStorageItem(StorageItem storageItem)
        {
            var result = true;

            if (await _unitOfWork.Repository<StorageItem>().DeleteAsync(storageItem))
            {
                var storageItemDeletionResult = _terminalService.GetStorageInstance().DeleteStorageItem(storageItem);
                if (storageItemDeletionResult != 1) throw new Exception("The storage item deletion has not been updated in the system.");
            }
            else result = false;

            return result;
        }

        /// <summary>
        /// Imports new storage items and deletes storage items with ids that are in the passed collection
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        /// <returns>Returns whether the import was successful</returns>
        public async Task<bool> ImportAndChangeStorageItems(List<StorageItem> newStorageItemsList)
        {
            var result = false;

            var currentStorageItems = _terminalService.GetStorageInstance().StorageItems;
            var newStorageItemsIds = newStorageItemsList.Select(storageItem => storageItem.Id).ToList();
            var storageItemsToDelete = currentStorageItems.Where(storageItem => newStorageItemsIds.Contains(storageItem.Id));

            foreach(var storageItemToDelete in storageItemsToDelete)
            {
                await _unitOfWork.Repository<StorageItem>().DeleteAsync(storageItemToDelete);
            }

            foreach (var newStorageItem in newStorageItemsList)
            {
                await _unitOfWork.Repository<StorageItem>().AddAsync(newStorageItem);
            }

            _terminalService.GetStorageInstance().ImportAndUpdatePassedStorageItems(newStorageItemsList);
            result = true;

            return result;
        }

        #endregion
    }
}
