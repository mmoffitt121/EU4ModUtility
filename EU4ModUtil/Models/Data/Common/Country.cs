using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EU4ModUtil.Models.Data.History;

namespace EU4ModUtil.Models.Data.Common
{
    internal class Country : ChangeableObject
    {
        // 00_countries.txt
        public int Index { get; set; }
        public string Tag { get; set; }
        public string Path { get; set; }

        // Localization
        public string LocalizedName { get; set; }
        public string LocalizedAdjective { get; set; }

        // Country
        //public GraphicalCulture graphicalCulture;
        public Color Color { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public Color RevoluationaryColors { get; set; }
        //public Council historicalCouncil;
        public int? HistoricalScore { get; set; }
        //public IdeaGroup[] historicalIdeaGroups;
        //public HistoricalUnit[] historicalUnits;
        //public MonarchName[] monarchNames;
        public string[] LeaderNames;
        public string[] ShipNames;
        public string[] ArmyNames;
        public string[] FleetNames;

        // History
        public HistoryEntry[] History;
        public Culture culture;
        public string Culture 
        {
            get
            {
                return culture?.Name;
            }
            set
            {

            }
        }

        public Religion religion;
        public string Religion 
        { 
            get
            {
                return religion?.ToString();
            }
            set
            {

            }
        }

        // Unsupported Values
        public List<AttributeValueObject> Other { get; set; }

        public enum PowerPointType
        {
            ADM,
            DIP,
            MIL
        }

        public Country(AttributeValueObject tagData, int index)
        {
            if (tagData == null || tagData.values == null || tagData.values.Count < 1)
            {
                return;
                throw new ArgumentNullException(nameof(tagData));
            }

            this.Index = index;

            Tag = tagData.attribute;
            Path = tagData.values[0].attribute;
        }

        public void SetCountryData(TXTFileObject country)
        {
            Other = new List<AttributeValueObject>();

            if (country == null || country.values == null || country.values.Count() < 1)
            {
                return;
                throw new ArgumentNullException(nameof(country));
            }

            foreach (AttributeValueObject obj in country.values)
            {
                switch (obj.attribute)
                {
                    case "color":
                    case "revolutionary_colors":
                        break;
                    case "historical_score":
                        break;
                    default:
                        Other.Add(obj);
                        break;
                }
            }
        }

        public void SetHistoryData()
        {

        }

        public void SetLocalisationData(Dictionary<string, string> dict)
        {
            if (dict.ContainsKey(Tag))
            {
                LocalizedName = dict[Tag];
            }

            if (dict.ContainsKey(Tag + "_ADJ"))
            {
                LocalizedAdjective = dict[Tag + "_ADJ"];
            }
        }
    }
}
