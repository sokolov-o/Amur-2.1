using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.Amur.ObsVsFcs
{
    /// <summary>
    /// Очень вредный класс. 
    /// 
    /// Но, пока без него никак, т.к. 
    /// пополнение наблюдённых параметров идёт с офсетами 0, 0.0, 
    /// а прогнозы идут с "нормальными" офсетами для параметров.
    /// 
    /// Поэтому здесь вот такое соответствие нужно - между переменной факта и офсетами прогноза.
    /// </summary>
    public partial class ObsVsFcs
    {
        /// <summary>
        /// Соответствие наблюдённой переменной и прогностического offset.
        /// </summary>
        static Dictionary<int/*variableIdObs*/, int[/*fcsOffsetTypeId;fcsOffsetValue*/]> _conformity;
        static public int[] GetFcsOffset(int obsVariableId)
        {
            int[] fcsOffset;
            if (!_conformity.TryGetValue(obsVariableId, out fcsOffset))
            {
                //MessageBox.Show("Для выбранного наблюдённого параметра отсутствует соответствующий ему offset в классе Obs2Fcs.");
                return null;
            }
            return fcsOffset;
        }
        static ObsVsFcs()
        {
            _conformity = new Dictionary<int, int[]>();

            _conformity.Add((int)EnumVariable.TempAirObs, new int[] { (int)EnumOffsetType.HeightAboveEarth, 2 });
            _conformity.Add((int)EnumVariable.PmslObs, new int[] { (int)EnumOffsetType.MSL, 0 });
            _conformity.Add((int)EnumVariable.WindSpeedObs, new int[] { (int)EnumOffsetType.HeightAboveEarth, 10 });
            _conformity.Add((int)EnumVariable.WindDirObs, new int[] { (int)EnumOffsetType.HeightAboveEarth, 10 });
        }
    }
}
