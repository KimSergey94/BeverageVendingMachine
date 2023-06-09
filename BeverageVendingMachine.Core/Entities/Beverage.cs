using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeverageVendingMachine.Core.Entities
{
    public class Beverage: BaseEntity
    {
        public Beverage() { }
        public Beverage(int id, string name, int cost) 
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
        public string Name { get; set; }
        public int Cost { get; set; }
    }
}
