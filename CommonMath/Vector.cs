using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOV.Common
{
    public class Vector
    {
        /// <summary>
        /// Минимальный угол (град) между азимутами.
        /// </summary>
        /// <returns>Минимальный угол (град) между азимутами.</returns>
        public static double GradBetween(double azimuth1, double azimuth2)
        {
            double d = Math.Abs(azimuth1 - azimuth2);
            return (d > 180) ? 360 - d : d;
        }
        /// <summary>
        /// Минимальный угол (град) между азимутами со знаком +(против часовой стрелки) - (по часовой стрелке).
        /// </summary>
        /// <returns>Минимальный угол (град) между азимутами.</returns>
        public static double GradBetweenSign(double azimuth1, double azimuth2)
        {
            if (double.IsNaN(azimuth1) || double.IsNaN(azimuth2))
                return double.NaN;
            double d = azimuth1 - azimuth2;
            return (Math.Abs(d) > 180) ? -1*Math.Sign(d)*(360 - Math.Abs(d)) : d;
        }
        public static double azimuth2Grad(double azimuth)
        {
            return 90.0 - azimuth;
        }
        public static double azimuth2Radians(double azimuth)
        {
            return grad2Radians(azimuth2Grad(azimuth));
        }
        private static double grad2Azimuth(double grad)
        {
            double d = (grad >= 0) ? 270.0 + 180.0 - grad : 90 + grad * -1;
            return (d > 360) ? d - 360 : d;
        }
        /// <summary>
        /// Откуда дует
        /// </summary>
        /// <param name="grad"></param>
        /// <returns></returns>
        private static double grad2AzimuthFrom(double grad)
        {
            return AzimuthTo2AzimuthFrom(grad2Azimuth(grad));
        }
        public static double AzimuthTo2AzimuthFrom(double d)
        {
            return (d > 180) ? d - 180 : d + 180;
        }
        public static double AzimuthFrom2AzimuthTo(double d)
        {
            return (d > 180) ? d - 180 : d + 180;
        }
        /// <summary>
        /// Азимут и модуль вектора -> U
        /// </summary>
        /// <param name="azimuth">азимут</param>
        /// <param name="mod">модуль вектора</param>
        /// <returns>U</returns>
        public static double azmod2U(double azimuth, double mod)
        {
            return mod * System.Math.Cos(
                    azimuth2Grad(azimuth) * System.Math.PI / 180.0);
        }
        public static double azmod2V(double azimuth, double mod)
        {
            return mod * System.Math.Sin(
                    azimuth2Grad(azimuth) * System.Math.PI / 180.0);
        }
        public static double uv2Module(double u, double v)
        {
            return System.Math.Sqrt(u * u + v * v);
        }
        public static double uv2Radians(double u, double v)
        {
            return System.Math.Atan2(v, u);
        }


        private static double uv2Grad(double u, double v)
        {
            return radians2Grad(uv2Radians(u, v));
        }
        public static double uv2Azimuth(double u, double v)
        {
            return grad2Azimuth(radians2Grad(uv2Radians(u, v)));
        }
        public static double uv2AzimuthFrom(double u, double v)
        {
            return grad2AzimuthFrom(radians2Grad(uv2Radians(u, v)));
        }
        
        public static double radians2Grad(double radians)
        {
            return radians * 180.0 / System.Math.PI;
        }
        public static double grad2Radians(double grad)
        {
            return (grad / 180.0) * System.Math.PI;
        }
        public static double[] uv2Module(double[][] uv)
        {
            double[] ret = new double[uv[0].Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = Math.Sqrt(uv[0][i] * uv[0][i] + uv[1][i] * uv[1][i]);
            }
            return ret;
        }
        private static double[] uv2Grad(double[] u, double[] v)
        {
            if (u.Length != v.Length) { throw new Exception("u.Length != v.Length"); }
            double[] ret = new double[u.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = uv2Grad(u[i], v[i]);
            }
            return ret;
        }
        public static double[] uv2Module(double[] u, double[] v)
        {
            if (u.Length != v.Length) { throw new Exception("u.Length != v.Length"); }
            double[] ret = new double[u.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = uv2Module(u[i], v[i]);
            }
            return ret;
        }
        public static double[] uv2Azimuth(double[] u, double[] v)
        {
            if (u.Length != v.Length) { throw new Exception("u.Length != v.Length"); }
            double[] ret = new double[u.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = uv2Azimuth(u[i], v[i]);
            }
            return ret;
        }
        public static double[] uv2AzimuthFrom(double[] u, double[] v)
        {
            if (u.Length != v.Length) { throw new Exception("u.Length != v.Length"); }
            double[] ret = new double[u.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = uv2AzimuthFrom(u[i], v[i]);
            }
            return ret;
        }
    }
}
