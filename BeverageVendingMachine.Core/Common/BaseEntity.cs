using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Common
{
    /// <summary>
    /// Abstract class as a base of entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Id of an entity
        /// </summary>
        public int Id { get; set; }
    }
}
