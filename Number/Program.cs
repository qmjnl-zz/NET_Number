using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Number
{
    static class NumberConverter
    {
        public enum TCheckResult
        {
            OK,
            Error
        }

        public struct TNumber
        {
            public string Roman { get; set; }
            public int Arabic { get; set; }
            public int Hierarchy { get; set; }
        }

        public static readonly int MaxValue = 3000;

        public static readonly List<TNumber> Numbers = new List<TNumber>() {
            new TNumber() { Roman =  "I", Arabic =    1, Hierarchy = 1 },
            new TNumber() { Roman = "IV", Arabic =    4, Hierarchy = 2 },
            new TNumber() { Roman =  "V", Arabic =    5, Hierarchy = 2 },
            new TNumber() { Roman = "IX", Arabic =    9, Hierarchy = 2 },
            new TNumber() { Roman =  "X", Arabic =   10, Hierarchy = 2 },
            new TNumber() { Roman = "XL", Arabic =   40, Hierarchy = 3 },
            new TNumber() { Roman =  "L", Arabic =   50, Hierarchy = 3 },
            new TNumber() { Roman = "XC", Arabic =   90, Hierarchy = 3 },
            new TNumber() { Roman =  "C", Arabic =  100, Hierarchy = 3 },
            new TNumber() { Roman = "CD", Arabic =  400, Hierarchy = 4 },
            new TNumber() { Roman =  "D", Arabic =  500, Hierarchy = 4 },
            new TNumber() { Roman = "CM", Arabic =  900, Hierarchy = 4 },
            new TNumber() { Roman =  "M", Arabic = 1000, Hierarchy = 4 }
        };

        public static TCheckResult CheckArabic(int value)
        {
            if (value <= 0 || value > MaxValue) return TCheckResult.Error;
            return TCheckResult.OK;
        }

        public static TCheckResult CheckRoman(string value)
        {
            value = value.ToUpper();

            if (string.IsNullOrEmpty(value)) return TCheckResult.Error;

            Match m = Regex.Match(value, "[^IVXLCDM]+", RegexOptions.IgnoreCase);
            if (m.Success) return TCheckResult.Error;

            m = Regex.Match(value, "(I{4,}|X{4,}|C{4,}|M{4,})", RegexOptions.IgnoreCase);
            if (m.Success) return TCheckResult.Error;

            int i = 0;
            while ((i + 1) < value.Length)
            {
                TNumber left = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i].ToString());
                TNumber right = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i + 1].ToString());

                if ((right.Hierarchy - left.Hierarchy) > 1) return TCheckResult.Error;
                i++;
            }

            return TCheckResult.OK;
        }

        public static string ToRoman(int value)
        {
            string outValue = "";
            for (int i = Numbers.Count - 1; i >= 0; i--)
            {
                outValue += string.Concat(Enumerable.Repeat(Numbers[i].Roman, value / Numbers[i].Arabic));
                value %= Numbers[i].Arabic;
            }
            return outValue;
        }

        public static int ToArabic(string value)
        {
            value = value.ToUpper();
            int outValue = 0;
            for (int i = 0; i < value.Length - 1; i++)
            {
                int left = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i].ToString()).Arabic;
                int right = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i + 1].ToString()).Arabic;
                outValue += (left < right) ? -left : left;
            }
            outValue += Numbers.Find(RomanNumber => RomanNumber.Roman == value[value.Length - 1].ToString()).Arabic;
            return outValue;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //TNumber.CheckRoman("Hello, World!");
            NumberConverter.CheckRoman("XiX");
            Console.WriteLine(NumberConverter.ToArabic("XiX"));
            //Console.WriteLine(TNumber.RomanToInt("Mim"));
            Console.WriteLine(NumberConverter.ToArabic("MCMXCIX"));
            Console.WriteLine(NumberConverter.ToArabic("MCMXCVii"));
            Console.WriteLine(NumberConverter.ToArabic("mmCcC"));
            //Console.WriteLine(TNumber.RomanDigitMap["A"]);
            Console.WriteLine(NumberConverter.ToRoman(2300));
            Console.WriteLine(NumberConverter.ToRoman(465));
            Console.WriteLine(NumberConverter.ToRoman(15));
        }
    }
}
