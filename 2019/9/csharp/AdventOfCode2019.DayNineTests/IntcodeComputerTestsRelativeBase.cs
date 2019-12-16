using System.Linq;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayNineTests
{
    public class IntcodeComputerTestsRelativeBase
    {

        [Fact]
        public void ShouldProcessRelativeAddressBaseOpcode()
        {
            var computer = IntcodeComputer.Create("109,19,99", maxMemory: 4096, relativeBase: 2000);

            computer.Process();

            computer.RelativeBase.Should().Be(2019);
        }

        [Fact]
        public void ShouldCopyItself()
        {
            const string program = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            var computer = IntcodeComputer.Create(program, 2048);

            computer.ProcessToEnd();

            string.Join(",", computer.Output.Select(x => x.ToString())).Should().Be(program);
        }
    }
}
