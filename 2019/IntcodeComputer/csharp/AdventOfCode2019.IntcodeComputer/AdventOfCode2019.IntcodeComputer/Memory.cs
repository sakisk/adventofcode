using System.Linq;

namespace AdventOfCode2019.IntcodeComputer
{
    public class Memory
    {
        public long[] Intcode { get; }
        
        public Memory(string program) => Intcode = program.Split(',').Select(long.Parse).ToArray();
    }
}