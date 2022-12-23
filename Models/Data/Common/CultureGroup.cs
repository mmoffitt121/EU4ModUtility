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
        private List<Culture> cultures;
        /// <summary>
        /// Cultures
        /// </summary>
        public List<Culture> Cultures
        {
            get { return cultures; }
            set 
            { 
                cultures = value;
                NotifyPropertyChanged(nameof(Cultures));
            }
        }

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
                        Culture newCulture = new Culture(cgo);
                        newCulture.Parent = this;
                        Cultures.Add(newCulture);
                        break;
                }
            }
        }

        public CultureGroup(string name) : base(name)
        {
            Cultures = new List<Culture>();
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
            StringBuilder sb = new StringBuilder();

            sb.Append(Name + " = {\n");

            if (!string.IsNullOrEmpty(GraphicalCulture)) { sb.Append("\tgraphical_culture = " + GraphicalCulture + "\n"); }
            if (!string.IsNullOrEmpty(SecondGraphicalCulture)) { sb.Append("\tsecond_graphical_culture = " + SecondGraphicalCulture + "\n"); }
            if (MaleNames != null && MaleNames.Count > 0) { sb.Append("\tmale_names = {\n\t\t" + MaleNames.ToArray().ArrayToString(10, 2, " ", "\n") + "\n\t}\n"); }
            if (FemaleNames != null && FemaleNames.Count > 0) { sb.Append("\tfemale_names = {\n\t\t" + FemaleNames.ToArray().ArrayToString(10, 2, " ", "\n") + "\n\t}\n"); }
            if (DynastyNames != null && DynastyNames.Count > 0) { sb.Append("\tdynasty_names = {\n\t\t" + DynastyNames.ToArray().ArrayToString(10, 2, " ", "\n") + "\n\t}\n"); }

            foreach (Culture c in Cultures)
            {
                sb.Append(c.ToString());
            }

            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
