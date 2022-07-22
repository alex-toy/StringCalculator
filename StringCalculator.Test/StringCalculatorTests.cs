using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Test
{
    public class StringCalculatorTests
    {
        [Test]
        public void Should_return_zero_When_empty_string()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("");
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Should_return_given_number_When_One_number_given()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("4");
            Assert.That(result, Is.EqualTo(4));
        }

        [TestCase("2,4", 6)]
        [TestCase("13,2", 15)]
        public void Should_return_sum_When_two_numbers_given(string input, int expectedResult)
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Should_return_sum_When_three_numbers_given()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("1,2,3");
            Assert.That(result, Is.EqualTo(6));
        }

    }

    public class StringCalculator
    {
        public int Add(string input)
        {
            if (input.Length == 0)
            {
                return 0;
            }
            int commaIndex = input.IndexOf(",");
            if (commaIndex != -1)
            {
                int sum = int.Parse(input.Substring(0, commaIndex));
                string remainder = input.Substring(commaIndex + 1);
                commaIndex = remainder.IndexOf(",");
                if (commaIndex != -1)
                {
                    sum += int.Parse(remainder.Substring(0, commaIndex));
                    sum += int.Parse(remainder.Substring(commaIndex + 1));
                }
                else
                {
                    sum += int.Parse(remainder);
                }
                return sum;
            }
            return int.Parse(input);
        }
    }
}
