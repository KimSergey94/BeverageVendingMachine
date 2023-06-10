using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities
{
    /// <summary>
    /// Storage item entity (vending machine storage item)
    /// </summary>
    public class StorageItem: BaseEntity, IStorageItem
    {
        public StorageItem() { }
        public StorageItem(int id, string name, int cost, string imageUrl) 
        {
            Id = id;
            Name = name;
            Cost = cost;
            ImageUrl = imageUrl;
        }

        /// <summary>
        /// Name of the storage item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the storage item
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Url path to image of the storage item
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
