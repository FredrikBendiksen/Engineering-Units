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

        char Symbol { get; set; }
        
        string Definition { get; set; }

        UOM BaseUnit { get; set; }
    }
}