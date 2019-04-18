/**
 * Implementation of the net.sf.geographiclib.GeodesicLine class
 *
 * Copyright (c) Charles Karney (2013) <charles@karney.com> and licensed
 * under the MIT/X11 License.  For more information, see
 * http://geographiclib.sourceforge.net/
 **********************************************************************/
using System;
namespace GeographicLib
{

    /**
     * A geodesic line.
     * <p>
     * GeodesicLine facilitates the determination of a series of points on a single
     * geodesic.  The starting point (<i>lat1</i>, <i>lon1</i>) and the azimuth
     * <i>azi1</i> are specified in the constructor.  {@link #Position Position}
     * returns the location of point 2 a distance <i>s12</i> along the geodesic.
     * Alternatively {@link #ArcPosition ArcPosition} gives the position of point 2
     * an arc length <i>a12</i> along the geodesic.
     * <p>
     * The calculations are accurate to better than 15 nm (15 nanometers).  See
     * Sec. 9 of
     * <a href="http://arxiv.org/abs/1102.1215v1">arXiv:1102.1215v1</a> for
     * details.  The algorithms used by this class are based on series expansions
     * using the flattening <i>f</i> as a small parameter.  These are only accurate
     * for |<i>f</i>| &lt; 0.02; however reasonably accurate results will be
     * obtained for |<i>f</i>| &lt; 0.2.
     * <p>
     * The algorithms are described in
     * <ul>
     * <li>
     *   C. F. F. Karney,
     *   <a href="http://dx.doi.org/10.1007/s00190-012-0578-z">
     *   Algorithms for geodesics</a>,
     *   J. Geodesy <b>87</b>, 43&ndash;55 (2013);
     *   DOI: <a href="http://dx.doi.org/10.1007/s00190-012-0578-z">
     *   10.1007/s00190-012-0578-z</a>;
     *   addenda: <a href="http://geographiclib.sf.net/geod-addenda.html">
     *   geod-addenda.html</a>.
     * </ul>
     * <p>
     * Here's an example of using this class
     * <pre>
     * {@code
     * import net.sf.geographiclib.*;
     * public class GeodesicLineTest {
     *   public static void main(String[] args) {
     *     // Print waypoints between JFK and SIN
     *     Geodesic geod = Geodesic.WGS84;
     *     double
     *       lat1 = 40.640, lon1 = -73.779, // JFK
     *       lat2 =  1.359, lon2 = 103.989; // SIN
     *     GeodesicData g = geod.Inverse(lat1, lon1, lat2, lon2,
     *                  GeodesicMask.DISTANCE | GeodesicMask.AZIMUTH);
     *     GeodesicLine line = new GeodesicLine(geod, lat1, lon1, g.azi1,
     *                  GeodesicMask.DISTANCE_IN | GeodesicMask.LONGITUDE);
     *     double
     *       s12 = g.s12,
     *       a12 = g.a12,
     *       ds0 = 500e3;        // Nominal distance between points = 500 km
     *     int Num = (int)(Math.ceil(s12 / ds0)); // The number of intervals
     *     {
     *       // Use intervals of equal length
     *       double ds = s12 / Num;
     *       for (int i = 0; i <= Num; ++i) {
     *         g = line.Position(i * ds,
     *                  GeodesicMask.LATITUDE | GeodesicMask.LONGITUDE);
     *         System.out.println(i + " " + g.lat2 + " " + g.lon2);
     *       }
     *     }
     *     {
     *       // Slightly faster, use intervals of equal arc length
     *       double da = a12 / Num;
     *       for (int i = 0; i <= Num; ++i) {
     *         g = line.ArcPosition(i * da,
     *                  GeodesicMask.LATITUDE | GeodesicMask.LONGITUDE);
     *         System.out.println(i + " " + g.lat2 + " " + g.lon2);
     *       }
     *     }
     *   }
     * }}</pre>
     **********************************************************************/
    public class GeodesicLine
    {

        private static readonly int nC1_ = Geodesic.nC1_;
        private static readonly int nC1p_ = Geodesic.nC1p_;
        private static readonly int nC2_ = Geodesic.nC2_;
        private static readonly int nC3_ = Geodesic.nC3_;
        private static readonly int nC4_ = Geodesic.nC4_;

        private double _lat1, _lon1, _azi1;
        private double _a, _f, _b, _c2, _f1, _salp0, _calp0, _k2,
          _salp1, _calp1, _ssig1, _csig1, _dn1, _stau1, _ctau1, _somg1, _comg1,
          _A1m1, _A2m1, _A3c, _B11, _B21, _B31, _A4, _B41;
        // index zero elements of _C1a, _C1pa, _C2a, _C3a are unused
        private double[] _C1a, _C1pa, _C2a, _C3a, _C4a;    // all the elements of _C4a are used
        private int _caps;

        private double _a13, _s13;

