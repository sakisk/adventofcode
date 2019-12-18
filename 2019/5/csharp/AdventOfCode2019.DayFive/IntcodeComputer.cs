using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayFive
{
    public class IntcodeComputer
    {
        public bool Completed { get; set; }
        public string Memory => string.Join(",", _memory.Select(x => x.ToString()));
        private int RelativeBase { get; set; }

        private readonly IList<long> _memory;
        private int _pc;
        private static long? _input;
        private MemoryManager _memoryManager;
        public IList<long?> Output { get; }

        public static IntcodeComputer Create(string program, int maxMemory = 0, int relativeBase = 0, long? input = default)
        {
            var intCode = ToIntcode(program);

            var maxMemorySize = maxMemory <= relativeBase + intCode.Count
                ? relativeBase + intCode.Count
                : maxMemory;

            return new IntcodeComputer(intCode, pc: 0, relativeBase: relativeBase, maxMemorySize: maxMemorySize, input: input);
        }

        private IntcodeComputer(IList<long> memory, int pc, int relativeBase, int maxMemorySize, long? input = default)
        {
            Output = new List<long?>();
            RelativeBase = relativeBase;
            _memory = maxMemorySize > 0
                ? new List<long>(memory.Concat(Enumerable.Repeat(0L, maxMemorySize - memory.Count)))
                : memory;
            _pc = pc;
            _input = input;
            _memoryManager = new MemoryManager(_memory, _pc, relativeBase);
        }

        public void Process()
        {
            var instructionSet = new Dictionary<int, Action>
            {
                {1, Add},
                {2, Multiply},
                {3, StoreInput},
                {4, StoreOutput},
                {5, JumpIfNotEqual},
                {6, JumpIfEqual},
                {7, LessThan},
                {8, Equals},
                {9, SetRelativeBase},
                {99, Halt}
            };

            instructionSet[_memoryManager.OperationCode].Invoke();
        }

        public void ProcessToEnd()
        {
            while(!Completed)
                Process();
        }

        private void Halt() => Completed = true;

        private void Add()
        {
            _memory[_memoryManager.GetAddressIndex(3)] = _memoryManager.Operand(1) + _memoryManager.Operand(2);
            _memoryManager.NextInstruction(4);
        }

        private void Multiply()
        {
            _memory[_memoryManager.GetAddressIndex(3)] = _memoryManager.Operand(1) * _memoryManager.Operand(2);
            _memoryManager.NextInstruction(4);
        }

        private void StoreInput()
        {
            _memory[_memoryManager.GetAddressIndex(1)] = _input ?? default;
            _memoryManager.NextInstruction(2);
        }

        private void StoreOutput()
        {
            Output.Add(_memoryManager.Operand(1));
            _memoryManager.NextInstruction(2);
        }

        private void JumpIfNotEqual()
        {
            if (_memoryManager.Operand(1) != 0)
                _memoryManager.ProgramCounter = (int) _memoryManager.Operand(2);
            else
                _memoryManager.ProgramCounter += 3;
        }

        private void JumpIfEqual()
        {
            if (_memoryManager.Operand(1) == 0)
                _memoryManager.ProgramCounter = (int) _memoryManager.Operand(2);
            else
                _memoryManager.ProgramCounter += 3;
        }

        private void LessThan()
        {
            _memory[_memoryManager.GetAddressIndex(3)] = _memoryManager.Operand(1) < _memoryManager.Operand(2) ? 1 : 0;
            _memoryManager.NextInstruction(4);
        }

        private void Equals()
        {
            _memory[_memoryManager.GetAddressIndex(3)] = _memoryManager.Operand(1) == _memoryManager.Operand(2) ? 1 : 0;
            _memoryManager.NextInstruction(4);
        }

        private void SetRelativeBase()
        {
            _memoryManager.RelativeBase += (int)_memoryManager.Operand(1);
            RelativeBase = _memoryManager.RelativeBase;
            _memoryManager.NextInstruction(2);
        }

        private static IList<long> ToIntcode(string program) => program.Split(',').Select(long.Parse).ToList();

        public void SetInput(long input) => _input = input;
    }

}