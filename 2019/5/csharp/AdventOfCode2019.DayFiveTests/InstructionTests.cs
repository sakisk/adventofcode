using System.Reflection.PortableExecutable;
using AdventOfCode2019.DayFive;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DayFiveTests
{
    public class InstructionTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(10101, 1)]
        public void OpcodeShouldBeCorrect(int intCode, int opCode) =>
            new Instruction(intCode).Opcode.Should().Be(opCode);

        [Theory]
        [InlineData(1002,ParameterMode.Position, ParameterMode.Immediate, ParameterMode.Position)] 
        [InlineData(11102,ParameterMode.Immediate, ParameterMode.Immediate, ParameterMode.Immediate)] 
        public void ShouldSetParameterModeCorrectly(int intCode, ParameterMode first, ParameterMode second, ParameterMode third)
        {
            var instruction = new Instruction(intCode);

            instruction.First.Should().Be(first);
            instruction.Second.Should().Be(second);
            instruction.Third.Should().Be(third);
        }
    }
}
