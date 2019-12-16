using System.IO;
using System.Linq;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFiveTests
{
    public class IntcodeComputerTests
    {
        [Fact]
        public void ShouldCompleteOnHaltOpcode()
        {
            var computer = IntcodeComputer.Create("99");
            computer.Process();
            computer.Completed.Should().BeTrue();
        }

        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99", default)]
        [InlineData("2,0,0,0,99", "4,0,0,0,99", default)]
        [InlineData("3,0,99", "1,0,99", 1)]
        [InlineData("1002,4,3,4,33", "1002,4,3,4,99")]
        public void ShouldExecuteOperation(string program, string result, int? input = default)
        {
            var computer = IntcodeComputer.Create(program, input: input);
            computer.Process();
            computer.Memory.Should().Be(result);
        }

        [Theory]
        [InlineData("4,0,99", 4)]
        [InlineData("4,2,99", 99)]
        public void ShouldOutput(string program, int output)
        {
            var computer = IntcodeComputer.Create(program);
            computer.Process();
            computer.Output.Last().Should().Be(output);
        }

        [Theory]
        [InlineData(1, 14155342)]
        [InlineData(5, 8684145)]
        public void SolutionForParts(int input, int solution)
        {
            var program = File.ReadAllText("input");
            var computer = IntcodeComputer.Create(program, input: input);

            computer.ProcessToEnd();

            computer.Output.Last().Should().Be(solution);
        }

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
        public void ShouldOutputResultForEnhancedInstructionSet(string program, int? input, int? output)
        {
            var computer = IntcodeComputer.Create(program, input: input);

            computer.ProcessToEnd();

            computer.Output.Last().Should().Be(output);
        }
    }
}
