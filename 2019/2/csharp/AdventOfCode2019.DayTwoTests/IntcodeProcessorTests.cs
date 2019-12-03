using System.IO;
using System.Linq;
using AdventOfCode2019.DayTwo;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayTwoTests
{
    public class IntcodeProcessorTests
    {
        [Theory]
        [InlineData("99,1,0,0,99", "99,1,0,0,99")]
        public void ShouldProcessProgramHaltOpcode(string input, string result) => IntcodeProcessor.Process(input).Should().Be(result);

        [Theory]
        [InlineData("1,0,0,3,99", "1,0,0,2,99")]
        public void GivenAnAdditionOpcode_WhenProcessed_ShouldAddNextTwoIntsAndStoreResultInPositionIndicatedByThirdInt(
            string input, string result) =>
            IntcodeProcessor.Process(input).Should().Be(result);

        [Theory]
        [InlineData("2,0,0,3,99", "2,0,0,4,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        public void GivenAMultiplicationOpcode_WhenProcessed_ShouldMultiplyNextTwoIntsAndStoreResultInPositionIndicatedByThirdInt(
            string input, string result) =>
            IntcodeProcessor.Process(input).Should().Be(result);

        [Theory]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void GivenAnIntcodeSequence_WhenProcessed_ShouldProcessOpcodesUntilHalt(string input, string result) => 
            IntcodeProcessor.Process(input).Should().Be(result);

        [Fact]
        public void ShouldProcessInputTo1202ProgramAlarm()
        {
            var input = File.ReadAllText("input").Split(',');

            input[1] = "12";
            input[2] = "2";

            var input1202ProgramAlarm = string.Join(',', input);

            IntcodeProcessor.Process(input1202ProgramAlarm).Split(',').First().Should().Be("8017076");
        }

        [Fact]
        public void ShouldProcessInputUntilCorrectPointerHasCorrectInstruction()
        {
            var input = File.ReadAllText("input");
            IntcodeProcessor.FindNounAndVerbForInstruction(input, 19690720).Should().Be(3146);
        }
    }
}
