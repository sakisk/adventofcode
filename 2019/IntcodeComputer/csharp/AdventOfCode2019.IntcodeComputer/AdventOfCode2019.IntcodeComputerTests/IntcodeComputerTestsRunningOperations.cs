using System.Linq;
using AdventOfCode2019.IntcodeComputer;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.IntcodeComputerTests
{
    public class IntcodeComputerTestsRunningOperations
    {
        [Fact]
        public void ShouldHalt()
        {
            var computer = new IntcodeComputerMachine("99");
            var results = computer.Run().ToArray();
            computer.Halted.Should().BeTrue();
        }

        [Theory]
        [InlineData("1,0,0,0", 0, 2L)]
        [InlineData("1,1,2,0", 0, 3L)]
        [InlineData("1,0,0,30", 30, 2L)]
        public void ShouldAdd(string program, int address, long result)
        {
            var computer = new IntcodeComputerMachine(program);
            var results = computer.Run().ToArray();
            computer.Memory[address].Should().Be(result);
        }

        [Theory]
        [InlineData("2,0,0,0", 0, 4L)]
        [InlineData("2,1,2,0", 0, 2L)]
        [InlineData("2,0,0,30", 30, 4L)]
        public void ShouldMultiply(string program, int address, long result)
        {
            var computer = new IntcodeComputerMachine(program);
            var results = computer.Run().ToArray();
            computer.Memory[address].Should().Be(result);
        }

        [Theory]
        [InlineData("1,0,0,3,99", 2)]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", 3)]
        public void ShouldRunMultipleInstructions(string program, int instructionCount) => 
            new IntcodeComputerMachine(program)
                .Run()
                .Should()
                .HaveCount(instructionCount);

        [Theory]
        [InlineData("1,0,0,0,99", "2,0,0,0,99")]
        [InlineData("2,3,0,3,99", "2,3,0,6,99")]
        [InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        public void ShouldHaveMemoryCorrectAfterRunningAllInstructions(string program, string memory)
        {
            var computer = new IntcodeComputerMachine(program);
            computer.Run().ToArray();
            computer.Memory.ToString().Should().Be(memory);
        }
    }
}