using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Entities
{
    /// <summary>
    /// Represents inventory items that are available for sale
    /// </summary>
    public interface IInventoryItem
    {
        /// <summary>
        /// Inventory item object
        /// </summary>
        IStorageItem StorageItem { get; set; }

        /// <summary>
        /// Foreign key to inventory item
        /// </summary>
        int StorageItemId { get; set; }

        /// <summary>
        /// The quantity of specified inventory item inside vending machine
        /// </summary>
        int Quantity { get; set; }
    }
}
