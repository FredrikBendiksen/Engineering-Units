using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Engineering_Units.Models;

namespace Engineering_Units.Data
{
    internal static class DataFetcher
    {
        readonly static string datasource = "mock";
        static readonly IFetchData data = CreateFetchData();

        public static IFetchData CreateFetchData()
        {
            IFetchData data;
            if (datasource == "XML")
            {
                data = new XMLReader();
            }
            else
            {
                data = new MockData();
            }

            return data;
        }

        public static UOM? GetUOM(string UOMName)
        {
            return data.GetUOM(UOMName);
        }

        public static List<QuantityClass> GetAllQuantityClasses()
        {
            return data.GetAllQuantityClasses();
        }

        public static List<UnitDimention> GetUnitDimentions()
        {
            return UnitDimentions;
        }

        public static List<UOM> GetUOMsForUnitDimention(string unitDimention)
        {
            return data.GetUOMsForUnitDimention(unitDimention);
        }

        public static List<UOM> GetUOMsForQuantityClass(string quantityClass)
        {
            return data.GetUOMsForQuantityClass(quantityClass);
        }
        private static List<UnitDimention> UnitDimentions
        {
            get => new List<UnitDimention>()
            {
                new UnitDimention('A', "angle", "radian"),
                new UnitDimention('D', "temperature difference", "kelvin"),
                new UnitDimention('I', "electrical current", "ampere"),
                new UnitDimention('J', "luminous intensity", "candela"),
                new UnitDimention('K', "thermodynamic temperature", "kelvin"),
                new UnitDimention('L', "length", "meter"),
                new UnitDimention('M', "mass", "kilogram"),
                new UnitDimention('N', "amount of substance", "mole"),
                new UnitDimention('S', "temperature solid angle", "steradian"),
                new UnitDimention('T', "time", "second"),
            };
        }
    }


    internal class XMLReader : IFetchData
    {
        // Reads XML-file
        public UOM? GetUOM(string UOMName)
        {
            throw new NotImplementedException();
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
