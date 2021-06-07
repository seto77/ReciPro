using System;

namespace Crystallography
{
    public class GammaFunction
    {
        static public double EulerGamma = 0.577215664901532860606512090082;

        /*
        public static double Digamma(double z)
        {
            int n;
            int nMax = 100000;
            double answer = 0;

            //if ((z - 1) / n)

            z = z - 1;
            for (n = nMax; n > 0; n--)
                answer += z / n / (n + z);

            return answer - EulerGamma;
        }
        */

        /*
        * polygamma --- Return the polygamma function {\psi}^k(x)
        *	If k=0,1,2,..., then returns digamma, trigamma,
        *	tetragamma,... function values respectively.
        *	Double precision (VAX D FORMAT 56 bits or IEEE DOUBLE 53 bits)
        *		$Author: tunenori $
        *		$Revision: 1.5 $
        *		$Date: 1993/08/09 11:21:19 $
        *
        * Special cases:
        *	polygamma(k, x) is NaN with signal if k < 0 or x < 0;
        *	polygamma(k, x) is INF with signal if x = 0;
        *	polygamma(k, +-Inf) is NaN with signal;
        *	polygamma(k, NaN) is that NaN with no signal.
        *
        *	Copyright (c) 1993 by RICOH Co., Ltd.
        *
        *	NO WARRANTIES
        *
        *	RICOH DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE,
        *	INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS,
        *	IN NO EVENT SHALL RIOCH BE LIABLE FOR ANY SPECIAL, INDIRECT OR
        *	CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
        *	LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT,
        *	NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN
        *	CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
         * http://coca.rd.dnc.ac.jp/tunenori/src/polygamma.c
        */

        private static double[] brn = {1.6666666666666666e-01, 3.3333333333333333e-02,
 2.3809523809523809e-02, 3.3333333333333333e-02, 7.5757575757575757e-02,
 2.5311355311355311e-01, 1.1666666666666667e+00, 7.0921568627450980e+00,
 5.4971177944862155e+01, 5.2912424242424242e+02};

        public static double polygamma(int k, double x)/* the derivative order number */	/* variable */
        {
            double s;	/* return value */
            double y;	/* minimum value more than `slv', adding `x' to integers */
            double x2;	/* x * x */
            double pk;	/* k! */
            double pxk;	/* pxk = pow(x, k+1)	*/
            double slv;	/* sufficient large value applied for asymptotic expansion */
            double f;
            int n;	/* [slv - x] */
            int i, j;
            int i2, isgn;

            if (k < 0 || x < 0)/* k < 0 or x < 0 or x is +-INF */
                return double.NaN;

            if (k > 3)
            {
                //calculation of `slv'
                f = 1.0;
                for (i = k + 19; i > 20; i--)
                    f *= (double)i;
                for (i = k + 1; i > 2; i--)
                    f /= (double)i;
                f *= (174611.0 / 55.0);	/* B_{20} / B_{2} */
                slv = 6.812921 * Math.Pow(f, 1.0 / 18.0);
                if (slv < 13.06)
                    slv = 13.06;
            }
            else/* 1 <= k <= 3 */
                slv = 13.06;

            pk = 1.0;
            for (i = 0; i < k; i++)
                pk *= (double)(i + 1);	/* pk = k! */

            if (x == 0)
                return double.PositiveInfinity;
            else if (x >= slv)
            {
                /* Adopted `x' to the asymptotic expansion. */
                s = 0.0;
                x2 = x * x;
                isgn = k % 2 == 1 ? -1 : 1;
                if (k == 0)/* digamma function */
                {
                    for (i = brn.Length - 1; i >= 0; i--)
                    {
                        i2 = 2 * (i + 1);
                        s += brn[i] / (double)i2 * isgn;
                        s /= x2;
                        isgn *= -1;
                    }
                    s += Math.Log(x) - 0.5 / x;
                }
                else
                {
                    /* k >= 1; trigamm, tetragamma, ... */
                    for (i = brn.Length - 1; i >= 0; i--)
                    {
                        f = 1.0;
                        i2 = 2 * (i + 1);
                        j = i2 + k - 1;
                        while (j > i2)
                            f *= (double)j--;
                        s += brn[i] * f * isgn;
                        s /= x2;
                        isgn *= -1;
                    }
                    for (i = 0; i < k; i++)
                        s /= x;
                    pxk = 1.0;
                    for (i = 0; i < k; i++)
                        pxk *= x;	/* pxk = pow(x, k) */

                    s -= pk * 0.5 / pxk / x * isgn;
                    f = pk / (double)k;
                    s -= f / pxk * isgn;
                }
            }
            else//x < slv
            {
                //Adopted `y' instead of `x' to the asymptotic expansion,
                //we calculation the value.
                n = (int)(slv - x);
                y = (double)n + x + 1.0;
                s = polygamma(k, y);
                isgn = k % 2 == 1 ? 1 : -1;
                for (i = 0; i <= n; i++)
                {
                    y -= 1.0;
                    if (Math.Abs(y) < 0.001)
                    {
                        if (x > 0)
                            y = x - (double)((int)(x + 0.5));
                        else
                            y = x - (double)((int)(x - 0.5));
                    }
                    pxk = 1.0;
                    for (j = 0; j < k; j++)
                        pxk *= y;	/* pxk = pow(y, k) */
                    if (pxk * y == 0)
                        return double.PositiveInfinity;
                    s += isgn * pk / pxk / y;
                }
            }
            return s;
        }

