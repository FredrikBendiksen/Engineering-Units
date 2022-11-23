using Engineering_Units;

IEngineeringUnits iEngineeringUnits = Factoring.GetEngineeringUnits();

while (true)
{
    Console.WriteLine("Method:");
    
    switch (Console.ReadLine())
    {
        case "Convert":
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
                Console.WriteLine($"{convertedValue} {name} {annoation}\n");
            }
            break;
        case "Alias":
            Console.WriteLine("Insert original UOM name:");
            string? uomName = Console.ReadLine();

            Console.WriteLine("Insert aliasName:");
            string? alias = Console.ReadLine();


            if (uomName == null || alias == null)
            {
                Console.WriteLine("Invalid inputs.");
            }
            else
            {
                Console.WriteLine($"{iEngineeringUnits.CreateAlias(uomName, alias)}\n");
            }
            break;
    }

}