using Engineering_Units.Models;

namespace Engineering_Units.Data;

internal interface IFetchData
{
    public UOM? GetUOM(string UOMName);
    public List<UOM> GetUOMsForUnitDimention(string unitDimention);
    public List<QuantityClass> GetAllQuantityClasses();
    public List<UOM> GetUOMsForQuantityClass(string quantityClass);
}