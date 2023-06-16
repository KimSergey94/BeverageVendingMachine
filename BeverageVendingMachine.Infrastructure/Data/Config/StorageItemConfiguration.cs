using BeverageVendingMachine.Core.Entities.Aggregates.StorageAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Infrastructure.Data.Config
{
    public class StorageItemConfiguration : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Cost).IsRequired();
        }
    }
}