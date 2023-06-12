using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Represent inventory items that are available for sale
    /// </summary>
    public class InventoryItem : BaseEntity, IInventoryItem
    {
        public InventoryItem()
        {

        }
        public InventoryItem(IStorageItem storageItem, int quantity)
        {
            StorageItem = storageItem;
            Quantity = quantity;
        }

        /// <summary>
        /// Inventory item object
        /// </summary>
        public IStorageItem StorageItem { get; set; }

        /// <summary>
        /// Foreign key to inventory item
        /// </summary>
        public int StorageItemId { get; set; }

        /// <summary>
        /// The quantity of specified inventory item inside vending machine
        /// </summary>
        public int Quantity { get; set; }
    }
}
