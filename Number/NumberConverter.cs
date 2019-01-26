using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Number
{
    static class NumberConverter
    {
        public enum CheckResult
        {
            OK,
            Error
        }

        public struct Number
        {
            public string Roman { get; set; }
            public int Arabic { get; set; }
            public int Hierarchy { get; set; }
        }

        public static readonly int MaxValue = 3000;

        public static readonly List<Number> Numbers = new List<Number>() {
            new Number() { Roman =  "I", Arabic =    1, Hierarchy = 1 },
            new Number() { Roman = "IV", Arabic =    4, Hierarchy = 2 },
            new Number() { Roman =  "V", Arabic =    5, Hierarchy = 2 },
            new Number() { Roman = "IX", Arabic =    9, Hierarchy = 2 },
            new Number() { Roman =  "X", Arabic =   10, Hierarchy = 2 },
            new Number() { Roman = "XL", Arabic =   40, Hierarchy = 3 },
            new Number() { Roman =  "L", Arabic =   50, Hierarchy = 3 },
            new Number() { Roman = "XC", Arabic =   90, Hierarchy = 3 },
            new Number() { Roman =  "C", Arabic =  100, Hierarchy = 3 },
            new Number() { Roman = "CD", Arabic =  400, Hierarchy = 4 },
            new Number() { Roman =  "D", Arabic =  500, Hierarchy = 4 },
            new Number() { Roman = "CM", Arabic =  900, Hierarchy = 4 },
            new Number() { Roman =  "M", Arabic = 1000, Hierarchy = 4 }
        };

        public static CheckResult CheckArabic(int value)
        {
            if (value <= 0 || value > MaxValue) return CheckResult.Error;
            return CheckResult.OK;
        }

        public static CheckResult CheckRoman(string value)
        {
            value = value.ToUpper();

            if (string.IsNullOrEmpty(value)) return CheckResult.Error;

            Match m = Regex.Match(value, "[^IVXLCDM]+", RegexOptions.IgnoreCase);
            if (m.Success) return CheckResult.Error;

            m = Regex.Match(value, "(I{4,}|X{4,}|C{4,}|M{4,})", RegexOptions.IgnoreCase);
            if (m.Success) return CheckResult.Error;

            int i = 0;
            while ((i + 1) < value.Length)
            {
                Number left = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i].ToString());
                Number right = Numbers.Find(RomanNumber => RomanNumber.Roman == value[i + 1].ToString());

                if ((right.Hierarchy - left.Hierarchy) > 1) return CheckResult.Error;
                i++;
            }

            return CheckResult.OK;
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
}
