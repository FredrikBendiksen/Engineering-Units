using Engineering_Units.Models;

namespace Engineering_Units;

internal static class Convertor
{
    internal static (decimal result, UOM? uom, string? errorMessage) Convert(decimal value, UOM? from, UOM? to)
    {
        if (from == null || to == null)
        {
            return (0, null, "UOMs not found");
        }
        if((from.ConversionParameters?.BaseUnit ?? from.Annotation) != (to.ConversionParameters?.BaseUnit ?? to.Annotation))
        {
            // Check for same BaseUnit. No ConversionParameters means baseUnit
            return (0, null, "UOMs are not of same base-unit");
        }

        decimal baseValue;
        if (from.ConversionParameters == null)
        {
            baseValue = value;
        }
        else
        {
            baseValue = (from.ConversionParameters.A / from.ConversionParameters.C)
                + (value * from.ConversionParameters.B / from.ConversionParameters.C);
        }

        decimal newValue;
        if (to.ConversionParameters == null)
        {
            newValue = baseValue;
        }
        else
        {
            newValue = - (to.ConversionParameters.A / to.ConversionParameters.B)
                + (baseValue * to.ConversionParameters.C / to.ConversionParameters.B);
        }

        return (newValue, to, null);
    }
}
