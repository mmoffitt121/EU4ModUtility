using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data;

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

        }

        // Countries

        /// <summary>
        /// Saves to common/countries
        /// </summary>
        public void WriteCountries()
        {

        }

        /// <summary>
        /// Saves to common/country_tags/00_countries.txt
        /// </summary>
        public void WriteCountryTags()
        {

        }

        /// <summary>
        /// Saves to localisation/
        /// </summary>
        public void WriteCountryLocalisation()
        {

        }

        public void WriteCountryHistory()
        {

        }
    }
}
