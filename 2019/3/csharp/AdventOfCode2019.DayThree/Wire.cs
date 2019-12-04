using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DayThree
{
    public class Wire
    {
        private readonly (int, int) _centralPort = (0, 0);
        private const char Right = 'R';
        private const char Left = 'L';
        private const char Up = 'U';
        private const char Down = 'D';
        private List<(int, int)> Trace { get; }
        public List<(int, int)> Vertical { get; }
        public List<(int, int)> Horizontal { get; }


        public Wire()
        {
            Tip = _centralPort;
            Horizontal = new List<(int, int)>();
            Vertical = new List<(int, int)>();
            Trace = new List<(int, int)>();
        }

        public void Parse(string[] traces)
        {
            foreach (var trace in traces)
            {
                switch (trace.First())
                {
                    case Right:
                        TraceRight(trace);
                        break;
                    case Left:
                        TraceLeft(trace);
                        break;
                    case Up:
                        TraceUp(trace);
                        break;
                    case Down:
                        TraceDown(trace);
                        break;
                    default: throw new Exception("Unknown trace direction");
                }
            }
        }

        private void TraceDown(string trace)
        {
            var length = int.Parse(trace.Substring(1, trace.Length - 1));
            for (var i = 1; i <= length; i++)
            {
                var point = (Tip.Item1, Tip.Item2 - i);

                Trace.Add(point);
                Vertical.Add(point);
            }

            Tip = Vertical.Last();
        }

        private void TraceUp(string trace)
        {
            var length = int.Parse(trace.Substring(1, trace.Length - 1));
            for (var i = 1; i <= length; i++)
            {
                var point = (Tip.Item1, Tip.Item2 + i);

                Trace.Add(point);
                Vertical.Add(point);
            }

            Tip = Vertical.Last();
        }

        private void TraceLeft(string trace)
        {
            var length = int.Parse(trace.Substring(1, trace.Length - 1));
            for (var i = 1; i <= length; i++)
            {
                var point = (Tip.Item1 - i, Tip.Item2);

                Trace.Add(point);
                Horizontal.Add(point);
            }

            Tip = Horizontal.Last();
        }

        private void TraceRight(string trace)
        {
            var length = int.Parse(trace.Substring(1, trace.Length - 1));
            for (var i = 1; i <= length; i++)
            {
                var point = (Tip.Item1 + i, Tip.Item2);

                Trace.Add(point);
                Horizontal.Add(point);
            }

            Tip = Horizontal.Last();
        }

        public IEnumerable<(int, int)> FindIntersections(Wire anotherWire)
        {
            if (Tip == _centralPort || anotherWire.Tip == _centralPort)
                yield break;

            var horizontalScan = Horizontal.Intersect(anotherWire.Vertical);
            var verticalScan = Vertical.Intersect(anotherWire.Horizontal);

            foreach (var intersection in horizontalScan.Concat(verticalScan))
            {
                yield return intersection;
            }
        }

        public int FindDistanceToNearestIntersectionFromCentralPort(Wire anotherWire) =>
            FindIntersections(anotherWire)
                .Select(point => Math.Abs(point.Item1) + Math.Abs(point.Item2))
                .OrderBy(x => x)
                .First();

        public int FindMinStepsIntersection(Wire anotherWire) =>
            FindIntersections(anotherWire)
                .Select(x => FindStepsTo(x) + anotherWire.FindStepsTo(x))
                .Min();

        public (int, int) Tip { get; set; }

        public int FindStepsTo((int x, int y) point) => Trace.FindIndex(0, p => p == point) + 1;
    }
}