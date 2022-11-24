using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Units.Models;

namespace Engineering_Units.Data
{
    internal class MockData : IFetchData
    {
        public List<UOM> uoms = new()
        {
            new UOM ("metre", "m", new List<QuantityClass>(){new QuantityClass("length", new List<string> { "metre" } )}, null),
            new UOM ("kilometre", "km", new List<QuantityClass>(){new QuantityClass("length", new List<string> { "metre" } )}, new ConversionParameters("metre", 0, 1000, 1, 0)),
        };

        public UOM? GetUOM(string UOMName)
        {
            UOM? uom = uoms.FirstOrDefault(x => x.Name == UOMName || x.Annotation == UOMName);
            return uom;
        }

        public List<QuantityClass> GetAllQuantityClasses()
        {
            return new List<QuantityClass>()
            {
                new QuantityClass("length"),
                new QuantityClass("weight")
            };
        }

        public List<UOM> GetUOMsForUnitDimention(string unitDimention)
        {
            return uoms;
        }

        public List<UOM> GetUOMsForQuantityClass(string quantityClass)
        {
            return uoms;
        }
    }

}