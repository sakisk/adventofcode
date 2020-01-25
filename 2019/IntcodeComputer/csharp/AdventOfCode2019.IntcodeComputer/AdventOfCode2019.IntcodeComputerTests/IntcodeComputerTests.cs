using AdventOfCode2019.IntcodeComputer;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.IntcodeComputerTests
{
    public class IntcodeComputerTests
    {
        [Fact]
        public void ShouldLoadProgramToMemory() => 
            new IntcodeComputerMachine("1,0,0,3,99").Memory.Intcode.Should().NotBeEmpty();
    }
}