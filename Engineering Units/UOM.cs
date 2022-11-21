using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class UOM
    {
        public string Name { get; set; }
        
        public List<QuantityClass> QuantityClass { get; set; }
        
        public string Annotation { get; set; }
        
        public ConversionParameters? ConversionParameters { get; set; }
    }
    
    internal class ConversionParameters
    {
        public UOM BaseUnit { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
    }
}
