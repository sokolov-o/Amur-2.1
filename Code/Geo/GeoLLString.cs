using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOV.Common;

namespace SOV.Geo
{
    /// <summary>
    /// Строковое представление координаты широты/долготы в форме градусы, минуты, секунды
    /// с возможностью дробной части на любом уровне.
    /// </summary>
    public class GeoLLString
    {
        string grd = null, min = null, sec = null;
        /// <summary>
        /// Задаёт (через parce) или возвращает (через toString) строковое представление гео-координаты.
        /// </summary>
        private string GradMinSec
        {
            set
            {
                String[] gms = value.TrimEnd().Split(' ');
                if (gms.Length > 3) throw new Exception("value.Length > 3: " + value);
                grd = gms[0];
                if (gms.Length > 1)
                {
                    if (grd.IndexOf('.') >= 0 && gms[1] != null)
                        throw new Exception("Неверный формат строки координаты: " + value);
                    min = gms[1];
                    if (gms.Length > 2)
                    {
                        if (min.IndexOf('.') >= 0 && gms[2] != null)
                            throw new Exception("Неверный формат строки координаты: " + value);
                        sec = gms[2];
                    }
                }

                // TEST
                double d = StrVia.ParseDouble(grd);
                if
                (
                    ((d < -90 || d > 90) && IsLat)
                    ||
                    ((d < -180 || d > 360) && !IsLat)
                )
                    throw new Exception("Ошибка в градусах: " + grd);

                d = Double.Parse(min);
                if (d < 0 || d > 60) throw new Exception("Ошибка в минутах: " + min);

                d = Double.Parse(sec);
                if (d < 0 || d > 60) throw new Exception("Ошибка в секундах: " + sec);
            }
        }
        /// <summary>
        /// Получить составную гео-координату в строковом виде.
        /// </summary>
        /// <param name="grd">Часть - градусы.</param>
        /// <param name="min">Часть - минуты.</param>
        /// <param name="sec">Часть - секунды.</param>
        /// <returns>Координата в строковом виде.</returns>
        override public string ToString()
        {
            return
                  ((grd == null || grd == "") ? "" : grd.Trim())
                + ((min == null || min == "") ? "" : " " + min.Trim())
                + ((sec == null || sec == "") ? "" : " " + sec.Trim());
        }
        bool _isLat;
        public bool IsLat
        {
            get { return _isLat; }
            private set { _isLat = value; }
        }
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        /// <param name="coord">Строковое представление координаты.</param>
        /// <param name="enumLatOrLon">Координата широта или долгота?</param>
        static public GeoLLString GetLatitude(string coord)
        {
            return new GeoLLString() { IsLat = true, GradMinSec = coord };
        }
        static public GeoLLString GetLongitude(string coord)
        {
            return new GeoLLString() { IsLat = false, GradMinSec = coord };
        }
        /// <summary>
        /// Преобразовать строковые представления частей координаты в градусы.
        /// </summary>
        public double ToGrad()
        {
            return Double.Parse(grd)
                + ((min == null) ? 0 : Double.Parse(min)) / 60.0
                + ((sec == null) ? 0 : Double.Parse(sec)) / 3600.0;
        }

    }
}
