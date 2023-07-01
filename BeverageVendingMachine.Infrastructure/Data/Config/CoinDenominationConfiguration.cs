using BeverageVendingMachine.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Infrastructure.Data.Config
{
    /// <summary>
    /// Coin denomination entity configuration for entity framework
    /// </summary>
    public class CoinDenominationConfiguration : IEntityTypeConfiguration<CoinDenomination>
    {
        public void Configure(EntityTypeBuilder<CoinDenomination> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Value).IsRequired().HasPrecision(4,2);
            builder.Property(p => p.StorageQuantity).IsRequired();
            builder.Property(p => p.DepositedQuantity).IsRequired();
            builder.Property(p => p.IsBlocked).IsRequired();
        }
    }
}
