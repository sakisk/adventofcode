namespace AdventOfCode2019.IntcodeComputer
{
    public class IntcodeComputerMachine 
    {
        private readonly int _pc;
        public Memory Memory { get; }

        public IntcodeComputerMachine(string program)
        {
            Memory = new Memory(program);
            _pc = 0;
        }

        public bool Run() => Memory.Intcode[_pc] != (long)Opcode.Halt;
    }

    public enum Opcode
    {
        Halt = 99
    }
}