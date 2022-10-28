using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using EU4ModUtil.Models.Data.History;

namespace EU4ModUtil.Models.Data.Common
{
    internal class Country : ChangeableObject
    {
        #region 00_countries.txt
        private int index;
        /// <summary>
        /// Index
        /// </summary>
        public int Index 
        {
            get { return index; }
            set 
            { 
                index = value;
            }
        }

        private string tag;
        /// <summary>
        /// Tag
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set
            {
                tag = value;
                NotifyPropertyChanged(nameof(Tag));
            }
        }

        private string path;
        /// <summary>
        /// Path
        /// </summary>
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyPropertyChanged(nameof(Path));
            }
        }
        #endregion

        #region Localization
        private string localizedName;
        /// <summary>
        /// Localized Name
        /// </summary>
        public string LocalizedName
        {
            get { return localizedName; }
            set
            {
                localizedName = value;
                NotifyPropertyChanged(nameof(LocalizedName));
            }
        }

        private string localizedAdjective;
        /// <summary>
        /// LocalizedAdejctive
        /// </summary>
        public string LocalizedAdjective
        {
            get { return localizedAdjective; }
            set
            {
                localizedAdjective = value;
                NotifyPropertyChanged(nameof(LocalizedAdjective));
            }
        }
        #endregion

        #region Country

        #region Color
        /// <summary>
        /// The country's color in SolidColorBrush form
        /// </summary>
        public SolidColorBrush BGColor
        {
            get
            {
                Color color = new Color();
                color.A = 255;
                color.R = (byte)R;
                color.G = (byte)G;
                color.B = (byte)B;
                return new SolidColorBrush(color);
            }
        }

        private int? r;
        /// <summary>
        /// R
        /// </summary>
        public int? R 
        {
            get { return r; }
            set
            {
                r = value;
                NotifyPropertyChanged(nameof(R));
                NotifyPropertyChanged(nameof(BGColor));
            }
        }

        private int? g;
        /// <summary>
        /// G
        /// </summary>
        public int? G 
        {
            get { return g; }
            set
            {
                g = value;
                NotifyPropertyChanged(nameof(G));
                NotifyPropertyChanged(nameof(BGColor));
            }
        }

        private int? b;
        /// <summary>
        /// B
        /// </summary>
        public int? B 
        {
            get { return b; }
            set
            {
                b = value;
                NotifyPropertyChanged(nameof(B));
                NotifyPropertyChanged(nameof(BGColor));
            }
        }

        /// <summary>
        /// Dictionary containing all revolutionary colors
        /// </summary>
        private Dictionary<int, (byte, byte, byte)> revolutionaryColors = new Dictionary<int, (byte, byte, byte)>
        {
            { 0, (255, 255, 255) },
            { 1, (20, 20, 20) },
            { 2, (117, 38, 143) },
            { 3, (113, 11, 43) },
            { 4, (97, 12, 12) },
            { 5, (175, 15, 15) },
            { 6, (188, 90, 27) },
            { 7, (64, 40, 22) },
            { 8, (244, 184, 12) },
            { 9, (17, 53, 13) },
            { 10, (46, 144, 55)},
            { 11, (18, 179, 113) },
            { 12, (50, 173, 192) },
            { 13, (30, 30, 128) },
            { 14, (116, 198, 240) },
            { 15, (0, 68, 131) },
            { 16, (200, 48, 40) }
        };

        private int? r1;
        public int? R1 
        { 
            get { return r1; }
            set
            {
                r1 = value;
                NotifyPropertyChanged(nameof(R1));
                NotifyPropertyChanged(nameof(BGColorR1));
            }
        }

        private int? r2;
        public int? R2 
        { 
            get { return r2; }
            set
            {
                r2 = value;
                NotifyPropertyChanged(nameof(R2));
                NotifyPropertyChanged(nameof(BGColorR2));
            }
        }

        private int? r3;
        public int? R3 
        { 
            get { return r3; }
            set
            {
                r3 = value;
                NotifyPropertyChanged(nameof(R3));
                NotifyPropertyChanged(nameof(BGColorR3));
            }
        }

        /// <summary>
        /// BGColorR1
        /// </summary>
        public SolidColorBrush BGColorR1
        {
            get
            {
                if (R1 == null || R1 < 0 || R1 > 16) return null;

                Color color = new Color();
                color.A = 255;
                color.R = revolutionaryColors[(int)R1].Item1;
                color.G = revolutionaryColors[(int)R1].Item2;
                color.B = revolutionaryColors[(int)R1].Item3;
                return new SolidColorBrush(color);
            }
        }
        /// <summary>
        /// BGColorR2
        /// </summary>
        public SolidColorBrush BGColorR2
        {
            get
            {
                if (R2 == null || R2 < 0 || R2 > 16) return null;

                Color color = new Color();
                color.A = 255;
                color.R = revolutionaryColors[(int)R2].Item1;
                color.G = revolutionaryColors[(int)R2].Item2;
                color.B = revolutionaryColors[(int)R2].Item3;
                return new SolidColorBrush(color);
            }
        }
        /// <summary>
        /// BGColorR3
        /// </summary>
        public SolidColorBrush BGColorR3
        {
            get
            {
                if (R3 == null || R3 < 0 || R3 > 16) return null;

                Color color = new Color();
                color.A = 255;
                color.R = revolutionaryColors[(int)R3].Item1;
                color.G = revolutionaryColors[(int)R3].Item2;
                color.B = revolutionaryColors[(int)R3].Item3;
                return new SolidColorBrush(color);
            }
        }
        #endregion
        private string graphicalCulture;
        /// <summary>
        /// Graphical Culture
        /// </summary>
        public string GraphicalCulture
        {
            get { return graphicalCulture; }
            set
            {
                graphicalCulture = value;
                NotifyPropertyChanged(nameof(GraphicalCulture));
            }
        }

        private int historicalScore;
        /// <summary>
        /// Historical Score
        /// </summary>
        public int HistoricalScore
        {
            get { return historicalScore; }
            set
            {
                historicalScore = value;
                NotifyPropertyChanged(nameof(HistoricalScore));
            }
        }

        private string historicalCouncil;
        /// <summary>
        /// Historical Council
        /// </summary>
        public string HistoricalCouncil
        {
            get { return historicalCouncil; }
            set
            {
                historicalCouncil = value;
                NotifyPropertyChanged(nameof(HistoricalCouncil));
            }
        }

        private string[] historicalIdeaGroups;
        /// <summary>
        /// Historical Idea Groups
        /// </summary>
        public string[] HistoricalIdeaGroups
        {
            get { return historicalIdeaGroups; }
            set
            {
                historicalIdeaGroups = value;
                NotifyPropertyChanged(nameof(HistoricalIdeaGroups));
            }
        }

        private string[] historicalUnits;
        /// <summary>
        /// Historical Units
        /// </summary>
        public string[] HistoricalUnits
        {
            get { return historicalUnits; }
            set
            {
                historicalUnits = value;
                NotifyPropertyChanged(nameof(HistoricalUnits));
            }
        }

        private string[] monarchNames;
        /// <summary>
        /// Monarch Names
        /// </summary>
        public string[] MonarchNames
        {
            get { return monarchNames; }
            set
            {
                monarchNames = value;
                NotifyPropertyChanged(nameof(MonarchNames));
            }
        }

        private string[] leaderNames;
        /// <summary>
        /// Leader Names
        /// </summary>
        public string[] LeaderNames
        {
            get { return leaderNames; }
            set
            {
                leaderNames = value;
                NotifyPropertyChanged(nameof(LeaderNames));
            }
        }

        private string[] shipNames;
        /// <summary>
        /// Ship Names
        /// </summary>
        public string[] ShipNames
        {
            get { return shipNames; }
            set
            {
                shipNames = value;
                NotifyPropertyChanged(nameof(ShipNames));
            }
        }

        private string[] armyNames;
        /// <summary>
        /// Army Names
        /// </summary>
        public string[] ArmyNames
        {
            get { return armyNames; }
            set
            {
                armyNames = value;
                NotifyPropertyChanged(nameof(ArmyNames));
            }
        }

        private string[] fleetNames;
        /// <summary>
        /// Fleet Names
        /// </summary>
        public string[] FleetNames
        {
            get { return fleetNames; }
            set
            {
                fleetNames = value;
                NotifyPropertyChanged(nameof(FleetNames));
            }
        }

        /// <summary>
        /// List of unsupported values kept so they don't get lost
        /// </summary>
        public List<AttributeValueObject> OtherCountryValues { get; set; }
        #endregion

        #region History
        private string culture;
        /// <summary>
        /// Culture
        /// </summary>
        public string Culture
        {
            get { return culture; }
            set
            {
                culture = value;
                NotifyPropertyChanged(nameof(Culture));
            }
        }

        private string religion;
        /// <summary>
        /// Religion
        /// </summary>
        public string Religion
        {
            get { return religion; }
            set
            {
                religion = value;
                NotifyPropertyChanged(nameof(Religion));
            }
        }

        private string government;
        /// <summary>
        /// Government
        /// </summary>
        public string Government
        {
            get { return government; }
            set
            {
                government = value;
                NotifyPropertyChanged(nameof(Government));
            }
        }

        private string unitType;
        /// <summary>
        /// Unit Type
        /// </summary>
        public string UnitType
        {
            get { return unitType; }
            set
            {
                unitType = value;
                NotifyPropertyChanged(nameof(UnitType));
            }
        }

        private string technologyGroup;
        /// <summary>
        /// Technology Group
        /// </summary>
        public string TechnologyGroup
        {
            get { return technologyGroup; }
            set
            {
                technologyGroup = value;
                NotifyPropertyChanged(nameof(TechnologyGroup));
            }
        }

        private int capital;
        /// <summary>
        /// Capital
        /// </summary>
        public int Capital
        {
            get { return capital; }
            set
            {
                capital = value;
                NotifyPropertyChanged(nameof(Capital));
            }
        }

        /// <summary>
        /// History
        /// </summary>
        public List<HistoryEntry> History { get; set; }
        #endregion

        #region Loading

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tagData"></param>
        /// <param name="index"></param>
        /// <exception cref="ArgumentNullException"></exception>
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
            Changed = false;
        }

        /// <summary>
        /// Country Data Setter
        /// </summary>
        /// <param name="country"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetCountryData(TXTFileObject country)
        {
            OtherCountryValues = new List<AttributeValueObject>();

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
                        int r;
                        int g;
                        int b;
                        if (int.TryParse(obj.values[0].attribute, out r))
                        {
                            this.r = r;
                        }
                        if (int.TryParse(obj.values[1].attribute, out g))
                        {
                            this.g = g;
                        }
                        if (int.TryParse(obj.values[2].attribute, out b))
                        {
                            this.b = b;
                        }
                        break;

                    case "revolutionary_colors":
                        int rr;
                        int rg;
                        int rb;
                        if (int.TryParse(obj.values[0].attribute, out rr))
                        {
                            this.r1 = rr;
                        }
                        if (int.TryParse(obj.values[1].attribute, out rg))
                        {
                            this.r2 = rg;
                        }
                        if (int.TryParse(obj.values[2].attribute, out rb))
                        {
                            this.r3 = rb;
                        }
                        break;

                    case "historical_score":
                        int hs;
                        if (int.TryParse(obj.value.attribute, out hs))
                        {
                            HistoricalScore = hs;
                        }
                        break;

                    case "graphical_culture":
                        GraphicalCulture = obj.value.attribute;
                        break;

                    default:
                        OtherCountryValues.Add(obj);
                        break;
                }
            }
            Changed = false;
        }

        /// <summary>
        /// History Data Setter
        /// </summary>
        /// <param name="history"></param>
        public void SetHistoryData(TXTFileObject history)
        {
            if (history == null) return;

            History = new List<HistoryEntry>();

            foreach (AttributeValueObject obj in history.values)
            {
                switch (obj.attribute)
                {
                    case "religion":
                        Religion = obj.value.attribute;
                        break;
                    case "primary_culture":
                        Culture = obj.value.attribute;
                        break;
                    case "government":
                        Government = obj.value.attribute;
                        break;
                    case "technology_group":
                        TechnologyGroup = obj.value.attribute;
                        break;
                    case "unit_type":
                        UnitType = obj.value.attribute;
                        break;
                    case "capital":
                        int cap;
                        if (int.TryParse(obj.value.attribute, out cap))
                        {
                            this.Capital = cap;
                        }
                        break;
                    default:
                        History.Add(new HistoryEntry(obj));
                        break;

                }
            }
            Changed = false;
        }

        /// <summary>
        /// Localisation Data Setter
        /// </summary>
        /// <param name="dict"></param>
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
            Changed = false;
        }

        #endregion

        #region Saving
        public string Get00_CountriesTXTEntry()
        {
            if (string.IsNullOrEmpty(Tag) || string.IsNullOrEmpty(Path)) return null;

            return Tag + " = \"" + Path + "\"";
        }

        public string GetCountryTXT()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("#Country Name: Please see filename.");
            if (GraphicalCulture != null) sb.Append("\n\ngraphical_culture = " + GraphicalCulture);
            if (HistoricalScore != 0) sb.Append("\n\nhistorical_score = " + HistoricalScore);
            if (R != null && G != null && B != null) sb.Append("\n\ncolor = { " + R + " " + G + " " + B + " }");
            if (R1 != null && R2 != null && R3 != null) sb.Append("\n\nrevolutionary_colors = { " + R1 + " " + R2 + " " + R3 + " }");
            if (OtherCountryValues != null)
            {
                sb.Append("\n\n");
                foreach (AttributeValueObject obj in OtherCountryValues)
                {
                    sb.Append(obj.ToString());
                }
            }

            return sb.ToString();
        }

        public string GetLocalisation()
        {
            StringBuilder sb = new StringBuilder();
            if (LocalizedName != null) sb.Append("\n " + Tag + ":0 \"" + LocalizedName + "\"");
            if (LocalizedAdjective != null) sb.Append("\n " + Tag + "_ADJ:0 \"" + LocalizedAdjective + "\"");

            return sb.ToString();
        }

        public string GetHistoryTXT()
        {
            foreach (HistoryEntry h in History)
            {
                Trace.WriteLine(h);
            }
            return "";
        }
        #endregion

        #region Adding
        public Country(int index)
        {
            Index = index;
            Tag = "NEW";
            Path = "countries/NEW - New Country.txt";
            
            R = 255;
            G = 255;
            B = 255;
        }
        #endregion
    }
}
