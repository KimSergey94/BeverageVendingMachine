using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.Core.Entities;

namespace BeverageVendingMachine.Infrastructure.Data
{
    public class BeverageVendingMachineContext : DbContext
    {
        public BeverageVendingMachineContext(DbContextOptions<BeverageVendingMachineContext> options) : base(options)
        {

        }

        public DbSet<CoinOperation> CoinOperations { get; set; }
        public DbSet<CoinDenomination> CoinDenominations { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}