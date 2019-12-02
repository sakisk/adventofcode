using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTwo
{
    public class IntcodeProcessor
    {
        private const int HaltOpcode = 99;
        private readonly IList<int> _input;
        private int _index;


        public static IntcodeProcessor Create(string input) => new IntcodeProcessor(input.Split(',').Select(int.Parse).ToList(), 0);
        private IntcodeProcessor(IList<int> input, int index, bool shouldHalt = false)
        {
            _input = input;
            _index = index;
            ShouldHalt = shouldHalt;
        }

        public IntcodeProcessor Process(IList<int> input)
        {
            var opcode = input[_index];


            if (opcode == HaltOpcode)
            {
                return new IntcodeProcessor(input, 0, shouldHalt: true);
            }

            if (opcode == 1)
            {
                var firstOperandIndex = input[_index + 1];
                var secondOperandIndex = input[_index + 2];
                var resultIndex = input[_index + 3];

                input[resultIndex] = input[firstOperandIndex] + input[secondOperandIndex];
                _index += 4;

                return new IntcodeProcessor(input, _index);
            }

            if (opcode == 2)
            {
                var firstOperandIndex = input[_index + 1];
                var secondOperandIndex = input[_index + 2];
                var resultIndex = input[_index + 3];

                input[resultIndex] = input[firstOperandIndex] * input[secondOperandIndex];
                _index += 4;

                return new IntcodeProcessor(input, _index);
            }

            return new IntcodeProcessor(input, _index);
        }

        public IntcodeProcessor Process() => Process(_input);

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

        public int FindNounAndVerbForInstruction(int instruction)
        {
            var noun = instruction / 614400 - 1;
            var verb = 0;
            var test = _input.ToList();

            test[1] = noun;
            test[2] = verb;

            var processor = new IntcodeProcessor(test, 0);

            verb = instruction - processor.ProcessUntilHalt().Instruction;

            return 100 * noun + verb;
        }

        public bool ShouldHalt { get; }
        public int Instruction => _input.First();
    }
}