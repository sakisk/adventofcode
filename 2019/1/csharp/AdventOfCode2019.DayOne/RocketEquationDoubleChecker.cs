namespace AdventOfCode2019.DayOne
{
    public class RocketEquationDoubleChecker
    {
        private readonly double _mass;
        private double _fuel;

        public RocketEquationDoubleChecker(double mass)
        {
            _mass = mass;
            _fuel = mass;
        }

        public RocketEquationDoubleChecker Next()
        {
            _fuel = new FuelCounterUpper(_mass).Count();

            return _fuel > 0 
                ? new RocketEquationDoubleChecker(_fuel)
                : null;
        }

        public double Fuel() => _fuel;

        public double TotalFuel()
        {
            var nextFuel = Next();
            var result = 0d;

            while(nextFuel != null)
            {
                result += nextFuel.Fuel();
                nextFuel = nextFuel.Next();
            }

            return result;
        }
    }
}