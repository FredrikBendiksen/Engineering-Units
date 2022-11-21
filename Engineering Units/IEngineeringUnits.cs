﻿namespace Engineering_Units;

public interface IEngineeringUnits
{
    public List<(char symbol, string definition, string baseUnit)> GetUnitDimensions();
    
    public List<(string name, string annotation)> GetUOMsForUnitDimension(string unitDimension);
    
    public List<string> GetAllQuantityClasses();
    
    public List<string> GetUOMsForQuantityClass(string quantityClass);
    
    public List<string> GetAliasesForUOMName(string uomName);
    
    public bool CreateAlias(string uomName, string newAlias);
    
    public (decimal val, string name, string annotation) Convert(decimal value, string fromUOM, string toUOM);
    
    public bool CreateSubQuantityClass(string name, List<string> uomNames);

    public bool CreateUOM(string name, string annotation, List<string> quantityClasses, string baseUOM,
        decimal converstionParameterA, decimal converstionParameterB, decimal converstionParameterC, decimal converstionParameterD);
}
