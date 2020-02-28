using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class StringExtensions
    {
        public static string Concat(this IEnumerable<string> strings)
        {
            return string.Concat(strings);
        }

        public static string Join(this IEnumerable<string> strings, char separator)
        {
            return string.Join(separator, strings);
        }
    }
}
