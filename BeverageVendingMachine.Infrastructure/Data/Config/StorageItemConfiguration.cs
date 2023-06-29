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
    public class StorageItemConfiguration : IEntityTypeConfiguration<StorageItem>
    {
        public void Configure(EntityTypeBuilder<StorageItem> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Cost).IsRequired().HasPrecision(6, 2);
            builder.Property(p => p.StorageQuantity).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired();
        }
    }
}