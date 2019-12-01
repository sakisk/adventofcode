using System;
using System.Linq;

namespace AdventOfCode2019.DayOne
{

    public class FuelCounterUpper
    {
        private readonly double[] _masses;
        private double _mass;
        private double _result;

        public FuelCounterUpper(double mass) : this(new[] { mass }) { }

        public FuelCounterUpper(double[] masses)
        {
            _masses = masses;
            _mass = masses.First();
        }

        public FuelCounterUpper DivideByThree()
        {
            _result = Math.Floor(_mass / 3);

            return this;
        }

        public FuelCounterUpper TakeAwayTwo()
        {
            _result -= 2;

            return this;
        }

        public double Sum()
        {
            foreach (var mass in _masses)
            {
                _mass = mass;
                _result += DivideByThree().TakeAwayTwo().Compute();
            }

            return _result;
        }

        public double Compute() => _result;
    }
}
