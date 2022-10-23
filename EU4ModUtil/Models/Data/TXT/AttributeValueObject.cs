using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data
{
    internal class AttributeValueObject
    {
        public string attribute;
        public List<AttributeValueObject> values;

        public AttributeValueObject value
        {
            get
            {
                if (values == null || values.Count == 0)
                {
                    return null;
                }
                return values[0];
            }
            set
            {
                values = new List<AttributeValueObject> { value };
            }
        }

        public AttributeValueObject(string attribute, AttributeValueObject value)
        {
            this.attribute = attribute;
            this.values = new List<AttributeValueObject> { value };
        }

        public AttributeValueObject(string attribute, List<AttributeValueObject> values)
        {
            this.attribute = attribute;
            this.values = values;
        }

        public AttributeValueObject()
        {

        }

        public override string ToString()
        {
            return "(" + attribute + " " + values?.ToArray().ArrayToString() + ")";
        }
    }
}
