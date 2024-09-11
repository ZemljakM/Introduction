using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Common
{
    public static class StringExtension
    {
        public static string AddWildcardPrefix(string input)
        {
            return "%" + input;
        }

        public static string AddWildcardSuffix(string input)
        {
            return input + "%";
        }
        public static string AddWildcardBoth(string input)
        {
            return "%" + input + "%";
        }

    }
}
