using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Regular
{
    class RegularExpressions
    {
        public RegularExpressions(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public bool isPhone(string text)
        {
            return Regex.IsMatch(text, @"^\+\d{1}\(\d{3}\)\d{3}\-\d{2}\-\d{2}$");
        }

        public bool isTime(string text)
        {
            return Regex.IsMatch(text, @"^(0[1-9]|1[0-2]):[0-5][0-9]\s(AM|PM)$");
        }

        public string convertTime(string text)
        {
            Match m = Regex.Match(text, @"^(0[1-9]|1[0-2]):([0-5][0-9])\s(AM|PM)$");
            string hourString = m.Groups[1].ToString();
            if (int.Parse(hourString[0].ToString()) == 0)
                hourString = hourString[1].ToString();
            int hour = int.Parse(hourString);
            bool isAm = m.Groups[3].ToString()=="AM";
            if (!isAm)
            {
                hour += 12;
            }
            return string.Format("{0}:{1}", hour, m.Groups[2].ToString());
        }
    }
}
