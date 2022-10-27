using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Data.History
{
    internal class HistoryEntry : ChangeableObject
    {
        public DateTime DateTime { get; set; }

        public HistoryEntry(AttributeValueObject obj)
        {
            return;
        }
    }
}
