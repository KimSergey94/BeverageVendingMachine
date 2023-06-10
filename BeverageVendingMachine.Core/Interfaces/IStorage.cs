using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces
{
    public interface IStorage
    {
        /// <summary>
        /// Storage item object
        /// </summary>
        IStorageItem StorageItem { get; set; }

        /// <summary>
        /// Foreign key to storage item
        /// </summary>
        int StorageItemId { get; set; }

        /// <summary>
        /// The quantity of specified storage item
        /// </summary>
        int Quantity { get; set; }
    }
}
