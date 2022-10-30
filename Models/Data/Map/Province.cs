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
        public string Region { get; set; }
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

        public HistoryEntry[] History { get; set; }
        #endregion
    }
}
