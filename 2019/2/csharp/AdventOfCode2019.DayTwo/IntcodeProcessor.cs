using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayTwo
{
    public class IntcodeProcessor
    {
        private const int HaltOpcode = 99;
        private const int AddOpcode = 1;
        private const int MultiplyOpcode = 2;
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

            if (opcode == AddOpcode)
            {
                ProcessOperand(input, _index, (first, second) => first + second);
                MoveToNextOperand();

                return new IntcodeProcessor(input, _index);
            }

            if (opcode == MultiplyOpcode)
            {
                ProcessOperand(input, _index, (first, second) => first * second);
                MoveToNextOperand();

                return new IntcodeProcessor(input, _index);
            }

            return new IntcodeProcessor(input, _index);

            void MoveToNextOperand() => _index += 4;
        }

        private static void ProcessOperand(IList<int> input, int index, Func<int, int, int> operation) =>
            input[input[index + 3]] = operation(input[input[index + 1]], input[input[index + 2]]);

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