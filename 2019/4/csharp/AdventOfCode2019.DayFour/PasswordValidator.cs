using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFour
{
    public class PasswordValidator : IValidatePassword 
    {
        public IList<(char, char)> Pairs;

        public bool IsValid(int password)
        {
            Pairs = GetAdjacentPairs(password);
            return HasSixDigits(password) && HasAtLeastADouble(Pairs) && DigitsIncreaseFromLeftToRight(Pairs);
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