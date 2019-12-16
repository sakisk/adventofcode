using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class MemoryManager
    {
        private readonly IList<long> _program;
        public int ProgramCounter { get; set; }

        public MemoryManager(IList<long> program, int programCounter, int relativeBase)
        {
            _program = program;
            ProgramCounter = programCounter;
            RelativeBase = relativeBase;
        }

        public int OperationCode 
            => int.Parse(new string(_program[ProgramCounter].ToString("D5").Substring(3, 2).ToCharArray()));

        public int RelativeBase { get; set; }

        public long Operand(int offset) => _program[GetAddressIndex(offset)];

        public int GetAddressIndex(int offset) 
            => _memoryIndexFuncs[AddressMode(offset)](offset, _program, ProgramCounter, RelativeBase);

        private readonly IDictionary<AddressMode, Func<int, IList<long>, int, int, int>> _memoryIndexFuncs = new Dictionary<AddressMode, Func<int, IList<long>, int, int, int>>
        {
            {
                DayFive.AddressMode.Positional, (offset, memory, counter, relativeBase) => (int)memory[counter + offset]
            },
            {
                DayFive.AddressMode.Immediate, (offset, memory, counter, relativeBase) => counter + offset
            },
            {
                DayFive.AddressMode.Relative, (offset, memory, counter, relativeBase) => (int)memory[counter + offset] + relativeBase
            }
        };

        private AddressMode AddressMode(int offset) => (AddressMode)_program[ProgramCounter].ToString("D5")
            .Substring(0, 3)
            .Reverse()
            .Select(x => (int)char.GetNumericValue(x))
            .ToList()[offset - 1];

        public void NextInstruction(int offset) => ProgramCounter += offset;
    }
}