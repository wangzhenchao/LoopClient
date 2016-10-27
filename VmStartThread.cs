using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
namespace LoopClient
{
    public delegate void VmStartCallBackDelegate(string msg, string vm);


    class VmStartThread
    {
        public VmStartCallBackDelegate callback;
        public vm item;
        public Panel vm_panel;
        public static string MSG_COMPLETE = "VM_RESTART_COMPLETE";
        public static string MSG_ERROR = "VM_RESTART_ERROR";
        public string msg = MSG_ERROR; 
        public bool isRestart = false;
        public int sleep = 0;

        public void doWork()
        {
            item = (vm)vm_panel.Tag;
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses();
            if (isRestart && item.pid!="")
            {
                try
                {
                    Process kill = new Process();
                    kill.StartInfo.UseShellExecute = false;
                    kill.StartInfo.CreateNoWindow = true;
                    kill.StartInfo.RedirectStandardInput = true;
                    kill.StartInfo.RedirectStandardOutput = true; 
                    kill.StartInfo.FileName = LoopConfig.bat + "kill_t.bat";
                    kill.StartInfo.Arguments = item.pid + "";
                    kill.Start();
                    kill.WaitForExit();
                    kill.Close();

                }
                catch (Exception ex)
                {
                    FileHelper.writeLogs(LoopConfig.log + "VmStartThread.txt", item.code + "  restart_kill_error: " + ex.StackTrace);
                    callback(msg, vm_panel.Name);
                    return;
                }
            }
            vm_panel.Controls["label_status"].Text = "正在启动..";
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.FileName = LoopConfig.bat + "vm_start.bat";
            p.StartInfo.Arguments = item.droid;
            p.Start();

            Thread.Sleep(3000);
            p.StartInfo.FileName = LoopConfig.bat + "kill_f.bat";
            p.StartInfo.Arguments = p.Id + "";
            p.Start();
            p.WaitForExit();
            p.Close();
            int i = 0;
            bool chek = true ;
            while (!checkPid())
            {
                i++;
                if (i >5)
                {
                    chek = false;
                    break;
                }
            }
            if (chek)
            {
                Thread.Sleep(sleep);
            }
            
            callback(msg, vm_panel.Name);
        }

        private bool checkPid()
        {
            Thread.Sleep(2000);
            bool flag = false;
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses(); 
            foreach (Process proc in list)
            {
                if (proc.ProcessName == "Droid4X")
                {
                    //  Console.WriteLine(item.code+"  "+proc.MainWindowTitle + ""); 
                    if (item.code.Trim() + LoopConfig.hmVersion == proc.MainWindowTitle)
                    {
                        item.pid = proc.Id + "";
                        vm_panel.Tag = item; 
                        msg = MSG_COMPLETE;
                        return true;
                    } 
                }
            }
            return flag;
        }


    }
}
