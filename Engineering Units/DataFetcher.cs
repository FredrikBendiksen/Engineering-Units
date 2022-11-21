using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Engineering_Units
{
    internal class DataFetcher
    {
        readonly static string datasource = "XML";
        // Delegates dataFetcher-calls to the datasources
        public static IFetchData CreateFetchData()
        {
            IFetchData data;
            if (datasource == "XML")
            {
                data = new XMLReader();
            }
            else
            {
                data = new MockData();
            }

            return data;
        }

        public static List<string> GetQuantClass()
        {
            IFetchData data = CreateFetchData();
            return data.GetQuantClass();

        }

    }

    internal class XMLReader : IFetchData
    {
        // Reads XML-file
        public List<string> GetQuantClass()
        {
            throw new NotImplementedException();
        }
    }

}
