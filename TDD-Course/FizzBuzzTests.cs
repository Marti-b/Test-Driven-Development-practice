using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDD
{
    /*
    * If divisible by 3       -> return "Fizz"
    * If divisible by 5       -> return "Buzz"
    * If divisible by 3 and 5 -> return "FizzBuzz"
    * Otherwise               -> return "" 
    */
    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase("Fizz", 3)]
        [TestCase("Fizz", 6)]
        [TestCase("Buzz", 5)]
        [TestCase("Buzz", 10)]
        [TestCase("FizzBuzz", 15)]
        [TestCase("FizzBuzz", 30)]
        [TestCase("", 7)]
        public void TestFizzBuzz(string expected, int number)
        {
            Assert.AreEqual(expected, FizzBuzz(number));
        }
        private string FizzBuzz(int number)
        {
            if (number % 3 == 0)
            {
                if (number % 15 == 0)
                {
                    return "FizzBuzz";
                }
                return "Fizz";
            }
            if (number % 5 == 0)
            {
                return "Buzz";
            }
            return "";
        }
    }

}
