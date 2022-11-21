using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class Alias
    {
        public Alias(string aliasName, string uomName)
        {
            AliasName = aliasName;
            UOMName = uomName;
        }

        public string AliasName { get; set; }

        public string UOMName { get; set; }
    }
}