        /**
         * Constructor for a geodesic line staring at latitude <i>lat1</i>, longitude
         * <i>lon1</i>, and azimuth <i>azi1</i> (all in degrees).
         * <p>
         * @param g A {@link Geodesic} object used to compute the necessary
         *   information about the GeodesicLine.
         * @param lat1 latitude of point 1 (degrees).
         * @param lon1 longitude of point 1 (degrees).
         * @param azi1 azimuth at point 1 (degrees).
         * <p>
         * <i>lat1</i> should be in the range [&minus;90&deg;, 90&deg;]; <i>lon1</i>
         * and <i>azi1</i> should be in the range [&minus;540&deg;, 540&deg;).
         * <p>
         * If the point is at a pole, the azimuth is defined by keeping <i>lon1</i>
         * fixed, writing <i>lat1</i> = &plusmn;(90&deg; &minus; &Epsilon;), and
         * taking the limit &Epsilon; &rarr; 0+.
         **********************************************************************/
        public GeodesicLine(Geodesic g,
                            double lat1, double lon1, double azi1) :
            this(g, lat1, lon1, azi1, GeodesicMask.ALL)
        { }

        /**
         * Constructor for a geodesic line staring at latitude <i>lat1</i>, longitude
         * <i>lon1</i>, and azimuth <i>azi1</i> (all in degrees) with a subset of the
         * capabilities included.
         * <p>
         * @param g A {@link Geodesic} object used to compute the necessary
         *   information about the GeodesicLine.
         * @param lat1 latitude of point 1 (degrees).
         * @param lon1 longitude of point 1 (degrees).
         * @param azi1 azimuth at point 1 (degrees).
         * @param caps bitor'ed combination of {@link GeodesicMask} values
         *   specifying the capabilities the GeodesicLine object should possess,
         *   i.e., which quantities can be returned in calls to {@link #Position
         *   Position}.
         * <p>
         * The {@link GeodesicMask} values are
         * <ul>
         * <li>
         *   <i>caps</i> |= GeodesicMask.LATITUDE for the latitude <i>lat2</i>; this
         *   is added automatically;
         * <li>
         *   <i>caps</i> |= GeodesicMask.LONGITUDE for the latitude <i>lon2</i>;
         * <li>
         *   <i>caps</i> |= GeodesicMask.AZIMUTH for the latitude <i>azi2</i>; this
         *   is added automatically;
         * <li>
         *   <i>caps</i> |= GeodesicMask.DISTANCE for the distance <i>s12</i>;
         * <li>
         *   <i>caps</i> |= GeodesicMask.REDUCEDLENGTH for the reduced length
         *   <i>m12</i>;
         * <li>
         *   <i>caps</i> |= GeodesicMask.GEODESICSCALE for the geodesic scales
         *   <i>M12</i> and <i>M21</i>;
         * <li>
         *   <i>caps</i> |= GeodesicMask.AREA for the Area <i>S12</i>;
         * <li>
         *   <i>caps</i> |= GeodesicMask.DISTANCE_IN permits the length of the
         *   geodesic to be given in terms of <i>s12</i>; without this capability the
         *   length can only be specified in terms of arc length;
         * <li>
         *   <i>caps</i> |= GeodesicMask.ALL for all of the above;
         * </ul>
         **********************************************************************/
        public GeodesicLine(Geodesic g,
                            double lat1, double lon1, double azi1,
                            int caps)
        {
            _a = g._a;
            _f = g._f;
            _b = g._b;
            _c2 = g._c2;
            _f1 = g._f1;
            // Always allow latitude and azimuth
            _caps = caps | GeodesicMask.LATITUDE | GeodesicMask.AZIMUTH;

            // Guard against underflow in salp0
            azi1 = Geodesic.AngRound(GeoMath.AngNormalize(azi1));
            lon1 = GeoMath.AngNormalize(lon1);
            _lat1 = lat1;
            _lon1 = lon1;
            _azi1 = azi1;
            // alp1 is in [0, pi]
            double alp1 = azi1 * GeoMath.Degree;
            // Enforce sin(pi) == 0 and cos(pi/2) == 0.  Better to face the ensuing
            // problems directly than to skirt them.
            _salp1 = azi1 == -180 ? 0 : Math.Sin(alp1);
            _calp1 = Math.Abs(azi1) == 90 ? 0 : Math.Cos(alp1);
            double cbet1, sbet1, phi;
            phi = lat1 * GeoMath.Degree;
            // Ensure cbet1 = +Epsilon at poles
            sbet1 = _f1 * Math.Sin(phi);
            cbet1 = Math.Abs(lat1) == 90 ? Geodesic.tiny_ : Math.Cos(phi);
            {
                Pair p = Geodesic.SinCosNorm(sbet1, cbet1);
                sbet1 = p.First; cbet1 = p.Second;
            }
            _dn1 = Math.Sqrt(1 + g._ep2 * GeoMath.Sq(sbet1));

            // Evaluate alp0 from sin(alp1) * cos(bet1) = sin(alp0),
            _salp0 = _salp1 * cbet1; // alp0 in [0, pi/2 - |bet1|]
            // Alt: calp0 = Hypot(sbet1, calp1 * cbet1).  The following
            // is slightly better (consider the case salp1 = 0).
            _calp0 = GeoMath.Hypot(_calp1, _salp1 * sbet1);
            // Evaluate sig with tan(bet1) = tan(sig1) * cos(alp1).
            // sig = 0 is nearest northward crossing of equator.
            // With bet1 = 0, alp1 = pi/2, we have sig1 = 0 (equatorial line).
            // With bet1 =  pi/2, alp1 = -pi, sig1 =  pi/2
            // With bet1 = -pi/2, alp1 =  0 , sig1 = -pi/2
            // Evaluate omg1 with tan(omg1) = sin(alp0) * tan(sig1).
            // With alp0 in (0, pi/2], quadrants for sig and omg coincide.
            // No atan2(0,0) ambiguity at poles since cbet1 = +Epsilon.
            // With alp0 = 0, omg1 = 0 for alp1 = 0, omg1 = pi for alp1 = pi.
            _ssig1 = sbet1; _somg1 = _salp0 * sbet1;
            _csig1 = _comg1 = sbet1 != 0 || _calp1 != 0 ? cbet1 * _calp1 : 1;
            {
                Pair p = Geodesic.SinCosNorm(_ssig1, _csig1);
                _ssig1 = p.First; _csig1 = p.Second;
            } // sig1 in (-pi, pi]
            // Geodesic.SinCosNorm(_somg1, _comg1); -- don't need to normalize!

            _k2 = GeoMath.Sq(_calp0) * g._ep2;
            double eps = _k2 / (2 * (1 + Math.Sqrt(1 + _k2)) + _k2);

            if ((_caps & GeodesicMask.CAP_C1) != 0)
            {
                _A1m1 = Geodesic.A1m1f(eps);
                _C1a = new double[nC1_ + 1];
                Geodesic.C1f(eps, _C1a);
                _B11 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C1a);
                double s = Math.Sin(_B11), c = Math.Cos(_B11);
                // tau1 = sig1 + B11
                _stau1 = _ssig1 * c + _csig1 * s;
                _ctau1 = _csig1 * c - _ssig1 * s;
                // Not necessary because C1pa reverts C1a
                //    _B11 = -SinCosSeries(true, _stau1, _ctau1, _C1pa, nC1p_);
            }

