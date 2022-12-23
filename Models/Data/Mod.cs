using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows.Media.Imaging;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.Generic;
using EU4ModUtil.Models.Data.Map;

namespace EU4ModUtil.Models.Data
{
    internal class Mod
    {
        internal Descriptor descriptor;
        internal List<Country> countries;
        internal List<Province> provinces;
        internal List<CultureGroup> cultureGroups;
        internal List<Religion> religions;
        internal List<AttributeValueObject> mapDefault;
        internal int equatorYOnProvinceImage;
        internal List<SuperRegion> superRegions;
        internal List<string> areas;
        internal List<string> continents;

        // Map Images
        public BitmapImage provincesImage;
        public BitmapImage heightMapImage;
        public BitmapImage riversImage;
        public BitmapImage terrainImage;
        public BitmapImage treesImage;

        // Localisation
        internal Dictionary<string, string> ZModLEnglish { get; set; }

        internal List<Culture> Cultures
        {
            get
            {
                List<Culture> result = new List<Culture>();
                foreach (CultureGroup group in cultureGroups)
                {
                    foreach (Culture culture in group.Cultures)
                    {
                        result.Add(culture);
                    }
                }
                return result;
            }
        }
    }
}
