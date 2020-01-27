using System;
using System.Collections.Generic;

namespace AdventOfCode2019.IntcodeComputer
{
    public class IntcodeComputerMachine
    {
        public Memory Memory { get; }
        public bool Halted { get; private set; }
        public IReadOnlyCollection<string> Log => _log.AsReadOnly();

        private int _programCounter;
        private readonly Dictionary<int, Func<string>> _instructionSet;
        private readonly List<string> _log;

        public IntcodeComputerMachine(string program)
        {
            Memory = new Memory(program);
            _programCounter = 0;
            _instructionSet = new Dictionary<int, Func<string>>
            {
                {1, Add},
                {2, Multiply},
                {99, Halt},
            };
            _log = new List<string>();
        }

        public IEnumerable<string> Run()
        {
            var pc = 0;

            while (true)
            {
                if (!_instructionSet.TryGetValue(Opcode, out var instruction))
                    break;

                yield return instruction();

                if (pc == _programCounter)
                    break;

                pc = _programCounter;
            }
        }

        private int Address(int i) => (int) Memory[_programCounter + i];

        private long Operand(int i) => Memory[Address(i)];

        private int Opcode => (int) Memory[_programCounter];

        private string Add()
        {
            var pc = _programCounter;
            
            Memory[Address(3)] = Operand(1) + Operand(2);
            _programCounter += 4;
            
            return string.Join(',', Memory[pc.._programCounter]);
        }

        private string Multiply()
        {
            var pc = _programCounter;
            
            Memory[Address(3)] = Operand(1) * Operand(2);
            _programCounter += 4;
            
            return string.Join(',', Memory[pc.._programCounter]);
        }

        private string Halt()
        {
            Halted = true;
            return "99";
        }
    }
}