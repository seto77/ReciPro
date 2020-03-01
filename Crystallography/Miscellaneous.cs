using System;
using System.Linq;
using System.Threading.Tasks;

namespace Crystallography
{
    public static class Miscellaneous
    {
        public static string Ordinal(this int number)
        {
            var ones = number % 10;
            var tens = Math.Floor(number / 10f) % 10;
            if (tens == 1)
                return number + "th";
            switch (ones)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
                default: return number + "th";
            }
        }

        public static bool IsFiniteNumber(params double[] d)
        {
            foreach (var value in d)
            {
                if (double.IsNaN(value))
                    return false;
                if (double.IsInfinity(value))
                    return false;
            }
            return true;
        }

        private static bool isDecimalPointCommaFlag = true;
        private static bool isDecimalPointComma = false;

        public static bool IsDecimalPointComma
        {
            get
            {
                if (isDecimalPointCommaFlag)
                {
                    double temp;
                    isDecimalPointComma = double.TryParse("1.000,01", out temp);
                    isDecimalPointCommaFlag = false;
                }
                return isDecimalPointComma;
            }
        }


        public static ParallelQuery<int> Sequence(int n) => Enumerable.Range(0, n).ToList().AsParallel();

    }
}