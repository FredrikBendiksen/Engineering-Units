using Engineering_Units.Data;
using Engineering_Units.Models;

namespace Engineering_Units;

internal class Controller : IEngineeringUnits
{
    private readonly DataHandler _dataHandler;

    public Controller(MemoryLists? memory = null)
    {
        _dataHandler = new DataHandler(memory);
    }

    public (decimal, string, string) Convert(decimal value, string fromUOM, string toUOM)
    {
        UOM? from = _dataHandler.GetUOM(fromUOM);
        UOM? to = _dataHandler.GetUOM(toUOM);

        (decimal convertedValue, UOM? convertedUOM, string? errorMessage) = Convertor.Convert(value, from, to);

        if (convertedUOM == null || errorMessage != null)
        {
            return (0, errorMessage ?? "", "Invalid");
        }

        return (convertedValue, convertedUOM.Name, convertedUOM.Annotation);
    }

    public string? CreateAlias(string uomName, string newAlias)
    {
        return _dataHandler.CreateAlias(uomName, newAlias);
    }

    public string? CreateSubQuantityClass(string name, List<string> uomNames)
    {
        return _dataHandler.CreateSubQuantityClass(new QuantityClass(name, uomNames));
    }

    public string? CreateUOM(string name, string annotation, List<string> quantityClasses, string baseUOM, decimal conversionParameterA, decimal conversionParameterB, decimal conversionParameterC, decimal conversionParameterD)
    {
        UOM newUOM = new UOM(name, annotation, quantityClasses.Select(qc => new QuantityClass(qc)).ToList(),
            new ConversionParameters(baseUOM, conversionParameterA, conversionParameterB, conversionParameterC, conversionParameterD));

        return _dataHandler.CreateUOM(newUOM);
    }

    public List<string> GetAliasesForUOMName(string uomName)
    {
        return _dataHandler.GetAliasesForUOM(uomName);
    }

    public List<string> GetAllQuantityClasses()
    {
        return _dataHandler.GetAllQuantityClasses().Select(x => x.Name).ToList();
    }

    public List<(char symbol, string definition, string baseUnit)> GetUnitDimensions()
    {
        List<UnitDimension> unitDimentions = DataHandler.GetUnitDimentions();
        var tuples = unitDimentions.Select(x => (x.Symbol, x.Definition, x.BaseUnit)).ToList();
        return tuples;
    }

    public List<(string name, string annotation)> GetUOMsForQuantityClass(string quantityClass)
    {
        return _dataHandler.GetUOMsForQuantityClass(quantityClass).Select(x => (x.Name, x.Annotation)).ToList();
    }

    public (char symbol, string definition, string baseUnit) GetUnitDimension(string search)
    {
        UnitDimension? unitDimension = DataHandler.GetUnitDimension(search);
        unitDimension ??= new UnitDimension(' ', "No unit dimension found", "");
        return (unitDimension.Symbol, unitDimension.Definition, unitDimension.BaseUnit);
    }
}
