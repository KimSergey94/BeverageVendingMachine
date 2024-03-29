﻿using BeverageVendingMachine.Core.Common;
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
        /// <returns>The created storage item</returns>
        public async Task<StorageItem> AddNewStorageItem(StorageItem newStorageItem)
        {
            try
            {
                var addedStorageItem = await _unitOfWork.Repository<StorageItem>().AddAsync(newStorageItem);
                await _unitOfWork.Complete();
                if (addedStorageItem != null) _terminalService.GetStorageInstance().AddStorageItem(addedStorageItem);
                return addedStorageItem;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>The updated storage item</returns>
        public async Task<StorageItem> UpdateStorageItem(StorageItem storageItem)
        {
            try
            {
                var itemFromStorage = _terminalService.GetStorageInstance().StorageItems.FirstOrDefault(item => item.Id == storageItem.Id);
                if(itemFromStorage != null)
                {
                    itemFromStorage.Name = storageItem.Name;
                    itemFromStorage.Cost = storageItem.Cost;
                    itemFromStorage.ImageUrl = storageItem.ImageUrl;
                    itemFromStorage.StorageQuantity = storageItem.StorageQuantity;
                }
                var updatedStorageItemEntity = await _unitOfWork.Repository<StorageItem>().UpdateAsync(itemFromStorage);
                await _unitOfWork.Complete();
                var storageItemUpdateResult = _terminalService.GetStorageInstance().UpdateStorageItem(updatedStorageItemEntity);
                if (storageItemUpdateResult == null) throw new Exception("The storage item has not been updated in the system. Has not been found in storage.");
                return storageItemUpdateResult;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <param name="storageItemId">storage item id</param>
        /// <returns>Returns whether the deletion was successful</returns>
        public async Task<bool> DeleteStorageItem(int storageItemId)
        {
            var result = false;
            var storageItem = _terminalService.GetStorageInstance().StorageItems.FirstOrDefault(storageItem => storageItem.Id == storageItemId);
            if (storageItem != null) 
            {
                try
                {
                    if(await _unitOfWork.Repository<StorageItem>().DeleteAsync(storageItem))
                    {
                        await _unitOfWork.Complete();
                        var storageItemDeletionResult = _terminalService.GetStorageInstance().DeleteStorageItem(storageItem);
                        if (storageItemDeletionResult != 1) throw new Exception("The storage item deletion has not been updated in the system.");
                        result = true;
                    }
                    else throw new Exception("The storage item could not be deleted from database.");
                }
                catch
                {
                    throw;
                }
            }
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

            var importedStorageItemsEntities = new List<StorageItem>();
            var currentStorageItemsIds = _terminalService.GetStorageInstance().StorageItems.Select(item => item.Id);

            var currentStorageItemsEntities = _unitOfWork.Repository<StorageItem>().GetAllAsync().Result.Where(item => currentStorageItemsIds.Contains(item.Id));

            foreach(var currentStorageItemsEntitity in currentStorageItemsEntities)
            {
                await _unitOfWork.Repository<StorageItem>().DeleteAsync(currentStorageItemsEntitity);
            }

            foreach (var newStorageItem in newStorageItemsList)
            {
                var newStorageItemEntity = await _unitOfWork.Repository<StorageItem>().AddAsync(newStorageItem);
                importedStorageItemsEntities.Add(newStorageItemEntity);
            }

            _terminalService.GetStorageInstance().ImportAndUpdatePassedStorageItems(importedStorageItemsEntities);
            await _unitOfWork.Complete();
            result = true;

            return result;
        }

        #endregion
    }
}
