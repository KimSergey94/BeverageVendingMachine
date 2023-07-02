using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Vending machine storage item entity
    /// </summary>
    public class StorageItem : BaseEntity
    {
        public StorageItem() { }
        public StorageItem(int id, string name, decimal cost, string imageUrl, int storageQuantity)
        {
            Id = id;
            Name = name;
            Cost = cost;
            ImageUrl = imageUrl;
            StorageQuantity = storageQuantity;
        }

        /// <summary>
        /// Name of the storage item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the storage item
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Url path to image of the storage item
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The quantity of the storage item inside the vending machine
        /// </summary>
        public int StorageQuantity { get; set; }
    }

    public static class StorageItemExtensions
    {
        public static Product ConvertToProduct(this StorageItem storageItem, bool isSelected)
        {
            return new Product(storageItem.Id, storageItem.Name, storageItem.Cost, storageItem.ImageUrl, storageItem.StorageQuantity, isSelected);
        }
        public static List<Product> ConvertToProduct(this List<StorageItem> storageItemsList, int isSelectedItemId)
        {
            List<Product> productsResultList  = new List<Product>();
            foreach (StorageItem storageItem in storageItemsList)
                productsResultList.Add(storageItem.ConvertToProduct(storageItem.Id == isSelectedItemId));
            return productsResultList;
        }
    }
}
