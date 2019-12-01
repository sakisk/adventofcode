using System;

namespace AdventOfCode2019.DayOne
{

    public class FuelCounterUpper
    {
        private readonly double _mass;
        private double _result;

        public FuelCounterUpper(int mass)
        {
            _mass = mass;
        }

        public FuelCounterUpper DivideByThree()
        {
            _result = Math.Floor(_mass / 3);

            return this;
        }

        public double Compute() => _result;
    }
}
