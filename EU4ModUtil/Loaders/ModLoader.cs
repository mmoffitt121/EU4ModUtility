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
            LoadCountries();
        }

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

        internal void LoadCountries()
        {
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

            if (File.Exists(appData.modPath + "\\localisation\\countries_l_english.yml"))
            {
                Dictionary<string, string> dict = YMLParser.ParseDictionary(appData.modPath + "\\localisation\\countries_l_english.yml");

                foreach (Country c in mod.countries)
                {
                    c.SetLocalizationData(dict);
                }
            }
        }

        public ModLoader(Mod mod, AppData appData)
        {
            this.mod = mod;
            this.appData = appData;
        }
    }
}
