using System;
using System.Collections.Generic;

namespace AdventOfCode2019.IntcodeComputer
{
    public class IntcodeComputerMachine
    {
        public Memory Memory { get; }
        public bool Halted { get; private set; }

        private int _programCounter;
        private readonly Dictionary<int, Action> _instructionSet;

        public IntcodeComputerMachine(string program)
        {
            Memory = new Memory(program);
            _programCounter = 0;
            _instructionSet = new Dictionary<int, Action>
            {
                {1, Add},
                {2, Multiply},
                {99, Halt},
            };
        }
        
        public void Run() => _instructionSet[Opcode]();

        private int Address(int i) => (int) Memory[_programCounter + i];
        
        private long Operand(int i) => Memory[Address(i)];
        
        private int Opcode => (int) Memory[_programCounter];
        
        private void Add()
        {
            Memory[Address(3)] = Operand(1) + Operand(2);
            _programCounter += 4;
        }
        
        private void Multiply()
        {
            Memory[Address(3)] = Operand(1) * Operand(2);
            _programCounter += 4;
        }

        private void Halt() => Halted = true;
    }
}