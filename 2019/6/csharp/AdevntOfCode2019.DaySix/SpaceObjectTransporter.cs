using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.DaySix
{
    public class SpaceObjectTransporter
    {
        private readonly OrbitGraph _orbitGraph;

        public SpaceObjectTransporter(OrbitGraph orbitGraph)
        {
            _orbitGraph = orbitGraph;
        }

        public int CountTransfers(string youObject, string santaObject)
        {
            var you = _orbitGraph.OrbitingObjects.Single(x => x.Value.DirectOrbits.ContainsKey(youObject)).Value;
            var santa = _orbitGraph.OrbitingObjects.Single(x => x.Value.DirectOrbits.ContainsKey(santaObject)).Value;

            var previous = new Dictionary<string, SpaceObject>();
            var queue = new Queue<SpaceObject>();
            queue.Enqueue(you);

            while (queue.Count > 0)
            {
                var centre = queue.Dequeue();
                foreach (var orbiting in centre.DirectOrbits)
                {
                    if (previous.ContainsKey(orbiting.Key))
                        continue;
                    previous[orbiting.Key] = centre;
                    queue.Enqueue(orbiting.Value);
                }
            }

            return ShortestPath();

            int ShortestPath()
            {
                var path = 0;

                var current = santa;

                while (current.Name != you.Name)
                {
                    path++;
                    current = previous[current.Name];
                }

                return path;
            }
        }
    }
}