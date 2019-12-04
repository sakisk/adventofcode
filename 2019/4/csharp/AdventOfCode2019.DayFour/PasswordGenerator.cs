using System.Collections.Generic;

namespace AdventOfCode2019.DayFour
{
    public class PasswordGenerator 
    {
        private readonly IValidatePassword _passwordValidator;

        public PasswordGenerator(IValidatePassword passwordValidator)
        {
            _passwordValidator = passwordValidator;
        }
        public int Next(int start)
        {
            while (true)
            {
                if (IsValidPassword(++start)) return start;
            }
        }

        public bool IsValidPassword(int password) => _passwordValidator.IsValid(password);


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