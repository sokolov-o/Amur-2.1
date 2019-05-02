using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    public static class Enums
    {
        static public string ToString(EnumLanguage lang)
        {
            return lang.ToString().ToLower();
        }

        static public EnumLanguage ParseLanguage(string lang)
        {
            foreach (var i in Enum.GetValues(typeof(EnumLanguage)))
            {
                EnumLanguage e = (EnumLanguage)i;
                if (e.ToString().ToLower() == lang.ToLower())
                    return e;
            }
            throw new Exception("Неизвестный язык " + lang);
        }
        static public EnumTime ParseTime(string name)
        {
            foreach (var i in Enum.GetValues(typeof(EnumTime)))
            {
                EnumTime e = (EnumTime)i;
                if (e.ToString().ToLower() == name.ToLower())
                    return e;
            }
            throw new Exception("Неизвестный временной период " + name);
        }
        static public EnumDateType[] ParseDateType(string[] type)
        {
            int[] iarr = Common.StrVia.ToListInt(type).ToArray();
            if (iarr == null)
                return null;

            EnumDateType[] e = new EnumDateType[type.Length];
            for (int i = 0; i < iarr.Length; i++)
            {
                e[i] = (EnumDateType)iarr[i];
            }
            return e;
        }
    }

    public enum EnumFormView { EditItem = 1, AddNewSets = 2, AddNewSet = 3 }
    public enum EnumLanguage { Rus = 0, Eng = 1, Unk = -1 }
    public enum EnumMathvar { Avg = 1, Std = 2 }

    public enum EnumUnit
    {
        Unknown = -1,
        DegreeCelsius = 96,
        Categorical = 258
    }
    public enum EnumDateType
    {

        /// <summary>
        /// ВСВ
        /// </summary>
        UTC = 1,
        /// <summary>
        /// Локальное время пункта.
        /// </summary>
        LOC = 2,
        /// <summary>
        /// Метео-сутки в соответствие с таблицей метео-зон.
        /// </summary>
        Meteo = 3,
        Unknown = 4,
        MeteoDay = 5,
        HydroDayNight = 6,
        MeteoDayNight = 7,
        /// <summary>
        /// Для месячных и др. периодов
        /// </summary>
        NotApplicable = 8
    }
    public enum EnumTime
    {
        Unknown = -1,

        Day = 104,
        DayHalf = 322,
        Hour = 103,
        Month = 106,

        DecadeOfYear = 318,
        PentadeOfYear = 321,

        HydroSeason = 323,
        YearCommon = 107,
        Second = 100
    }

    //public enum EnumSiteType
    //{
    //    StandartObsLocation = 1,
    //    AHK = 2
    //}
    public enum EnumGeoObject
    {
        River = 41,
        GeoReg = 103
    }
    public enum EnumGeneralCategory
    {
        Meteo = 2
    }
    public enum EnumSampleMedium
    {
        Air = 1
    }
    public enum EnumSiteAttrType
    {
        HydroSeasonMonthStart = 115,
        DiffGageAGK = 32,
        DiffTempAGK = 33,
        /// <summary>
        /// Отметка 0 графика
        /// </summary>
        ZeroMarkChart = 8,
        UTCOffset = 1003,
        MeteoZoneId = 1007
    }
    public enum EnumValueType
    {
        DerivedValue = 1,
        FieldObservation = 2,
        Forecast = 3
    }
    public enum EnumDataType
    {
        Average = 1,
        Maximum = 8,
        Minimum = 10,
        Incremental = 7,
        Cumulative = 6,

        Continuous = 5,

        Poyma = 19,
        NYa = 20,
        OYa = 21
    }
    public enum EnumStationType
    {
        MeteoStation = 1,
        HydroPost = 2,
        MorePost = 3,
        GaugingPost = 10,
        AHK = 6,
        AMK = 5,
        GeoObject = 12
    }
    public enum EnumOffsetType
    {
        NoOffset = 0,
        /// <summary>
        /// Маршрут поле-лес
        /// </summary>
        MarshrutPoleLes = 1,
        /// <summary>
        /// Уровень моря
        /// </summary>
        MSL = 101,
        /// <summary>
        /// Уровень над поверхностью земли.
        /// </summary>
        HeightAboveEarth = 100,
        /// <summary>
        /// Поверхность земли или воды.
        /// </summary>
        SurfaceEarthOrWater = 102,
        /// <summary>
        /// Окрестность станции
        /// </summary>
        StationNearby = 103
    }
    public enum EnumLegalEntity
    {
        FERHRI = 777,
        DVUGMS = 0,
        PUGMS = 243
    }
    public enum EnumCodeForm
    {
        Unknown = 0,
        /// <summary>
        /// SYNOP
        /// </summary>
        KH01 = 1,
        /// <summary>
        /// HYDRO
        /// </summary>
        KH15 = 2,
        /// <summary>
        /// SNOW
        /// </summary>
        KH24 = 6
    }
    [DataContract]
    public enum EnumMethod
    {
        [EnumMember]
        Unknown = 7,
        [EnumMember]
        ObservationInSitu = 0,
        [EnumMember]
        Operator = 5,
        /// <summary>
        /// Интерполяция с кривой связи Q=f(H)
        /// </summary>
        [EnumMember]
        InterpCurve = 2,
        /// <summary>
        /// Прогноз Романского, Вербицкой, 2016
        /// </summary>
        [EnumMember]
        WRFARW_HBRK15 = 100,
        /// <summary>
        /// Прогноз волнения по региону
        /// Тихий океан с шагом 0.5 град (Вражкин А.Н.)
        /// </summary>
        [EnumMember]
        WAVE_VVO_PACIFIC_0p5 = 105,
        /// <summary>
        /// Прогноз GFS 0.5 grad
        /// </summary>
        [EnumMember]
        GFS = 102,
        /// <summary>
        /// Прогноз синопика
        /// </summary>
        [EnumMember]
        Sinopsis = 106,
        /// <summary>
        /// Прогноз синопика
        /// </summary>
        [EnumMember]
        Climate = 1000
    }
    public enum EnumVariableType
    {
        Gage = 100,
        Precipitation = 200,
        TemperatureDewPoint = 84,
        Temperature = 269,
        VaporPressureDeficit = 276,
        Direction = 292,
        WindVelocity = 293,
        VectorXComponent = 10134,
        VectorYComponent = 10135
    }
    [DataContract]
    public enum EnumVariable
    {
        /// <summary>
        /// Относительная влажность наблюдённая в срок (%)
        /// </summary>
        Rh = 9,
        UWindFcs = 1109,
        VWindFcs = 1110,
        /// <summary>
        /// Расход воды средний за сутки
        /// </summary>
        DischargeAvgDay = 48,
        /// <summary>
        /// Расход воды средний за месяц
        /// </summary>
        DischargeAvgMonth = 31,
        /// <summary>
        /// Расход воды
        /// </summary>
        Discharge = 14,
        /// <summary>
        /// Объем стока, мес
        /// </summary>
        WMonth = 30,
        /// <summary>
        /// Уровень воды, измеренный
        /// </summary>
        GageHeightF = 2,
        /// <summary>
        /// Изменение уровня воды, см/сутки
        /// </summary>
        GageHeightShiftDay = 28,
        /// <summary>
        /// Уровень воды за сутки - avg
        /// </summary>
        GageHeightAvgDay = 47,
        /// <summary>
        /// Уровень воды за сутки - min
        /// </summary>
        GageHeightMinDay = 61,
        /// <summary>
        /// Уровень воды за сутки - max
        /// </summary>
        GageHeightMaxDay = 62,
        /// <summary>
        /// Уровень воды за год - avg
        /// </summary>
        GageHeightAvgYear = 29,
        /// <summary>
        /// Уровень воды за год - min
        /// </summary>
        GageHeightMinYear = 67,
        /// <summary>
        /// Уровень воды за год - max
        /// </summary>
        GageHeightMaxYear = 68,
        /// <summary>
        /// Уровень воды за месяц - avg
        /// </summary>
        GageHeightAvgMonth = 18,
        /// <summary>
        /// Уровень воды за месяц - min
        /// </summary>
        GageHeightMinMonth = 63,
        /// <summary>
        /// Уровень воды за месяц - max
        /// </summary>
        GageHeightMaxMonth = 64,
        /// <summary>
        /// Уровень воды за декаду - avg
        /// </summary>
        GageHeightAvgDecade = 40,
        /// <summary>
        /// Уровень воды за декаду - min
        /// </summary>
        GageHeightMinDecade = 65,
        /// <summary>
        /// Уровень воды за декаду - max
        /// </summary>
        GageHeightMaxDecade = 66,

        /// <summary>
        /// Толщина льда, измерение.
        /// </summary>
        IceDepthF = 21,
        /// <summary>
        /// Ледовые явления, вектор.
        /// </summary>
        IcePhenom = 33,
        /// <summary>
        /// Температура воды, измеренный
        /// </summary>
        TempWaterF = 13,
        /// <summary>
        /// Температура воздуха, измеренный
        /// </summary>
        TempAirObs = 10,
        /// <summary>
        /// Темп. воздуха, сут ср. набл.
        /// </summary>
        TempAirObsDaylyMean = 1015,
        /// <summary>
        /// Осадки за полусутки, измеренный
        /// </summary>
        PrecipDay12F = 45,
        /// <summary>
        /// Осадки за сутки, измеренный
        /// </summary>
        PrecipDay24F = 23,
        /// <summary>
        /// Осадки за час, измеренный
        /// </summary>
        PrecipDay01F = 22,
        /// <summary>
        /// Осадки за 3 часа, прогноз
        /// </summary>
        PrecipHour03Fcs = 1046,
        /// <summary>
        /// Сумма осадков за месяц
        /// </summary>
        PrecipMonth = 55,
        PmslObs = 12,
        /// <summary>
        /// Сумма осадков за декаду
        /// </summary>
        PrecipDecade = 54,

        /// <summary>
        /// Высота снеж. покрова на маршруте (поле, лес)
        /// </summary>
        SnowDepthF = 49,
        /// <summary>
        /// Высота снежного покрова на льду (код)
        /// </summary>
        SnowDepthIce = 50,
        /// <summary>
        /// Запас воды в снеге.
        /// </summary>
        WaterPotentialF = 20,
        /// <summary>
        /// Состояние водного объекта (код).
        /// </summary>
        WOState = 34,
        /// <summary>
        /// Скорость ветра, набл.
        /// </summary>
        WindSpeedObs = 7,
        /// <summary>
        /// Направление ветра, набл.
        /// </summary>
        WindDirObs = 1,
        /// <summary>
        /// Скорость ветра, прогн.
        /// </summary>
        WindSpeedFcs = 1030,
        /// <summary>
        /// Направление ветра, набл.
        /// </summary>
        WindDirFcs = 1029

    }
    public enum EnumFlagAQC : byte
    {
        NoAQC = 0,
        Success = 1,
        Error = 2,
        Approved = 40,
        Deleted = 255
    }
    public enum EnumSiteXSiteType : int
    {
        Precipitation1For2 = 1
    }
}
