using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engineering_Units.Models;

namespace Engineering_Units.Data
{
    internal class DataHandler
    {
        private const int lastUsedUOMsToStore = 10;
        private List<UOM> LastUsedUOMs;
        private List<UOM> CustomUnits;
        private List<QuantityClass> SubQuantityClasses;
        private List<Alias> Aliases;

        public DataHandler()
        {
            LastUsedUOMs = new List<UOM>();
            CustomUnits = new List<UOM>();
            SubQuantityClasses = new List<QuantityClass>();
            Aliases = new List<Alias>();
        }

        private UOM? GetUOMFromMemory(string UOMName)
        {
            UOM? uom = LastUsedUOMs.FirstOrDefault(u => u.Name == UOMName || u.Annotation == UOMName);
            uom ??= CustomUnits.FirstOrDefault(u => u.Name == UOMName || u.Annotation == UOMName);
            uom?.UpdateLastUsed();

            return uom;
        }

        private void SaveUOMToMemory(UOM uom)
        {
            if (LastUsedUOMs.Any(u => u.Name == uom.Name) || CustomUnits.Any(u => u.Name == uom.Name))
            {
                // Already exists in memory
                return;
            }

            // Remove the least used UOM
            if (LastUsedUOMs.Count + 1 >= lastUsedUOMsToStore)
            {
                LastUsedUOMs.Remove(LastUsedUOMs.First(u => u.LastUsed == LastUsedUOMs.Min(x => x.LastUsed)));
            }

            // Add the new UOM
            uom.UpdateLastUsed();
            LastUsedUOMs.Add(uom);
        }

        internal UOM? GetUOM(string UOMName)
        {
            if (string.IsNullOrEmpty(UOMName))
            {
                return null;
            }

            Alias? alias = Aliases.FirstOrDefault(a => a.AliasName == UOMName);
            if (alias != null)
            {
                UOMName = alias.UOMName;
            }

            UOM? uom = GetUOMFromMemory(UOMName);
            if (uom == null)
            {
                // If not exists in memory; fetch from XML and update memory
                uom = DataFetcher.GetUOM(UOMName);
                if (uom != null)
                {
                    SaveUOMToMemory(uom);
                }
            }
            return uom;
        }

        internal static List<UnitDimention> GetUnitDimentions()
        {
            return DataFetcher.GetUnitDimentions();
        }

        internal static List<UOM> GetUOMsForUnitDimension(string unitDimention)
        {
            return DataFetcher.GetUOMsForUnitDimention(unitDimention);
        }

        internal List<QuantityClass> GetAllQuantityClasses()
        {
            // Returns custom subQuantityClasses in addition to quantityClasses from XML
            List<QuantityClass> quantityClasses = SubQuantityClasses;
            quantityClasses.AddRange(DataFetcher.GetAllQuantityClasses());
            return quantityClasses;
        }

        internal List<UOM> GetUOMsForQuantityClass(string quantityClass)
        {
            QuantityClass? subQuantityClass = SubQuantityClasses.FirstOrDefault(x => x.Name == quantityClass);
            List<UOM> uoms;
            if (subQuantityClass?.UnitNames != null)
            {
                uoms = subQuantityClass.UnitNames.Select(x => GetUOM(x)).Where(x => x != null).Select(x => x!).ToList();
            }
            else
            {
                uoms = DataFetcher.GetUOMsForQuantityClass(quantityClass);
            }
            return uoms;
        }

        internal List<string> GetAliasesForUOM(string uomName)
        {
            return Aliases.Where(x => x.UOMName == uomName).Select(x => x.AliasName).ToList();
        }

        internal bool CreateAlias(string uomName, string newAlias)
        {
            if (string.IsNullOrEmpty(newAlias) || string.IsNullOrEmpty(newAlias))
            {
                return false;
            }
            Aliases.Add(new Alias(newAlias, uomName));
            return true;
        }

        internal bool CreateSubQuantityClass(QuantityClass newQuantityClass)
        {
            if (string.IsNullOrEmpty(newQuantityClass?.Name) || newQuantityClass.UnitNames == null || newQuantityClass.UnitNames.Count == 0)
            {
                // Skip if any not all parameters are set
                return false;
            }
            string? baseUOM = null;
            foreach(string uomName in newQuantityClass.UnitNames)
            {
                UOM? uom = GetUOM(uomName);
                if (uom == null)
                {
                    // Skip if any uomName doesn't exist
                    return false;
                }
                string thisBaseUOM = uom.ConversionParameters == null ? uom.Name : uom.ConversionParameters.BaseUnit;
                baseUOM ??= thisBaseUOM;
                if (baseUOM != thisBaseUOM)
                {
                    // Skip if not all uoms has same baseUOM
                    return false;
                }
            }
            SubQuantityClasses.Add(newQuantityClass);
            return true;
        }

        internal bool CreateUOM(UOM newUOM)
        {
            if (string.IsNullOrEmpty(newUOM?.Name) || string.IsNullOrEmpty(newUOM.Annotation)
                || newUOM.QuantityClasses == null || newUOM.QuantityClasses.Count == 0
                || newUOM.ConversionParameters == null)
            {
                return false;
            }

            CustomUnits.Add(newUOM);
            return true;
        }
    }
}
