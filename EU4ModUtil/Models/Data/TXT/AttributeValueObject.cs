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
        public AttributeValueObject[] values;

        public AttributeValueObject value
        {
            get
            {
                if (values == null || values.Length == 0)
                {
                    return null;
                }
                return values[0];
            }
        }

        public AttributeValueObject(string attribute, AttributeValueObject value)
        {
            this.attribute = attribute;
            this.values = new AttributeValueObject[] { value };
        }

        public AttributeValueObject(string attribute, AttributeValueObject[] values)
        {
            this.attribute = attribute;
            this.values = values;
        }

        public override string ToString()
        {
            return "(" + attribute + " " + values.ArrayToString() + ")";
        }
    }
}
