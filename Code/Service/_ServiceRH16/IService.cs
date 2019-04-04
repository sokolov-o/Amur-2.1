using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FERHRI.Amur.ServiceRH16
{
    [ServiceContract]
    public interface IService
    {
        #region COMMON
        /// <summary>
        /// Открытие рабочей сессии.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Идентификатор сессии - целое число большее нуля.</returns>
        [OperationContract]
        long Open(string userName, string password);
        /// <summary>
        /// Закрытие рабочей сессии по её идентификатору.
        /// </summary>
        /// <param name="hSvc">Идентификатор сессии, полученный методом Open.</param>
        [OperationContract]
        void Close(int hSvc);
        #endregion

        [OperationContract]
        List<DateTime> GetAvailableTimePeriod(long hSvc);
        [OperationContract]
        List<Station> GetAvailableStations(long hSvc);
        [OperationContract]
        List<Variable> GetAvailableVariables(long hSvc);
        [OperationContract]
        DataValue GetDataObservation(long hSvc, int stationId, int variableId, DateTime dateDay);
        [OperationContract]
        List<DataValue> GetDataObservations(long hSvc, List<int> stationId, int variableId, DateTime dateDay);
        [OperationContract]
        Dictionary<DateTime, List<DataForecast>> GetDataForecast(long hSvc, int stationId, int variableId, DateTime dateDayFcs);
        [OperationContract]
        Dictionary<DateTime, List<DataForecast>> GetDataForecasts(long hSvc, List<int> stationsId, int variableId, DateTime dateDayFcs);
    }
}
