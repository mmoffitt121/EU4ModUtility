using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.IO;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Parsers;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Util;

namespace EU4ModUtil.Loaders
{
    internal class ModLoader
    {
        private Mod mod;
        private AppData appData;

        internal void LoadMod(string path)
        {
            appData.modPath = path;
            LoadMod();
        }

        internal void LoadMod()
        {
            LoadDescriptor();
            LoadImage();
            LoadMapImage();
            LoadCultures();
            LoadCountries();
            LoadProvinces();
            LoadMapRegions();
        }

        #region Mod Info

        internal void LoadDescriptor()
        {
            TXTFileObject desc = TXTParser.Parse(appData.modPath + "/descriptor.mod");
            mod.descriptor = new Descriptor(desc);
        }

        internal void LoadImage()
        {
            if (File.Exists(appData.modPath + "/" + mod.descriptor.picture))
            {
                mod.descriptor.bitmap = new BitmapImage();
                mod.descriptor.bitmap.BeginInit();
                mod.descriptor.bitmap.UriSource = new Uri(appData.modPath + "/" + mod.descriptor.picture);
                mod.descriptor.bitmap.EndInit();
            }
            else
            {
                mod.descriptor.bitmap = null;
            }
        }

        internal void LoadMapImage()
        {
            if (File.Exists(appData.modPath + "/map/provinces.bmp"))
            {
                mod.provincesImage = new BitmapImage();
                mod.provincesImage.BeginInit();
                mod.provincesImage.UriSource = new Uri(appData.modPath + "/map/provinces.bmp");
                mod.provincesImage.EndInit();

                
                for (int i = 0; i < mod.provincesImage.Width; i++)
                {
                    for (int j = 0; j < mod.provincesImage.Height; j++)
                    {

                    }
                }
            }
            else
            {
                mod.provincesImage = null;
            }
        }

        #endregion

        #region Countries

