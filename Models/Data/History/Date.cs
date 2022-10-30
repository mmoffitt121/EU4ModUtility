using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EU4ModUtil.Models.Data.History
{
    public class Date
    {
        public int year;
        public int month;
        public int day;

        public Date()
        {
            year = 1;
            month = 1;
            day = 1;
        }

        public Date(DateTime date)
        {
            year = date.Year;
            month = date.Month;
            day = date.Day;
        }

        public Date(string[] date)
        {
            try
            {
                year = int.Parse(date[0]);
                month = int.Parse(date[1]);
                day = int.Parse(date[2]);
            }
            catch (Exception)
            {
                throw new Exception("The date could not be parsed");
            }
        }

        public override string ToString()
        {
            return year + "." + month + "." + day;
        }

        public static bool TryParse(string date, out Date result)
        {
            try
            {
                string[] split = date.Split('.');
                if (split.Length == 3 && int.TryParse(split[0], out int out0) && int.TryParse(split[1], out int out1) && int.TryParse(split[2], out int out2))
                {
                    result = new Date(date.Split('.'));
                    return true;
                }
                else
                {
                    result = null;
                    return false;
                }
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public int GetIntDate()
        {
            return year * 10000 + month * 100 + day;
        }
    }
}
