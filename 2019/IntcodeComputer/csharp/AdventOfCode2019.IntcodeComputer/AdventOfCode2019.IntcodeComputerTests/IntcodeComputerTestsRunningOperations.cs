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
            computer.Run();
            computer.Halted.Should().BeTrue();
        }

        [Theory]
        [InlineData("1,0,0,0", 0, 2L)]
        [InlineData("1,1,2,0", 0, 3L)]
        [InlineData("1,0,0,30", 30, 2L)]
        public void ShouldAdd(string program, int address, long result)
        {
            var computer = new IntcodeComputerMachine(program);
            computer.Run();
            computer.Memory[address].Should().Be(result);
        }

        [Theory]
        [InlineData("2,0,0,0", 0, 4L)]
        [InlineData("2,1,2,0", 0, 2L)]
        [InlineData("2,0,0,30", 30, 4L)]
        public void ShouldMultiply(string program, int address, long result)
        {
            var computer = new IntcodeComputerMachine(program);
            computer.Run();
            computer.Memory[address].Should().Be(result);
        }
    }
}