using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Map
{
    public class MapArea : ChangeableObject
    {
        private string name;
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

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

        private string localizedName2;
        /// <summary>
        /// Second Localized Name
        /// </summary>
        public string LocalizedName2
        {
            get { return localizedName2; }
            set
            {
                localizedName2 = value;
                NotifyPropertyChanged(nameof(LocalizedName2));
            }
        }

        private string localizedAdjective;
        /// <summary>
        /// Localized Adjective
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

        /// <summary>
        /// Parent Node
        /// </summary>
        public MapArea Parent { get; set; }
    }
}
