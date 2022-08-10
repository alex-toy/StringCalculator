using System.Text.RegularExpressions;

namespace StringCalculator.Test
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            var delimitors = GetStandardDelimitors();

            if (input.HasCustomDelimitors())
            {
                delimitors.RegisterCustomDelimitors(input);
                input = RemoveCustomDelimitorDeclarationFrom(input);
            }

            var numbers = input.ExtractNumbers(delimitors);
            ThrowExceptionIfThereIsAnyNegativeNumber(numbers);
            numbers = FilterOutNumbersHigherThanThreshold(numbers, 1000);

            int sum = numbers.Sum();
            return sum;
        }

        private static IEnumerable<int> FilterOutNumbersHigherThanThreshold(IEnumerable<int> numbers, int threshold)
        {
            return numbers.Where(n => n <= threshold);
        }

        private static void ThrowExceptionIfThereIsAnyNegativeNumber(IEnumerable<int> numbers)
        {
            IEnumerable<int> negativeNumbers = ExtractNegativeNumbers(numbers);
            if (HasNegativeNumbers(negativeNumbers))
            {
                throw new Exception($"negatives not allowed: {String.Join(", ", negativeNumbers)}");
            }
        }

        private static IEnumerable<int> ExtractNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Where(n => n < 0);
        }

        private static string RemoveCustomDelimitorDeclarationFrom(string input)
        {
            int endOfCustomDelimitorDeclarationIndex = 4;
            int endOfSquareBracketIndex = input.IndexOf(']');
            if (endOfSquareBracketIndex != -1)
            {
                endOfCustomDelimitorDeclarationIndex = endOfSquareBracketIndex + 2;
            }
            input = input.Substring(endOfCustomDelimitorDeclarationIndex);
            return input;
        }

        private static IList<string> GetStandardDelimitors()
        {
            return new List<string>() { ",", "\n" };
        }

        private static bool HasNegativeNumbers(IEnumerable<int> negativeNumbers)
        {
            return negativeNumbers.Any();
        }
    }

    internal static class StringExtensions
    {
        public static bool HasCustomDelimitors(this string input)
        {
            return input.StartsWith("//");
        }

        public static IEnumerable<int> ExtractNumbers(this string input, IEnumerable<string> delimitors)
        {
            return input
                .Split(delimitors.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
        }
    }

    internal static class DelimitorExtension
    {
        public static void RegisterCustomDelimitors(this IList<string> delimitors, string input)
        {
            int startOfSquareBracketIndex = input.IndexOf('[');
            if (startOfSquareBracketIndex != -1)
            {
                int endOfSquareBracketIndex = input.IndexOf(']');
                delimitors.Add(
                    input.Substring(
                        startOfSquareBracketIndex + 1,
                        endOfSquareBracketIndex - startOfSquareBracketIndex -1));
            }
            else
            {
                const int delimitorIndex = 2;
                delimitors.Add(input[delimitorIndex].ToString());
            }
        }
    }
}
