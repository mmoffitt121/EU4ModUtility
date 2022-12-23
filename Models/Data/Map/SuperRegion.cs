using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Map
{
    public class SuperRegion : MapArea
    {
        public List<Region> Regions { get; set; }
        public bool RestrictCharter { get; set; }

        public SuperRegion(AttributeValueObject obj, List<Region> regions)
        {
            Name = obj.attribute;
        }
    }
}
