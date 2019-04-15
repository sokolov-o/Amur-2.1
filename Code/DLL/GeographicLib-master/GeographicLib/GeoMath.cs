/**
 * Implementation of the net.sf.geographiclib.GeoMath class
 *
 * Copyright (c) Charles Karney (2013) <charles@karney.com> and licensed
 * under the MIT/X11 License.  For more information, see
 * http://geographiclib.sourceforge.net/
 **********************************************************************/
using System;
namespace GeographicLib
{ 

/**
 * Mathematical functions needed by GeographicLib.
 * <p>
 * Define mathematical functions and constants so that any version of Java
 * can be used.
 **********************************************************************/
public class GeoMath {
  /**
   * The number of binary Digits in the fraction of a double precision
   * number (equivalent to C++'s {@code numeric_limits<double>::Digits}).
   **********************************************************************/
  public static readonly int Digits = 53;
  /**
   * Equivalent to C++'s {@code numeric_limits<double>::Epsilon()}.  In Java
   * version 1.5 and later, Math.ulp(1.0) can be used.
   **********************************************************************/
  public static readonly double Epsilon = Math.Pow(0.5, Digits - 1);
  /**
   * Equivalent to C++'s {@code numeric_limits<double>::Min()}.  In Java
   * version 1.6 and later, Double.MIN_NORMAL can be used.
   **********************************************************************/
  public static readonly double Min = Math.Pow(0.5, 1022);
  /**
   * The number of radians in a Degree.  In Java version 1.2 and later,
   * Math.toRadians and Math.toDegrees can be used.
   **********************************************************************/
  public static readonly double Degree = Math.PI / 180;

  /**
   * Square a number.
   * <p>
   * @param x the argument.
   * @return <i>x</i><sup>2</sup>.
   **********************************************************************/
  public static double Sq(double x) { return x * x; }

  /**
   * The hypotenuse function avoiding underflow and overflow.  In Java version
   * 1.5 and later, Math.Hypot can be used.
   * <p>
   * @param x the First argument.
   * @param y the Second argument.
   * @return sqrt(<i>x</i><sup>2</sup> + <i>y</i><sup>2</sup>).
   **********************************************************************/
  public static double Hypot(double x, double y) {
    x = Math.Abs(x); y = Math.Abs(y);
    double a = Math.Max(x, y), b = Math.Min(x, y) / (a != 0 ? a : 1);
    return a * Math.Sqrt(1 + b * b);
    // For an alternative method see
    // C. Moler and D. Morrision (1983) http://dx.doi.org/10.1147/rd.276.0577
    // and A. A. Dubrulle (1983) http://dx.doi.org/10.1147/rd.276.0582
  }

  /**
   * log(1 + <i>x</i>) accurate near <i>x</i> = 0.  In Java version 1.5 and
   * later, Math.Log1p can be used.
   * <p>
   * This is taken from D. Goldberg,
   * <a href="http://dx.doi.org/10.1145/103162.103163">What every computer
   * scientist should know about floating-point arithmetic</a> (1991),
   * Theorem 4.  See also, N. J. Higham, Accuracy and Stability of Numerical
   * Algorithms, 2nd Edition (SIAM, 2002), Answer to Problem 1.5, p 528.
   * <p>
   * @param x the argument.
   * @return log(1 + <i>x</i>).
   **********************************************************************/
  public static double Log1p(double x) {
    double
      y = 1 + x,
      z = y - 1;
    // Here's the explanation for this magic: y = 1 + z, exactly, and z
    // approx x, thus log(y)/z (which is nearly constant near z = 0) returns
    // a good approximation to the true log(1 + x)/x.  The multiplication x *
    // (log(y)/z) introduces little additional error.
    return z == 0 ? x : x * Math.Log(y) / z;
  }

