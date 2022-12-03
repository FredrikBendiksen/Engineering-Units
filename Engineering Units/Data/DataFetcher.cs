using Engineering_Units.Models;

namespace Engineering_Units.Data;

internal static class DataFetcher
{
    readonly static string datasource = "mock";
    static readonly IDataSource data = CreateFetchData();

    public static IDataSource CreateFetchData()
    {
        IDataSource data;
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

    public static List<UnitDimension> GetUnitDimensions()
    {
        return UnitDimensions;
    }

    public static UnitDimension? GetUnitDimension(string searchString)
    {
        return UnitDimensions.FirstOrDefault(x => x.Symbol.ToString() == searchString.ToUpper() || x.Definition == searchString.ToLower());
    }

    public static List<UOM> GetUOMsForQuantityClass(string quantityClass)
    {
        return data.GetUOMsForQuantityClass(quantityClass);
    }
    private static List<UnitDimension> UnitDimensions
    {
        get => new List<UnitDimension>()
        {
            new UnitDimension('A', "angle", "radian"),
            new UnitDimension('D', "temperature difference", "kelvin"),
            new UnitDimension('I', "electrical current", "ampere"),
            new UnitDimension('J', "luminous intensity", "candela"),
            new UnitDimension('K', "thermodynamic temperature", "kelvin"),
            new UnitDimension('L', "length", "meter"),
            new UnitDimension('M', "mass", "kilogram"),
            new UnitDimension('N', "amount of substance", "mole"),
            new UnitDimension('S', "temperature solid angle", "steradian"),
            new UnitDimension('T', "time", "second"),
        };
    }
}


internal class XMLReader : IDataSource
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

    public List<UOM> GetUOMsForQuantityClass(string quantityClass)
    {
        throw new NotImplementedException();
    }
}
