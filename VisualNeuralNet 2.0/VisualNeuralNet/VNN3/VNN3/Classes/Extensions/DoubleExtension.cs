using System;

namespace VNN.Classes.Extensions
{
    public static class DoubleExtension
    {
        public const double Precision = 0.0000001;
        public static bool AlmostEquals(this double double1, double double2, double precision = Precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }
    }
}