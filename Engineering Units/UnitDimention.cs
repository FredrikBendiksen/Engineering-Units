namespace Engineering_Units
{
    internal class UnitDimention
    {
        public UnitDimention(char symbol, string definition, UOM baseUnit)
        {
            Symbol = symbol;
            Definition = definition;
            BaseUnit = baseUnit;
        }

        public char Symbol { get; set; }
        
        public string Definition { get; set; }

        public UOM BaseUnit { get; set; }
    }
}