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
        public int index { get; set; }
        public string tag { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        // Localization
        public string localizedName { get; set; }
        public string lcoalizedAdjective { get; set; }

        // Country
        //public GraphicalCulture graphicalCulture;
        public Color color { get; set; }
        public Color revoluationaryColors { get; set; }
        //public Council historicalCouncil;
        public int? historicalScore { get; set; }
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

        public Country(AttributeValueObject tagData, int index)
        {
            if (tagData == null || tagData.values == null || tagData.values.Count < 1)
            {
                return;
                throw new ArgumentNullException(nameof(tagData));
            }

            this.index = index;

            tag = tagData.attribute;
            path = tagData.values[0].attribute;
        }

        public void SetCountryData(AttributeValueObject country)
        {

        }

        public void SetHistoryData()
        {

        }
    }
}
