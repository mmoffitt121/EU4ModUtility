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
            changed.Add(WriteProvinceDefinitions());
            changed.Add(WriteProvinceNamesLocalisation());
            changed.Add(WriteProvinceAdjectivesLocalisation());
            changed.AddRange(WriteProvinceHistory());
            changed.Add(WriteProvinceArea());
            changed.Add(WriteProvinceContinent());
            changed.Add(WriteProvinceClimate());
            changed.Add(WriteProvinceDefault());

            SetProvincesUnchanged();

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
            File.WriteAllLines(path, lines);

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
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

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
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return "\\localisation\\prov_names_adj_l_english.yml";
        }

        public List<string> WriteProvinceHistory()
        {
            List<string> changed = new List<string>();
            foreach (Province p in mod.provinces)
            {
                if (p.Changed)
                {
                    Trace.WriteLine(p.Number + " " + p.Changed);
                    DirectoryInfo dir = new DirectoryInfo(appData.modPath + "\\history\\provinces");
                    FileInfo fi = dir.GetFiles(p.Number + " - *.*")?.FirstOrDefault();
                    string path;
                    if (fi == null)
                    {
                        path = appData.modPath + "\\history\\provinces\\" + p.Number + " - " + p.LocalizedName + ".txt";
                    }
                    else
                    {
                        path = fi.ToString();
                    }

                    string txt = p.GetHistoryTXT();
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

        public string WriteProvinceArea()
        {
            Dictionary<string, List<int>> dict = mod.areas.ToDictionary(a => a, a => new List<int>());
            foreach (Province p in mod.provinces)
            {
                if (p.Area == null || !dict.ContainsKey(p.Area)) continue;

                dict[p.Area].Add(p.Number);
            }

            string path = appData.modPath + "\\map\\area.txt";
            File.WriteAllText(path, dict.ShortValueDictionaryToTXTString(6));

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

            string path = appData.modPath + "\\map\\continent.txt";
            File.WriteAllText(path, dict.ShortValueDictionaryToTXTString(12));

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
            List<string> impassable = new List<string>();

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
                if (p.Impassable)
                {
                    impassable.Add(p.Number.ToString());
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
                { "impassable", impassable},
                { "mild_monsoon", mildMonsoon },
                { "normal_monsoon", normalMonsoon },
                { "severe_monsoon", severeMonsoon }
            };

            string values = dict.ShortValueDictionaryToTXTString(8) + "\nequator_y_on_province_image = " + mod.equatorYOnProvinceImage;

            string path = appData.modPath + "\\map\\climate.txt";
            File.WriteAllText(path, values);

            return "\\map\\climate.txt";
        }

        public string WriteProvinceDefault()
        {
            Dictionary<string, AttributeValueObject> dict = mod.mapDefault.Where(avo => !avo.attribute.Equals("canal_definition")).ToDictionary(x => x.attribute, x => x);
            List<AttributeValueObject> canals = mod.mapDefault.Where(avo => avo.attribute.Equals("canal_definition")).ToList();


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
            sb.Append("force_coastal = {   }\n");
            sb.Append(dict["definitions"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["provinces"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["positions"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["terrain"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["rivers"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["terrain_definition"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["heightmap"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["tree_definition"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["continent"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["adjacencies"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["climate"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["region"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["superregion"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["area"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["provincegroup"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["ambient_object"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["seasons"].ForceSingleValueQuoted());
            sb.Append("\n");
            sb.Append(dict["trade_winds"].ForceSingleValueQuoted());
            sb.Append("\n");

            foreach (AttributeValueObject canalObj in canals)
            {
                sb.Append(canalObj);
            }

            sb.Append(dict["tree"]);

            string path = appData.modPath + "\\map\\default.map";
            File.WriteAllText(path, sb.ToString());

            return "\\map\\default.map";
        }

        /// <summary>
        /// Sets value as unchanged
        /// </summary>
        public void SetProvincesUnchanged()
        {
            foreach (Province p in mod.provinces)
            {
                p.Changed = false;
            }
        }
        #endregion

        #region Cultures
        public bool WriteCultures()
        {
            return WriteCulturesTXT() && WriteCultureLocalization();
        }

        public bool WriteCulturesTXT()
        {
            StringBuilder sb = new StringBuilder();
            foreach (CultureGroup c in mod.cultureGroups)
            {
                sb.Append(c.ToString());
            }
            string path = appData.modPath + "\\common\\cultures\\00_cultures.txt";
            //Trace.WriteLine("CULTURES: " + sb.ToString());
            File.WriteAllText(path, sb.ToString());

            return true;
        }

        public bool WriteCultureLocalization()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("l_english:\n");
            foreach (CultureGroup c in mod.cultureGroups)
            {
                if (c.LocalizedName != null && !c.LocalizedName.Equals(""))
                {
                    sb.Append(" " + c.Name + ":0 " + "\"" + c.LocalizedName + "\"\n");
                }
                foreach(Culture cl in c.Cultures)
                {
                    if (cl.LocalizedName != null && !cl.LocalizedName.Equals(""))
                    {
                        sb.Append(" " + cl.Name + ":0 " + "\"" + cl.LocalizedName + "\"\n");
                    }
                }
            }
            string path = appData.modPath + "\\localisation\\z_culture_l_english.yml";
            //Trace.WriteLine(sb.ToString());
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            return true;
        }
        #endregion
    }
}
