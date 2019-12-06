using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DaySix
{
    public class OrbitMap
    {
        public const string CentreOfMass = "COM";

        public OrbitMap()
        {
            Orbits = new Dictionary<string, Orbit>
            {
                {
                    CentreOfMass, new Orbit(CentreOfMass)
                }
            };
        }

        public IDictionary<string, Orbit> Orbits { get; }

        public void AddOrbit(string orbit)
        {
            var names = orbit.Split(')');

            var centre = TryGetOrbit(names.First());
            var orbiting = TryGetOrbit(names.Last());

            centre.AddOrbit(orbiting);

            Orbit TryGetOrbit(string name)
            {
                if (Orbits.TryGetValue(name, out var o))
                    return o;

                o = new Orbit(name);
                Orbits.Add(name, o);

                return o;
            }
        }

        public int CountOrbitsFrom(string centre)
        {
            var paths = 0;
            var queue = new Queue<Orbit>();

            queue.Enqueue(Orbits[centre]);

            while (queue.Count > 0)
            {
                var o = queue.Dequeue();
                foreach (var directOrbit in o.DirectOrbits.Values)
                {
                    queue.Enqueue(directOrbit);
                    paths++;
                }
            }

            return paths;
        }

        public int CountOrbitsForAllObjects() => Orbits.Keys.Sum(CountOrbitsFrom);
    }
}