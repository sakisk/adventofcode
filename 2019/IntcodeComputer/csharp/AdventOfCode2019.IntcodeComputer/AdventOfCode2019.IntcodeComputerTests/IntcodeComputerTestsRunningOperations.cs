using AdventOfCode2019.IntcodeComputer;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.IntcodeComputerTests
{
    public class IntcodeComputerTestsRunningOperations
    {
        [Fact]
        public void GivenHaltOperationShouldExitRunning()
        {
            new IntcodeComputerMachine("99").Run().Should().BeFalse();
        }
    }
}