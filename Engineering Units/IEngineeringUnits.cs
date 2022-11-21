using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    public interface IEngineeringUnits
    {
        public (decimal, string, string) Convert(decimal value, string fromUOM, string toUOM);
    }
}
