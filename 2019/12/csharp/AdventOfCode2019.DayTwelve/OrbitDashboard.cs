using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019.DayTwelve
{
    public class OrbitDashboard
    {
        public IEnumerable<(long X, long Y, long Z)> Moons { get; private set; }
        public IEnumerable<(long X, long Y, long Z)> Velocities { get; private set; }
        public long TotalEnergy => PotentialEnergy.Zip(KineticEnergy, (p, k) => p * k).Sum();

        private readonly Regex _moonPositionRegex =
            new Regex(@"<x=(?'X'-?\d*), y=(?'Y'-?\d*), z=(?'Z'-?\d*)>", RegexOptions.Multiline);

        public OrbitDashboard(string moonStartingPositions)
        {
            Moons = _moonPositionRegex
                .Matches(moonStartingPositions)
                .Cast<Match>()
                .Select(x => (
                    X: long.Parse(x.Groups["X"].Value),
                    Y: long.Parse(x.Groups["Y"].Value),
                    Z: long.Parse(x.Groups["Z"].Value)))
                .ToList();

            Velocities = Moons.Select(x => (X: 0L, Y: 0L, Z: 0L)).ToList();
        }

        public void ApplyGravity() => ApplyGravity(Moons.ToArray());

        private void ApplyGravity((long X, long Y, long Z)[] moons)
        {
            var velocitiesByPosition = NextStep(moons);

            Velocities = velocitiesByPosition
                .Select(x => x.Velocity)
                .Zip(Velocities,
                    (previous, current) => (previous.X + current.X, previous.Y + current.Y, previous.Z + current.Z))
                .ToArray();

            Moons = Velocities
                .Zip(Moons, (moon, velocity) => (moon.X + velocity.X, moon.Y + velocity.Y, moon.Z + velocity.Z))
                .ToArray();
        }

        private IEnumerable<long> PotentialEnergy =>
            Moons.Select(x => Math.Abs(x.X) + Math.Abs(x.Y) + Math.Abs(x.Z));

        private IEnumerable<long> KineticEnergy =>
            Velocities.Select(x => Math.Abs(x.X) + Math.Abs(x.Y) + Math.Abs(x.Z));

        public IEnumerable<((long X, long Y, long Z) Position, (long X, long Y, long Z) Velocity)> NextStep(
            (long X, long Y, long Z)[] moons) =>
            from first in moons.Select((position, index) => new {Position = position, Index = index})
            from second in moons.Select((position, index) => new {Position = position, Index = index})
            where first.Index != second.Index
            let velocityChange = new
            {
                first.Position,
                Velocity = (
                    X: VelocityChange(first.Position.X, second.Position.X),
                    Y: VelocityChange(first.Position.Y, second.Position.Y),
                    Z: VelocityChange(first.Position.Z, second.Position.Z))
            }
            group velocityChange by velocityChange.Position
            into velocityChanges
            select (
                Position: velocityChanges.Key,
                Velocity:
                (
                    X: velocityChanges.Sum(x => x.Velocity.X),
                    Y: velocityChanges.Sum(x => x.Velocity.Y),
                    Z: velocityChanges.Sum(x => x.Velocity.Z)
                )
            );

        private static long VelocityChange(long a, long b) => a < b ? 1 : a > b ? -1 : 0;

        public void Move(in long steps)
        {
            for (var i = 0L; i < steps; i++)
                ApplyGravity();
        }

        public long StepsUntilFirstMatchingState()
        {
            var statesX = new HashSet<((long, long, long, long), (long, long, long, long))>();
            var statesY = new HashSet<((long, long, long, long), (long, long, long, long))>();
            var statesZ = new HashSet<((long, long, long, long), (long, long, long, long))>();

            var matchesX = false;
            var matchesY = false;
            var matchesZ = false;

            while (!matchesX || !matchesY || !matchesZ)
            {
                var moons = Moons.ToArray();
                var velocities = Velocities.ToArray();

                if (!matchesX)
                    matchesX = !statesX.Add(MoonsX(moons, velocities));
                if (!matchesY)
                    matchesY = !statesY.Add(MoonsY(moons,velocities));
                if (!matchesZ)
                    matchesZ = !statesZ.Add(MoonsZ(moons,velocities));

                ApplyGravity();
            }

            return Lcm(statesX.Count, Lcm(statesY.Count, statesZ.Count));
            
            ((long, long, long, long), (long, long, long, long)) MoonsX((long X, long Y, long Z)[] moons,
                (long X, long Y, long Z)[] velocities) => (
                (moons[0].X, moons[1].X, moons[2].X, moons[3].X),
                (velocities[0].X, velocities[1].X, velocities[2].X, velocities[3].X));
            
            ((long, long, long, long), (long, long, long, long)) MoonsY((long X, long Y, long Z)[] moons,
                (long X, long Y, long Z)[] velocities) => (
                (moons[0].Y, moons[1].Y, moons[2].Y, moons[3].Y),
                (velocities[0].Y, velocities[1].Y, velocities[2].Y, velocities[3].Y));
            
            ((long, long, long, long), (long, long, long, long)) MoonsZ((long X, long Y, long Z)[] moons,
                (long X, long Y, long Z)[] velocities) => (
                (moons[0].Z, moons[1].Z, moons[2].Z, moons[3].Z),
                (velocities[0].Z, velocities[1].Z, velocities[2].Z, velocities[3].Z));
        }

        private static long Gcd(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            for (;;)
            {
                long remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            }
        }

        private long Lcm(long a, long b) => a * b / Gcd(a, b);
    }
}