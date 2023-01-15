using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4ModUtil.Models.Macros.Provinces
{
    public enum ProvinceMacroColumnType
    {
        Select,
        ID,
        Name,
        LocalisedName,
        LocalisedAdjective,
        Color,
        Owner,
        Controller,
        Cores,
        Culture,
        Religion,
        TradeGood,
        TradeNode,
        Tax,
        Production,
        Manpower,
        City,
        HRE,
        DiscoveredBy,
        Unrest,
        CapitalName,
        Area,
        Continent,
        Impassable,
        ProvinceType,
        Climate,
        Winter,
        Monsoon
    }

    public enum ColorOperationType
    {
        None,
        Fixed,
        Increment,
        Decrement
    }
}
