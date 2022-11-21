using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    internal class DataHandler
    {
        const int lastUsedUOMsToStore = 10;
        List<UOM> LastUsedUOMs = new();
        List<UOM> CustomUnits = new();
        List<QuantityClass> SubQuantityClasses = new();
        List<Alias> Aliases = new();

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

        internal UOM GetUOM(string UOMName)
        {
            UOM? uom = GetUOMFromMemory(UOMName);
            if (uom == null)
            {
                // If not exists in memory; fetch from XML and update memory
                uom = DataFetcher.GetUOM(UOMName);
                SaveUOMToMemory(uom);
            }
            return uom;
        }

        internal static List<UnitDimention> GetUnitDimentions()
        {
            return DataFetcher.GetUnitDimentions();
        }

        internal static List<UOM> GetUOMForUnitDimention(string unitDimention)
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
            if (subQuantityClass != null)
            {
                uoms = subQuantityClass.UnitNames.Select(x => GetUOM(x)).ToList();
            }
            else
            {
                uoms = DataFetcher.GetUOMsForQuantityClass(quantityClass).ToList();
            }
            return uoms;
        }

        internal List<string> GetAliasesForUOM(string uomName)
        {
            return Aliases.Where(x => x.UOMName == uomName).Select(x => x.AliasName).ToList();
        }

        internal void CreateSubQuantityClass(QuantityClass newQuantityClass)
        {
            SubQuantityClasses.Add(newQuantityClass);
        }

        internal void CreateUOM(UOM newUOM)
        {
            CustomUnits.Add(newUOM);
        }
    }
}
