using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoopClient
{
    class LoopConfig
    {

        public static string host = "http://wangzhenchao.repairsky.svc.linnyou.com";

        public static string get_url = host+"/client/listDevices";
        public static string get_test_url = " http://guanliying.repairsky.svc.linnyou.com/client/listDevices?game=test";
        // public static string get_test_url = "http://lijinling.repairsky.svc.linnyou.com/client/listDevices"; 
        public static string post_url = host +"/client/updateLevel?";
        public static string post_test_url = "http://lijinling.repairsky.svc.linnyou.com/client/updateLevel?";
         
        public static string currenPath  = Application.StartupPath;   
        public static string xmlfolder =currenPath + @"\xml\";  

        public static string devicesXml = xmlfolder + @"devices.xml";  
        public static string vmsXml = xmlfolder + @"vms.xml";
        public static string scriptXml = xmlfolder + "script.xml";
        public static string proxysXml = xmlfolder + @"proxys.xml";  
         
        public static string bat = currenPath + @"\bat\";
        public static string bat_temp = currenPath + @"\temp\";
        public static string temp = currenPath + @"\temp\";
        public static string log = currenPath + @"\log\"; 
       
        public static string dev_db = currenPath+@"\db\dev.db";
        public static string apks = currenPath + @"\apks\";  
        public static string vm_share = @"E:\mobileShare\";

         public static string hmVersion = " 0.8.7 Beta";
         //  public static string hmVersion = " 0.10.2 Beta"; 

        public static string qq_package = "com.tencent.mobileqq";



    }
}
