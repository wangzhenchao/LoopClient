using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopClient
{
    public class script
    {
        public string game_platform { get; set; }
        public string game_code { get; set; }
      //  public string game_package { get; set; }

        public List<string> game_packages { get; set; }
        public string game_activity { get; set; }
        public string game_name { get; set; }
        public string filter { get; set; }

        public string platform_code { get; set; }
        public string platform_name { get; set; }

        public string apk_package { get; set; }
        public string apk_activity { get; set; }
        public int apk_level { get; set; }
        public bool isLogin_code { get; set; }
        public bool isLogin_qq { get; set; }
        public int click_count { get; set; }
        public int click_delay { get; set; }

        public bool isRecord_server { get; set; }
          

   //     public int apk_start_time { get; set; } 
    }
}
