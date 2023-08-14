using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static string[] SplitNewLine(this string str) {
        string[] splitStr = str.Split(new string[] { Environment.NewLine },
                                      StringSplitOptions.None);
        return splitStr;
    }
}
