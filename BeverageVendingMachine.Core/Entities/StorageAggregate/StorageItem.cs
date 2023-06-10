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
    public class StorageItem : IStorageItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Cost { get; set; }
        public string ImageUrl { get; set; }
    }
}
