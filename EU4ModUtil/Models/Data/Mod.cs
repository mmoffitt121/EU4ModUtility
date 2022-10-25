using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.Map;

namespace EU4ModUtil.Models.Data
{
    internal class Mod
    {
        internal Descriptor descriptor;
        internal List<Country> countries;
        internal List<Province> provinces;
        internal List<Culture> cultures;
        internal List<CultureGroup> cultureGroups;
        internal List<Religion> religions;
    }
}
