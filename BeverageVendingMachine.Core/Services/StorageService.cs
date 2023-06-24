﻿using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.DTOs;
using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Repositories;
using BeverageVendingMachine.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Services
{
    /// <summary>
    /// Represents vending machine physical storage. Implementation of storage operations
    /// </summary>
    public class StorageService : IStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StorageService(IUnitOfWork unitOfWork)//, List<CoinDenomination> coinDenominations, List<StorageItem> storageItems)
        {
            _unitOfWork = unitOfWork;
            CoinDenominations = _unitOfWork.Repository<CoinDenomination>().GetAllAsync().Result as List<CoinDenomination>;
            StorageItems = _unitOfWork.Repository<StorageItem>().GetAllAsync().Result.ToList();
            //CoinDenominations = coinDenominations;// _unitOfWork.Repository<CoinDenomination>().GetAllAsync().Result as List<CoinDenomination>;
            //StorageItems = storageItems;// _unitOfWork.Repository<StorageItem>().GetAllAsync().Result as List<IStorageItem>;
        }

        #region Fields

        /// <summary>
        /// The total deposited amount from all deposited coins
        /// </summary>
        public decimal DepositedAmount
        {
            get
            {
                var result = 0.00m;
                foreach (var coinDenominations in CoinDenominations)
                    result += coinDenominations.DepositedQuantity * coinDenominations.Value;
                return result;
            }
        }

        /// <summary>
        /// CoinDenomination entities
        /// </summary>
        public List<CoinDenomination> CoinDenominations { get; } = new List<CoinDenomination>();

        /// <summary>
        /// Represents items inside vending machine storage
        /// </summary>
        public List<StorageItem> StorageItems { get; set; } = new List<StorageItem>();

        #endregion


        #region Coins operations

        /// <summary>
        /// To deposit a coin to a vending machine temporary storage for a purchase
        /// </summary>
        /// <param name="coinDenominationId">Id of the coin denomination you want to deposit</param>
        /// <returns>Deposited coin denomination entity</returns>
        public CoinDenomination DepositCoin(int coinDenominationId)
        {
            var storageCoinDenomination = CoinDenominations.FirstOrDefault(coinDenom => coinDenom.Id == coinDenominationId);
            storageCoinDenomination.DepositedQuantity += 1;
            return storageCoinDenomination;
        }

        #region Take coins by amount operations

        /// <summary>
        /// Takes passed amount from deposited coins and returns coins collection object
        /// </summary>
        /// <param name="amount">The amount to take from deposited coins</param>
        public CoinsCollection TakeAmountFromDepositedCoins(decimal amount)
        {
            try
            {
                return TakeAmountFromCoins(amount, StorageCoinsTypeEnum.Deposited);
            }
            catch
            {
                throw new Exception($"Not enough deposited coins in the amount of {amount}");
            }
        }

        /// <summary>
        /// Provides coins collection for a change to a customer
        /// </summary>
        /// <param name="amount">The change amount needed to be returned to a user</param>
        /// <returns>The coins collection to provide to user</returns>
        public CoinsCollection GetCoinsForChange(decimal amount)
        {
            try
            {
                return TakeAmountFromCoins(amount, StorageCoinsTypeEnum.Storage);
            }
            catch
            {
                throw new Exception($"Not enough coins for change in the amount of {amount}");
            }
        }

        /// <summary>
        /// Takes provided amount from the coin denominations collection
        /// </summary>
        /// <param name="amount">The amount needed to be taken from coins</param>
        /// <param name="storageCoinsType">Storage type of coins (deposited or already in storage)</param>
        /// <returns>Taken amount in coins representation</returns>
        private CoinsCollection TakeAmountFromCoins(decimal amount, StorageCoinsTypeEnum storageCoinsType)
        {
            var result = new CoinsCollection(new Dictionary<decimal, int>());
            foreach (var coinDenomination in CoinDenominations.OrderByDescending(x => x.Value))
            {
                var takenCoins = TakeMaxCoinsByDenomination(coinDenomination.Value, amount, storageCoinsType);
                amount -= takenCoins[coinDenomination.Value] * coinDenomination.Value;
                result.CoinDenominationsQuantity.Add(coinDenomination.Value, takenCoins[coinDenomination.Value]);
            }

            if (amount > 0)
            {
                foreach (var coinDenominationGroup in result.CoinDenominationsQuantity) DepositCoinsToCollection(coinDenominationGroup.Key, coinDenominationGroup.Value, storageCoinsType);
                throw new Exception($"Not enough coins in the amount of {amount}");
            }
            else return result;
        }

        /// <summary>
        /// Takes max coins from the provided collection by denomination
        /// </summary>
        /// <param name="coinDenomination">coin denomination</param>
        /// <param name="amount">The total amount left</param>
        /// <param name="storageCoinsType">Storage type of coins (deposited or already in storage)</param>
        /// <returns>Dictionary of coin denomination and its quantity</returns>
        private Dictionary<decimal, int> TakeMaxCoinsByDenomination(decimal coinDenomination, decimal amount, StorageCoinsTypeEnum storageCoinsType)
        {
            var result = new Dictionary<decimal, int>();
            var coinDenominationEntity = CoinDenominations.FirstOrDefault(x => x.Value == coinDenomination);
            var totalCoins = storageCoinsType == StorageCoinsTypeEnum.Storage ? coinDenominationEntity.StorageQuantity : coinDenominationEntity.DepositedQuantity;
            var coinsNeeded = amount / coinDenomination;

            if (totalCoins >= coinsNeeded)
            {
                if (storageCoinsType == StorageCoinsTypeEnum.Storage) coinDenominationEntity.StorageQuantity -= (int)coinsNeeded;
                else if (storageCoinsType == StorageCoinsTypeEnum.Deposited) coinDenominationEntity.DepositedQuantity -= (int)coinsNeeded;
                result.Add(coinDenomination, (int)coinsNeeded);
            }
            else
            {
                if (storageCoinsType == StorageCoinsTypeEnum.Storage) coinDenominationEntity.StorageQuantity -= totalCoins;
                else if (storageCoinsType == StorageCoinsTypeEnum.Deposited) coinDenominationEntity.DepositedQuantity -= totalCoins;
                result.Add(coinDenomination, totalCoins);
            }
            return result;
        }

        /// <summary>
        /// Deposit coins to a provided collection
        /// </summary>
        /// <param name="coinDenomination">Coin denomination you want to deposit</param>
        /// <param name="coinsAmount">Amount of coins you want to deposit</param>
        /// <param name="storageCoinsType">Storage type of coins (deposited or already in storage)</param>
        private void DepositCoinsToCollection(decimal coinDenomination, int coinsAmount, StorageCoinsTypeEnum storageCoinsType)
        {
            var coinDenominationEntity = CoinDenominations.FirstOrDefault(x => x.Value == coinDenomination);
            if (coinDenominationEntity == null) throw new Exception($"Not found coin denomination: {coinDenomination}");

            if (StorageCoinsTypeEnum.Storage == storageCoinsType) coinDenominationEntity.StorageQuantity -= coinsAmount;
            else if (StorageCoinsTypeEnum.Deposited == storageCoinsType) coinDenominationEntity.DepositedQuantity -= coinsAmount;
        }

        #endregion

        #endregion


        #region Storage items operations

        /// <summary>
        /// Takes all items from vending machine storage
        /// </summary>
        /// <returns>Returns all storage items</returns>
        public List<StorageItem> TakeAllItemsFromStorage()
        {
            var result = StorageItems.Take(StorageItems.Count).ToList();
            StorageItems = StorageItems.Except(result).ToList();
            return result;
        }

        /// <summary>
        /// Takes item from vending machine storage
        /// </summary>
        /// <param name="item">Item from vending machine storage</param>
        /// <returns>Returns taken item from storage</returns>
        /// <exception cref="Exception">Not found purchase item</exception>
        public StorageItem TakeItemFromStorageItems(int storageItemId)
        {
            var storageItem = StorageItems.FirstOrDefault(x => x.Id == storageItemId);
            return storageItem == null ? throw new Exception($"Not found purchase item with an Id: {storageItemId}") : storageItem;
        }

        /// <summary>
        /// Adds passed item to vending machine storage
        /// </summary>
        /// <param name="item">Item to add to vending machine storage</param>
        /// <returns>Returns 1 if successful, 0 if the item is already in storage, -1 if there was error</returns>
        public int AddStorageItem(StorageItem item)
        {
            var result = 1;
            if (!StorageItems.Contains(item)) StorageItems.Add(item);
            else result = 0;
            return result;
        }

        /// <summary>
        /// Updates storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        public int UpdateStorageItem(StorageItem item)
        {
            var result = 1;
            var storageItem = StorageItems.FirstOrDefault(storageItem => storageItem.Id == item.Id);

            if (storageItem == null) result = 0;
            else
            {
                storageItem.Name = item.Name;
                storageItem.StorageQuantity = item.StorageQuantity;
                storageItem.ImageUrl = item.ImageUrl;
                storageItem.Cost = item.Cost;
            }
            return result;
        }

        /// <summary>
        /// Deletes storage item
        /// </summary>
        /// <param name="storageItem">storage item update entity</param>
        /// <returns>Returns 1 if successful, 0 if the item is not found, -1 if there was error</returns>
        public int DeleteStorageItem(StorageItem item)
        {
            var result = 1;
            var storageItem = StorageItems.FirstOrDefault(storageItem => storageItem.Id == item.Id);

            if (storageItem == null) result = 0;
            else StorageItems.Remove(storageItem);

            return result;
        }

        /// <summary>
        /// Imports new storage items and deletes storage items with ids that are in the passed collection
        /// </summary>
        /// <param name="newStorageItemsList">Collection of new storage items </param>
        public void ImportAndUpdatePassedStorageItems(List<StorageItem> newStorageItemsList)
        {
            var newStorageItemsIds = newStorageItemsList.Select(storageItem => storageItem.Id).ToList();

            //needs to be checked
            StorageItems.RemoveAll(storageItem => newStorageItemsIds.Contains(storageItem.Id));

            foreach (var newStorageItem in newStorageItemsList)
                StorageItems.Add(newStorageItem);
        }
        #endregion
    }

    /// <summary>
    /// Storage type of coins (deposited or already in storage)
    /// </summary>
    public enum StorageCoinsTypeEnum
    {
        Deposited = 0,
        Storage = 1
    }
}