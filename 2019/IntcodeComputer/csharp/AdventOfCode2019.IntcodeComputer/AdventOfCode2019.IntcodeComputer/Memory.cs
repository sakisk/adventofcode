using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode2019.IntcodeComputer
{
    public class Memory
    {
        public ImmutableArray<long> Intcode { get; }
        
        private readonly Dictionary<long, long> _mem;

        public Memory(string program)
        {
            Intcode = program.Split(',').Select(long.Parse).ToImmutableArray();
            _mem = new Dictionary<long, long>();
        }

        public long this[int address]
        {
            get => _mem.ContainsKey(address) 
                ? _mem[address] 
                : address >= 0 && address < Intcode.Length 
                    ? Intcode[address] 
                    : 0;
            set => _mem[address] = value;
        }
    }
}