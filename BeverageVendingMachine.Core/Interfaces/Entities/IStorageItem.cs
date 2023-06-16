using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Entities
{
    /// <summary>
    /// Interface of vending machine storage item
    /// </summary>
    public interface IStorageItem
    {
        /// <summary>
        /// Id of the storage item entity
        /// </summary>
        public int Id { get; set; }

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
