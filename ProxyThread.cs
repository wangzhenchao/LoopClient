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
    public delegate void ProxyCallBackDelegate(string msg, proxyItem item);

    class ProxyThread
    {
        public ProxyCallBackDelegate callback;
        public static string MSG_COMPLETE = "COMPLETE";
        public static string MSG_ERROR = "ERROR";
        public string msg = MSG_ERROR; 
         
        string url = "";


        public string game_code="";

   

        public void doWork()
        {
            GC.Collect();

            try
            {
                List<proxyItem> proxy_list = new List<proxyItem>();
                Dictionary<string, proxyItem> proxy_dic = new Dictionary<string, proxyItem>();

                string url = @"http://dev.kuaidaili.com/api/getproxy/?orderid=916102979740268&num=10&b_pcchrome=1&b_pcie=1&b_pcff=1&b_android=1&protocol=1&method=2&an_ha=1&quality=1&sort=2&dedup=1&format=json&sep=1";

                url = @"http://dev.kuaidaili.com/api/getproxy/?orderid=916102979740268&num=10&b_pcchrome=1&b_pcie=1&b_pcff=1&protocol=1&method=2&an_an=1&an_ha=1&sp1=1&sp2=1&dedup=1&format=json&sep=1";


                string res = HttpHelper.SendGet(url);
                Console.WriteLine(res);


                ProxyJson msg = JsonHelper.FromJson<ProxyJson>(res);
                List<string> lst = msg.data.proxy_list;


                if (lst.Count == 0)
                {
                    List<proxyItem> proxy = new List<proxyItem>();
                    proxy.Add(new proxyItem("120.132.78.176:9090"));
                    add(proxy_dic, proxy);
                } 

                foreach (string info in lst)
                {
                    string[] item = info.Split(new char[] { ':' });
                    proxy_dic.Add(item[0], new proxyItem(info));
                }


                
                foreach (KeyValuePair<string, proxyItem> pair in proxy_dic)
                {
                    proxyItem item = pair.Value;

                    if (ping(item))
                    {
                        callback(MSG_COMPLETE, item);
                    }
                }

               
            }
            catch (Exception)
            {
                callback(MSG_ERROR, new proxyItem());
                return;
            }
               
        }

        private void add(Dictionary<string, proxyItem> dic, List<proxyItem> list)
        {
            foreach (proxyItem item in list)
            {
                if (!dic.ContainsKey(item.proxyIp))
                {
                    dic.Add(item.proxyIp, item);
                }
            }

        }
        public bool ping(proxyItem item)
        {
            bool flag = false;
            Process start = new Process();
            start.StartInfo.UseShellExecute = false;
            start.StartInfo.CreateNoWindow = true;
            start.StartInfo.RedirectStandardInput = true;
            start.StartInfo.RedirectStandardOutput = true;
            string temp_start = LoopConfig.bat_temp + "_tcp.bat";
            File.Copy(LoopConfig.bat + "tcp.bat", temp_start, true);
            start.StartInfo.FileName = temp_start;

            start.StartInfo.Arguments = item.proxyIp + "  " + item.proxyPort;
            start.Start(); 
            StreamReader output_reader = start.StandardOutput;//截取输出流         
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                Console.WriteLine(output);
                if (output.Contains("open"))
                {
                    Console.WriteLine(output);
                    flag= true;
                    break;
                }
            }
            output_reader.Close(); 
            start.WaitForExit();
            start.Close(); 
            if (File.Exists(temp_start)) File.Delete(temp_start);

            return flag;

        }

        /////////////////

    }
}
