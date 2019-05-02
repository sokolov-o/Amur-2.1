using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Common
{
    public class BaseDataManager : Common.ADbNpgsql
    {
        protected BaseDataManager(string connectionString) : base(connectionString) {}

        static Dictionary<string, BaseDataManager> _dm = new Dictionary<string, BaseDataManager>();

        /// <summary>
        /// Экземпляр с заданной строкой подключения.
        /// </summary>
        /// <param name="connectionString">Строка подключения</param>
        /// <param name="type">Тип создаваемого экземпляра</param>
        public static BaseDataManager GetInstance(string connectionString, Type type)
        {
            BaseDataManager ret;
            if (!_dm.TryGetValue(type.FullName + "_" + connectionString, out ret))
            {
                ConstructorInfo magicConstructor = type.GetConstructor(new Type[] { Type.GetType("System.String") });
                ret = (BaseDataManager)magicConstructor.Invoke(new object[] {connectionString});
                _dm.Add(type.FullName + "_" + connectionString, ret);
            }
            return ret;
        }

        /// <summary>
        /// Удалить все экземпляры.
        /// </summary>
        public static void DeleteInstances()
        {
            _dm.Clear();
        }
    }
}
