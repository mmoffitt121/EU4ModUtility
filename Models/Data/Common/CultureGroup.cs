using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Common
{
    internal class CultureGroup : ChangeableObject
    {
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public List<Culture> Cultures { get; set; }
        public CultureGroup(AttributeValueObject obj)
        {
            Name = obj.attribute;

            Cultures = new List<Culture>();
            foreach (AttributeValueObject cultureObj in obj.values)
            {
                Cultures.Add(new Culture(cultureObj));
            }
        }

        public void SetLocalisationData(Dictionary<string, string> dict)
        {

        }
    }
}
