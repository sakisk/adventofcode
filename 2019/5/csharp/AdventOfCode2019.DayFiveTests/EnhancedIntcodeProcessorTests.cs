using System.IO;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFiveTests
{
    public class EnhancedIntcodeProcessorTests
    {
        [Theory]
        [InlineData("99,1,0,0,99", "99,1,0,0,99")]
        [InlineData("1,0,0,3,99", "1,0,0,2,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        public void ShouldProcessMathematicalOpcodes(string input, string result) => EnhancedIntcodeProcessor.Process(input).Intcode.Should().Be(result);

        [Theory]
        [InlineData("3,4,99,0,0", 1, "3,4,99,0,1", null)]
        [InlineData("4,3,99,1", null, "4,3,99,1", 1)]
        public void ShouldProcessIOOpcodes(string input, int? inputParameter, string result, int? outputParameter)
        {
            var output = EnhancedIntcodeProcessor.Process(input, inputParameter);
            output.Output.Should().Be(outputParameter);
            output.Intcode.Should().Be(result);
        }

        [Theory()]
        [InlineData("1002,4,3,4,33", "1002,4,3,4,99")]
        public void ShouldProcessIntermediateModeOpcodes(string input, string result) => EnhancedIntcodeProcessor.Process(input).Intcode.Should().Be(result);

        [Fact]
        public void ShouldOutputCorrectResultForPuzzleInput() => EnhancedIntcodeProcessor.Process(File.ReadAllText("input"), 1).Output.Should().Be(14155342);

        [Theory]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [InlineData("3,9,8,9,10,9,4,9,99,-1,8", 7, 0)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [InlineData("3,9,7,9,10,9,4,9,99,-1,8", 8, 0)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [InlineData("3,3,1108,-1,8,3,4,3,99", 7, 0)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [InlineData("3,3,1107,-1,8,3,4,3,99", 8, 0)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [InlineData("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1, 1)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1, 999)]
        [InlineData("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 9, 1001)]
        public void ShouldOutputCorrectResultForThermalRadiatorsTest(string input, int? inputParameter, int? output) =>
            EnhancedIntcodeProcessor.Process(input, inputParameter).Output.Should().Be(output);
        
        [Fact]
        public void ShouldOutputCorrectResultForPartTwo() => EnhancedIntcodeProcessor.Process(File.ReadAllText("input"), 5).Output.Should().Be(8684145);
    }
}
