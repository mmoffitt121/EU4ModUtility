using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EU4ModUtil.Models.Data.History;

namespace EU4ModUtil.Models.Data.Common
{
    internal class Country
    {
        // 00_countries.txt
        public int index { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        // Localization
        public string localizedName;
        public string lcoalizedAdjective;

        // Country
        //public GraphicalCulture graphicalCulture;
        public Color color;
        public Color revoluationaryColors;
        //public Council historicalCouncil;
        public int? historicalScore;
        //public IdeaGroup[] historicalIdeaGroups;
        //public HistoricalUnit[] historicalUnits;
        //public MonarchName[] monarchNames;
        public string[] leaderNames;
        public string[] shipNames;
        public string[] armyNames;
        public string[] fleetNames;

        // History
        public HistoryEntry[] history;

        public enum PowerPointType
        {
            ADM,
            DIP,
            MIL
        }

        public Country(AttributeValueObject rawData, int index)
        {
            if (rawData == null || rawData.values == null)
            {
                return;
                throw new ArgumentNullException(nameof(rawData));
            }

            this.index = index;

            /*replacePaths = new List<string>();
            List<AttributeValueObject> values = new List<AttributeValueObject>();

            foreach (AttributeValueObject item in rawData.values)
            {
                if (item.attribute == null)
                {
                    continue;
                }
                switch (item.attribute.ToLower())
                {
                    case "version":
                        version = item.value.attribute;
                        break;
                    case "replace_path":
                        replacePaths.Add(item.value.attribute);
                        break;
                    case "tags":
                        tags = LoadTags(item);
                        break;
                    case "name":
                        name = item.value.attribute;
                        break;
                    case "supported_version":
                        supportedVersion = item.value.attribute;
                        break;
                    case "picture":
                        picture = item.value.attribute;
                        break;
                    default:
                        values.Add(item);
                        break;
                }
            }

            this.values = values.ToArray();*/
        }
    }
}
