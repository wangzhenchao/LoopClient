using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using SQLiteQueryBrowser;

namespace LoopClient
{
    public delegate void RequestCallBackDelegate(string msg, List<deviceItem> devlist);

    class RequestThread
    {
        public RequestCallBackDelegate callback;
        public static string MSG_COMPLETE = "COMPLETE";
        public static string MSG_ERROR = "ERROR";
        public string msg = MSG_ERROR; 
        // string url = "http://lijinling.repairsky.svc.linnyou.com/client/listDevices";
        string url = "http://guanliying.repairsky.svc.linnyou.com/client/listDevices";


        public string game_code="";

        public void doWork()
        {
            GC.Collect();

            if (game_code != "")
            {
                url = url + "?" + "game=" + game_code; 
            } 
            string res = HttpHelper.SendGet(url);
            MsgJson msg = JsonHelper.FromJson<MsgJson>(res); 
            List<deviceItem> lst = msg.data.devices;
            string game =msg.data.game; 
            foreach (deviceItem item in lst)
            {
                item.game = game; 
            }
             
            callback(MSG_COMPLETE, lst);
        }
      



        /////////////////

    }
}
