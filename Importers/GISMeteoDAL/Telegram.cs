using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.GISMeteo
{
    public class Telegram
    {
        public DateTime DateObserv { get; set; }
        public DateTime DateRecieve { get; set; }
        public StationGM Station { get; set; }

        public string TelegramText { get; set; }
        public List<MDBDATA> MDBDatas { get; set; }

        public override string ToString()
        {
            string ret = Station.Sign + " " + Station.Name + ";" + DateObserv.ToString() + ";" + DateRecieve + ";";
            ret += (this.TelegramText.Length < 1000) ? "\n" + TelegramText : "";
            return ret;
        }

        public Telegram
        (
            DateTime dateObserv,
            DateTime dateRecieve,
            StationGM station,
            string telegram
        )
        {
            DateObserv = dateObserv;
            DateRecieve = dateRecieve;
            Station = station;
            TelegramText = telegram;

            MDBDatas = new List<MDBDATA>();
        }
    }
}
