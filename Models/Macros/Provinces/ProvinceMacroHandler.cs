using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data.Map;

namespace EU4ModUtil.Models.Macros.Provinces
{
    internal static class ProvinceMacroHandler
    {
        #region Edit
        public static void SetName(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Name = value);
        }
        public static void SetLocalizedName(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.LocalizedName = value);
        }
        public static void SetLocalizedAdjective(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.LocalizedAdjective = value);
        }
        public static void SetOwner(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Owner = value);
        }
        public static void SetController(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Controller = value);
        }
        public static void SetCulture(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Culture = value);
        }
        public static void SetReligion(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Religion = value);
        }
        public static void SetDiscoveredBy(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.DiscoveredBy = value);
        }
        public static void SetUnrest(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Unrest = value);
        }
        public static void SetCapitalName(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Capital = value);
        }
        public static void SetArea(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Area = value);
        }
        public static void SetContinent(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.Continent = value);
        }
        public static void SetCores(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.AddCore = value);
        }
        public static void SetTradeGood(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.TradeGoods = value);
        }
        public static void SetTradeNode(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.TradeNode = value);
        }
        public static void SetTax(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.BaseTax = value);
        }
        public static void SetProduction(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.BaseProduction = value);
        }
        public static void SetManpower(this List<Province> provinces, string value)
        {
            provinces.ForEach(p => p.BaseManpower = value);
        }

        public static void SetR(this List<Province> provinces, int initial, int increment, int interval)
        {
            int counter = initial;
            int intervalCounter = 0;
            foreach (Province p in provinces)
            {
                p.R = counter;
                intervalCounter++;
                if (intervalCounter >= interval)
                {
                    counter = ((counter + increment) % 256 + 256) % 256;
                    intervalCounter = 0;
                }
            }
        }
        public static void SetG(this List<Province> provinces, int initial, int increment, int interval)
        {
            int counter = initial;
            int intervalCounter = 0;
            foreach (Province p in provinces)
            {
                p.G = counter;
                intervalCounter++;
                if (intervalCounter >= interval)
                {
                    counter = ((counter + increment) % 256 + 256) % 256;
                    intervalCounter = 0;
                }
            }
        }
        public static void SetB(this List<Province> provinces, int initial, int increment, int interval)
        {
            int counter = initial;
            int intervalCounter = 0;
            foreach (Province p in provinces)
            {
                p.B = counter;
                intervalCounter++;
                if (intervalCounter >= interval)
                {
                    counter = ((counter + increment) % 256 + 256) % 256;
                    intervalCounter = 0;
                }
            }
        }
        #endregion

        #region Add
        public static void Add(this List<Province> provinces, AddableProvince toAdd)
        {

        }
        #endregion
    }
}
