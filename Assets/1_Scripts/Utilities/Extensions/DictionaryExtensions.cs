using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chi.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool IsNullorEmpty<T1,T2>(this IDictionary<T1,T2> dictionary) {
            if (dictionary?.Any() != true) return true;
            else return false;
        }

        public static bool HaveNullValues<T1,T2>(this IDictionary<T1, T2> dictionary) {
            if (dictionary.Values.Any(x => x == null)) return true;
            else return false;
        }

        public static void Test() {

        }
    }
}

