using System.Linq;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayNineTests
{
    public class IntcodeComputerTestsBigNumbers
    {

        [Fact]
        public void ShouldOutput16digitNumber()
        {
            var computer = IntcodeComputer.Create("1102,34915192,34915192,7,4,7,99,0");

            computer.ProcessToEnd();

            computer.Output.Last().ToString().Should().HaveLength(16);
        }

        [Fact]
        public void ShouldOutputBigNumbers()
        {
            var computer = IntcodeComputer.Create("104,1125899906842624,99");

            computer.ProcessToEnd();

            computer.Output.Last().Should().Be(1125899906842624);
        }
    }
}