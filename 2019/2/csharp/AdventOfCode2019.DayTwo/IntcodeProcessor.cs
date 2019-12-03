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

        public static string Process(string input) => Process(InputToIntcode(input));

        public static int FindNounAndVerbForInstruction(string input, int targetInstruction) => FindNounAndVerbForInstruction(InputToIntcode(input), targetInstruction);

        private static string Process(IList<int> input)
        {
            var index = 0;

            while (true)
            {
                if (input[index] == HaltOpcode)
                {
                    return IntcodeToString(input);
                }

                if (input[index] == AddOpcode)
                {
                    ProcessOperand(input, index, (first, second) => first + second);
                    index += 4;
                }
                else if (input[index] == MultiplyOpcode)
                {
                    ProcessOperand(input, index, (first, second) => first * second);
                    index += 4;
                }
            }
        }

        private static int FindNounAndVerbForInstruction(IEnumerable<int> input, int targetInstruction)
        {
            var noun = targetInstruction / 614400 - 1;
            var verb = 0;
            var test = input.ToList();

            test[1] = noun;
            test[2] = verb;

            Process(test);

            verb = targetInstruction - test.First();

            return 100 * noun + verb;
        }
        private static string IntcodeToString(IList<int> input) => string.Join(",", input.Select(x => x.ToString()).ToArray());

        private static IList<int> InputToIntcode(string input) => input.Split(',').Select(int.Parse).ToList();

        private static void ProcessOperand(IList<int> input, int index, Func<int, int, int> operation) =>
            input[input[index + 3]] = operation(input[input[index + 1]], input[input[index + 2]]);

    }
}