        internal void LoadCountries()
        {
            // Tag & Country file
            if (File.Exists(appData.modPath + "\\common\\country_tags\\00_countries.txt"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\common\\country_tags\\00_countries.txt");
                mod.countries = new List<Country>();
                for (int i = 0; i < obj.values.Length; i++)
                {
                    Country country = new Country(obj.values[i], i);
                    mod.countries.Add(country);
                }

                foreach (Country c in mod.countries)
                {
                    TXTFileObject cObj = TXTParser.Parse(appData.modPath + "\\common\\" + c.Path.Replace("/", "\\"));
                    c.SetCountryData(cObj);
                }
            }

            // Localisation
            if (File.Exists(appData.modPath + "\\localisation\\countries_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\countries_l_english.yml");

                foreach (Country c in mod.countries)
                {
                    c.SetLocalisationData(dict);
                }
            }

            // History
            DirectoryInfo dir = new DirectoryInfo(appData.modPath + "\\history\\countries");
            foreach (Country c in mod.countries)
            {
                FileInfo fi = dir.GetFiles(c.Tag + "*.*")?.FirstOrDefault();
                if (fi != null)
                {
                    TXTFileObject hist = TXTParser.Parse(fi.ToString());
                    c.SetHistoryData(hist);
                }
            }
        }

        #endregion

        #region Provinces
        internal void LoadProvinces()
        {
            // Definition
            if (File.Exists(appData.modPath + "\\map\\definition.csv"))
            {
                string[][] csv = CSVParser.Parse(appData.modPath + "\\map\\definition.csv");
                mod.provinces = new List<Province>();
                for (int i = 0; i < csv.Length; i++)
                {
                    Province province = new Province(csv[i]);
                    if (province.valid)
                    {
                        mod.provinces.Add(province);
                    }
                }
            }

            // Localisation
            if (File.Exists(appData.modPath + "\\localisation\\prov_names_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\prov_names_l_english.yml");

                foreach (Province p in mod.provinces)
                {
                    p.SetLocalisedName(dict);
                }
            }
            if (File.Exists(appData.modPath + "\\localisation\\prov_names_adj_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\prov_names_adj_l_english.yml");

                foreach (Province p in mod.provinces)
                {
                    p.SetLocalisedAdjective(dict);
                }
            }

            // History - !!! NEEDS OPTIMIZING BAD !!!
            DirectoryInfo dir = new DirectoryInfo(appData.modPath + "\\history\\provinces");
            foreach (Province p in mod.provinces)
            {
                FileInfo fi = dir.GetFiles(p.Number + "*.*")?.FirstOrDefault();

                if (fi == null) continue;
                string filePath = appData.modPath + "\\history\\provinces\\" + p.Number.ToString();
                if (!(fi.ToString().StartsWith(filePath + " ") || fi.ToString().StartsWith(filePath + "-"))) continue;

                TXTFileObject hist = TXTParser.Parse(fi.ToString());
                p.SetHistoryData(hist);
            }
            
            // Dictionary for quicker assignment
            Dictionary<int, Province> pDict = mod.provinces.ToDictionary(p => p.Number, p => p);
            
            // Area
            if (File.Exists(appData.modPath + "\\map\\area.txt"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\map\\area.txt");
                mod.areas = new List<string>();
                foreach (AttributeValueObject area in obj.values)
                {
                    foreach (AttributeValueObject provNum in area.values)
                    {
                        if (!int.TryParse(provNum.attribute, out int key))
                        {
                            continue;
                        }
                        
                        if (pDict.TryGetValue(key, out Province province))
                        {
                            province.Area = area.attribute;
                        }
                    }
                    mod.areas.Add(area.Attribute);
                }
            }

            // Continent
            if (File.Exists(appData.modPath + "\\map\\continent.txt"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\map\\continent.txt");
                mod.continents = new List<string>();
                foreach (AttributeValueObject continent in obj.values)
                {
                    foreach (AttributeValueObject provNum in continent.values)
                    {
                        if (!int.TryParse(provNum.attribute, out int key))
                        {
                            continue;
                        }

                        if (pDict.TryGetValue(key, out Province province))
                        {
                            province.Continent = continent.attribute;
                        }
                    }
                    mod.continents.Add(continent.attribute);
                }
            }

            // Climate
            if (File.Exists(appData.modPath + "\\map\\climate.txt"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\map\\climate.txt");
                foreach (AttributeValueObject climate in obj.values)
                {
                    switch (climate.attribute)
                    {
                        case "tropical":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.SpecialClimate = SpecialClimate.Tropical;
                                }
                            }
                            break;
                        case "arid":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.SpecialClimate = SpecialClimate.Arid;
                                }
                            }
                            break;
                        case "arctic":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.SpecialClimate = SpecialClimate.Arctic;
                                }
                            }
                            break;
                        case "mild_winter":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Winter = Winter.Mild;
                                }
                            }
                            break;
                        case "normal_winter":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Winter = Winter.Normal;
                                }
                            }
                            break;
                        case "severe_winter":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Winter = Winter.Severe;
                                }
                            }
                            break;
                        case "impassable":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Impassable = true;
                                }
                            }
                            break;
                        case "mild_monsoon":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Monsoon = Monsoon.Mild;
                                }
                            }
                            break;
                        case "normal_monsoon":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Monsoon = Monsoon.Normal;
                                }
                            }
                            break;
                        case "severe_monsoon":
                            foreach (AttributeValueObject attr in climate.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.Monsoon = Monsoon.Severe;
                                }
                            }
                            break;
                        case "equator_y_on_province_image":
                            if (int.TryParse(climate.value.attribute, out int eq))
                            {
                                mod.equatorYOnProvinceImage = eq;
                            }
                            break;
                        default:
                            break;
                    }
                    
                }
            }

            // Default
            if (File.Exists(appData.modPath + "\\map\\default.map"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\map\\default.map");
                mod.mapDefault = new List<AttributeValueObject>();
                foreach (AttributeValueObject def in obj.values)
                {
                    switch (def.attribute)
                    {
                        case "sea_starts":
                            foreach (AttributeValueObject attr in def.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.ProvinceType = ProvinceType.Sea;
                                }
                            }
                            break;
                        case "lakes":
                            foreach (AttributeValueObject attr in def.values)
                            {
                                if (!int.TryParse(attr.attribute, out int key))
                                {
                                    continue;
                                }

                                if (pDict.TryGetValue(key, out Province province))
                                {
                                    province.ProvinceType = ProvinceType.Lake;
                                }
                            }
                            break;
                        default:
                            mod.mapDefault.Add(def);
                            break;
                    }

                }
            }

            foreach (Province p in mod.provinces)
            {
                p.Changed = false;
            }
        }
        #endregion

        #region Cultures
        public void LoadCultures()
        {
            if (File.Exists(appData.modPath + "\\common\\cultures\\00_cultures.txt"))
            {
                TXTFileObject obj = TXTParser.Parse(appData.modPath + "\\common\\cultures\\00_cultures.txt");
                mod.cultureGroups = new List<CultureGroup>();
                for (int i = 0; i < obj.values.Length; i++)
                {
                    CultureGroup group = new CultureGroup(obj.values[i]);
                    mod.cultureGroups.Add(group);
                }
            }
            else
            {
                return;
            }

            if (File.Exists(appData.modPath + "\\localisation\\z_culture_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\z_culture_l_english.yml");

                foreach (CultureGroup g in mod.cultureGroups)
                {
                    g.SetLocalisationData(dict);
                }
            }
        }
        #endregion

        #region MapRegions

        /// <summary>
        /// Must be loaded after Provinces
        /// </summary>
        public void LoadMapRegions()
        {
            if (File.Exists(appData.modPath + "\\map\\superregion.txt") 
                && File.Exists(appData.modPath + "\\map\\region.txt") 
                && File.Exists(appData.modPath + "\\map\\area.txt"))
            {
                TXTFileObject srObj = TXTParser.Parse(appData.modPath + "\\map\\superregion.txt");
                TXTFileObject rObj = TXTParser.Parse(appData.modPath + "\\map\\region.txt");
                TXTFileObject aObj = TXTParser.Parse(appData.modPath + "\\map\\area.txt");

                mod.superRegions = new List<SuperRegion>();

                /*for (int i = 0; i < obj.values.Length; i++)
                {
                    CultureGroup group = new CultureGroup(obj.values[i]);
                    mod.cultureGroups.Add(group);
                }*/

                Trace.WriteLine(srObj);
                Trace.WriteLine(rObj);
                Trace.WriteLine(aObj);
            }
            else
            {
                return;
            }

            if (File.Exists(appData.modPath + "\\localisation\\z_areas_regions_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\z_areas_regions_l_english.yml");

                foreach (CultureGroup g in mod.cultureGroups)
                {
                    g.SetLocalisationData(dict);
                }
            }
        }
        #endregion

        public ModLoader(Mod mod, AppData appData)
        {
            this.mod = mod;
            this.appData = appData;
        }
    }
}
