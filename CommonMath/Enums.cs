﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    /// <summary>
    /// Порядок байт.
    /// </summary>
    public enum EnumByteOrder { LITTLE_ENDIAN, BIG_ENDIAN }

    /// <summary>
    /// Математические (статистические и др.) переменные.
    /// Коды Sakura.Hmdic
    /// </summary>
    public enum EnumMathVar
    {
        ABSDEV = 7,
        ANOM = 12,
        AVG = 1,
        AVG_TEND = 82,
        COUNT = 87,
        DEV2 = 9,
        DEVAVG = 28,
        MAX = 2,
        MIN = 3,
        /// <summary>
        /// Абсолютный минимум
        /// </summary>
        MINABS = 90,
        /// <summary>
        /// Абсолютный максимум
        /// </summary>
        MAXABS = 91,
        /// <summary>
        /// Средний минимум
        /// </summary>
        MINAVG = 92,
        /// <summary>
        /// Средний максимум
        /// </summary>
        MAXAVG = 93,
        P_KQQQ = 84,
        P_s_KQQQ = 85,
        P_st_KQQQ = 86,
        R = 5,
        S_FCS = 49,
        /// <summary>
        /// Отношение СКО прогнозов к СКО от нормы РД РД 52.27.284-91, стр. 117, ф. 99,100
        /// </summary>
        S_SIGMA_U = 47,
        /// <summary>
        /// Отношение СКО прогнозов к СКО от среднего за период прогноза
        /// </summary>
        S_SIGMA_F = 48,
        /// <summary>
        /// Отношение СКО прогнозов к СКО за период прогноза от нормы 
        /// </summary>
        S_SIGMA_7 = 56, // Отношение СКО прогнозов к СКО за период прогноза от нормы  РД РД 52.27.284-91, стр. 117, ф. 99,100
        STD = 4,
        STD_NM1_TEND = 81,
        STD_NM1 = 83,
        /// <summary>
        /// Сумма значений
        /// </summary>
        SUM = 94,
        /// <summary>
        /// Обеспеченность для аномалий затем осреднение по по региону
        /// </summary>
        P86TAMONTH = 14,//	Обеспеченность для аномалий затем осреднение по по региону(стр. 12 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
        /// <summary>
        /// Ro - оправд-ть аномалии сезонной Та по знаку 
        /// </summary>
        RO82TASEASON = 15,//Параметр Ro - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// <summary>
        /// Q - погрешность аномалии сезонной Та по величине 
        /// </summary>
        Q82TASEASON = 17,//Параметр Q - погрешность аномалии сезонной Та по величине (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// <summary>
        /// 
        /// </summary>
        P82TASEASON = 18,//Параметр P - оправд-ть аномалии сезонной Та по знаку (стр. 21,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// <summary>
        /// 
        /// </summary>
        PZHURTA = 35,//Оценка для температуры воздуха по P c предварительным осреднением факт и прог. аномалии (по станциям) и потом P по этим двум цифрам (Журавлева, м. Свинухова)
        /// <summary>
        /// 
        /// </summary>
        P82RRMONTH = 20,//Параметр P - показатель качества прогноза месячной суммы осадков (стр. 19, ф. 4  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// <summary>
        /// Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
        /// </summary>
        P82RR_minus_P_CLM = 68,//Разность оценки качества месячных прогнозов осадков (P82RRMONTH) и оценки климатического прогноза
        /// <summary>
        /// Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
        /// </summary>
        P82RR_P_CLM = 66,//Отношение оценки качества месячных прогнозов осадков (P82RRMONTH) к оценке климатического прогноза
        /// <summary>
        /// 
        /// </summary>
        PZHURRR = 40,//Оценка для осадков по P c предварительным осреднением факт и прог. аномалии (по станциям) и потом P по этим двум цифрам (Журавлева, м. Свинухова)
        /// <summary>
        /// Оценка оправдываемости прогноза Та
        /// </summary>
        P02TAHOUR = 23,//Оценка оправдываемости прогноза Та  (стр. 21,  "Наставление по крат. прог., 2002,Спб, ГМИздат").
        /// <summary>
        /// Оценка прогноза  аномалий ср. мес. Та по классам
        /// </summary>
        P86TAMONTH_cls = 25,//Оценка прогноза  аномалий ср. мес. Та по классам (стр. 15 "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат")
        /// <summary>
        /// Параметр P - оправд-ть аномалии месячной Та
        /// </summary>
        P82TAMONTH = 26,//Параметр P - оправд-ть аномалии месячной Та (стр.14,  "Наставление..., 1986, р.2, ч.VI, Москва, ГМИздат").
        /// <summary>
        /// Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
        /// </summary>
        P86TA_P_CLM = 69,//Отношение оценки качества месячных прогнозов температуры (P86TAMONTH) к оценке климатического прогноза
        /// <summary>
        /// Разность оценки качества месячных прогнозов температуры (P86TAMONTH) и оценки климатического прогноза
        /// </summary>
        P86TA_minus_P_CLM = 71,
        VALUE = 27,
        /// <summary>
        /// Максимальное абсолютное отклонение двух рядов
        /// </summary>
        ABSDEVMAX = 88,
        /// <summary>
        /// Минимальное абсолютное отклонение двух рядов
        /// </summary>
        ABSDEVMIN = 89,
        /// <summary>
        /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 2 м/с и более
        /// </summary>
        PV2Pr = 29,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 2 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
        /// <summary>
        /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 5 м/с и более
        /// </summary>
        PV5Pr = 30,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 5 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
        /// <summary>
        /// Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 10 м/с и более
        /// </summary>
        PV10Pr = 31,//Pv2%	Оценка Рv (%) для отклонений прогнозируемой скорости от фактической на 10 м/с и более (по «Методическими указаниями» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.). MATLAB
        /// <summary>
        /// Оценка направления ветра (Наставление по службе прог., 1981)
        /// </summary>
        SIGMA_WDIR_SIN = 41,//	Оценка направления ветра (Наставление по службе прог., 1981)
        /// <summary>
        /// Оценка направления ветра  0 - 30 grad
        /// </summary>
        SIGMA0_WDIR_RD = 42,//	Оценка направления ветра  0 - 30 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
        /// <summary>
        /// Оценка направления ветра  31 - 60 grad 
        /// </summary>
        SIGMA31_WDIR_RD = 43,//Оценка направления ветра  31 - 60 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
        /// <summary>
        /// Оценка направления ветра  61 - 90 grad 
        /// </summary>
        SIGMA61_WDIR_RD = 44,//Оценка направления ветра  61 - 90 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
        /// <summary>
        /// Оценка направления ветра  > 90 grad 
        /// </summary>
        SIGMA91_WDIR_RD = 45,//Оценка направления ветра  > 90 grad (стр. 41, «РД Методические указания» РД 52.27.284-91, п.1.2.3.3? Москва,1991, 119 c.).
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков в классе ниже нормы (80%) (Оценка «угадывания класса»)
        /// </summary>
        P3_Minus1 = 51,	//Параметр P - показатель качества прогноза месячной суммы осадков в классе ниже нормы (<80%) (Оценка «угадывания класса»)
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков в классе нормы (80% - 120%) (Оценка «угадывания класса»)
        /// </summary>
        P3_0 = 52,//	Параметр P - показатель качества прогноза месячной суммы осадков в классе нормы (>=80% и <=120%) (Оценка «угадывания класса»)
        /// <summary>
        /// Параметр P - показатель качества прогноза месячной суммы осадков в классе выше нормы (>120%) (Оценка «угадывания класса»)
        /// </summary>
        P3_Plus1 = 53//	Параметр P - показатель качества прогноза месячной суммы осадков в классе выше нормы (>120%) (Оценка «угадывания класса»)
    }
}
