using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.DayFive;

namespace AdventOfCode2019.DayEleven
{
    public class EmergencyHullPaintingRover
    {
        public (int X, int Y) Position { get; private set; }
        public char Direction { get; private set; }
        public IList<(char Color, (int X, int Y) Panel)> PanelsDrawn { get; }

        public EmergencyHullPaintingRover() : this((0, 0), '^', null) { }

        public EmergencyHullPaintingRover((int X, int Y) position, char direction) : this(position, direction, null) { }

        public EmergencyHullPaintingRover((int X, int Y) position, char direction, List<(char Color, (int X, int Y))> panelsDrawn)
        {
            Position = position;
            Direction = direction;
            PanelsDrawn = panelsDrawn ?? new List<(char Color, (int X, int Y) Panel)>();
        }

        public void Paint(in int color)
        {
            var newColor = color == 0 ? '.' : '#';

            PanelsDrawn.Add((newColor, Position));
        }

        public void Turn(in int turn)
        {
            Position = _positionTransitions[(Direction, turn)].Invoke(Position);
            Direction = _directionTransitions[(Direction, turn)];
        }

        public void PaintRegistration(string instructions, int color)
        {
            var brain = IntcodeComputer.Create(instructions, maxMemory: 2048, input: color);
            var done = new List<int>();

            while (!brain.Completed)
            {
                brain.Process();

                if (brain.Output.Count - done.Count == 2)
                {
                    var paint =(int)brain.Output[^2];
                    var turn = (int)brain.Output[^1];

                    Paint(paint);
                    Turn(turn);

                    done.AddRange(new[] { paint, turn });
                    brain.SetInput(Camera());
                }
            }
        }

        private readonly IDictionary<(char Direction, int Turn), char> _directionTransitions = new Dictionary<(char Direction, int Turn), char>
        {
            {(Direction:'^', Turn:0), '<' },
            {(Direction:'^', Turn:1), '>' },
            {(Direction:'<', Turn:0), 'v' },
            {(Direction:'<', Turn:1), '^' },
            {(Direction:'v', Turn:0), '>' },
            {(Direction:'v', Turn:1), '<' },
            {(Direction:'>', Turn:0), '^' },
            {(Direction:'>', Turn:1), 'v' },
        };

        private readonly IDictionary<(char Direction, int Turn), Func<(int X, int Y), (int X, int Y)>> _positionTransitions = new Dictionary<(char Direction, int Turn), Func<(int X, int Y), (int X, int Y)>>
        {
            {('^', 0), position => (position.X - 1, position.Y) },
            {('^', 1), position => (position.X + 1, position.Y) },
            {('<', 0), position => (position.X, position.Y - 1) },
            {('<', 1), position => (position.X, position.Y + 1) },
            {('v', 0), position => (position.X + 1, position.Y) },
            {('v', 1), position => (position.X - 1, position.Y) },
            {('>', 0), position => (position.X, position.Y + 1) },
            {('>', 1), position => (position.X, position.Y - 1) },
        };

        public long Camera()
        {
            if (PanelsDrawn.Any(panel => panel.Panel == Position))
                return PanelsDrawn.Last(x => x.Panel == Position).Color == '.' ? 0 : 1;

            return default;
        }
    }
}