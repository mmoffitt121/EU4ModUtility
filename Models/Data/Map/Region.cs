using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Map
{
    public class Region : MapArea
    {
        public List<Area> Areas { get; set; }
        public List<(string, string)> Monsoon { get; set; }
    }
}
