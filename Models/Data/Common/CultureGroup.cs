using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data.Common
{
    internal class CultureGroup : Culture
    {
        /// <summary>
        /// Cultures
        /// </summary>
        public List<Culture> Cultures { get; set; }

        public CultureGroup(AttributeValueObject obj) : base()
        {
            Name = obj.attribute;

            Cultures = new List<Culture>();
            foreach (AttributeValueObject cgo in obj.values)
            {
                switch (cgo.attribute)
                {
                    case "dynasty_names":
                        DynastyNames = new List<string>();
                        foreach (AttributeValueObject o in cgo.values)
                        {
                            DynastyNames.Add(o.attribute);
                        }
                        break;
                    case "male_names":
                        MaleNames = new List<string>();
                        foreach (AttributeValueObject o in cgo.values)
                        {
                            MaleNames.Add(o.attribute);
                        }
                        break;
                    case "female_names":
                        FemaleNames = new List<string>();
                        foreach (AttributeValueObject o in cgo.values)
                        {
                            FemaleNames.Add(o.attribute);
                        }
                        break;
                    case "graphical_culture":
                        GraphicalCulture = cgo.value.attribute;
                        break;
                    case "second_graphical_culture":
                        SecondGraphicalCulture = cgo.value.attribute;
                        break;
                    case "country":
                        break;
                    case "province":
                        break;
                    default:
                        Cultures.Add(new Culture(cgo));
                        break;
                }
            }
        }

        public override void SetLocalisationData(Dictionary<string, string> dict)
        {
            string loc;
            if (dict.TryGetValue(Name, out loc))
            {
                LocalizedName = loc;
            }

            foreach (Culture c in Cultures)
            {
                c.SetLocalisationData(dict);
            }
        }

        public override string ToString()
        {
            return Name + ", " + LocalizedName + ": [" + Cultures.ToArray().ArrayToString() + "]";
        }
    }
}