        /// <summary> Returns <code>sqrt(a<sup>2</sup> + b<sup>2</sup>)</code>
        /// without underflow/overlow.</summary>
        public static double Hypot(double a, double b)
        {
            double r;

            if (Math.Abs(a) > Math.Abs(b))
            {
                r = b / a;
                r = Math.Abs(a) * Math.Sqrt(1 + r * r);
            }
            else if (b != 0)
            {
                r = a / b;
                r = Math.Abs(b) * Math.Sqrt(1 + r * r);
            }
            else r = 0.0;

            return r;
        }

        /// <summary>
        /// Returns the greatest common divisor of two integers using euclids algorithm.
        /// </summary>
        /// <returns>gcd(a,b)</returns>
        public static long Gcd(long a, long b)
        {
            long rem;
            while (b != 0)
            {
                rem = a % b;
                a = b;
                b = rem;
            }
            return Math.Abs(a);
        }

        /// <summary>
        /// Computes the extended greatest common divisor, such that a*x + b*y = gcd(a,b).
        /// </summary>
        /// <returns>gcd(a,b)</returns>
        /// <example>
        /// <code>
        /// long x,y,d;
        /// d = Fn.Gcd(45,18,out x, out y);
        /// -> d == 9 && x == 1 && y == -2
        /// </code>
        /// The gcd of 45 and 18 is 9: 18 = 2*9, 45 = 5*9. 9 = 1*45 -2*18, therefore x=1 and y=-2.
        /// </example>
        public static long Gcd(long a, long b, out long x, out long y)
        {
            long rem, quot, tmp;
            long mp = 1, np = 0, m = 0, n = 1;

            while (b != 0)
            {
                quot = a / b;
                rem = a % b;
                a = b;
                b = rem;

                tmp = m;
                m = mp - quot * m;
                mp = tmp;

                tmp = n;
                n = np - quot * n;
                np = tmp;
            }

            if (a >= 0)
            {
                x = mp;
                y = np;
                return a;
            }
            else
            {
                x = -mp;
                y = -np;
                return -a;
            }
        }

        /// <summary>
        /// Returns the least common multiple of two integers using euclids algorithm.
        /// </summary>
        /// <returns>lcm(a,b)</returns>
        public static long Lcm(long a, long b)
        {
            // TODO: Direct Implementation for preventing overflows.
            if (a == 0 && b == 0)
                return 0;
            return Math.Abs(a * b) / Gcd(a, b);
        }

        /// <summary>
        /// Returns the natural logarithm of Gamma for a real value > 0
        /// </summary>
        /// <param name="xx">A real value for Gamma calculation</param>
        /// <returns>A value ln|Gamma(xx))| for xx > 0</returns>
        public static double GammaLn(double xx)
        {
            // TODO: check
            double x, y, ser, temp;
            double[] coefficient = new double[]{76.18009172947146,-86.50535032941677,
                                                   24.01409824083091,-1.231739572450155,0.1208650973866179e-2,-0.5395239384953e-5};
            int j;
            y = x = xx;
            temp = x + 5.5;
            temp -= ((x + 0.5) * Math.Log(temp));
            ser = 1.000000000190015;
            for (j = 0; j <= 5; j++)
                ser += (coefficient[j] / ++y);
            return -temp + Math.Log(2.50662827463100005 * ser / x);
        }

        private static double[] coefficient = new double[]{76.18009172947146,-86.50535032941677,
                                                   24.01409824083091,-1.231739572450155,0.1208650973866179e-2,-0.5395239384953e-5};

        /// <summary>
        /// Returns Gamma for a real value > 0
        /// </summary>
        /// <param name="xx">A real value for Gamma calculation</param>
        /// <returns>A value Gamma(xx))| for xx > 0</returns>
        /*public static double Gamma(double xx)
        {
            // TODO: check
            // TODO: check
            double x, y, ser, temp;

            int j;
            y = x = xx;
            temp = x + 5.5;
            temp -= ((x + 0.5) * Math.Log(temp));
            ser = 1.000000000190015;
            for (j = 0; j <= 5; j++)
                ser += (coefficient[j] / ++y);
            return Math.Exp(-temp) * 2.50662827463100005 * ser / x;

            //return Math.Exp(GammaLn(xx-1));
        }*/

