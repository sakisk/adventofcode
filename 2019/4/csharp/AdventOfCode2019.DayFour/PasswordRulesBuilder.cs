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

        public PasswordRulesBuilder ForPartTwo()
        {
            _rules.Add(HasSixDigits);
            _rules.Add(DigitsIncreaseFromLeftToRight);
            _rules.Add(HasAtLeastOneDigitExactlyTwice);

            return this;
        }

        public IEnumerable<Func<Password, bool>> Build() => _rules;

        private static bool HasSixDigits(Password password) => password.NumberOfDigits == 6;
        
        private static bool HasAtLeastADouble(Password password) => password.AdjacentDigitsPairs.Any(x => x.Item1 == x.Item2);

        private static bool DigitsIncreaseFromLeftToRight(Password password) => !password.AdjacentDigitsPairs.Any(x => x.Item1 > x.Item2);

        private static bool HasAtLeastOneDigitExactlyTwice(Password password) => password.DigitsFrequencies.Any(x => x.Value == 2);
    }
}