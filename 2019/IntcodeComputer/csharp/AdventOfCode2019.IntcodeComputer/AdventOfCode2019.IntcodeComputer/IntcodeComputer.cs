using System;

namespace AdventOfCode2019.IntcodeComputer
{
    public class IntcodeComputerMachine
    {
        public Memory Memory { get; }
        public bool Halted => Opcode == Opcode.Halt;
        
        private int _programCounter;
        
        public IntcodeComputerMachine(string program)
        {
            Memory = new Memory(program);
            _programCounter = 0;
        }

        private Opcode Opcode => (Opcode)Memory.Intcode[_programCounter];

        public void Run()
        {
            var opcode = Opcode;
            int Address(int i) => (int)Memory[_programCounter + i];
            long Operand(int i) => Memory[Address(i)];

            switch (opcode)
            {
                case Opcode.Add: 
                    Memory[Address(3)] = Operand(1) + Operand(2);
                    _programCounter += 4;
                    break;
                case Opcode.Multiply:
                    Memory[Address(3)] = Operand(1) * Operand(2);
                    _programCounter += 4;
                    break;
                case Opcode.Halt: break;
                default: throw new ArgumentException($"Invalid opcode: {opcode}");
            }
        }
    }

    public enum Opcode
    {
        Add = 1,
        Multiply = 2,
        Halt = 99
    }
}