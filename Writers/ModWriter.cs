using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Util;
using System.Reflection.Metadata.Ecma335;

namespace EU4ModUtil.Writers
{
    internal class ModWriter
    {
        private Mod mod;
        private AppData appData;

        public ModWriter(Mod mod, AppData appData)
        {
            this.mod = mod;
            this.appData = appData;
        }

        public void WriteMod()
        {
            WriteCountries();
            WriteProvinces();
        }

        #region Countries

        /// <summary>
        /// Writes all country data to mod on disk
        /// </summary>
        public List<string> WriteCountries()
        {
            List<string> changed = new List<string>();
            changed.Add(WriteCountryTags());
            changed.AddRange(WriteCountryInfo());
            changed.Add(WriteCountryLocalisation());
            changed.AddRange(WriteCountryHistory());

            SetUnchanged();

            return changed;
        }

        /// <summary>
        /// Saves to common/country_tags/00_countries.txt
        /// </summary>
        public string WriteCountryTags()
        {
            List<string> lines = new List<string>();
            foreach (Country c in mod.countries)
            {
                lines.Add(c.Get00_CountriesTXTEntry());
            }

            string path = appData.modPath + "\\common\\country_tags\\00_countries.txt";
            File.WriteAllLines(path, lines);

            return "\\common\\country_tags\\00_countries.txt";
        }

        /// <summary>
        /// Saves to common/countries
        /// </summary>
        public List<string> WriteCountryInfo()
        {
            List<string> changed = new List<string>();
            foreach (Country c in mod.countries)
            {
                if (c.Changed)
                {
                    string txt = c.GetCountryTXT();
                    string path = appData.modPath + "\\common\\" + c.Path;
                    File.WriteAllText(path, txt);
                    changed.Add("\\common\\" + c.Path);
                }
            }
            return changed;
        }

        /// <summary>
        /// Saves to localisation/countries_l_english.yml
        /// </summary>
        public string WriteCountryLocalisation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("l_english:");
            foreach (Country c in mod.countries)
            {
                sb.Append(c.GetLocalisation());
            }

            string path = appData.modPath + "\\localisation\\countries_l_english.yml";
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\localisation\\countries_l_english.yml";
        }

        /// <summary>
        /// Saves to history
        /// </summary>
        public List<string> WriteCountryHistory()
        {
            List<string> changed = new List<string>();
            foreach (Country c in mod.countries)
            {
                if (c.Changed)
                {
                    DirectoryInfo dir = new DirectoryInfo(appData.modPath + "\\history\\countries");
                    FileInfo fi = dir.GetFiles(c.Tag + "*.*")?.FirstOrDefault();
                    string path;
                    if (fi == null)
                    {
                        path = appData.modPath + "\\history\\countries\\" + c.Tag + " - " + c.LocalizedName + ".txt";
                    }
                    else
                    {
                        path = fi.ToString();
                    }

                    string txt = c.GetHistoryTXT();
                    File.WriteAllText(path, txt);

                    int indx = path.IndexOf(appData.modPath);
                    string shortPath;
                    if (indx != -1)
                    {
                        shortPath = path.Remove(indx, appData.modPath.Length);
                    }
                    else
                    {
                        shortPath = path;
                    }

                    changed.Add(shortPath);
                }
            }
            return changed;
        }

        /// <summary>
        /// Sets value as unchanged
        /// </summary>
        public void SetUnchanged()
        {
            foreach (Country c in mod.countries)
            {
                c.Changed = false;
            }
        }

        #endregion

        #region Provinces
        /// <summary>
        /// Writes all country data to mod on disk
        /// </summary>
        public List<string> WriteProvinces()
        {
            List<string> changed = new List<string>();
            //changed.Add(WriteCountryTags());
            //changed.AddRange(WriteCountryInfo());
            changed.Add(WriteProvinceDefinitions());
            changed.Add(WriteProvinceNamesLocalisation());
            changed.Add(WriteProvinceAdjectivesLocalisation());
            changed.Add(WriteProvinceHistory());
            changed.Add(WriteProvinceArea());

            SetUnchanged();

            return changed;
        }

        public string WriteProvinceDefinitions()
        {
            List<string> lines = new List<string>();
            lines.Add("province;red;green;blue;x;x");
            foreach (Province p in mod.provinces)
            {
                lines.Add(p.GetDefinition());
            }

            string path = appData.modPath + "\\map\\definition.csv";
            //File.WriteAllLines(path, lines);

            return "\\map\\definition.csv";
        }

        public string WriteProvinceNamesLocalisation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("l_english:");
            foreach (Province p in mod.provinces)
            {
                sb.Append(p.GetLocalisedName());
            }

            string path = appData.modPath + "\\localisation\\prov_names_l_english.yml";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\localisation\\prov_names_adj_l_english.yml";
        }

        public string WriteProvinceAdjectivesLocalisation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("l_english:");
            foreach (Province p in mod.provinces)
            {
                sb.Append(p.GetLocalisedAdjective());
            }

            string path = appData.modPath + "\\localisation\\prov_names_adj_l_english.yml";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\localisation\\prov_names_adj_l_english.yml";
        }

        public string WriteProvinceHistory()
        {
            return "";
        }

        public string WriteProvinceArea()
        {
            Dictionary<string, List<int>> dict = mod.areas.ToDictionary(a => a, a => new List<int>());
            foreach (Province p in mod.provinces)
            {
                if (p.Area == null || !dict.ContainsKey(p.Area)) continue;

                dict[p.Area].Add(p.Number);
            }

            Trace.WriteLine(dict.ShortValueDictionaryToTXTString(2));
            string path = appData.modPath + "\\map\\area.txt";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "Area";
        }

        public string WriteProvinceContinent()
        {
            return "Continent";
        }

        public string WriteProvinceClimate()
        {
            return "Climate";
        }

        public string WriteProvinceDefault()
        {
            return "Default";
        }
        #endregion
    }
}
