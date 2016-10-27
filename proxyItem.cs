using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopClient
{
    public class proxyItem
    {
        public string proxyIp { get; set; }
        public string proxyPort { get; set; }
        public string proxyFilter { get; set; }

        public proxyItem(string info)
        {
            string[] item = info.Split(new char[] { ':' });

            this.proxyIp = item[0];
            this.proxyPort = item[1];


        }
        public proxyItem()
        {  
        }
    }
}
