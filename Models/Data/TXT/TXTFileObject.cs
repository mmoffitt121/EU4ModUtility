using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4ModUtil.Util;

namespace EU4ModUtil.Models.Data
{
    public class TXTFileObject
    {
        public AttributeValueObject[] values;

        public TXTFileObject()
        {

        }

        public override string ToString()
        {
            return values?.ArrayToString();
        }
    }
}
