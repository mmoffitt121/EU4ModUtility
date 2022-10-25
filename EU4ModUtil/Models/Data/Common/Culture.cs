using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Common
{
    internal class Culture : ChangeableObject
    {
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string LocalizedAdjective { get; set; }
        public Color Color { get; set; }
    }
}
