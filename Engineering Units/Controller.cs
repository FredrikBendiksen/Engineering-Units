using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Units.Data;
using Engineering_Units.Models;

namespace Engineering_Units;

internal class Controller : IEngineeringUnits
{
    private readonly DataHandler _dataHandler;

    public Controller()
    {
        _dataHandler = new DataHandler();
    }

    public (decimal, string, string) Convert(decimal value, string fromUOM, string toUOM)
    {
        UOM? from = _dataHandler.GetUOM(fromUOM);
        UOM? to = _dataHandler.GetUOM(toUOM);

        (decimal convertedValue, UOM? convertedUOM) = Conversion.Convert(value, from, to);

        if (convertedUOM == null)
        {
            return (0, "Invalid", "Invalid");
        }

        return (convertedValue, convertedUOM.Name, convertedUOM.Annotation);
    }

    public bool CreateAlias(string uomName, string newAlias)
    {
        return _dataHandler.CreateAlias(uomName, newAlias);
    }

    public bool CreateSubQuantityClass(string name, List<string> uomNames)
    {
        return _dataHandler.CreateSubQuantityClass(new QuantityClass(name, uomNames));
    }

    public bool CreateUOM(string name, string annotation, List<string> quantityClasses, string baseUOM, decimal converstionParameterA, decimal converstionParameterB, decimal converstionParameterC, decimal converstionParameterD)
    {
        UOM newUOM = new UOM(name, annotation, quantityClasses.Select(qc => new QuantityClass(qc)).ToList(),
            new ConversionParameters(baseUOM, converstionParameterA, converstionParameterB, converstionParameterC, converstionParameterD));

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
        return DataHandler.GetUnitDimentions().Select(x => (x.Symbol, x.Definition, x.BaseUnit )).ToList();
    }

    public List<string> GetUOMsForQuantityClass(string quantityClass)
    {
        return _dataHandler.GetUOMsForQuantityClass(quantityClass).Select(x => x.Name).ToList();
    }

    public List<(string name, string annotation)> GetUOMsForUnitDimension(string unitDimension)
    {
        return DataHandler.GetUOMsForUnitDimension(unitDimension).Select(x => (x.Name, x.Annotation)).ToList();
    }
}
