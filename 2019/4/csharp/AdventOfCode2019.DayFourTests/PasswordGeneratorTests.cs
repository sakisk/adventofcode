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
        public void ShouldValidatePassword(int password, bool valid) => PasswordGenerator.IsValidPassword(password).Should().Be(valid);

        [Theory]
        [InlineData(111111, 111112)]
        [InlineData(111119, 111122)]
        [InlineData(111199, 111222)]
        public void ShouldGenerateNextValidPassword(int seed, int next) => PasswordGenerator.Next(seed).Should().Be(next);

        [Theory]
        [InlineData(111111, 111111, 1)]
        [InlineData(111111, 111112, 2)]
        [InlineData(137683, 596253, 1864)]
        public void ShouldGenerateValidPasswordsInRange(int start, int end, int count) =>
            PasswordGenerator.PasswordsInRange(start, end).Distinct().Count().Should().Be(count);
    }
}
