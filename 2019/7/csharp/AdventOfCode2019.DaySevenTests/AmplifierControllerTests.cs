using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.DayFive;
using AdventOfCode2019.DaySeven;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2019.DaySevenTests
{
    public class AmplifierControllerTests
    {
        [Theory]
        [InlineData("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0", 0, 43210)]
        [InlineData("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", "0,1,2,3,4", 0, 54321)]
        [InlineData("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", "1,0,4,3,2", 0, 65210)]
        public void ShouldOutputCorrectThrusterSignalForSerialController(string program, string phaseSettings, int input, int thrusterSignal)
        {
            var phases = phaseSettings.Split(",").Select(int.Parse).ToArray();
            new SerialAmplifierController(program, input).ThrusterSignal(phases).Should().Be(thrusterSignal);
        }

        [Fact]
        public void SolutionForPartOne()
        {
            var program = File.ReadAllText("input");
            var controller = new SerialAmplifierController(program, 0);
            new[] { 0, 1, 2, 3, 4 }
                .Permutations()
                .Select(x => controller.ThrusterSignal(x.ToArray()))
                .Max().Should().Be(75228);
        }

        [Theory]
        [InlineData("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", "9,8,7,6,5", 0, 139629729)]
        public void ShouldOutputCorrectThrusterSignalForControllerWithLoopback(string program, string phaseSettings, int input, int thrusterSignal)
        {
            var phases = phaseSettings.Split(",").Select(int.Parse).ToArray();
            new LoopbackAmplifierController(program, input).ThrusterSignal(phases).Should().Be(thrusterSignal);
        }

    }
    public class LoopbackAmplifierController
    {
        private readonly string _program;
        private readonly int _input;

        public LoopbackAmplifierController(string program, int input)
        {
            _program = program;
            _input = input;
        }

        public int ThrusterSignal(int[] phases)
        {
            var currentPhase = 0;
            var outputs = new List<int> { _input };
            var amplifiers = Enumerable.Repeat(IntcodeComputer.Create(_program), phases.Length).ToArray();
            var thrusterSignal = 0;

            var current = amplifiers[currentPhase].Process(_input);

            while (currentPhase < phases.Length)
            {
                if (amplifiers.All(x => x.Completed))
                    break;
                
                if (current.Output.HasValue)
                {
                    amplifiers[currentPhase] = current;
                    outputs.Add(current.Output.Value);

                    if (++currentPhase == phases.Length)
                    {
                        current = amplifiers.First();
                        currentPhase = 0;
                        thrusterSignal = amplifiers.Last().Output.Value;
                    }
                }

                current = current.Process(outputs.Last());
            }

            return thrusterSignal;
        }

    }
}
