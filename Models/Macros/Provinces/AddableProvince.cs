using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Models.Data.Map;

namespace EU4ModUtil.Models.Macros.Provinces
{
    internal class AddableProvince : Province
    {
        public int Count { get; set; }
        public ColorOperationType ROp { get; set; }
        public ColorOperationType GOp { get; set; }
        public ColorOperationType BOp { get; set; }
        public int RIncrement { get; set; }
        public int GIncrement { get; set; }
        public int BIncrement { get; set; }
        public int RInterval { get; set; }
        public int GInterval { get; set; }
        public int BInterval { get; set; }
        public AddableProvince() : base(new string[] { "New Province", "0", "0", "0" })
        {

        }
    }
}
