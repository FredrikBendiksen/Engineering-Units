using Engineering_Units.Models;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;



namespace Engineering_Units.Data;

internal static class DataFetcher
{
    readonly static string datasource = "XML";
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
    private readonly string xmlPath = GetXMLPath();
    private static string GetXMLPath()
    {
        string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "";
        string xmlPath = solutiondir + "\\Engineering Units\\poscUnits.xml";
        return xmlPath;
    }
    public UOM? GetUOM(string UOMName)
    {
        UOM uom = new();
        using (XmlReader xmlReader = XmlReader.Create(xmlPath))
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 1;
            decimal D = 0;
            string baseunit = "";

            while (xmlReader.Read())  //reader.Read() returns the Boolean value indicating whether there is a XML statement or not.
            {
                if (xmlReader.IsStartElement()) //If Yes, then we try to check if the current statement contains a starting element or not
                {

                    if ("Name" == xmlReader.Name.ToString())
                    {
                        string name = xmlReader.ReadString();
                        if (name == UOMName)
                        {
                            uom.Name = name;

                            if (xmlReader.ReadToNextSibling("CatalogSymbol"))
                            {
                                string catalog = xmlReader.ReadString();
                                uom.Annotation = catalog;
                            }
                            if (xmlReader.ReadToNextSibling("ConversionToBaseUnit"))
                            { 
                                    baseunit = xmlReader.GetAttribute("baseUnit") ?? ""; 
                            }
                            else {  
                                return uom;
                            }
                            var subtreeReader = xmlReader.ReadSubtree();
                            subtreeReader.Read();

                            while (subtreeReader.Name.Contains("ConversionToBaseUnit") || subtreeReader.Name == "")
                            {
                                subtreeReader.Read();
                            }

                            if (subtreeReader.Name.Contains("Factor"))
                            {
                                B = xmlReader.ReadElementContentAsDecimal();   
                            }


                            else if (subtreeReader.Name.Contains("Fraction"))
                            {
                                xmlReader.ReadToDescendant("Numerator");
                                B = xmlReader.ReadElementContentAsDecimal();
                                xmlReader.ReadToNextSibling("Denominator");
                                C = xmlReader.ReadElementContentAsDecimal();
                            }
                            else if (subtreeReader.Name.Contains("Formula"))
                            {
                                xmlReader.ReadToDescendant("A");
                                A = xmlReader.ReadElementContentAsDecimal();
                                xmlReader.ReadToNextSibling("B");
                                B = xmlReader.ReadElementContentAsDecimal();
                                xmlReader.ReadToNextSibling("C");
                                C = xmlReader.ReadElementContentAsDecimal();
                                xmlReader.ReadToNextSibling("D");
                                D = xmlReader.ReadElementContentAsDecimal();  

                            }

                            subtreeReader.Close();
                        }
                    }
                }
            }
            uom.ConversionParameters = new ConversionParameters(baseunit, A, B, C, D);

        }
        return uom;
    }

    public List<QuantityClass> GetAllQuantityClasses()
    {
        List<QuantityClass> qclass = new List<QuantityClass>();
        List<string> qlist = new List<string>();
        using (XmlReader xmlReader = XmlReader.Create(xmlPath))
        {
            
            while (xmlReader.Read())  //reader.Read() returns the Boolean value indicating whether there is a XML statement or not.
            {
                if (xmlReader.IsStartElement() && xmlReader.Name.ToString()=="QuantityType")
                {
                       qlist.Add(xmlReader.ReadString());
                    
                }
            }
            qclass=qlist.Distinct().Select(q=>new QuantityClass(q)).ToList();

        }


        return qclass;
    }

    public List<UOM> GetUOMsForQuantityClass(string quantityClass)
    {  
        List<UOM> uoms = new List<UOM>();
        using (XmlReader xmlReader = XmlReader.Create(xmlPath))
        {
            
            while (xmlReader.Read())  //reader.Read() returns the Boolean value indicating whether there is a XML statement or not.
            {
                if (xmlReader.IsStartElement()) //If Yes, then we try to check if the current statement contains a starting element or not
                {
                    string annotation = "";
                    if ("UnitsOfMeasure" == xmlReader.Name.ToString())
                    {
                        annotation = xmlReader.GetAttribute("annotation")??"";
                    }

                    if ("Name" == xmlReader.Name.ToString())
                    {
                        string name = xmlReader.ReadString();


                        while (xmlReader.ReadToNextSibling("QuantityType") == true)
                        {
                            string quantityType = xmlReader.ReadElementContentAsString();

                            if (quantityType == quantityClass)
                            {
                                uoms.Add(new UOM(name, annotation, new List<QuantityClass>(), null));

                            }
                        }
                    }
                }
            }
        }
        return uoms;

    }
}
 

