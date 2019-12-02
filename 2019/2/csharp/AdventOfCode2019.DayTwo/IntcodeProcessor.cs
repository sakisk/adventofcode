using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTwo
{
    public class IntcodeProcessor
    {
        private readonly IList<int> _input;
        private int _index;

        public static IntcodeProcessor Create(string input) => new IntcodeProcessor(input.Split(',').Select(int.Parse).ToList(), 0);
        private IntcodeProcessor(IList<int> input, int index)
        {
            _input = input;
            _index = index;
        }

        public IntcodeProcessor Process()
        {
            var opcode = _input[_index];

            if (opcode == 99)
            {
                return new IntcodeProcessor(_input, 0);
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

            return new IntcodeProcessor(_input, _index);
        }


        public string Result => string.Join(",", _input.Select(x => x.ToString()).ToArray());
    }
}