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
    public class Province : ChangeableObject
    {
        #region Definition Members
        private int number;
        /// <summary>
        /// Number
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        private string name;
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
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
            set
            {

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
            set
            {

            }
        }

        #endregion
        #endregion

        #region Map Members
        private string area;
        /// <summary>
        /// Area
        /// </summary>
        public string Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }

        private string continent;
        /// <summary>
        /// Continent
        /// </summary>
        public string Continent
        {
            get
            {
                return continent;
            }
            set
            {
                continent = value;
            }
        }

        private bool impassable;
        /// <summary>
        /// Impassable
        /// </summary>
        public bool Impassable
        {
            get
            {
                return impassable;
            }
            set
            {
                impassable = value;
            }
        }

        private SpecialClimate specialClimate;
        /// <summary>
        /// Special Climate
        /// </summary>
        public SpecialClimate SpecialClimate
        {
            get
            {
                return specialClimate;
            }
            set
            {
                specialClimate = value;
            }
        }

        private Winter winter;
        /// <summary>
        /// Winter
        /// </summary>
        public Winter Winter
        {
            get
            {
                return winter;
            }
            set
            {
                winter = value;
            }
        }

        private Monsoon monsoon;
        /// <summary>
        /// Monsoon
        /// </summary>
        public Monsoon Monsoon
        {
            get
            {
                return monsoon;
            }
            set
            {
                monsoon = value;
            }
        }

        private ProvinceType provinceType;
        /// <summary>
        /// ProvinceType
        /// </summary>
        public ProvinceType ProvinceType
        {
            get
            {
                return provinceType;
            }
            set
            {
                provinceType = value;
            }
        }
        #endregion

        #region Localization Members
        private string localizedName;
        /// <summary>
        /// Localized Name
        /// </summary>
        public string LocalizedName
        {
            get
            {
                return localizedName;
            }
            set
            {
                localizedName = value;
            }
        }

        private string localizedAdjective;
        /// <summary>
        /// Localized Adjective
        /// </summary>
        public string LocalizedAdjective
        {
            get
            {
                return localizedAdjective;
            }
            set
            {
                localizedAdjective = value;
            }
        }
        #endregion

        #region History Members
        private string owner;
        /// <summary>
        /// Owner
        /// </summary>
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
                NotifyPropertyChanged(nameof(Owner));
            }
        }
        private string controller;
        /// <summary>
        /// Controller
        /// </summary>
        public string Controller
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
                NotifyPropertyChanged(nameof(Controller));
            }
        }
        private string addCore;
        /// <summary>
        /// Add Core
        /// </summary>
        public string AddCore
        {
            get
            {
                return addCore;
            }
            set
            {
                addCore = value;
                NotifyPropertyChanged(nameof(AddCore));
            }
        }
        private string culture;
        /// <summary>
        /// Culture
        /// </summary>
        public string Culture
        {
            get
            {
                return culture;
            }
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
            get
            {
                return religion;
            }
            set
            {
                religion = value;
                NotifyPropertyChanged(nameof(Religion));
            }
        }
        private string tradeGoods;
        /// <summary>
        /// Trade Goods
        /// </summary>
        public string TradeGoods
        {
            get
            {
                return tradeGoods;
            }
            set
            {
                tradeGoods = value;
                NotifyPropertyChanged(nameof(TradeGoods));
            }
        }
        private string baseTax;
        /// <summary>
        /// Base Tax
        /// </summary>
        public string BaseTax
        {
            get
            {
                return baseTax;
            }
            set
            {
                baseTax = value;
                NotifyPropertyChanged(nameof(BaseTax));
            }
        }
        private string baseProduction;
        /// <summary>
        /// Base Production
        /// </summary>
        public string BaseProduction
        {
            get
            {
                return baseProduction;
            }
            set
            {
                baseProduction = value;
                NotifyPropertyChanged(nameof(BaseProduction));
            }
        }
        private string baseManpower;
        /// <summary>
        /// Base Manpower
        /// </summary>
        public string BaseManpower
        {
            get
            {
                return baseManpower;
            }
            set
            {
                baseManpower = value;
                NotifyPropertyChanged(nameof(BaseManpower));
            }
        }
        private bool isCity;
        /// <summary>
        /// Is City
        /// </summary>
        public bool IsCity
        {
            get
            {
                return isCity;
            }
            set
            {
                isCity = value;
                NotifyPropertyChanged(nameof(IsCity));
            }
        }
        private bool hre;
        /// <summary>
        /// Is HRE
        /// </summary>
        public bool HRE
        {
            get
            {
                return hre;
            }
            set
            {
                hre = value;
                NotifyPropertyChanged(nameof(HRE));
            }
        }
        private string discoveredBy;
        /// <summary>
        /// Discovered By
        /// </summary>
        public string DiscoveredBy
        {
            get
            {
                return discoveredBy;
            }
            set
            {
                discoveredBy = value;
                NotifyPropertyChanged(nameof(DiscoveredBy));
            }
        }
        private string unrest;
        /// <summary>
        /// Unrest
        /// </summary>
        public string Unrest
        {
            get
            {
                return unrest;
            }
            set
            {
                unrest = value;
                NotifyPropertyChanged(nameof(Unrest));
            }
        }
        private string capital;
        /// <summary>
        /// Capital
        /// </summary>
        public string Capital
        {
            get
            {
                return capital;
            }
            set
            {
                capital = value;
                NotifyPropertyChanged(nameof(Capital));
            }
        }

        public List<HistoryEntry> History { get; set; }
        #endregion

        #region Loading
        public bool valid = false;
        public Province(string[] data)
        {
            Define(data);
            Changed = false;
        }

        public Province(int number, (int, int, int) color, string name)
        {
            Number = number;
            r = color.Item1;
            g = color.Item2;
            b = color.Item3;
            Name = name;
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

            Changed = false;
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

            Changed = false;
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

        public string GetHistoryTXT()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Owner)) sb.Append("\nowner = " + Owner);
            if (!string.IsNullOrEmpty(Controller)) sb.Append("\ncontroller = " + Controller);

            if (!string.IsNullOrEmpty(AddCore))
            {
                string[] split_add_core = AddCore.Split(new char[]{ ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach  (string s in split_add_core)
                {
                    if (!string.IsNullOrEmpty(s.Trim())) sb.Append("\nadd_core = " + s);
                }
            }

            if (!string.IsNullOrEmpty(Culture)) sb.Append("\nculture = " + Culture);
            if (!string.IsNullOrEmpty(Religion)) sb.Append("\nreligion = " + Religion);
            if (!string.IsNullOrEmpty(TradeGoods)) sb.Append("\ntrade_goods = " + TradeGoods);
            if (!string.IsNullOrEmpty(BaseTax)) sb.Append("\nbase_tax = " + BaseTax);
            if (!string.IsNullOrEmpty(BaseProduction)) sb.Append("\nbase_production = " + BaseProduction);
            if (!string.IsNullOrEmpty(BaseManpower)) sb.Append("\nbase_manpower = " + BaseManpower);
            sb.Append("\nis_city = " + (IsCity ? "yes" : "no"));
            sb.Append("\nhre = " + (HRE ? "yes" : "no"));

            if (!string.IsNullOrEmpty(DiscoveredBy))
            {
                string[] split_discovered_by = DiscoveredBy.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in split_discovered_by)
                {
                    if (!string.IsNullOrEmpty(s.Trim())) sb.Append("\ndiscovered_by = " + s);
                }
            }

            if (!string.IsNullOrEmpty(Unrest)) sb.Append("\nunrest = " + Unrest);
            if (!string.IsNullOrEmpty(Capital)) sb.Append("\ncapital = \"" + Capital + "\"");

            sb.Append("\n\n");

            if (History != null)
            {
                foreach (HistoryEntry h in History)
                {
                    sb.Append(h);
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
