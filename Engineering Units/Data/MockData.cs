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
            new UOM
            {
                Name= "metre",
                Annotation= "m",
                QuantityClasses = new List<QuantityClass>(){
                    new QuantityClass("length", new List<string> { "metre" } )
                }
            },
            new UOM
            {
                Name= "kilometre",
                Annotation= "km",
                QuantityClasses = new List<QuantityClass>(){
                    new QuantityClass("length", new List<string> { "metre" })
                },
                ConversionParameters = new ConversionParameters("metre", 0, 1000, 1, 0)
            }
        };

        public UOM? GetUOM(string UOMName)
        {
            UOM? uom = uoms.FirstOrDefault(x => x.Name == UOMName || x.Annotation == UOMName);
            uom ??= new UOM()
            {
                Name = "test",
                Annotation = "t",
                QuantityClasses = new List<QuantityClass>(){
                    new QuantityClass("length", new List < string > { "metre" })
                },
                ConversionParameters = new ConversionParameters("metre", 0, 1000, 1, 0)
            };
            return uom;
        }

        public List<QuantityClass> GetAllQuantityClasses()
        {
            throw new NotImplementedException();
        }

        public List<UOM> GetUOMsForUnitDimention(string unitDimention)
        {
            throw new NotImplementedException();
        }

        public List<UOM> GetUOMsForQuantityClass(string quantityClass)
        {
            throw new NotImplementedException();
        }
    }

}