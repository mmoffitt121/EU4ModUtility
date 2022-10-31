using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.History;

namespace EU4ModUtil.Models.Data.Map
{
    internal class Province : ChangeableObject
    {
        #region Definition
        public int Number { get; set; }
        public string Name { get; set; }
        #region Color
        /// <summary>
        /// The province's color in SolidColorBrush form
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
        /// Gets the province color as a hexidecimal string
        /// </summary>
        public string ColorHex
        {
            get
            {
                string r = (R == null ? "00" : this.r?.ToString("X2"));
                string g = (G == null ? "00" : this.g?.ToString("X2"));
                string b = (B == null ? "00" : this.b?.ToString("X2"));

                return r + g + b;
            }
        }

        #endregion
        #endregion

        #region Map
        public string Area { get; set; }
        public string Continent { get; set; }
        public bool Impassable { get; set; }
        public SpecialClimate SpecialClimate { get; set; }
        public Winter Winter { get; set; }
        public Monsoon Monsoon { get; set; }
        public ProvinceType ProvinceType { get; set; }
        #endregion

        #region Localization
        public string LocalizedName { get; set; }
        public string LocalizedAdjective { get; set; }
        #endregion

        #region History
        public string Owner { get; set; }
        public string Controller { get; set; }
        public string AddCore { get; set; }
        public string Culture { get; set; }
        public string Religion { get; set; }
        public string TradeGoods { get; set; }
        public string BaseTax { get; set; }
        public string BaseProduction { get; set; }
        public string BaseManpower { get; set; }
        public bool IsCity { get; set; }
        public bool HRE { get; set; }
        public string DiscoveredBy { get; set; }
        public string Unrest { get; set; }
        public string Capital { get; set; }

        public List<HistoryEntry> History { get; set; }
        #endregion

        #region Loading
        public bool valid = false;
        public Province(string[] data)
        {
            Define(data);
            Changed = false;
        }

        public void Define(string[] definition)
        {
            if (definition == null || definition.Length < 6)
            {
                valid = false;
                Changed = false;
                return;
            }

            if (int.TryParse(definition[0], out int number) &&
                int.TryParse(definition[1], out int r) &&
                int.TryParse(definition[2], out int g) &&
                int.TryParse(definition[3], out int b))
            {
                Number = number;
                R = r;
                G = g;
                B = b;
                Name = definition[4];

                valid = true;
                Changed = false;
                return;
            }
            else
            {
                valid = false;
                Changed = false;
                return;
            }
        }

        public void SetLocalisedName(Dictionary<string, string> dict)
        {
            if (dict.ContainsKey("PROV" + Number))
            {
                LocalizedName = dict["PROV" + Number];
            }
            Changed = false;
        }

        public void SetLocalisedAdjective(Dictionary<string, string> dict)
        {
            if (dict.ContainsKey("PROV_ADJ" + Number))
            {
                LocalizedAdjective = dict["PROV_ADJ" + Number];
            }
            Changed = false;
        }

        public void SetHistoryData(TXTFileObject history)
        {
            if (history == null || history.values == null) return;

            History = new List<HistoryEntry>();

            foreach (AttributeValueObject obj in history.values)
            {
                switch (obj.attribute)
                {
                    case "owner":
                        Owner = obj.value.attribute;
                        break;
                    case "controller":
                        Controller = obj.value.attribute;
                        break;
                    case "add_core":
                        AddAddCore(obj.value.attribute);
                        break;
                    case "culture":
                        Culture = obj.value.attribute;
                        break;
                    case "religion":
                        Religion = obj.value.attribute;
                        break;
                    case "trade_goods":
                        TradeGoods = obj.value.attribute;
                        break;
                    case "base_tax":
                        BaseTax = obj.value.attribute;
                        break;
                    case "base_production":
                        BaseProduction = obj.value.attribute;
                        break;
                    case "base_manpower":
                        BaseManpower = obj.value.attribute;
                        break;
                    case "is_city":
                        IsCity = obj.value.attribute.Equals("yes");
                        break;
                    case "hre":
                        HRE = obj.value.attribute.Equals("yes");
                        break;
                    case "discovered_by":
                        AddDiscoveredBy(obj.value.attribute);
                        break;
                    case "unrest":
                        Unrest = obj.value.attribute;
                        break;
                    case "capital":
                        Capital = obj.value.attribute;
                        break;
                    default:
                        History.Add(new HistoryEntry(obj));
                        break;

                }
            }
            Changed = false;
        }

        public void AddAddCore(string addCore)
        {
            if (string.IsNullOrEmpty(addCore)) return;

            if (string.IsNullOrEmpty(AddCore))
            {
                AddCore = new string(addCore);
            }
            else
            {
                AddCore += ", " + addCore;
            }
        }

        public void AddDiscoveredBy(string discoveredBy)
        {
            if (string.IsNullOrEmpty(discoveredBy)) return;

            if (string.IsNullOrEmpty(DiscoveredBy))
            {
                DiscoveredBy = new string(discoveredBy);
            }
            else
            {
                DiscoveredBy += ", " + discoveredBy;
            }
        }
        #endregion

        #region Saving
        public string GetDefinition()
        {
            return Number + ";" + R + ";" + G + ";" + B + ";" + Name + ";x";
        }

        public string GetLocalisedName()
        {
            if (!string.IsNullOrEmpty(LocalizedName)) return "\n PROV" + Number + ":0 \"" + LocalizedName + "\"";

            return "";
        }
        public string GetLocalisedAdjective()
        {
            if (!string.IsNullOrEmpty(LocalizedAdjective)) return "\n PROV_ADJ" + Number + ":0 \"" + LocalizedAdjective + "\"";

            return "";
        }

        #endregion
    }
}
