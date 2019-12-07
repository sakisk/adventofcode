using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class EnhancedIntcodeProcessor
    {
        private const int Equal = 8;
        private const int LessThan = 7;
        private const int JumpIfFalse = 6;
        private const int JumpIfTrue = 5;
        private const int InputOpcode = 3;
        private const int OutputOpcode = 4;
        private const int AddOpcode = 1;
        private const int MultiplyOpcode = 2;
        private const int HaltOpcode = 99;

        public static IntcodeOutput Process(string input, int? inputParameter = default) => Process(new IntcodeSource(input, inputParameter));

        private static IntcodeOutput Process(IntcodeSource intcodeSource)
        {
            var instructionPointer = 0;
            var input = intcodeSource.Intcode;
            int? output = null;

            while (true)
            {
                var instruction = new Instruction(input[instructionPointer]);
                if (instruction.Opcode == HaltOpcode)
                {
                    return new IntcodeOutput(input, output);
                }

                if (instruction.Opcode == AddOpcode)
                {
                    ProcessOperand(input, instructionPointer, instruction, (first, second) => first + second);
                    instructionPointer += 4;
                }
                else if (instruction.Opcode == MultiplyOpcode)
                {
                    ProcessOperand(input, instructionPointer, instruction, (first, second) => first * second);
                    instructionPointer += 4;
                }
                else if (instruction.Opcode == InputOpcode)
                {
                    input[input[instructionPointer + 1]] = intcodeSource.Input.Value;
                    instructionPointer += 2;
                }
                else if (instruction.Opcode == OutputOpcode)
                {
                    var first = instruction.First == ParameterMode.Immediate
                        ? input[instructionPointer + 1]
                        : input[input[instructionPointer + 1]];

                    output = first;
                    instructionPointer += 2;
                }
                else if (instruction.Opcode == JumpIfTrue)
                {
                    var first = instruction.First == ParameterMode.Immediate
                        ? input[instructionPointer + 1]
                        : input[input[instructionPointer + 1]];
                    var second = instruction.Second == ParameterMode.Immediate
                        ? input[instructionPointer + 2]
                        : input[input[instructionPointer + 2]];

                    instructionPointer = first != 0 ? second : instructionPointer + 3;
                }
                else if (instruction.Opcode == JumpIfFalse)
                {
                    var first = instruction.First == ParameterMode.Immediate
                        ? input[instructionPointer + 1]
                        : input[input[instructionPointer + 1]];
                    var second = instruction.Second == ParameterMode.Immediate
                        ? input[instructionPointer + 2]
                        : input[input[instructionPointer + 2]];

                    instructionPointer = first == 0 ? second : instructionPointer + 3;
                }
                else if (instruction.Opcode == LessThan)
                {
                    var first = instruction.First == ParameterMode.Immediate
                        ? input[instructionPointer + 1]
                        : input[input[instructionPointer + 1]];
                    var second = instruction.Second == ParameterMode.Immediate
                        ? input[instructionPointer + 2]
                        : input[input[instructionPointer + 2]];
                    var third = instruction.Third == ParameterMode.Immediate
                        ? instructionPointer + 3
                        : input[instructionPointer + 3];

                    input[third] = first < second ? 1 : 0;

                    instructionPointer += 4;
                }
                else if (instruction.Opcode == Equal)
                {
                    var first = instruction.First == ParameterMode.Immediate
                        ? input[instructionPointer + 1]
                        : input[input[instructionPointer + 1]];
                    var second = instruction.Second == ParameterMode.Immediate
                        ? input[instructionPointer + 2]
                        : input[input[instructionPointer + 2]];
                    var third = instruction.Third == ParameterMode.Immediate
                        ? instructionPointer + 3
                        : input[instructionPointer + 3];

                    input[third] = first == second ? 1 : 0;

                    instructionPointer += 4;
                }

                var test = string.Join(",", input);
            }
        }

        private static void ProcessOperand(IList<int> input, int index, Instruction instruction, Func<int, int, int> operation)
        {
            var first = instruction.First == ParameterMode.Position
                ? input[input[index + 1]]
                : input[index + 1];
            var second = instruction.Second == ParameterMode.Position
                ? input[input[index + 2]]
                : input[index + 2];
            var third = instruction.Third == ParameterMode.Position
                ? input[index + 3]
                : index + 3;

            input[third] = operation(first, second);
        }
    }

    public class Instruction
    {
        private readonly string _intCode;

        public Instruction(int intCode)
        {
            _intCode = intCode.ToString("D5");
        }

        public int Opcode => int.Parse(_intCode.Substring(3));
        public ParameterMode First => (ParameterMode)char.GetNumericValue(_intCode[2]);
        public ParameterMode Second => (ParameterMode)char.GetNumericValue(_intCode[1]);
        public ParameterMode Third => (ParameterMode)char.GetNumericValue(_intCode[0]);
    }
}