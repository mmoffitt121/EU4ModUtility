﻿using System;
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
            changed.AddRange(WriteProvinceHistory());
            changed.Add(WriteProvinceArea());
            changed.Add(WriteProvinceContinent());
            changed.Add(WriteProvinceClimate());
            changed.Add(WriteProvinceDefault());

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

        public List<string> WriteProvinceHistory()
        {
            return new List<string>() { "" };
        }

        public string WriteProvinceArea()
        {
            Dictionary<string, List<int>> dict = mod.areas.ToDictionary(a => a, a => new List<int>());
            foreach (Province p in mod.provinces)
            {
                if (p.Area == null || !dict.ContainsKey(p.Area)) continue;

                dict[p.Area].Add(p.Number);
            }

            Trace.WriteLine(dict.ShortValueDictionaryToTXTString(6));
            string path = appData.modPath + "\\map\\area.txt";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\map\\area.txt";
        }

        public string WriteProvinceContinent()
        {
            Dictionary<string, List<int>> dict = mod.continents.ToDictionary(a => a, a => new List<int>());
            foreach (Province p in mod.provinces)
            {
                if (p.Continent == null || !dict.ContainsKey(p.Continent)) continue;

                dict[p.Continent].Add(p.Number);
            }

            Trace.WriteLine(dict.ShortValueDictionaryToTXTString(6));
            string path = appData.modPath + "\\map\\continent.txt";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\map\\continent.txt";
        }

        public string WriteProvinceClimate()
        {
            List<string> tropical = new List<string>();
            List<string> arid = new List<string>();
            List<string> arctic = new List<string>();
            List<string> mildWinter = new List<string>();
            List<string> normalWinter = new List<string>();
            List<string> severeWinter = new List<string>();
            List<string> mildMonsoon = new List<string>();
            List<string> normalMonsoon = new List<string>();
            List<string> severeMonsoon = new List<string>();

            foreach (Province p in mod.provinces)
            {
                switch (p.SpecialClimate)
                {
                    case SpecialClimate.Tropical:
                        tropical.Add(p.Number.ToString());
                        break;
                    case SpecialClimate.Arid:
                        arid.Add(p.Number.ToString());
                        break;
                    case SpecialClimate.Arctic:
                        arctic.Add(p.Number.ToString());
                        break;
                    default:
                        break;
                }
                switch (p.Winter)
                {
                    case Winter.Mild:
                        mildWinter.Add(p.Number.ToString());
                        break;
                    case Winter.Normal:
                        normalWinter.Add(p.Number.ToString());
                        break;
                    case Winter.Severe:
                        severeWinter.Add(p.Number.ToString());
                        break;
                    default:
                        break;
                }
                switch (p.Monsoon)
                {
                    case Monsoon.Mild:
                        mildMonsoon.Add(p.Number.ToString());
                        break;
                    case Monsoon.Normal:
                        normalMonsoon.Add(p.Number.ToString());
                        break;
                    case Monsoon.Severe:
                        severeMonsoon.Add(p.Number.ToString());
                        break;
                    default:
                        break;
                }
            }

            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>()
            {
                { "tropical", tropical },
                { "arid", arid },
                { "arctic", arctic },
                { "mild_winter", mildWinter },
                { "normal_winter", normalWinter },
                { "severe_winter", severeWinter },
                { "mild_monsoon", mildMonsoon },
                { "normal_monsoon", normalMonsoon },
                { "severe_monsoon", severeMonsoon },
            };

            string values = dict.ShortValueDictionaryToTXTString(8) + "\nequator_y_on_province_image = " + mod.equatorYOnProvinceImage;

            Trace.WriteLine(values);
            string path = appData.modPath + "\\map\\climate.txt";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\map\\climate.txt";
        }

        public string WriteProvinceDefault()
        {
            Dictionary<string, AttributeValueObject> dict = mod.mapDefault.Where(avo => !avo.attribute.Equals("canal_definition")).ToDictionary(x => x.attribute, x => x);

            dict.Add("sea_starts", new AttributeValueObject() { attribute = "sea_starts" });
            dict.Add("lakes", new AttributeValueObject() { attribute = "lakes" });
            dict["sea_starts"].values = new List<AttributeValueObject>();
            dict["lakes"].values = new List<AttributeValueObject>();

            foreach (Province p in mod.provinces)
            {
                switch (p.ProvinceType)
                {
                    case ProvinceType.Sea:
                        dict["sea_starts"].values.Add(new AttributeValueObject() { attribute = p.Number.ToString() });
                        break;
                    case ProvinceType.Lake:
                        dict["lakes"].values.Add(new AttributeValueObject() { attribute = p.Number.ToString() });
                        break;
                    default:
                        break;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(dict["width"]);
            sb.Append(dict["height"]);
            sb.Append("\n");
            sb.Append(dict["max_provinces"]);
            sb.Append(dict["sea_starts"]);
            sb.Append("\n");
            sb.Append(dict["only_used_for_random"]);
            sb.Append("\n");
            sb.Append(dict["lakes"]);

            Trace.WriteLine(sb);
            string path = appData.modPath + "\\map\\default.txt";
            //File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\map\\default.txt";
        }
        #endregion
    }
}
