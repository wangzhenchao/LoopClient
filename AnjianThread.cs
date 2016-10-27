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
    public delegate void AnjianStartCallBackDelegate(string msg, Panel panel); 
    class AnjianThread
    {
        public AnjianStartCallBackDelegate callback; 
        public static string MSG_START_COMPLETE = "START_COMPLETE";  
        public static string MSG_ERROR = "ERROR";
        public string msg = MSG_ERROR;  
        public int sleep = 0;
        public Panel vm_panel;
        public vm item;
        public string vm_temp; 

        public string package;
        public string activity;

        Process start = null;
        Process click = null;

        int startid = 0;
        int clickid = 0;

        public void kill(int pid, string method)
        {
            try
            {
                Process kill = new Process();
                kill.StartInfo.UseShellExecute = false;
                kill.StartInfo.CreateNoWindow = true;
                kill.StartInfo.RedirectStandardInput = true;
                kill.StartInfo.RedirectStandardOutput = true;
                kill.StartInfo.FileName = LoopConfig.bat + "kill_t.bat";
                kill.StartInfo.Arguments = pid + "";
                kill.Start();
                kill.WaitForExit();
                kill.Close();
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "AnjianThread.txt", item.code + "  " + method + "_kill_error: " + ex.StackTrace);
            }
        }
        public void KillCMD()
        { 
            if (startid != 0)
            {
                kill(startid, "start");
            } 
            if (clickid != 0)
            {
                kill(clickid, "click");
            } 
        } 

        public void doStart()
        {
            item = (vm)vm_panel.Tag;
            vm_temp = item.code; 
            int i=0;
            while(!restart()){
                if (i > 5 )
                {
                    msg = MSG_ERROR;
                    FileHelper.writeLogs(LoopConfig.log + "Anjian_Ghost.txt", item.code + "  restart_anjian_error: >5");
                    callback(msg, vm_panel);
                    return;
                }
                i++;
                Thread.Sleep(2000); 
            }
            do_click(); 
            callback(msg, vm_panel);
        } 

        public void do_click() { 
            try
            {
                click = new Process();
                click.StartInfo.UseShellExecute = false;
                click.StartInfo.CreateNoWindow = true;
                click.StartInfo.RedirectStandardInput = true;
                click.StartInfo.RedirectStandardOutput = true;
                string temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_anjian_click.bat";
                File.Copy(LoopConfig.bat + "anjian_click.bat", temp_start, true);
                click.StartInfo.FileName = temp_start;
                string title = item.code + "--" + "anjian_click";
                click.StartInfo.Arguments = item.host + "  " + title;
                vm_panel.Controls["vm_bat"].Text = "anjian_click.bat";
                click.Start();
                clickid = click.Id;
                click.WaitForExit();
                click.Close();
                click = null;
                clickid = 0;
                if (File.Exists(temp_start)) File.Delete(temp_start);
                msg = MSG_START_COMPLETE;
            }
            catch (Exception ex)
            {
                KillCMD();
                msg = MSG_ERROR; 
                FileHelper.writeLogs(LoopConfig.log + "Anjian_Ghost.txt", item.code + "  do_click_error: " + ex.StackTrace); 
            } 
        }
      
        static string getName()
        {
            string str = "";
            Random randrom = new Random((int)DateTime.Now.Ticks);
            // string chars = "0123456789ABCDEFGHIJKLMNOPQSTUVWXYZabcdefghijklmnopqstuvwxyz";
            string chars = "0123456789";
            for (int i = 0; i < 3; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
        }
        public bool restart()
        {
            bool flag = false;
            try
            {
                start = new Process();
                start.StartInfo.UseShellExecute = false;
                start.StartInfo.CreateNoWindow = true;
                start.StartInfo.RedirectStandardInput = true;
                start.StartInfo.RedirectStandardOutput = true;
                string temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_anjian_start.bat";
                File.Copy(LoopConfig.bat + "anjian_start.bat", temp_start, true);

                string title = item.code + "  " + "anjian_start";
                vm_panel.Controls["vm_bat"].Text = "anjian_start.bat";
                start.StartInfo.FileName = temp_start;
                start.StartInfo.Arguments = item.host + "  " + package + "  " + package + @"/" + activity + "  " + title; 
                start.Start();
                startid = start.Id;
                StreamReader output_reader = start.StandardOutput;//截取输出流         
                while (!output_reader.EndOfStream)
                {
                    string output = output_reader.ReadLine();
                    Console.WriteLine(output);
                    if (output.StartsWith("Status"))
                    {
                        Console.WriteLine(item.host + "  " + item.code + " andjiang " + "  " + output);
                        if (output == "Status: ok")
                        {
                            flag = true; 
                        }
                        else
                        {
                            flag = false;
                        }
                        break;
                    }
                }
                output_reader.Close(); 
                start.WaitForExit();
                start.Close();
                start = null;
                startid = 0;
                if (File.Exists(temp_start)) File.Delete(temp_start);
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "Anjian_Ghost.txt", item.code + "  restart_anjian_error: " + ex.StackTrace); 
                KillCMD(); 
                callback(msg, vm_panel); 
                return false;
            }
            return flag; 
        }
         
    }
}
