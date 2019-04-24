using System;
using System.Runtime.InteropServices;

namespace SOV.GISMeteo
{
    class GismeteoDLL
    {
        [DllImport(@"windbr32.dll")]
        public static extern int MdbOpenR(string Filename);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbClose(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetClear(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetCodeForm(int hDB, int Code);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbAddCodeForm(int hDB, int Code);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetPname(int hDB, int Param);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbAddPname(int hDB, int Param);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbNext(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern bool MdbIsMultiPoint(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern bool MdbIsFirstPoint(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern bool MdbSkipPoints(int hDB);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetObsInt(int hDB, [MarshalAs(UnmanagedType.LPStruct)] DateTimeStruct lpdtBegin, int duration);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetObsStart(int hDB, [MarshalAs(UnmanagedType.LPStruct)] DateTimeStruct lpdtBegin);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetObsEnd(int hDB, [MarshalAs(UnmanagedType.LPStruct)] DateTimeStruct lpdtEnd);

        [DllImport(@"windbr32.dll")]
        public static extern bool MdbGetData(int hDB, [MarshalAs(UnmanagedType.LPStruct)] MDBDATA dRecord);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetGridCccc(int hDB, string szString);

        [DllImport(@"windbr32.dll")]
        public static extern LatLon MdbGetMcoords(int hDb);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetGridPtime(int hDb, int nHours);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetGridPtime(int hDb);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetGlims(int hDb, int nSouth, int nWest, int nNorth, int nEast);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetLevel(int hDb, int nLevelType, int nLevelValue);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbSetLevelValue(int hDb, int nLevelValue);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetPosSize(int hDb, int wFlags);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbTell(int hDb, int wFlags, byte[] buf, int nSize); // заполнение области контрольной точки

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSeek(int hDb, int wFlags, byte[] buf, int nSize); // восстановление положения в базе данных

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetCodeForm(int hDb);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetIndex(int hDb);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetIndexFilter(int hDb, [MarshalAs(UnmanagedType.FunctionPtr)] Delegate lpUserFunc, int lParam); // Заказ списка индексов станций

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetIndex(int hDb, int Index);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetCallSign(int hDb, byte[] buf, int nSize);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbSetCallSign(int hDb, string szCallSign);
        /// <summary>
        /// Get observation time - получение информации о времени наблюдений
        /// </summary>
        /// <param name="hDB"></param>
        /// <param name="lpdtObsTime"></param>
        [DllImport(@"windbr32.dll")]
        public static extern void MdbGetObsTime(int hDB, [MarshalAs(UnmanagedType.LPStruct)] DateTimeStruct lpdtObsTime);

        [DllImport(@"windbr32.dll")]
        public static extern void MdbGetRcvTime(int hDB, [MarshalAs(UnmanagedType.LPStruct)] DateTimeStruct lpdtObsTime);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetHeight(int hDb);

        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetLevel(int hDb);

        /// <summary>
        /// Возвращает длину текста телеграммы
        /// </summary>
        /// <param name="hDb"></param>
        /// <returns></returns>
        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetTextSize(int hDb);

        /// <summary>
        /// Позволяет извлечь текст телеграммы
        /// </summary>
        /// <param name="hDb"></param>
        /// <param name="buf"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        [DllImport(@"windbr32.dll")]
        public static extern int MdbGetText(int hDb, byte[] buf, int nSize);
    }
}
