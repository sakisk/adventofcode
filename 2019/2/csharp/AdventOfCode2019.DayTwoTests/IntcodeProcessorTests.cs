using AdventOfCode2019.DayTwo;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayTwoTests
{
    public class IntcodeProcessorTests
    {
        [Theory]
        [InlineData("99", "99")]
        [InlineData("99,1,0,0,99", "99,1,0,0,99")]
        public void ShouldProcessProgramHaltOpcode(string input, string result) =>
            new IntcodeProcessor(input).Process().Should().Be(result);
    }
}
