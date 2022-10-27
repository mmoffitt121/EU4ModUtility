using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
        public string GraphicalCulture { get; set; }
        public int HistoricalScore { get; set; }

        // -=-=-=-=-=-=-=-=-=-=-
        // Colors
        // -=-=-=-=-=-=-=-=-=-=-

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

        /// <summary>
        /// B
        /// </summary>
        private int? b;
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
                if (R2 == null || R1 < 0 || R1 > 16) return null;

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
                if (R3 == null || R1 < 0 || R1 > 16) return null;

                Color color = new Color();
                color.A = 255;
                color.R = revolutionaryColors[(int)R3].Item1;
                color.G = revolutionaryColors[(int)R3].Item2;
                color.B = revolutionaryColors[(int)R3].Item3;
                return new SolidColorBrush(color);
            }
        }



        //public Council historicalCouncil;
        //public int? HistoricalScore { get; set; }
        //public IdeaGroup[] historicalIdeaGroups;
        //public HistoricalUnit[] historicalUnits;
        //public MonarchName[] monarchNames;
        //public string[] LeaderNames;
        //public string[] ShipNames;
        //public string[] ArmyNames;
        //public string[] FleetNames;

        /// <summary>
        /// History
        /// </summary>
        public List<HistoryEntry> History { get; set; }
        /// <summary>
        /// Culture
        /// </summary>
        public string Culture { get; set; }
        /// <summary>
        /// Religion
        /// </summary>
        public string Religion { get; set; }
        /// <summary>
        /// Government
        /// </summary>
        public string Government { get; set; }

        /// <summary>
        /// List of unsupported values kept so they don't get lost
        /// </summary>
        public List<AttributeValueObject> Other { get; set; }

        public enum PowerPointType
        {
            ADM,
            DIP,
            MIL
        }

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
        }

        /// <summary>
        /// Country Data Setter
        /// </summary>
        /// <param name="country"></param>
        /// <exception cref="ArgumentNullException"></exception>
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
                        int r;
                        int g;
                        int b;
                        if (int.TryParse(obj.values[0].attribute, out r))
                        {
                            R = r;
                        }
                        if (int.TryParse(obj.values[1].attribute, out g))
                        {
                            G = g;
                        }
                        if (int.TryParse(obj.values[2].attribute, out b))
                        {
                            B = b;
                        }
                        break;

                    case "revolutionary_colors":
                        int rr;
                        int rg;
                        int rb;
                        if (int.TryParse(obj.values[0].attribute, out rr))
                        {
                            R1 = rr;
                        }
                        if (int.TryParse(obj.values[1].attribute, out rg))
                        {
                            R2 = rg;
                        }
                        if (int.TryParse(obj.values[2].attribute, out rb))
                        {
                            R3 = rb;
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
                        Other.Add(obj);
                        break;
                }
            }
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
                    default:
                        History.Add(new HistoryEntry(obj));
                        break;

                }
            }
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
        }
    }
}
