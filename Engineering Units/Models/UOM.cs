namespace Engineering_Units.Models;

public class UOM
{
    public UOM() { }
    public UOM(string name, string annotation, List<QuantityClass> quantityClasses, ConversionParameters? conversionParameters)
    {
        Name = name;
        Annotation = annotation;
        QuantityClasses = quantityClasses;
        ConversionParameters = conversionParameters;
    }

    public string Name { get; set; }

    public string Annotation { get; set; }

    public List<QuantityClass> QuantityClasses { get; set; }

    public ConversionParameters? ConversionParameters { get; set; }

    public DateTime? LastUsed { get; set; }

    public void UpdateLastUsed()
    {
        LastUsed = DateTime.Now;
    }
}

public class ConversionParameters
{
    public ConversionParameters(string baseUnit, decimal a, decimal b, decimal c, decimal d)
    {
        BaseUnit = baseUnit;
        A = a;
        B = b;
        C = c;
        D = d;
    }
    public string BaseUnit { get; set; }
    public decimal A { get; set; }
    public decimal B { get; set; }
    public decimal C { get; set; }
    public decimal D { get; set; }
}
