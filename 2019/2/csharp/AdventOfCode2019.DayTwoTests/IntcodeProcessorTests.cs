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
            IntcodeProcessor.Create(input).Process().Result.Should().Be(result);

        [Theory]
        [InlineData("1,0,0,3,99", "1,0,0,2,99")]
        public void GivenAnAdditionOpcode_WhenProcessed_ShouldAddNextTwoIntsAndStoreResultInPositionIndicatedByThirdInt(
            string input, string result) =>
            IntcodeProcessor.Create(input).Process().Result.Should().Be(result);
    }
}
