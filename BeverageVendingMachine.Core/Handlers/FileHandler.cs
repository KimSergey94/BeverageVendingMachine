using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Core.Interfaces.Handlers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Handlers
{
    /// <summary>
    /// File handling handler
    /// </summary>
    public class FileHandler : IFileHandler
    {
        /// <summary>
        /// Parses json file data into a collection of storage items
        /// </summary>
        /// <param name="file">JSON file</param>
        /// <returns>A collection of storage items parsed from json file data</returns>
        /// <exception cref="Exception">Corrupted file (length < 0)</exception>
        public static List<StorageItem> ExtractStorageItemsFromFile(IFormFile file)
        {
            var extension = System.IO.Path.GetExtension(file.FileName);
            if (file.Length < 0) throw new Exception("File seems to be corrupted.");
            return Deserialize<List<StorageItem>>(file);

            if (extension != ".json") throw new Exception("Wrong file format");
            else
            {
                if (file.Length < 0) throw new Exception("File seems to be corrupted.");
                return Deserialize<List<StorageItem>>(file);
            }
        }
        /// <summary>
        /// Parses json file data into a collection of storage items
        /// </summary>
        /// <param name="file">JSON file</param>
        /// <returns>A collection of storage items parsed from json file data</returns>
        /// <exception cref="Exception">Corrupted file (length < 0)</exception>
        List<StorageItem> IFileHandler.ExtractStorageItemsFromFile(IFormFile file)
        {
            return ExtractStorageItemsFromFile(file);
        }

        /// <summary>
        /// Deserializes passed json file into provided generic type param T
        /// </summary>
        /// <typeparam name="T">Generic type param T</typeparam>
        /// <param name="file">JSON file</param>
        /// <returns>Deserialized T class object</returns>
        public static T Deserialize<T>(IFormFile file) where T : class
        {
            string fileContent = null;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
        /// <summary>
        /// Deserializes passed json file into provided generic type param T
        /// </summary>
        /// <typeparam name="T">Generic type param T</typeparam>
        /// <param name="file">JSON file</param>
        /// <returns>Deserialized T class object</returns>
        T IFileHandler.Deserialize<T>(IFormFile file) where T : class
        {
            return Deserialize<T>(file);
        }
    }
}
