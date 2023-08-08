using System;
using System.Collections.Generic;
using System.Linq;

namespace Chi.Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsNullorEmpty<T>(this T[] array) {
            if (array?.Any() != true) return true;
            else return false;
        }

        public static bool HaveNullElements<T>(this T[] array) {
            if (array.Any(x => x == null)) return true;
            else return false;
        }

    }
}

