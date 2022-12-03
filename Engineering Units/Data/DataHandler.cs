using Engineering_Units.Models;

namespace Engineering_Units.Data;

internal class DataHandler
{
    private const int LastUsedUOMsToStore = 10;
    private MemoryLists _memory;

    public DataHandler(MemoryLists? memory = null)
    {
        _memory = memory ?? new MemoryLists();
    }

    private UOM? GetUOMFromMemory(string UOMName)
    {
        UOM? uom = _memory.LastUsedUOMs.FirstOrDefault(u => u.Name == UOMName || u.Annotation == UOMName);
        uom ??= _memory.CustomUnits.FirstOrDefault(u => u.Name == UOMName || u.Annotation == UOMName);
        uom?.UpdateLastUsed();

        return uom;
    }

    private void SaveUOMToMemory(UOM uom)
    {
        if (_memory.LastUsedUOMs.Any(u => u.Name == uom.Name) || _memory.CustomUnits.Any(u => u.Name == uom.Name))
        {
            // Already exists in memory
            return;
        }

        // Remove the least used UOM
        if (_memory.LastUsedUOMs.Count + 1 >= LastUsedUOMsToStore)
        {
            _memory.LastUsedUOMs.Remove(_memory.LastUsedUOMs.First(u => u.LastUsed == _memory.LastUsedUOMs.Min(x => x.LastUsed)));
        }

        // Add the new UOM
        uom.UpdateLastUsed();
        _memory.LastUsedUOMs.Add(uom);
    }

    private Alias? GetAlias(string alias)
    {
        return _memory.Aliases.FirstOrDefault(a => a.AliasName == alias); ;
    }

    internal UOM? GetUOM(string UOMName)
    {
        if (string.IsNullOrEmpty(UOMName))
        {
            return null;
        }

        Alias? alias = GetAlias(UOMName);
        if (alias != null)
        {
            UOMName = alias.UOMName;
        }

        UOM? uom = GetUOMFromMemory(UOMName);
        if (uom == null)
        {
            // If not exists in memory; fetch from XML and update memory
            uom = DataFetcher.GetUOM(UOMName);
            if (uom != null)
            {
                SaveUOMToMemory(uom);
            }
        }
        return uom;
    }

    internal static List<UnitDimension> GetUnitDimentions()
    {
        return DataFetcher.GetUnitDimensions();
    }

    internal static UnitDimension? GetUnitDimension(string searchString)
    {
        return DataFetcher.GetUnitDimension(searchString);
    }

    internal List<QuantityClass> GetAllQuantityClasses()
    {
        // Returns custom subQuantityClasses in addition to quantityClasses from XML
        List<QuantityClass> quantityClasses = _memory.SubQuantityClasses;
        quantityClasses.AddRange(DataFetcher.GetAllQuantityClasses());
        return quantityClasses;
    }

    internal List<UOM> GetUOMsForQuantityClass(string quantityClass)
    {
        QuantityClass? subQuantityClass = _memory.SubQuantityClasses.FirstOrDefault(x => x.Name == quantityClass);
        List<UOM> uoms;
        if (subQuantityClass?.UnitNames != null)
        {
            uoms = subQuantityClass.UnitNames.Select(x => GetUOM(x)).Where(x => x != null).Select(x => x!).ToList();
        }
        else
        {
            uoms = DataFetcher.GetUOMsForQuantityClass(quantityClass);
        }
        return uoms;
    }

    internal List<string> GetAliasesForUOM(string uomName)
    {
        return _memory.Aliases.Where(x => x.UOMName == uomName).Select(x => x.AliasName).ToList();
    }

    internal string? CreateAlias(string uomName, string newAlias)
    {
        if (string.IsNullOrEmpty(newAlias) || string.IsNullOrEmpty(newAlias))
        {
            return "Invalid parameters";
        }
        if (GetUOM(newAlias) != null)
        {
            return "Alias or UOM already exists";
        }
        _memory.Aliases.Add(new Alias(newAlias, uomName));
        return null; // Success
    }

    internal string? CreateSubQuantityClass(QuantityClass newQuantityClass)
    {
        if (string.IsNullOrEmpty(newQuantityClass?.Name) || newQuantityClass.UnitNames == null || newQuantityClass.UnitNames.Count == 0)
        {
            // Skip if any not all parameters are set
            return "Invalid parameters";
        }
        if (_memory.SubQuantityClasses.Any(x => x.Name == newQuantityClass.Name))
        {
            return "SubQuantityClass already exists";
        }

        string? baseUOM = null;
        foreach(string uomName in newQuantityClass.UnitNames)
        {
            UOM? uom = GetUOM(uomName);
            if (uom == null)
            {
                // Skip if any uomName doesn't exist
                return "Not all UOMs exists";
            }
            string thisBaseUOM = uom.ConversionParameters == null ? uom.Name : uom.ConversionParameters.BaseUnit;
            baseUOM ??= thisBaseUOM;
            if (baseUOM != thisBaseUOM)
            {
                // Skip if not all uoms has same baseUOM
                return "Not all UOMs are of the same base-unit";
            }
        }
        _memory.SubQuantityClasses.Add(newQuantityClass);
        return null; // Success 
    }

    internal string? CreateUOM(UOM newUOM)
    {
        if (string.IsNullOrEmpty(newUOM?.Name) || string.IsNullOrEmpty(newUOM.Annotation)
            || newUOM.QuantityClasses == null || newUOM.QuantityClasses.Count == 0
            || newUOM.ConversionParameters == null)
        {
            return "Invalid parameters";
        }
        if (GetUOM(newUOM.Name) != null)
        {
            return "UOM-name already exists";
        }

        _memory.CustomUnits.Add(newUOM);
        return null; // Success
    }
}
