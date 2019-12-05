using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFour
{
    public class PasswordGenerator 
    {
        private readonly IEnumerable<Func<Password, bool>> _rules;

        public PasswordGenerator(IEnumerable<Func<Password, bool>> rules)
        {
            _rules = rules;
        }

        public int Next(int start)
        {
            while (true)
            {
                if (IsValidPassword(++start)) return start;
            }
        }

        public bool IsValidPassword(int password) => _rules.Select(validate => validate(new Password(password))).All(x => x);


        public IEnumerable<int> PasswordsInRange(int start, int end)
        {
            var nextPassword = start;

            if (IsValidPassword(nextPassword))
                yield return nextPassword;

            if (IsValidPassword(end))
                yield return end;

            while (nextPassword < end)
            {
                nextPassword = Next(nextPassword);
                if (nextPassword < end)
                    yield return nextPassword;
            }
        }
    }
}