using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.DayFive;

namespace AdventOfCode2019.DayThirteen
{
    public class ArcadeGame
    {
        private readonly IntcodeComputer _computer;

        public ArcadeGame(string gameCode)
        {
            _computer = IntcodeComputer.Create(gameCode, maxMemory: 4096);
        }

        public void Start()
        {
            _computer.ProcessToEnd();
        }

        public bool Finished => _computer.Completed;

        public IEnumerable<(long X, long Y, long Id)> Grid => _computer.Output
            .Select((x,i) => new {Output = x, Index = i})
            .GroupBy(chunk => chunk.Index / 3)
            .Select(c => c.Where(x => x.Output.HasValue).Select(x => x.Output.Value).ToArray())
            .Select(tile => (X: tile[0], Y: tile[1], Id: tile[2]));

        public IEnumerable<(long X, long Y, long Id)> Blocks => Grid.Where(tile => tile.Id == 2);
    }
}