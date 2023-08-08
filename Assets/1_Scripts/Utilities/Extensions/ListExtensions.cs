using System;
using System.Collections.Generic;
using System.Linq;

namespace Chi.Utilities.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullorEmpty<T>(this IList<T> list) {
            if (list?.Any() != true) return true;
            else return false ;
        }

        public static bool HaveNullElements<T>(this IList<T> list) {
            if (list.Any(x => x == null)) return true;
            else return false;
        }

        public static void Test() {

        }
    }
}

