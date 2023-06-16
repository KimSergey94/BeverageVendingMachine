using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.DTOs
{
    public class NewStorageItem
    {
        /// <summary>
        /// Name of the new storage item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the new storage item
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Url path to image of the new storage item
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The quantity of the new storage item added to inventory
        /// </summary>
        public int Quantity { get; set; }
    }
}
