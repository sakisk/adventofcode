using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFour
{
    public class PasswordRulesBuilder
    {
        private readonly List<Func<Password, bool>> _rules = new List<Func<Password, bool>>();

        public PasswordRulesBuilder ForPartOne()
        {
            _rules.Add(HasSixDigits);
            _rules.Add(HasAtLeastADouble);
            _rules.Add(DigitsIncreaseFromLeftToRight);

            return this;
        }

        public IEnumerable<Func<Password, bool>> Build() => _rules;

        private static bool HasSixDigits(Password password) => password.Value >= 111111;
        private static bool HasAtLeastADouble(Password password) => password.PairsOfDigits.Any(x => x.Item1 == x.Item2);
        private static bool DigitsIncreaseFromLeftToRight(Password password) => !password.PairsOfDigits.Any(x => x.Item1 > x.Item2);

        public PasswordRulesBuilder ForPartTwo()
        {
            _rules.Add(HasSixDigits);
            _rules.Add(DigitsIncreaseFromLeftToRight);
            _rules.Add(HasAtLeastOneDigitExactlyTwice);

            return this;
        }

        private static bool HasAtLeastOneDigitExactlyTwice(Password password) =>
            password.Value.ToString().ToCharArray()
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count())
                .Any(x => x.Value == 2);
    }
}