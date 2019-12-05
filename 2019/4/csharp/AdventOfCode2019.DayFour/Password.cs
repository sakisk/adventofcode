using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFour
{
    public class Password
    {
        public int Value { get; }
        public IList<(char, char)> AdjacentDigitsPairs { get; }
        public IDictionary<char, int> DigitsFrequencies { get; }

        public Password(int value)
        {
            Value = value;
            AdjacentDigitsPairs = GetAdjacentPairs(value);
            DigitsFrequencies = GetDigitsFrequencies(value);
        }

        private static IList<(char, char)> GetAdjacentPairs(int password)
        {
            var digits = password.ToString().ToCharArray();

            return digits.Zip(digits.Skip(1), (first, second) => (first, second)).ToList();
        }

        private static IDictionary<char, int> GetDigitsFrequencies(int password) =>
            password.ToString().ToCharArray()
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
    }
}