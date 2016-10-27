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
    public delegate void InstallCallBackDelegate(string msg,Panel panel);
    class InstallThread
    {
        public InstallCallBackDelegate callback;
        public static string MSG_INSTALL_COMPLETE = "MSG_INSTALL_COMPLETE";
        public static string MSG_UNINSTALL_COMPLETE = "MSG_UNINSTALL_COMPLETE";  
        public static string MSG_ERROR = "ERROR";
        public string msg = MSG_ERROR;   
        public Panel vm_panel;
        public vm item;
        public bool cmd;
        public string vm_temp; 
        public string apk_path;
        public string uninstall_package;

        Process install = null;

        int installid = 0; 

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
                FileHelper.writeLogs(LoopConfig.log + "InstallThread.txt", item.code + "  " + method + "_kill_error: " + ex.StackTrace);
            }
        }
        public void KillCMD()
        {
            if (installid != 0)
            {
                kill(installid, "install");
            } 
           
        }
         
        public void doInstall()
        {
            item = (vm)vm_panel.Tag;
            vm_temp = item.code;
            do_install();
            callback(msg, vm_panel);
        }

        public void doUinstall()
        {
            item = (vm)vm_panel.Tag;
            vm_temp = item.code;
            try
            {
                install = new Process();
                if (!cmd)
                {
                    install.StartInfo.UseShellExecute = false;
                    install.StartInfo.CreateNoWindow = true;
                    install.StartInfo.RedirectStandardInput = true;
                    install.StartInfo.RedirectStandardOutput = true;
                }
              
                string temp_start = LoopConfig.bat_temp + vm_temp + "_vm_uninstall.bat";
                File.Copy(LoopConfig.bat + "vm_uninstall.bat", temp_start, true);
                install.StartInfo.FileName = temp_start;
                string title = item.code + "--" + "vm_uninstall";
                install.StartInfo.Arguments = item.host + "  " + uninstall_package + "  " + title;
                vm_panel.Controls["label_status"].Text = "正在卸载 " + uninstall_package;
                install.Start();
                installid = install.Id; 
                install.WaitForExit();
                install.Close();
                install = null;
                installid = 0;
                if (File.Exists(temp_start)) File.Delete(temp_start);
                vm_panel.Controls["label_status"].Text = "卸载完成";

                msg = MSG_UNINSTALL_COMPLETE;
                callback(msg, vm_panel);
            }
            catch (Exception ex)
            {
                KillCMD();
                msg = MSG_ERROR;
                FileHelper.writeLogs(LoopConfig.log + "InstallThread.txt", item.code + "  do_click_error: " + ex.StackTrace);
            }   
        }


        public void do_install() { 
            try
            {
                install = new Process();

                if (!cmd)
                {
                    install.StartInfo.UseShellExecute = false;
                    install.StartInfo.CreateNoWindow = true;
                    install.StartInfo.RedirectStandardInput = true;
                    install.StartInfo.RedirectStandardOutput = true;
                    string temp_start = LoopConfig.bat_temp + vm_temp + "_vm_install.bat";
                    File.Copy(LoopConfig.bat + "vm_install.bat", temp_start, true);
                    install.StartInfo.FileName = temp_start;
                    string title = item.code + "--" + "vm_install";
                    install.StartInfo.Arguments = item.host + "  " + apk_path + "  " + title;
                    vm_panel.Controls["label_status"].Text = "正在安装";
                    install.Start();
                    StreamReader output_reader = install.StandardOutput;//截取输出流         
                    while (!output_reader.EndOfStream)
                    {
                        string output = output_reader.ReadLine();
                        Console.WriteLine(output);

                        if (output == "Success")
                        {
                            msg = MSG_INSTALL_COMPLETE;
                            break;
                        }
                    }
                    output_reader.Close();
                    installid = install.Id;
                    install.WaitForExit();
                    install.Close();
                    if (cmd)
                    {
                        msg = MSG_INSTALL_COMPLETE;
                    }
                    install = null;
                    installid = 0;
                    if (File.Exists(temp_start)) File.Delete(temp_start);
                }
                else
                { 
                    string temp_start = LoopConfig.bat_temp + vm_temp + "_vm_install.bat";
                    File.Copy(LoopConfig.bat + "vm_install_pause.bat", temp_start, true);
                    install.StartInfo.FileName = temp_start;
                    string title = item.code + "--" + "vm_install";
                    install.StartInfo.Arguments = item.host + "  " + apk_path + "  " + title;
                    vm_panel.Controls["label_status"].Text = "正在安装";
                    install.Start();
                    
                    installid = install.Id;
                    install.WaitForExit();
                    install.Close();
                    msg = MSG_INSTALL_COMPLETE;
                    install = null;
                    installid = 0;
                    if (File.Exists(temp_start)) File.Delete(temp_start); 
                }
                   
            }
            catch (Exception ex)
            {
                KillCMD();
                msg = MSG_ERROR;
                FileHelper.writeLogs(LoopConfig.log + "InstallThread.txt", item.code + "  do_click_error: " + ex.StackTrace); 
            }   
        } 
    }
}
