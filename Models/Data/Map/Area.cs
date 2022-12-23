using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Map
{
    public class Area : MapArea
    {
        public List<Province> Provinces { get; set; }

        public Area(AttributeValueObject obj, List<Province> provinces)
        {
            Name = obj.attribute;
            Provinces = provinces.Where(p => p.Area == Name).ToList();
        }
    }
}
