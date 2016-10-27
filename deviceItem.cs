using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopClient
{
    public class deviceItem
    {
         
        public string imei { get; set; }
        public string mac { get; set; }
        public string phone { get; set; }
        public string sim_iccid { get; set; }
        public int sim_state { get; set; }
        public string sim_imsi { get; set; }
        public string device_id { get; set; }
        public string device_type { get; set; }
        public string wifi_ssid { get; set; }
        public string wifi_bssid { get; set; }
        public string ip { get; set; }
        public string gateway { get; set; }
        public string operator_id { get; set; }
        public string operator_name { get; set; }
        public string android_id { get; set; } 
        public string brand { get; set; }
        public string device { get; set; }
        public string product { get; set; }
        public string board { get; set; } 
        public string hardware { get; set; }
        public int active { get; set; }
        //public int notify { get;set;}
        public string uname { get; set; } 
        public string password { get; set; } 
        public int level { get; set; }
        public int old_level { get; set; } 
        public string game { get; set; }
        public string platform { get; set; } 
        public bool isError { get; set; }

        public int server_id { get; set; }
        public string packageName { get; set; }
         

    }
}
