using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTwo
{
    public class IntcodeProcessor
    {
        private const int HaltOpcode = 99;
        private readonly IList<int> _input;
        private int _index;
        public bool ShouldHalt { get; }

        public static IntcodeProcessor Create(string input) => new IntcodeProcessor(input.Split(',').Select(int.Parse).ToList(), 0);
        private IntcodeProcessor(IList<int> input, int index, bool shouldHalt = false)
        {
            _input = input;
            _index = index;
            ShouldHalt = shouldHalt;
        }

        public IntcodeProcessor Process()
        {
            var opcode = _input[_index];


            if (opcode == HaltOpcode)
            {
                return new IntcodeProcessor(_input, 0, shouldHalt: true);
            }

            if (opcode == 1)
            {
                var firstOperandIndex = _input[_index + 1];
                var secondOperandIndex = _input[_index + 2];
                var resultIndex = _input[_index + 3];

                _input[resultIndex] = _input[firstOperandIndex] + _input[secondOperandIndex];
                _index += 4;

                return new IntcodeProcessor(_input, _index);
            }

            if (opcode == 2)
            {
                var firstOperandIndex = _input[_index + 1];
                var secondOperandIndex = _input[_index + 2];
                var resultIndex = _input[_index + 3];

                _input[resultIndex] = _input[firstOperandIndex] * _input[secondOperandIndex];
                _index += 4;

                return new IntcodeProcessor(_input, _index);
            }

            return new IntcodeProcessor(_input, _index);
        }

        public string Result => string.Join(",", _input.Select(x => x.ToString()).ToArray());

        public IntcodeProcessor ProcessUntilHalt()
        {
            var processor = this;

            while (!processor.ShouldHalt)
            {
                processor = Process();
            }

            return processor;
        }
    }
}