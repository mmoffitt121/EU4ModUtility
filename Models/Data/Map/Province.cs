using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.History;

namespace EU4ModUtil.Models.Data.Map
{
    internal class Province
    {
        public int Id { get; set; }


        public string LocalizedName { get; set; }
        public Culture Culture { get; set; }
        public Religion Religion { get; set; }

        public HistoryEntry[] History { get; set; }
    }
}
