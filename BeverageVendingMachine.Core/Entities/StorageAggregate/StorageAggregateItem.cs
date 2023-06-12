using BeverageVendingMachine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    /// <summary>
    /// Represents vending machine storage item (beverages)
    /// </summary>
    public class StorageAggregateItem : IStorageItem
    {
        /// <summary>
        /// Id of the storage item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the storage item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the storage item
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Url path to image of the storage item
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The quantity of the specified storage item/items
        /// </summary>
        public int Quantity { get; set; }
    }
}
