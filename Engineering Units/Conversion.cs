using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class Conversion
    {
        public static (decimal result, UOM? uom) Convert(decimal value, UOM? from, UOM? to)
        {
            if (from == null || to == null)
            {
                return (0, null);
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

            return (newValue, to);
        }
    }
}
