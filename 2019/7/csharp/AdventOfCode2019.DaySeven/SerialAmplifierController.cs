using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.DayFive;

namespace AdventOfCode2019.DaySeven
{
    public class SerialAmplifierController
    {
        private readonly string _program;
        private readonly int _input;

        public SerialAmplifierController(string program, int input)
        {
            _program = program;
            _input = input;
        }

        public int ThrusterSignal(int[] phases)
        {
            var currentPhase = 0;
            var current = IntcodeComputer.Create(_program).Process(phases[currentPhase]);
            var outputs = new List<int> { _input };

            current = current.Process(_input);

            while (currentPhase < phases.Length)
            {
                if (current.Output.HasValue)
                {
                    outputs.Add(current.Output.Value);

                    if (++currentPhase == phases.Length)
                        break;

                    current = IntcodeComputer.Create(_program).Process(phases[currentPhase]);

                    current = current.Process(outputs.Last());
                }

                current = current.Process(outputs.Last());
            }

            return outputs.Last();
        }

    }
}