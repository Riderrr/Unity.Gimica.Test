using System;
using System.Collections.Generic;
using UnityEngine;


public static class NumberFormatter
{
    private static readonly Dictionary<int, string> Abbreviations = new()
    {
        { 1, "K" },
        { 2, "M" },
        { 3, "B" },
        { 4, "t" },
        { 5, "q" },
        { 6, "Q" },
        { 7, "s" },
        { 8, "S" },
        { 9, "o" },
        { 10, "n" },
        { 11, "d" },
        { 12, "U" },
        { 13, "D" },
        { 14, "T" },
        { 15, "Qt" },
        { 16, "Qd" },
        { 17, "Sd" },
        { 18, "St" },
        { 19, "O" },
        { 20, "N" },
        { 21, "v" },
        { 22, "c" }
    };

    public static string FormatNumber(double num)
    {
        if (num < 1000)
            return num.ToString("#,0");

        var exp = (int)(Math.Log(num) / Math.Log(1000));

        if (exp > 22) // Max supported abbreviation
            exp = 22;

        var divisor = Math.Pow(1000, exp);
        var shortNumber = num / divisor;

        string abbreviation;
        if (!Abbreviations.TryGetValue(exp, out abbreviation))
        {
            Debug.LogError("No abbreviation for exponent: " + exp);
            return num.ToString("#,0");
        }

        return shortNumber.ToString("0.##") + abbreviation;
    }

    public static string FormatNumber(int num)
    {
        return FormatNumber((double)num);
    }

    public static string FormatNumber(float num)
    {
        return FormatNumber((double)num);
    }
}