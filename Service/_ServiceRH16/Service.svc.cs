using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FERHRI.Amur.DataP;

namespace FERHRI.Amur.ServiceRH16
{
    public partial class Service : IService
    {
        public List<DateTime> GetAvailableTimePeriod(long hSvc)
        {
            return new List<DateTime>(_dateSFLimit);
        }
        public List<Station> GetAvailableStations(long hSvc)
        {
            return _stations;
        }
        public List<Variable> GetAvailableVariables(long hSvc)
        {
            return _clientAggrVariables;
        }
        public DataValue GetDataObservation(long hSvc, int stationId, int variableId, DateTime dateDay)
        {
            List<DataValue> ret = GetDataObservations(hSvc, new List<int>(new int[] { stationId }), variableId, dateDay);
            return (ret.Count == 1) ? ret[0] : null;
        }
        public List<DataValue> GetDataObservations(long hSvc, List<int> stationsId, int variableId, DateTime dateDay)
        {
            CheckAvailability(stationsId, variableId, dateDay);
            DataManagerData(hSvc);
            DataManagerMeta(hSvc);

            // DETERMINATE VAR INI
            Variable varAggr = _clientAggrVariables.Find(x => x.Id == variableId);
            int i = _clientAggrVariables.IndexOf(varAggr);
            Meta.Variable varIni = DataManagerMeta(hSvc).VariableRepository.Select(_obsIniVariables[i].Id);
            // GET HourDayStartUTC
            Dictionary<int, int> siteHourDayStartUTC = new Dictionary<int, int>();
            foreach (var item in _stations.Where(x => stationsId.Exists(y => y == x.Id)))
            {
                siteHourDayStartUTC.Add(item.Id, item.UTCHourDayStart);
            }
            // AGGREGATE
            List<DataP.DataDV> ddvs = Observations.AggregateDay(stationsId, varIni,
                (int)_obsKeyMSOO[0], (int)_obsKeyMSOO[1], (int)_obsKeyMSOO[2], (double)_obsKeyMSOO[3], dateDay, dateDay,
                siteHourDayStartUTC, varAggr.DataTypeId);
            // CONVERT AGGR RESULT 2 DATAVALUE LIST
            List<DataValue> ret = new List<DataValue>();
            foreach (var item in ddvs)
            {
                ret.Add(new DataValue() { Date = item.Date, StationId = (int)item.Param[0], VariableId = variableId, Value = item.Value, Count = (int)item.Param[1] });
            }
            return ret;
        }

        public Dictionary<DateTime, List<DataForecast>> GetDataForecasts(long hSvc, List<int> stationsId, int variableId, DateTime dateDayFcs)
        {
            CheckAvailability(stationsId, variableId, dateDayFcs);
            DataManagerData(hSvc);
            DataManagerMeta(hSvc);

            // DETERMINATE VAR INI FCS
            Variable varAggr = _clientAggrVariables.Find(x => x.Id == variableId);
            int i = _clientAggrVariables.IndexOf(varAggr);
            Meta.Variable varIni = DataManagerMeta(hSvc).VariableRepository.Select(_fcsIniVariables[i].Id);
            // GET HourDayStartUTC
            Dictionary<int, int> siteUTCHourDayStart = new Dictionary<int, int>();
            Dictionary<int, double> siteUTCOffset = new Dictionary<int, double>();
            foreach (var item in _stations.Where(x => stationsId.Exists(y => y == x.Id)))
            {
                siteUTCHourDayStart.Add(item.Id, item.UTCHourDayStart);
                siteUTCOffset.Add(item.Id, item.UTCOffset);
            }
            // AGGREGATE
            Dictionary<DateTime, List<DataDV>> ddv = Forecast.AggregateDay(stationsId, varIni,
                (int)_fcsKeyMSOO[0], (int)_fcsKeyMSOO[1], (int)_fcsKeyMSOO[2], (double)_fcsKeyMSOO[3], dateDayFcs, dateDayFcs,
                siteUTCHourDayStart, siteUTCOffset, varAggr.DataTypeId);

            // CONVERT AGGR RESULT 2 DATAVALUE LIST
            Dictionary<DateTime, List<DataForecast>> ret = new Dictionary<DateTime, List<DataForecast>>();
            foreach (KeyValuePair<DateTime, List<DataDV>> kvp in ddv)
            {
                List<DataForecast> dfs = new List<DataForecast>();

                foreach (DataDV dv in kvp.Value)
                {
                    dfs.Add(new DataForecast()
                    {
                        Date = dv.Date,
                        StationId = (int)dv.Param[0],
                        VariableId = variableId,
                        Value = dv.Value,
                        Count = (int)dv.Param[1],
                        MinLagFcs = (double)dv.Param[2],
                        MaxLagFcs = (double)dv.Param[3]
                    });
                }
                ret.Add(kvp.Key, dfs);
            }
            return ret;
        }

        public Dictionary<DateTime, List<DataForecast>> GetDataForecast(long hSvc, int stationId, int variableId, DateTime dateDayFcs)
        {
            Dictionary<DateTime, List<DataForecast>> ret = GetDataForecasts(hSvc, new List<int>(new int[] { stationId }), variableId, dateDayFcs);
            return (ret.Count != 1) ? ret : null;
        }

        void CheckAvailability(List<int> stationId, int variableId, DateTime date)
        {
            if (stationId.Exists(x => !_stations.Exists(y => y.Id == x)))

            if (!_clientAggrVariables.Exists(x => x.Id == variableId))
                throw new Exception(string.Format("Указан недоступный для клиента параметр с id={0}.", variableId));

            if (date < _dateSFLimit[0] || date > _dateSFLimit[1])
                throw new Exception(string.Format(
                    "Указана недоступная для клиента дата для чтения данных: {0}. Доступный для чтения данных период {1} - {2}",
                    date, _dateSFLimit[0], _dateSFLimit[1]
                    ));

        }
    }
}
