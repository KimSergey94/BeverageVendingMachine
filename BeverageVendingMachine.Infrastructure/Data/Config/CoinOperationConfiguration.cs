using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeverageVendingMachine.Core.Entities;

namespace BeverageVendingMachine.Infrastructure.Data.Config
{
    public class CoinOperationConfiguration : IEntityTypeConfiguration<CoinOperation>
    {
        public void Configure(EntityTypeBuilder<CoinOperation> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.IsDeposited).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.CoinDenominationId).IsRequired();
            builder.HasOne(c => c.CoinDenomination).WithMany().HasForeignKey(p => p.CoinDenominationId);
        }
    }
}