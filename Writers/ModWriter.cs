using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Util;

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
        }

        // Countries

        /// <summary>
        /// Writes all country data to mod on disk
        /// </summary>
        public List<string> WriteCountries()
        {
            List<string> changed = new List<string>();
            changed.Add(WriteCountryTags());
            changed.AddRange(WriteCountryInfo());
            changed.Add(WriteCountryLocalisation());

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
            return new List<string> { "" };
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
    }
}
