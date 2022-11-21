using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units.Models
{
    internal class QuantityClass
    {
        public QuantityClass(string name, List<string> unitNames)
        {
            Name = name;
            UnitNames = unitNames;
        }
        public QuantityClass(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<string>? UnitNames { get; set; }

    }
}
