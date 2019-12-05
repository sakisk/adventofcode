using System.Linq;
using AdventOfCode2019.DayFour;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFourTests
{
    public class PasswordGeneratorTests
    {
        [Theory]
        [InlineData(111111, true)]
        [InlineData(223450, false)]
        [InlineData(123789, false)]
        public void ShouldValidatePasswordForPartOne(int password, bool valid)
        {
            var ruleSet = new PasswordRulesBuilder().ForPartOne().Build();

            new PasswordGenerator(ruleSet).IsValidPassword(password).Should().Be(valid);
        }

        [Theory]
        [InlineData(112233, true)]
        [InlineData(123444, false)]
        [InlineData(111122, true)]
        public void ShouldValidatePasswordForPartTwo(int password, bool valid)
        {
            var ruleSet = new PasswordRulesBuilder().ForPartTwo().Build();

            new PasswordGenerator(ruleSet).IsValidPassword(password).Should().Be(valid);
        }

        [Theory]
        [InlineData(111111, 111112)]
        [InlineData(111119, 111122)]
        [InlineData(111199, 111222)]
        public void ShouldGenerateNextValidPasswordForPartOne(int seed, int next)
        {
            var ruleSet = new PasswordRulesBuilder().ForPartOne().Build();

            new PasswordGenerator(ruleSet).Next(seed).Should().Be(next);
        }

        [Theory]
        [InlineData(111111, 111112, 2)]
        [InlineData(137683, 596253, 1864)]
        public void ShouldGenerateValidPasswordsInRangeForPartOne(int start, int end, int count)
        {
            var ruleSet = new PasswordRulesBuilder().ForPartOne().Build();

            new PasswordGenerator(ruleSet).PasswordsInRange(start, end).Count().Should().Be(count);
        }

        [Theory]
        [InlineData(137683, 596253, 1258)]
        public void ShouldGenerateValidPasswordsInRangeForPartTwo(int start, int end, int count)
        {
            var ruleSet = new PasswordRulesBuilder().ForPartTwo().Build();

            new PasswordGenerator(ruleSet).PasswordsInRange(start, end).Count().Should().Be(count);
        }
    }
}
