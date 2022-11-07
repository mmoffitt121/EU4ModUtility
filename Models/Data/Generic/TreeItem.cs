using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Generic
{
    public class TreeItem
    {
        public string Name { get; set; }
        public Dictionary<string, string> Localization { get; set; }
        public Dictionary<string, TreeItem> Children { get; set; }
    }
}
