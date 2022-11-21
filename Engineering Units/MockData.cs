using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class MockData : IFetchData
    {
        public List<string> GetQuantClass()
        {
            List<string> listOfTypes = new List<string>()
            {
               "quantity type1",
               "quantity type2",
               "quantity type3"
             };


            return listOfTypes;
        }
    }

}