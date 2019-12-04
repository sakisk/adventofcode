using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFourTests
{
    public class PasswordGenerator
    {
        public static int Next(int start)
        {
            while (true)
            {
                if (IsValidPassword(++start)) return start;
            }
        }

        public static bool IsValidPassword(int password)
        {
            var adjacentPairs = GetAdjacentPairs(password);
            return HasSixDigits(password) && HasAtLeastADouble(adjacentPairs) && DigitsIncreaseFromLeftToRight(adjacentPairs);
        }

        public static IEnumerable<int> PasswordsInRange(Range range)
        {
            var nextPassword = range.Start.Value;

            if (IsValidPassword(nextPassword))
                yield return nextPassword;

            if (IsValidPassword(range.End.Value))
                yield return range.End.Value;

            while (nextPassword < range.End.Value)
            {
                nextPassword = Next(nextPassword);
                if (nextPassword < range.End.Value)
                    yield return nextPassword;
            }
        }

        private static bool HasSixDigits(int password) => password >= 111111;

        private static bool HasAtLeastADouble(IEnumerable<(char, char)> pairs) => pairs.Any(x => x.Item1 == x.Item2);

        private static bool DigitsIncreaseFromLeftToRight(IEnumerable<(char, char)> pairs) => !pairs.Any(x => x.Item1 > x.Item2);

        private static IList<(char, char)> GetAdjacentPairs(int password)
        {
            var digits = password.ToString().ToCharArray();

            return digits.Zip(digits.Skip(1), (first, second) => (first, second)).ToList();
        }
    }
}