using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFour
{
    public class Password
    {
        public int Value { get; }
        public IList<(char, char)> PairsOfDigits { get; set; }

        public Password(int value)
        {
            Value = value;
            PairsOfDigits = GetAdjacentPairs(value);
        }

        private static IList<(char, char)> GetAdjacentPairs(int password)
        {
            var digits = password.ToString().ToCharArray();

            return digits.Zip(digits.Skip(1), (first, second) => (first, second)).ToList();
        }
    }
}