  /**
   * The inverse hyperbolic tangent function.  This is defined in terms of
   * GeoMath.Log1p(<i>x</i>) in order to maintain accuracy near <i>x</i> = 0.
   * In addition, the odd parity of the function is enforced.
   * <p>
   * @param x the argument.
   * @return Atanh(<i>x</i>).
   **********************************************************************/
  public static double Atanh(double x)  {
    double y = Math.Abs(x);     // Enforce odd parity
    y = Math.Log(2 * y/(1 - y))/2;
    return x < 0 ? -y : y;
  }

  /**
   * The cube root function.  In Java version 1.5 and later, Math.cbrt can be
   * used.
   * <p>
   * @param x the argument.
   * @return the real cube root of <i>x</i>.
   **********************************************************************/
  public static double cbrt(double x) {
    double y = Math.Pow(Math.Abs(x), 1/3.0); // Return the real cube root
    return x < 0 ? -y : y;
  }

  /**
   * The error-free sum of two numbers.
   * <p>
   * @param u the First number in the sum.
   * @param v the Second number in the sum.
   * @return Pair(<i>s</i>, <i>t</i>) with <i>s</i> = round(<i>u</i> +
   *   <i>v</i>) and <i>t</i> = <i>u</i> + <i>v</i> - <i>s</i>.
   * <p>
   * See D. E. Knuth, TAOCP, Vol 2, 4.2.2, Theorem B.
   **********************************************************************/
  public static Pair sum(double u, double v) {
    double s = u + v;
    double up = s - v;
    double vpp = s - up;
    up -= u;
    vpp -= v;
    double t = -(up + vpp);
    // u + v =       s      + t
    //       = round(u + v) + t
    return new Pair(s, t);
  }

  /**
   * Normalize an angle (restricted input range).
   * <p>
   * @param x the angle in degrees.
   * @return the angle reduced to the range [&minus;180&deg;, 180&deg;).
   * <p>
   * <i>x</i> must lie in [&minus;540&deg;, 540&deg;).
   **********************************************************************/
  public static double AngNormalize(double x)
  { return x >= 180 ? x - 360 : (x < -180 ? x + 360 : x); }

  /**
   * Normalize an arbitrary angle.
   * <p>
   * @param x the angle in degrees.
   * @return the angle reduced to the range [&minus;180&deg;, 180&deg;).
   * <p>
   * The range of <i>x</i> is unrestricted.
   **********************************************************************/
  public static double AngNormalize2(double x)
  { return AngNormalize(x % 360.0); }

  /**
   * Difference of two angles reduced to [&minus;180&deg;, 180&deg;]
   * <p>
   * @param x the First angle in degrees.
   * @param y the Second angle in degrees.
   * @return <i>y</i> &minus; <i>x</i>, reduced to the range [&minus;180&deg;,
   *   180&deg;].
   * <p>
   * <i>x</i> and <i>y</i> must both lie in [&minus;180&deg;, 180&deg;].  The
   * result is equivalent to computing the difference exactly, reducing it to
   * (&minus;180&deg;, 180&deg;] and rounding the result.  Note that this
   * prescription allows &minus;180&deg; to be returned (e.g., if <i>x</i> is
   * tiny and negative and <i>y</i> = 180&deg;).
   **********************************************************************/
  public static double AngDiff(double x, double y) {
    double d, t;
    { Pair r = sum(-x, y); d = r.First; t = r.Second; }
    if ((d - 180.0) + t > 0.0) // y - x > 180
      d -= 360.0;            // exact
    else if ((d + 180.0) + t <= 0.0) // y - x <= -180
      d += 360.0;            // exact
    return d + t;
  }
  /**
   * Test for finiteness.
   * <p>
   * @param x the argument.
   * @return true if number is finite, false if NaN or infinite.
   **********************************************************************/
  public static bool isfinite(double x) {
    return Math.Abs(x) <= Double.MaxValue;
  }

  public static double LatFix(double x)
  {
      return Math.Abs(x) > 90 ? Double.NaN : x;
  }

