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
        string Name { get; set; }
        
        List<QuantityClass> QuantityClass { get; set; }
        
        string Annotation { get; set; }
        
        ConversionParameters? ConversionParameters { get; set; }
    }
    
    internal class ConversionParameters
    {
        UOM BaseUnit { get; set; }
        int A { get; set; }
        int B { get; set; }
        int C { get; set; }
        int D { get; set; }
    }
}
