using BeverageVendingMachine.Core.Entities;
using BeverageVendingMachine.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeverageVendingMachine.Infrastructure.SeedData
{
    public class BeverageVendingMachineContextSeed
    {
        public static async Task SeedAsync(BeverageVendingMachineContext beverageVendingMachineContext, ILoggerFactory loggerFactory)
        {
            try
            {

                if (!beverageVendingMachineContext.CoinDenominations.Any() && !beverageVendingMachineContext.CoinOperations.Any() && !beverageVendingMachineContext.StorageItems.Any())
                {
                    var coinDenominationsData = File.ReadAllText("../BeverageVendingMachine.Infrastructure/SeedData/coinDenominations.json");
                    var coinDenominations = JsonSerializer.Deserialize<List<CoinDenomination>>(coinDenominationsData);

                    foreach (var coinDenomination in coinDenominations)
                        beverageVendingMachineContext.CoinDenominations.AddAsync(coinDenomination);
                    await beverageVendingMachineContext.SaveChangesAsync();


                    var storageItemsData = File.ReadAllText("../BeverageVendingMachine.Infrastructure/SeedData/storageItems.json");
                    var storageItems = JsonSerializer.Deserialize<List<StorageItem>>(storageItemsData);

                    foreach (var storageItem in storageItems)
                        beverageVendingMachineContext.StorageItems.AddAsync(storageItem);
                    await beverageVendingMachineContext.SaveChangesAsync();


                    var coinOperationsData = File.ReadAllText("../BeverageVendingMachine.Infrastructure/SeedData/coinOperations.json");
                    var coinOperations = JsonSerializer.Deserialize<List<CoinOperation>>(coinOperationsData);

                    foreach (var coinOperation in coinOperations)
                        beverageVendingMachineContext.CoinOperations.AddAsync(coinOperation);
                    await beverageVendingMachineContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BeverageVendingMachineContextSeed>();
                logger.LogError(ex, "Something went wrong.");
            }
        }
    }
}