        /// <summary>
        /// Returns a factorial of an integer number (n!)
        /// </summary>
        /// <param name="n">The value to be factorialized</param>
        /// <returns>The double precision result</returns>
        public static double Factorial(int n)
        {
            // TODO: check
            int ntop = 4;
            double[] a = new double[32];
            a[0] = 1.0; a[1] = 1.0; a[2] = 2.0; a[3] = 6.0; a[4] = 24.0;
            int j;
            if (n < 0)
                throw new ArgumentException("Factorial expects a positive argument", "n");
            if (n > 32)
                return Math.Exp(GammaLn(n + 1.0));
            while (ntop < n)
            {
                j = ntop++;
                a[ntop] = a[j] * ntop;
            }
            return a[n];
        }

        /// <summary>
        /// Returns a binomial coefficient of n and k as a double precision number
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n)
                return 0;
            return Math.Floor(0.5 + Math.Exp(FactorialLn(n) - FactorialLn(k) - FactorialLn(n - k)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double FactorialLn(int n)
        {
            // TODO: check
            double[] a = new double[101];
            if (n < 0)
                throw new ArgumentException("Factorial expects a positive argument", "n");
            if (n <= 1)
                return 0.0d;
            if (n <= 100)
            {
                a[n] = GammaLn(n + 1.0d);
                return (a[n] == 0.0d) ? a[n] : (a[n]); // TODO: historic hulk?
            }
            else
            {
                return GammaLn(n + 1.0d);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        public static double Beta(double z, double w)
        {
            return Math.Exp(GammaLn(z) + GammaLn(w) - GammaLn(z + w));
        }

        private static double[] P = {
  1.60119522476751861407E-4,
  1.19135147006586384913E-3,
  1.04213797561761569935E-2,
  4.76367800457137231464E-2,
  2.07448227648435975150E-1,
  4.94214826801497100753E-1,
  9.99999999999999996796E-1
};

        private static double[] Q = {
-2.31581873324120129819E-5,
 5.39605580493303397842E-4,
-4.45641913851797240494E-3,
 1.18139785222060435552E-2,
 3.58236398605498653373E-2,
-2.34591795718243348568E-1,
 7.14304917030273074085E-2,
 1.00000000000000000320E0
};

        //static double MAXGAM = 171.624376956302725;
        //static double LOGPI = 1.14472988584940017414;

        /* Stirling's formula for the gamma function */

        private static double[] STIR = {
 7.87311395793093628397E-4,
-2.29549961613378126380E-4,
-2.68132617805781232825E-3,
 3.47222221605458667310E-3,
 8.33333333333482257126E-2,
};

        private static double MAXSTIR = 143.01608;
        private static double SQTPI = 2.50662827463100050242;

        /* Gamma function computed by Stirling's formula.
         * The polynomial STIR is valid for 33 <= x <= 172.
         */

        private static double stirf(double x)
        {
            double y, w, v;

            w = 1.0 / x;
            w = 1.0 + w * polevl(w, STIR, 4);
            y = Math.Exp(x);
            if (x > MAXSTIR)
            { /* Avoid overflow in pow() */
                v = Math.Pow(x, 0.5 * x - 0.25);
                y = v * (v / y);
            }
            else
            {
                y = Math.Pow(x, x - 0.5) / y;
            }
            y = SQTPI * y * w;
            return (y);
        }

        private static double polevl(double x, double[] coef, int N)
        {
            double ans;
            int i;

            int j = 0;
            ans = coef[j++];
            i = N;

            do
                ans = ans * x + coef[j++];
            while (--i != 0);

            return ans;
        }

        public static double gamma(double x)
        {
            double p, q, z;
            int i;

            int sgngam = 1;

            q = Math.Abs(x);

            if (q > 33.0)
            {
                if (x < 0.0)
                {
                    p = (int)Math.Floor(q);
                    if (p == q)
                        return double.NaN;
                    i = (int)p;
                    if ((i & 1) == 0)
                        sgngam = -1;
                    z = q - p;
                    if (z > 0.5)
                    {
                        p += 1.0;
                        z = q - p;
                    }
                    z = q * Math.Sin(Math.PI * z);
                    if (z == 0.0)
                        return double.NaN;
                    z = Math.Abs(z);
                    z = Math.PI / (z * stirf(q));
                }
                else
                {
                    z = stirf(x);
                }
                return (sgngam * z);
            }

            z = 1.0;
            while (x >= 3.0)
            {
                x -= 1.0;
                z *= x;
            }

            while (x < 0.0)
            {
                if (x > -1.0E-9)
                    goto small;
                z /= x;
                x += 1.0;
            }

            while (x < 2.0)
            {
                if (x < 1.0E-9)
                    goto small;
                z /= x;
                x += 1.0;
            }

            if (x == 2.0)
                return (z);

            x -= 2.0;
            p = polevl(x, P, 6);
            q = polevl(x, Q, 7);
            return (z * p / q);

        small:
            if (x == 0.0)
            {
                return double.NaN;
            }
            else
                return (z / ((1.0 + 0.5772156649015329 * x) * x));
        }
    }
}