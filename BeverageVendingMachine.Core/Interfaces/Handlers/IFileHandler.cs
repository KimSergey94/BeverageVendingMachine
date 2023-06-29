using BeverageVendingMachine.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Interfaces.Handlers
{
    /// <summary>
    /// File handling handler
    /// </summary>
    public interface IFileHandler
    {
        /// <summary>
        /// Parses json file data into a collection of storage items
        /// </summary>
        /// <param name="file">JSON file</param>
        /// <returns>A collection of storage items parsed from json file data</returns>
        List<StorageItem> ExtractStorageItemsFromFile(IFormFile file);

        /// <summary>
        /// Deserializes passed json file into provided generic type param T
        /// </summary>
        /// <typeparam name="T">Generic type param T</typeparam>
        /// <param name="file">JSON file</param>
        /// <returns>Deserialized T class object</returns>
        T Deserialize<T>(IFormFile file) where T : class;
    }
}
