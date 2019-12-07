using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DaySix
{
    public class OrbitCounter
    {
        private readonly OrbitGraph _orbitGraph;

        public OrbitCounter(OrbitGraph orbitGraph)
        {
            _orbitGraph = orbitGraph;
        }

        public int CountOrbitsFrom(string centre)
        {
            var paths = 0;
            var queue = new Queue<SpaceObject>();

            queue.Enqueue(_orbitGraph.OrbitingObjects[centre]);

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

        public int CountOrbitsForAllObjects() => _orbitGraph.OrbitingObjects.Keys.Sum(CountOrbitsFrom);
    }
}