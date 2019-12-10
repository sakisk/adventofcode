using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class IntcodeComputer
    {
        public int? Output { get; }

        public bool Completed { get; }

        public string Program => string.Join(",", _program.Select(x => x.ToString()));

        private readonly IList<int> _program;

        private int _pc;

        public static IntcodeComputer Create(string program) => new IntcodeComputer(ToInt(program), 0);

        private IntcodeComputer(IList<int> program, int pc, int? output = default, bool completed = false)
        {
            Output = output;
            Completed = completed;
            _program = program;
            _pc = pc;
        }

        public IntcodeComputer Process(int? input = default)
        {
            switch (int.Parse(new string(_program[_pc].ToString("D5").Substring(3, 2).ToCharArray())))
            {
                case 99:
                    return new IntcodeComputer(_program, _pc,  completed: true);
                case 1:
                    _program[MemoryAddress(3)] = Operand(1) + Operand(2);
                    return new IntcodeComputer(_program, _pc + 4);
                case 2:
                    _program[MemoryAddress(3)] = Operand(1) * Operand(2);
                    return new IntcodeComputer(_program, _pc + 4);
                case 3:
                    if (input.HasValue) _program[MemoryAddress(1)] = input.Value;
                    return new IntcodeComputer(_program, _pc + 2);
                case 4:
                    return new IntcodeComputer(_program, _pc + 2, output: Operand(1));
                case 5:
                    _pc = Operand(1) != 0 ? Operand(2) : _pc + 3;
                    return new IntcodeComputer(_program, _pc);
                case 6:
                    _pc = Operand(1) == 0 ? Operand(2) : _pc + 3;
                    return new IntcodeComputer(_program, _pc);
                case 7:
                    _program[MemoryAddress(3)] = Operand(1) < Operand(2) ? 1 : 0;
                    return new IntcodeComputer(_program, _pc + 4);
                case 8:
                    _program[MemoryAddress(3)] = Operand(1) == Operand(2) ? 1 : 0;
                    return new IntcodeComputer(_program, _pc + 4);
                default:
                    return new IntcodeComputer(_program, _pc);
            }
        }

        private int Mode(int offset) => _program[_pc].ToString("D5")
            .Substring(0, 3)
            .Reverse()
            .Select(x => (int)char.GetNumericValue(x))
            .ToList()[offset - 1];

        private int Operand(int offset) => Mode(offset) == 0
            ? _program[_program[_pc + offset]]
            : _program[_pc + offset];

        private int MemoryAddress(int offset) => Mode(3) == 0
            ? _program[_pc + offset]
            : _pc + offset;

        private static IList<int> ToInt(string program) => program.Split(',').Select(int.Parse).ToList();
    }
}