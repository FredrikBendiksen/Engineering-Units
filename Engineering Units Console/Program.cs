using Engineering_Units;

IEngineeringUnits iEngineeringUnits = Factoring.GetEngineeringUnits();

while (true)
{
    Console.WriteLine("Insert value:");
    decimal val = decimal.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Insert from-UOM:");
    string? from = Console.ReadLine();

    Console.WriteLine("Insert to-UOM:");
    string? to = Console.ReadLine();


    if (from == null || to == null)
    {
        Console.WriteLine("Invalid UOM.");
    }
    else
    {
        (decimal convertedValue, string name, string annoation) = iEngineeringUnits.Convert(val, from, to);
        Console.WriteLine($"{convertedValue} {name} {annoation}");
    }
}