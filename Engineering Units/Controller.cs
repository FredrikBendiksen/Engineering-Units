using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Units.Data;
using Engineering_Units.Models;

namespace Engineering_Units
{
    internal class Controller : IEngineeringUnits
    {
        private DataHandler _dataHandler = new DataHandler();

        public (decimal, string, string) Convert(decimal value, string fromUOM, string toUOM)
        {
            UOM? from = _dataHandler.GetUOM(fromUOM);
            UOM? to = _dataHandler.GetUOM(toUOM);

            (decimal convertedValue, UOM? convertedUOM) = Conversion.Convert(value, from, to);

            if (convertedUOM == null)
            {
                return (0, "", "Invalid");
            }

            return (convertedValue, convertedUOM.Name, convertedUOM.Annotation);
        }
    }
}
