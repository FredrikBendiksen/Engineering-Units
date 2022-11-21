using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units.Models
{
    internal class UOM
    {
        public string Name { get; set; }

        public List<QuantityClass> QuantityClass { get; set; }

        public string Annotation { get; set; }

        public ConversionParameters? ConversionParameters { get; set; }

        public DateTime? LastUsed { get; set; }

        public void UpdateLastUsed()
        {
            LastUsed = DateTime.Now;
        }
    }

    internal class ConversionParameters
    {
        public ConversionParameters(string baseUnit, decimal a, decimal b, decimal c, decimal d)
        {
            BaseUnit = baseUnit;
            A = a;
            B = b;
            C = c;
            D = d;
        }
        public string BaseUnit { get; set; }
        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }
        public decimal D { get; set; }
    }
}
