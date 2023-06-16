using BeverageVendingMachine.Core.Common;
using BeverageVendingMachine.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate
{
    /// <summary>
    /// Vending machine storage item entity
    /// </summary>
    public class StorageItem : BaseEntity, IStorageItem
    {
        public StorageItem() { }
        public StorageItem(int id, string name, decimal cost, string imageUrl)
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
}
