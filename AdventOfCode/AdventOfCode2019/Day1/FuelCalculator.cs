using System;

namespace AdventOfCode2019.Day1
{
    public static class FuelCalculator
    {
        public static double Calculate(double mass) => Math.Floor(mass / 3) - 2;
    }
}