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

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4", 10)]
        public void Should_return_sum_When_unknown_amount_of_numbers_given(string input, int expectedSum)
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add(input);
            Assert.That(result, Is.EqualTo(expectedSum));
        }

        [Test]
        public void Should_return_sum_when_newline_as_separator_and_two_numbers_provided()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("1\n2");
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Should_accept_custom_delimiters_at_the_start_of_input()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("//;\n1;2");
            Assert.That(result, Is.EqualTo(3));
        }

        [TestCase("-2", "negatives not allowed: -2")]
        [TestCase("-2,-3", "negatives not allowed: -2, -3")]
        public void Should_throw_exception_with_explicit_message_When_negative_numbers_provided(string input, string expectedErrorMessage)
        {
            StringCalculator stringCalculator = new StringCalculator();
            Assert.That(() => stringCalculator.Add(input), Throws.Exception.With.Message.EqualTo(expectedErrorMessage));
        }

        [Test]
        public void Should_Ignore_numbers_higher_than_one_thousand_When_present_in_input()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("1001");
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Should_accept_custom_delimiters_in_square_brackets()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("//[*]\n1*2");
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Should_accept_custom_delimiters_of_any_size_in_square_brackets()
        {
            StringCalculator stringCalculator = new StringCalculator();
            int result = stringCalculator.Add("//[***]\n1***2");
            Assert.That(result, Is.EqualTo(3));
        }

    }   
}

