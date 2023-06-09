using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities.StorageAggregate
{
    public enum CoinDenomination
    {
        [EnumMember(Value = "1")]
        One = 1,
        [EnumMember(Value = "2")]
        Two = 2,
        [EnumMember(Value = "5")]
        Five = 5, 
        [EnumMember(Value = "10")]
        Ten = 10
    }
}
