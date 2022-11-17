using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class DataHandler
    {
        List<UOM> LastUsedUOMs = new();
        List<UOM> CustomUnits = new();
        List<QuantityClass> SubQuantityClasses = new();
        List<Alias> Aliases = new();
    }
}
