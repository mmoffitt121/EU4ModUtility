using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Models.Macros.Provinces;
using EU4ModUtil.Models.Data;

namespace EU4ModUtil.ViewModels
{
    public class SetProvinceValueViewModel : ChangeableObject
    {
        /// <summary>
        /// Provinces
        /// </summary>
        public List<Province> Provinces { get; set; }
        /// <summary>
        /// Column Type
        /// </summary>
        public ProvinceMacroColumnType ColumnType { get; set; }

        #region Color Macro
        /// <summary>
        /// The province's color in SolidColorBrush form
        /// </summary>
        public SolidColorBrush BGColor
        {
            get
            {
                Color color = new Color();
                color.A = 255;
                color.R = (byte)r;
                color.G = (byte)g;
                color.B = (byte)b;
                return new SolidColorBrush(color);
            }
        }

        private int r;
        /// <summary>
        /// R
        /// </summary>
        public string R 
        {
            get => r.ToString();
            set
            {
                int val;
                if (Int32.TryParse(value, out val) && 0 <= val && val <= 255)
                {
                    r = val;
                    NotifyPropertyChanged(nameof(R));
                    NotifyPropertyChanged(nameof(BGColor));
                }
                else if (string.IsNullOrEmpty(value))
                {
                    r = 0;
                }
            }
        }

        private int g;
        /// <summary>
        /// G
        /// </summary>
        public string G 
        {
            get => g.ToString();
            set
            {
                int val;
                if (Int32.TryParse(value, out val) && 0 <= val && val <= 255)
                {
                    g = val;
                    NotifyPropertyChanged(nameof(G));
                    NotifyPropertyChanged(nameof(BGColor));
                }
                else if (string.IsNullOrEmpty(value))
                {
                    g = 0;
                }
            }
        }

        private int b;
        /// <summary>
        /// B
        /// </summary>
        public string B
        {
            get => b.ToString();
            set
            {
                int val;
                if (Int32.TryParse(value, out val) && 0 <= val && val <= 255)
                {
                    b = val;
                    NotifyPropertyChanged(nameof(B));
                    NotifyPropertyChanged(nameof(BGColor));
                }
                else if (string.IsNullOrEmpty(value))
                {
                    b = 0;
                }
            }
        }

        public ColorOperationType ROperation { get; set; }
        public ColorOperationType GOperation { get; set; }
        public ColorOperationType BOperation { get; set; }
        public int RIncrement { get; set; } = 10;
        public int GIncrement { get; set; } = 10;
        public int BIncrement { get; set; } = 10;
        public int RInterval { get; set; } = 1;
        public int GInterval { get; set; } = 1;
        public int BInterval { get; set; } = 1;
        #endregion

        public SetProvinceValueViewModel(List<Province> provinces)
        {
            this.Provinces = provinces;
        }

        public SetProvinceValueViewModel() { }
    }
}
