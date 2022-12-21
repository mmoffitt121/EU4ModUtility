using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.Common
{
    internal class Culture : ChangeableObject
    {
        private string name;
        /// <summary>
        /// Name
        /// </summary>
        public string Name 
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Localised Name
        /// </summary>
        public string LocalizedName { get; set; }

        /// <summary>
        /// Primary Country Tag
        /// </summary>
        public string Primary { get; set; }

        /// <summary>
        /// Graphical Culture
        /// </summary>
        public string GraphicalCulture { get; set; }

        /// <summary>
        /// Second Graphical Culture
        /// </summary>
        public string SecondGraphicalCulture { get; set; }

        /// <summary>
        /// Dynasty Names
        /// </summary>
        public List<string> DynastyNames { get; set; }

        /// <summary>
        /// Male Names
        /// </summary>
        public List<string> MaleNames { get; set; }

        /// <summary>
        /// Female Names
        /// </summary>
        public List<string> FemaleNames { get; set; }

        public Culture(AttributeValueObject obj)
        {
            Name = obj.attribute;

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
                    case "primary":
                        Primary = cgo.value.attribute;
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
                        break;
                }
            }
        }

        public Culture()
        {

        }

        public Culture(string name)
        {
            Name = name;
        }

        public virtual void SetLocalisationData(Dictionary<string, string> dict)
        {
            string loc;
            if (dict.TryGetValue(Name, out loc))
            {
                LocalizedName = loc;
            }
        }

        public override string ToString()
        {
            return "(" + Name + ", " + LocalizedName + ")";
        }
    }
}
