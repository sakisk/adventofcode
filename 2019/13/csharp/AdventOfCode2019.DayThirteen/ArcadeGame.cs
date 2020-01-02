using System.Collections.Generic;
using System.Collections.Specialized;
using AdventOfCode2019.DayFive;

namespace AdventOfCode2019.DayThirteen
{
    public class ArcadeGame
    {
        private readonly IntcodeComputer _computer;
        private int? _direction;

        public ArcadeGame(string gameCode) => _computer = IntcodeComputer.Create(gameCode, maxMemory: 4096);

        public void Start(int quarters) => _computer.MemoryStore[0] = quarters;

        public IEnumerable<(long X, long Y, long Id)> RenderArtifacts()
        {
            var artifact = new List<long>(3);
            _computer.Output.CollectionChanged += OutputChanged;
            while (!_computer.Completed)
            {
                if (artifact.Count == 3)
                {
                    yield return (X: artifact[0], Y: artifact[1], Id: artifact[2]);
                    artifact.Clear();
                }

                if (_direction.HasValue)
                    _computer.Process(_direction.Value);
                else
                    _computer.Process();
            }

            void OutputChanged(object sender, NotifyCollectionChangedEventArgs args)
            {
                if (args.Action != NotifyCollectionChangedAction.Add)
                    return;
                artifact.Add((long) args.NewItems[0]);
            }
        }

        public int Play()
        {
            var ballX = 0L;
            var paddleX = 0L;
            var score = 0;

            foreach (var artifact in RenderArtifacts())
            {
                if (artifact.Id == 3) paddleX = artifact.X;
                if (artifact.Id == 4) ballX = artifact.X;
                if (artifact.X == -1 && artifact.Y == 0) score = (int) artifact.Id;

                _direction = ballX < paddleX ? -1 : ballX > paddleX ? 1 : 0;
            }

            return score;
        }
    }
}