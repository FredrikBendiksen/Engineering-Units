namespace Engineering_Units;

public interface IEngineeringUnits
{
    public List<(char symbol, string definition, string baseUnit)> GetUnitDimensions();
    
    public (char symbol, string definition, string baseUnit) GetUnitDimension(string searchString);
    
    public List<string> GetAllQuantityClasses();
    
    public List<(string name, string annotation)> GetUOMsForQuantityClass(string quantityClass);
    
    public List<string> GetAliasesForUOMName(string uomName);
    
    public string? CreateAlias(string uomName, string newAlias);
    
    public (decimal val, string name, string annotation) Convert(decimal value, string fromUOM, string toUOM);
    
    public string? CreateSubQuantityClass(string name, List<string> uomNames);

    public string? CreateUOM(string name, string annotation, List<string> quantityClasses, string baseUOM,
        decimal conversionParameterA, decimal conversionParameterB, decimal conversionParameterC, decimal conversionParameterD);
}