    public static double AngRound(double x) {
    // The makes the smallest gap in x = 1/16 - nextafter(1/16, 0) = 1/2^57
    // for reals = 0.7 pm on the earth if x is an angle in degrees.  (This
    // is about 1000 times more resolution than we get with angles around 90
    // degrees.)  We use this to avoid having to deal with near singular
    // cases when x is non-zero but tiny (e.g., 1.0e-200).  This converts -0 to
    // +0; however tiny negative numbers get converted to -0.
    const double z = 1/16.0;
    if (x == 0) return 0;
    double y = Math.Abs(x);
    // The compiler mustn't "simplify" z - (z - y) to y
    y = y < z ? z - (z - y) : y;
    return x < 0 ? -y : y;
  }

    public static Pair AngDiff(double x, double y, bool retPair)
    {
        double d, t;
        {
            Pair r = sum(AngNormalize(x), AngNormalize(-y));
            d = -AngNormalize(r.First); t = r.Second;
        }
        return sum(d == 180 && t < 0 ? -180 : d, -t);
    }

    /**
     * Evaluate the sine and cosine function with the argument in degrees
     *
     * @param x in degrees.
     * @return Pair(<i>s</i>, <i>t</i>) with <i>s</i> = sin(<i>x</i>) and
     *   <i>c</i> = cos(<i>x</i>).
     *
     * The results obey exactly the elementary properties of the trigonometric
     * functions, e.g., sin 9&deg; = cos 81&deg; = &minus; sin 123456789&deg;.
     **********************************************************************/
    public static Pair sincosd(double x)
    {
        // In order to minimize round-off errors, this function exactly reduces
        // the argument to the range [-45, 45] before converting it to radians.
        double r; int q;
        r = x % 360.0;
        q = (int)Math.Floor(r / 90 + 0.5);
        r -= 90 * q;
        // now abs(r) <= 45
        r = r * Math.PI / 180.0;
        // Possibly could call the gnu extension sincos
        double s = Math.Sin(r), c = Math.Cos(r);
        double sinx, cosx;
        switch (q & 3)
        {
            case 0: sinx = s; cosx = c; break;
            case 1: sinx = c; cosx = 0 - s; break;
            case 2: sinx = 0 - s; cosx = 0 - c; break;
            default: sinx = 0 - c; cosx = s; break; // case 3
        }
        return new Pair(sinx, cosx);
    }
    public static double atan2d(double y, double x)
    {
        // In order to minimize round-off errors, this function rearranges the
        // arguments so that result of atan2 is in the range [-pi/4, pi/4] before
        // converting it to degrees and mapping the result to the correct
        // quadrant.
        int q = 0;
        if (Math.Abs(y) > Math.Abs(x)) { double t; t = x; x = y; y = t; q = 2; }
        if (x < 0) { x = -x; ++q; }
        // here x >= 0 and x >= abs(y), so angle is in [-pi/4, pi/4]
        double ang = Math.Atan2(y, x)*180.0/Math.PI;
        switch (q)
        {
            // Note that atan2d(-0.0, 1.0) will return -0.  However, we expect that
            // atan2d will not be called with y = -0.  If need be, include
            //
            //   case 0: ang = 0 + ang; break;
            //
            // and handle mpfr as in AngRound.
            case 1: ang = (y > 0 ? 180 : -180) - ang; break;
            case 2: ang = 90 - ang; break;
            case 3: ang = -90 + ang; break;
        }
        return ang;
    }

    public static Pair norm(double sinx, double cosx)
    {
        double r = hypot(sinx, cosx);
        return new Pair(sinx / r, cosx / r);
    }

    public static double hypot(double x, double y)
    {
        x = Math.Abs(x); y = Math.Abs(y);
        double a = Math.Max(x, y), b = Math.Min(x, y) / (a != 0 ? a : 1);
        return a * Math.Sqrt(1 + b * b);
        // For an alternative method see
        // C. Moler and D. Morrision (1983) https://doi.org/10.1147/rd.276.0577
        // and A. A. Dubrulle (1983) https://doi.org/10.1147/rd.276.0582
    }
  private GeoMath() {}
}
}