using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDD
{
    [TestFixture]
    class RomanNumeralTests
    {
        [TestCase(1, "I")]
        [TestCase(5, "V")]
        [TestCase(10, "X")]
        [TestCase(2, "II")]
        [TestCase(4, "IV")]
        [TestCase(14, "XIV")]
        [TestCase(9, "IX")]
        public void Parse(int expected, string roman)
        {
            Assert.AreEqual(expected, Roman.Parse(roman));
        }
    }
    public class Roman
    {
        private static readonly Dictionary<char, int> map = new Dictionary<char, int>()
        {
            {'I', 1 },
            {'V', 5 },
            {'X', 10 },
            {'L', 50 },
            {'C', 100 },
            {'D', 500 },
            {'M', 1000 }
        };
        public static int Parse(string roman)
        {
            int result = 0;

            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && map[roman[i]] < map[roman[i + 1]])
                {
                    result -= map[roman[i]];
                }
                else
                {
                    result += map[roman[i]];
                }
               
            }
            return result;

        }
    }

}
