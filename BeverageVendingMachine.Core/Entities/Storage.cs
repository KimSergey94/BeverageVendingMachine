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
    /// Represent storage of the vending machine
    /// </summary>
    public class Storage : BaseEntity, IStorage
    {
        public Storage()
        {

        }
        public Storage(IStorageItem storageItem, int quantity)
        {
            StorageItem = storageItem;
            Quantity = quantity;
        }

        /// <summary>
        /// Storage item object
        /// </summary>
        public IStorageItem StorageItem { get; set; }

        /// <summary>
        /// Foreign key to storage item
        /// </summary>
        public int StorageItemId { get; set; }

        /// <summary>
        /// The quantity of specified storage item
        /// </summary>
        public int Quantity { get; set; }
    }
}
