using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Number
{
    class Program
    {
        private struct ConvertCase
        {
            public string Roman { get; set; }
            public int Arabic { get; set; }
            public NumberConverter.CheckResult CheckResult { get; set; }
        }

        private static readonly List<ConvertCase> ConvertCases = new List<ConvertCase>()
        {
            new ConvertCase(){ Roman =              "I", Arabic =    1 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =             "II", Arabic =    2 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =             "xV", Arabic =   15 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =            "XiX", Arabic =   19 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =          "CDLXV", Arabic =  465 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =          "mmCcC", Arabic = 2300 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =  "MDCCCLXXXVIII", Arabic = 1888 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman = "MMDCCCLXXXVIII", Arabic = 2888 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =       "MMCMXCIX", Arabic = 2999 /*, CheckResult = NumberConverter.CheckResult.OK*/ },
            new ConvertCase(){ Roman =            "MMM", Arabic = 3000 /*, CheckResult = NumberConverter.CheckResult.OK*/ }
        };

        static void Main(string[] args)
        {
            ////TNumber.CheckRoman("Hello, World!");

            Console.WriteLine("\n  Arabic to Roman Test\n------------------------\n");
            foreach (var item in ConvertCases)
            {
                switch (item.CheckResult)
                {
                    case NumberConverter.CheckResult.OK:
                        Assert.AreEqual(item.Roman.ToUpper(), NumberConverter.ToRoman(item.Arabic));
                        Console.WriteLine("  {0,14}  to  {1,14}\n", item.Arabic, item.Roman.ToUpper());
                        break;
                    case NumberConverter.CheckResult.Error:
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("\n  Roman to Arabic Test\n------------------------\n");
            foreach (var item in ConvertCases)
            {
                switch (item.CheckResult)
                {
                    case NumberConverter.CheckResult.OK:
                        Assert.AreEqual(item.Arabic, NumberConverter.ToArabic(item.Roman));
                        Console.WriteLine("  {0,14}  to  {1,14}\n", item.Roman.ToUpper(), item.Arabic);
                        break;
                    case NumberConverter.CheckResult.Error:
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("\n  Test for input value (Arabic)\n---------------------------------\n");
            foreach (var item in new int[] { -1, 0, 1, 2, 2998, 2999, 3000, 3001 })
            {
                string isCorrect = NumberConverter.CheckArabic(item) == NumberConverter.CheckResult.OK ? "correct" : "incorrect";
                Console.WriteLine("  {0,14}  is  {1,14}\n", item, isCorrect);
            }

            Console.WriteLine("\n  Test for input value (Roman)\n--------------------------------\n");
            foreach (var item in new string[] { "", "IIii", "MDCXLVI", "IVXLCDM", "Hello, World!", "MIM", "Civic", "CcvII", "ABC" })
            {
                string isCorrect = NumberConverter.CheckRoman(item) == NumberConverter.CheckResult.OK ? "correct" : "incorrect";
                Console.WriteLine("  {0,14}  is  {1,14}\n", item, isCorrect);
            }
        }
    }
}
