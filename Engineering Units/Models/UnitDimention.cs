namespace Engineering_Units.Models
{
    internal class UnitDimention
    {
        public UnitDimention(char symbol, string definition, string baseUnit)
        {
            Symbol = symbol;
            Definition = definition;
            BaseUnit = baseUnit;
        }

        public char Symbol { get; set; }

        public string Definition { get; set; }

        public string BaseUnit { get; set; }
    }
}