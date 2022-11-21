using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class Controller : IEngineeringUnits
    {
        public List<UOM> mockList = new List<UOM>() // Mock
        {
            new UOM
            {
                Name= "metre",
                Annotation= "m",
                QuantityClass = new List<QuantityClass>(){
                    new QuantityClass()
                    {
                        Name = "length",
                        UnitNames= new List<string> { "metre" }
                    }
                }
            },
            new UOM
            {
                Name= "kilometre",
                Annotation= "km",
                QuantityClass = new List<QuantityClass>(){
                    new QuantityClass()
                    {
                        Name = "length",
                        UnitNames= new List<string> { "metre" }
                    }
                },
                ConversionParameters = new ConversionParameters("metre", 0, 1000, 1, 0)
            }
        };

        public (decimal, string, string) Convert(decimal value, string fromUOM, string toUOM)
        {
            UOM? from = mockList.FirstOrDefault(x => x.Annotation == fromUOM); // Mock
            UOM? to = mockList.FirstOrDefault(x => x.Annotation == toUOM); // Mock
            // TODO: Check on name as well as annotation

            (decimal convertedValue, UOM? convertedUOM) = Conversion.Convert(value, from, to);

            if (convertedUOM == null)
            {
                return (0, "", "Invalid");
            }

            return (convertedValue, convertedUOM.Name, convertedUOM.Annotation);
        }
    }
}