            if ((_caps & GeodesicMask.CAP_C1p) != 0)
            {
                _C1pa = new double[nC1p_ + 1];
                Geodesic.C1pf(eps, _C1pa);
            }

            if ((_caps & GeodesicMask.CAP_C2) != 0)
            {
                _C2a = new double[nC2_ + 1];
                _A2m1 = Geodesic.A2m1f(eps);
                Geodesic.C2f(eps, _C2a);
                _B21 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C2a);
            }

            if ((_caps & GeodesicMask.CAP_C3) != 0)
            {
                _C3a = new double[nC3_];
                g.C3f(eps, _C3a);
                _A3c = -_f * _salp0 * g.A3f(eps);
                _B31 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C3a);
            }

            if ((_caps & GeodesicMask.CAP_C4) != 0)
            {
                _C4a = new double[nC4_];
                g.C4f(eps, _C4a);
                // Multiplier = a^2 * e^2 * cos(alpha0) * sin(alpha0)
                _A4 = GeoMath.Sq(_a) * _calp0 * _salp0 * g._e2;
                _B41 = Geodesic.SinCosSeries(false, _ssig1, _csig1, _C4a);
            }
        }

        /**
         * A default constructor.  If GeodesicLine.Position is called on the
         * resulting object, it returns immediately (without doing any calculations).
         * The object can be set with a call to {@link Geodesic.Line}.  Use {@link
         * Init()} to test whether object is still in this uninitialized state.
         * (This constructor was useful in C++, e.g., to allow vectors of
         * GeodesicLine objects.  It may not be needed in Java, so make it private.)
         **********************************************************************/
        private GeodesicLine() { _caps = 0; }

        /**
         * Compute the position of point 2 which is a distance <i>s12</i> (meters)
         * from point 1.
         * <p>
         * @param s12 distance between point 1 and point 2 (meters); it can be
         *   negative.
         * @return a {@link GeodesicData} object with the following fields:
         *   <i>lat1</i>, <i>lon1</i>, <i>azi1</i>, <i>lat2</i>, <i>lon2</i>,
         *   <i>azi2</i>, <i>s12</i>, <i>a12</i>.  Some of these results may be
         *   missing if the GeodesicLine did not include the relevant capability.
         * <p>
         * The values of <i>lon2</i> and <i>azi2</i> returned are in the range
         * [&minus;180&deg;, 180&deg;).
         * <p>
         * The GeodesicLine object <i>must</i> have been constructed with <i>caps</i>
         * |= {@link GeodesicMask#DISTANCE_IN}; otherwise no parameters are set.
         **********************************************************************/
        public GeodesicData Position(double s12)
        {
            return Position(false, s12,
                            GeodesicMask.LATITUDE | GeodesicMask.LONGITUDE |
                            GeodesicMask.AZIMUTH);
        }
        /**
         * Compute the position of point 2 which is a distance <i>s12</i> (meters)
         * from point 1 and with a subset of the geodesic results returned.
         * <p>
         * @param s12 distance between point 1 and point 2 (meters); it can be
         *   negative.
         * @param outmask a bitor'ed combination of {@link GeodesicMask} values
         *   specifying which results should be returned.
         * @return a {@link GeodesicData} object including the requested results.
         * <p>
         * The GeodesicLine object <i>must</i> have been constructed with <i>caps</i>
         * |= {@link GeodesicMask#DISTANCE_IN}; otherwise no parameters are set.
         * Requesting a value which the GeodesicLine object is not capable of
         * computing is not an error (no parameters will be set).
         **********************************************************************/
        public GeodesicData Position(double s12, int outmask)
        {
            return Position(false, s12, outmask);
        }

        /**
         * Compute the position of point 2 which is an arc length <i>a12</i>
         * (degrees) from point 1.
         * <p>
         * @param a12 arc length between point 1 and point 2 (degrees); it can
         *   be negative.
         * @return a {@link GeodesicData} object with the following fields:
         *   <i>lat1</i>, <i>lon1</i>, <i>azi1</i>, <i>lat2</i>, <i>lon2</i>,
         *   <i>azi2</i>, <i>s12</i>, <i>a12</i>.  Some of these results may be
         *   missing if the GeodesicLine did not include the relevant capability.
         * <p>
         * The values of <i>lon2</i> and <i>azi2</i> returned are in the range
         * [&minus;180&deg;, 180&deg;).
         * <p>
         * The GeodesicLine object <i>must</i> have been constructed with <i>caps</i>
         * |= {@link GeodesicMask#DISTANCE_IN}; otherwise no parameters are set.
         **********************************************************************/
        public GeodesicData ArcPosition(double a12)
        {
            return Position(true, a12,
                            GeodesicMask.LATITUDE | GeodesicMask.LONGITUDE |
                            GeodesicMask.AZIMUTH | GeodesicMask.DISTANCE);
        }
        /**
         * Compute the position of point 2 which is an arc length <i>a12</i>
         * (degrees) from point 1 and with a subset of the geodesic results returned.
         * <p>
         * @param a12 arc length between point 1 and point 2 (degrees); it can
         *   be negative.
         * @param outmask a bitor'ed combination of {@link GeodesicMask} values
         *   specifying which results should be returned.
         * @return a {@link GeodesicData} object giving <i>lat1</i>, <i>lon2</i>,
         *   <i>azi2</i>, and <i>a12</i>.
         * <p>
         * The GeodesicLine object <i>must</i> have been constructed with <i>caps</i>
         * |= {@link GeodesicMask#DISTANCE_IN}; otherwise no parameters are set.
         * Requesting a value which the GeodesicLine object is not capable of
         * computing is not an error (no parameters will be set).
         **********************************************************************/
        public GeodesicData ArcPosition(double a12, int outmask)
        {
            return Position(true, a12, outmask);
        }

        /**
         * The general position function.  {@link #Position(double, int) Position}
         * and {@link #ArcPosition(double, int) ArcPosition} are defined in terms of
         * this function.
         * <p>
         * @param arcmode bool flag determining the meaning of the Second
         *   parameter; if arcmode is false, then the GeodesicLine object must have
         *   been constructed with <i>caps</i> |= {@link GeodesicMask#DISTANCE_IN}.
         * @param s12_a12 if <i>arcmode</i> is false, this is the distance between
         *   point 1 and point 2 (meters); otherwise it is the arc length between
         *   point 1 and point 2 (degrees); it can be negative.
         * @param outmask a bitor'ed combination of {@link GeodesicMask} values
         *   specifying which results should be returned.
         * @return a {@link GeodesicData} object with the requested results.
         * <p>
         * The {@link GeodesicMask} values possible for <i>outmask</i> are
         * <ul>
         * <li>
         *   <i>outmask</i> |= GeodesicMask.LATITUDE for the latitude <i>lat2</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.LONGITUDE for the latitude <i>lon2</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.AZIMUTH for the latitude <i>azi2</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.DISTANCE for the distance <i>s12</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.REDUCEDLENGTH for the reduced length
         *   <i>m12</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.GEODESICSCALE for the geodesic scales
         *   <i>M12</i> and <i>M21</i>.
         * <li>
         *   <i>outmask</i> |= GeodesicMask.AREA for the Area <i>S12</i>.
         * </ul>
         * <p>
         * Requesting a value which the GeodesicLine object is not capable of
         * computing is not an error; Double.NaN is returned instead.
         **********************************************************************/
        public GeodesicData Position(bool arcmode, double s12_a12, int outmask)
        {
            outmask &= _caps & GeodesicMask.OUT_ALL;
            GeodesicData r = new GeodesicData();
            if (!(Init() &&
                   (arcmode ||
                    (_caps & GeodesicMask.DISTANCE_IN & GeodesicMask.OUT_ALL) != 0)))
                // Uninitialized or impossible distance calculation requested
                return r;
            r.lat1 = _lat1; r.lon1 = _lon1; r.azi1 = _azi1;

            // Avoid warning about uninitialized B12.
            double sig12, ssig12, csig12, B12 = 0, AB1 = 0;
            if (arcmode)
            {
                // Interpret s12_a12 as spherical arc length
                r.a12 = s12_a12;
                sig12 = s12_a12 * GeoMath.Degree;
                double s12a = Math.Abs(s12_a12);
                s12a -= 180 * Math.Floor(s12a / 180);
                ssig12 = s12a == 0 ? 0 : Math.Sin(sig12);
                csig12 = s12a == 90 ? 0 : Math.Cos(sig12);
            }
            else
            {
                // Interpret s12_a12 as distance
                r.s12 = s12_a12;
                double
                  tau12 = s12_a12 / (_b * (1 + _A1m1)),
                  s = Math.Sin(tau12),
                  c = Math.Cos(tau12);
                // tau2 = tau1 + tau12
                B12 = -Geodesic.SinCosSeries(true,
                                              _stau1 * c + _ctau1 * s,
                                              _ctau1 * c - _stau1 * s,
                                              _C1pa);
                sig12 = tau12 - (B12 - _B11);
                r.a12 = sig12 / GeoMath.Degree;
                ssig12 = Math.Sin(sig12); csig12 = Math.Cos(sig12);
                if (Math.Abs(_f) > 0.01)
                {
                    // Reverted distance series is inaccurate for |f| > 1/100, so correct
                    // sig12 with 1 Newton iteration.  The following table shows the
                    // approximate maximum error for a = WGS_a() and various f relative to
                    // GeodesicExact.
                    //     erri = the error in the inverse solution (nm)
                    //     errd = the error in the direct solution (series only) (nm)
                    //     errda = the error in the direct solution (series + 1 Newton) (nm)
                    //
                    //       f     erri  errd errda
                    //     -1/5    12e6 1.2e9  69e6
                    //     -1/10  123e3  12e6 765e3
                    //     -1/20   1110 108e3  7155
                    //     -1/50  18.63 200.9 27.12
                    //     -1/100 18.63 23.78 23.37
                    //     -1/150 18.63 21.05 20.26
                    //      1/150 22.35 24.73 25.83
                    //      1/100 22.35 25.03 25.31
                    //      1/50  29.80 231.9 30.44
                    //      1/20   5376 146e3  10e3
                    //      1/10  829e3  22e6 1.5e6
                    //      1/5   157e6 3.8e9 280e6
                    double
                      ssig2 = _ssig1 * csig12 + _csig1 * ssig12,
                      csig2 = _csig1 * csig12 - _ssig1 * ssig12;
                    B12 = Geodesic.SinCosSeries(true, ssig2, csig2, _C1a);
                    double serr = (1 + _A1m1) * (sig12 + (B12 - _B11)) - s12_a12 / _b;
                    sig12 = sig12 - serr / Math.Sqrt(1 + _k2 * GeoMath.Sq(ssig2));
                    ssig12 = Math.Sin(sig12); csig12 = Math.Cos(sig12);
                    // Update B12 below
                }
            }

            double omg12, lam12, lon12;
            double ssig3, csig3, sbet3, cbet3, somg3, comg3, salp3, calp3;
            // sig2 = sig1 + sig12
            ssig3 = _ssig1 * csig12 + _csig1 * ssig12;
            csig3 = _csig1 * csig12 - _ssig1 * ssig12;
            double dn2 = Math.Sqrt(1 + _k2 * GeoMath.Sq(ssig3));
            if ((outmask & (GeodesicMask.DISTANCE | GeodesicMask.REDUCEDLENGTH |
                            GeodesicMask.GEODESICSCALE)) != 0)
            {
                if (arcmode || Math.Abs(_f) > 0.01)
                    B12 = Geodesic.SinCosSeries(true, ssig3, csig3, _C1a);
                AB1 = (1 + _A1m1) * (B12 - _B11);
            }
            // sin(bet2) = cos(alp0) * sin(sig2)
            sbet3 = _calp0 * ssig3;
            // Alt: cbet3 = Hypot(csig3, salp0 * ssig3);
            cbet3 = GeoMath.Hypot(_salp0, _calp0 * csig3);
            if (cbet3 == 0)
                // I.e., salp0 = 0, csig3 = 0.  Break the degeneracy in this case
                cbet3 = csig3 = Geodesic.tiny_;
            // tan(omg2) = sin(alp0) * tan(sig2)
            somg3 = _salp0 * ssig3; comg3 = csig3;  // No need to normalize
            // tan(alp0) = cos(sig2)*tan(alp2)
            salp3 = _salp0; calp3 = _calp0 * csig3; // No need to normalize
            // omg12 = omg2 - omg1
            omg12 = Math.Atan2(somg3 * _comg1 - comg3 * _somg1,
                          comg3 * _comg1 + somg3 * _somg1);

            if ((outmask & GeodesicMask.DISTANCE) != 0 && arcmode)
                r.s12 = _b * ((1 + _A1m1) * sig12 + AB1);

            if ((outmask & GeodesicMask.LONGITUDE) != 0)
            {
                lam12 = omg12 + _A3c *
                  (sig12 + (Geodesic.SinCosSeries(true, ssig3, csig3, _C3a)
                             - _B31));
                lon12 = lam12 / GeoMath.Degree;
                // Use GeoMath.AngNormalize2 because longitude might have wrapped
                // multiple times.
                lon12 = GeoMath.AngNormalize2(lon12);
                r.lon2 = GeoMath.AngNormalize(_lon1 + lon12);
            }

            if ((outmask & GeodesicMask.LATITUDE) != 0)
                r.lat2 = Math.Atan2(sbet3, _f1 * cbet3) / GeoMath.Degree;

            if ((outmask & GeodesicMask.AZIMUTH) != 0)
                // minus signs give range [-180, 180). 0- converts -0 to +0.
                r.azi2 = 0 - Math.Atan2(-salp3, calp3) / GeoMath.Degree;

            if ((outmask &
                 (GeodesicMask.REDUCEDLENGTH | GeodesicMask.GEODESICSCALE)) != 0)
            {
                double
                  B22 = Geodesic.SinCosSeries(true, ssig3, csig3, _C2a),
                  AB2 = (1 + _A2m1) * (B22 - _B21),
                  J12 = (_A1m1 - _A2m1) * sig12 + (AB1 - AB2);
                if ((outmask & GeodesicMask.REDUCEDLENGTH) != 0)
                    // Add parens around (_csig1 * ssig3) and (_ssig1 * csig3) to ensure
                    // accurate cancellation in the case of coincident points.
                    r.m12 = _b * ((dn2 * (_csig1 * ssig3) - _dn1 * (_ssig1 * csig3))
                                - _csig1 * csig3 * J12);
                if ((outmask & GeodesicMask.GEODESICSCALE) != 0)
                {
                    double t = _k2 * (ssig3 - _ssig1) * (ssig3 + _ssig1) / (_dn1 + dn2);
                    r.M12 = csig12 + (t * ssig3 - csig3 * J12) * _ssig1 / _dn1;
                    r.M21 = csig12 - (t * _ssig1 - _csig1 * J12) * ssig3 / dn2;
                }
            }

            if ((outmask & GeodesicMask.AREA) != 0)
            {
                double
                  B42 = Geodesic.SinCosSeries(false, ssig3, csig3, _C4a);
                double salp12, calp12;
                if (_calp0 == 0 || _salp0 == 0)
                {
                    // alp12 = alp2 - alp1, used in atan2 so no need to normalized
                    salp12 = salp3 * _calp1 - calp3 * _salp1;
                    calp12 = calp3 * _calp1 + salp3 * _salp1;
                    // The right thing appears to happen if alp1 = +/-180 and alp2 = 0, viz
                    // salp12 = -0 and alp12 = -180.  However this depends on the sign
                    // being attached to 0 correctly.  The following ensures the correct
                    // behavior.
                    if (salp12 == 0 && calp12 < 0)
                    {
                        salp12 = Geodesic.tiny_ * _calp1;
                        calp12 = -1;
                    }
                }
                else
                {
                    // tan(alp) = tan(alp0) * sec(sig)
                    // tan(alp2-alp1) = (tan(alp2) -tan(alp1)) / (tan(alp2)*tan(alp1)+1)
                    // = calp0 * salp0 * (csig1-csig3) / (salp0^2 + calp0^2 * csig1*csig3)
                    // If csig12 > 0, write
                    //   csig1 - csig3 = ssig12 * (csig1 * ssig12 / (1 + csig12) + ssig1)
                    // else
                    //   csig1 - csig3 = csig1 * (1 - csig12) + ssig12 * ssig1
                    // No need to normalize
                    salp12 = _calp0 * _salp0 *
                      (csig12 <= 0 ? _csig1 * (1 - csig12) + ssig12 * _ssig1 :
                       ssig12 * (_csig1 * ssig12 / (1 + csig12) + _ssig1));
                    calp12 = GeoMath.Sq(_salp0) + GeoMath.Sq(_calp0) * _csig1 * csig3;
                }
                r.S12 = _c2 * Math.Atan2(salp12, calp12) + _A4 * (B42 - _B41);
            }

            return r;
        }

        /**
         * @return true if the object has been initialized.
         **********************************************************************/
        private bool Init() { return _caps != 0; }

        /**
         * @return <i>lat1</i> the latitude of point 1 (degrees).
         **********************************************************************/
        public double Latitude()
        { return Init() ? _lat1 : Double.NaN; }

        /**
         * @return <i>lon1</i> the longitude of point 1 (degrees).
         **********************************************************************/
        public double Longitude()
        { return Init() ? _lon1 : Double.NaN; }

        /**
         * @return <i>azi1</i> the azimuth (degrees) of the geodesic line at point 1.
         **********************************************************************/
        public double Azimuth()
        { return Init() ? _azi1 : Double.NaN; }

        /**
         * @return <i>azi0</i> the azimuth (degrees) of the geodesic line as it
         *   crosses the equator in a northward direction.
         **********************************************************************/
        public double EquatorialAzimuth()
        {
            return Init() ?
              Math.Atan2(_salp0, _calp0) / GeoMath.Degree : Double.NaN;
        }

        /**
         * @return <i>a1</i> the arc length (degrees) between the northward
         *   equatorial crossing and point 1.
         **********************************************************************/
        public double EquatorialArc()
        {
            return Init() ?
              Math.Atan2(_ssig1, _csig1) / GeoMath.Degree : Double.NaN;
        }

        /**
         * @return <i>a</i> the equatorial radius of the ellipsoid (meters).  This is
         *   the value inherited from the Geodesic object used in the constructor.
         **********************************************************************/
        public double MajorRadius()
        { return Init() ? _a : Double.NaN; }

        /**
         * @return <i>f</i> the flattening of the ellipsoid.  This is the value
         *   inherited from the Geodesic object used in the constructor.
         **********************************************************************/
        public double Flattening()
        { return Init() ? _f : Double.NaN; }

        /**
         * @return <i>caps</i> the computational capabilities that this object was
         *   constructed with.  LATITUDE and AZIMUTH are always included.
         **********************************************************************/
        public int Capabilities() { return _caps; }

        /**
         * @param testcaps a set of bitor'ed {@link GeodesicMask} values.
         * @return true if the GeodesicLine object has all these capabilities.
         **********************************************************************/
        public bool Capabilities(int testcaps)
        {
            testcaps &= GeodesicMask.OUT_ALL;
            return (_caps & testcaps) == testcaps;
        }

        private void LineInit(Geodesic g,
                        double lat1, double lon1,
                        double azi1, double salp1, double calp1,
                        int caps)
        {
            _a = g._a;
            _f = g._f;
            _b = g._b;
            _c2 = g._c2;
            _f1 = g._f1;
            // Always allow latitude and azimuth and unrolling the longitude
            _caps = caps | GeodesicMask.LATITUDE | GeodesicMask.AZIMUTH |
              GeodesicMask.LONG_UNROLL;

            _lat1 = GeoMath.LatFix(lat1);
            _lon1 = lon1;
            _azi1 = azi1; _salp1 = salp1; _calp1 = calp1;
            double cbet1, sbet1;
            {
                Pair p = GeoMath.sincosd(GeoMath.AngRound(_lat1));
                sbet1 = _f1 * p.First; cbet1 = p.Second;
            }
            // Ensure cbet1 = +epsilon at poles
            {
                Pair p = GeoMath.norm(sbet1, cbet1);
                sbet1 = p.First; cbet1 = Math.Max(Geodesic.tiny_, p.Second);
            }
            _dn1 = Math.Sqrt(1 + g._ep2 * GeoMath.Sq(sbet1));

            // Evaluate alp0 from sin(alp1) * cos(bet1) = sin(alp0),
            _salp0 = _salp1 * cbet1; // alp0 in [0, pi/2 - |bet1|]
            // Alt: calp0 = hypot(sbet1, calp1 * cbet1).  The following
            // is slightly better (consider the case salp1 = 0).
            _calp0 = GeoMath.hypot(_calp1, _salp1 * sbet1);
            // Evaluate sig with tan(bet1) = tan(sig1) * cos(alp1).
            // sig = 0 is nearest northward crossing of equator.
            // With bet1 = 0, alp1 = pi/2, we have sig1 = 0 (equatorial line).
            // With bet1 =  pi/2, alp1 = -pi, sig1 =  pi/2
            // With bet1 = -pi/2, alp1 =  0 , sig1 = -pi/2
            // Evaluate omg1 with tan(omg1) = sin(alp0) * tan(sig1).
            // With alp0 in (0, pi/2], quadrants for sig and omg coincide.
            // No atan2(0,0) ambiguity at poles since cbet1 = +epsilon.
            // With alp0 = 0, omg1 = 0 for alp1 = 0, omg1 = pi for alp1 = pi.
            _ssig1 = sbet1; _somg1 = _salp0 * sbet1;
            _csig1 = _comg1 = sbet1 != 0 || _calp1 != 0 ? cbet1 * _calp1 : 1;
            {
                Pair p = GeoMath.norm(_ssig1, _csig1);
                _ssig1 = p.First; _csig1 = p.Second;
            } // sig1 in (-pi, pi]
            // GeoMath.norm(_somg1, _comg1); -- don't need to normalize!

            _k2 = GeoMath.Sq(_calp0) * g._ep2;
            double eps = _k2 / (2 * (1 + Math.Sqrt(1 + _k2)) + _k2);

            if ((_caps & GeodesicMask.CAP_C1) != 0)
            {
                _A1m1 = Geodesic.A1m1f(eps);
                _C1a = new double[nC1_ + 1];
                Geodesic.C1f(eps, _C1a);
                _B11 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C1a);
                double s = Math.Sin(_B11), c = Math.Cos(_B11);
                // tau1 = sig1 + B11
                _stau1 = _ssig1 * c + _csig1 * s;
                _ctau1 = _csig1 * c - _ssig1 * s;
                // Not necessary because C1pa reverts C1a
                //    _B11 = -SinCosSeries(true, _stau1, _ctau1, _C1pa, nC1p_);
            }

            if ((_caps & GeodesicMask.CAP_C1p) != 0)
            {
                _C1pa = new double[nC1p_ + 1];
                Geodesic.C1pf(eps, _C1pa);
            }

            if ((_caps & GeodesicMask.CAP_C2) != 0)
            {
                _C2a = new double[nC2_ + 1];
                _A2m1 = Geodesic.A2m1f(eps);
                Geodesic.C2f(eps, _C2a);
                _B21 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C2a);
            }

            if ((_caps & GeodesicMask.CAP_C3) != 0)
            {
                _C3a = new double[nC3_];
                g.C3f(eps, _C3a);
                _A3c = -_f * _salp0 * g.A3f(eps);
                _B31 = Geodesic.SinCosSeries(true, _ssig1, _csig1, _C3a);
            }

            if ((_caps & GeodesicMask.CAP_C4) != 0)
            {
                _C4a = new double[nC4_];
                g.C4f(eps, _C4a);
                // Multiplier = a^2 * e^2 * cos(alpha0) * sin(alpha0)
                _A4 = GeoMath.Sq(_a) * _calp0 * _salp0 * g._e2;
                _B41 = Geodesic.SinCosSeries(false, _ssig1, _csig1, _C4a);
            }
        }

        internal GeodesicLine(Geodesic g,
                               double lat1, double lon1,
                               double azi1, double salp1, double calp1,
                               int caps, bool arcmode, double s13_a13)
        {
            LineInit(g, lat1, lon1, azi1, salp1, calp1, caps);
            GenSetDistance(arcmode, s13_a13);
        }

        public void SetDistance(double s13)
        {
            _s13 = s13;
            GeodesicData g = Position(false, _s13, 0);
            _a13 = g.a12;
        }

        /**
         * Specify position of point 3 in terms of arc length.
         *
         * @param a13 the arc length from point 1 to point 3 (degrees); it
         *   can be negative.
         *
         * The distance <i>s13</i> is only set if the GeodesicLine object has been
         * constructed with <i>caps</i> |= {@link GeodesicMask#DISTANCE}.
         **********************************************************************/
        void SetArc(double a13)
        {
            _a13 = a13;
            GeodesicData g = Position(true, _a13, GeodesicMask.DISTANCE);
            _s13 = g.s12;
        }

        /**
         * Specify position of point 3 in terms of either distance or arc length.
         *
         * @param arcmode boolean flag determining the meaning of the Second
         *   parameter; if <i>arcmode</i> is false, then the GeodesicLine object must
         *   have been constructed with <i>caps</i> |=
         *   {@link GeodesicMask#DISTANCE_IN}.
         * @param s13_a13 if <i>arcmode</i> is false, this is the distance from
         *   point 1 to point 3 (meters); otherwise it is the arc length from
         *   point 1 to point 3 (degrees); it can be negative.
         **********************************************************************/
        public void GenSetDistance(bool arcmode, double s13_a13)
        {
            if (arcmode)
                SetArc(s13_a13);
            else
                SetDistance(s13_a13);
        }

        public double GenDistance(bool arcmode)
        { return Init() ? (arcmode ? _a13 : _s13) : Double.NaN; }

        /**
         * @return <i>s13</i>, the distance to point 3 (meters).
         **********************************************************************/
        public double Distance() { return GenDistance(false); }

        

    }
}