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
    public delegate void LoopCallBackDelegate(string msg, string droid, deviceItem dev = null);

    class LoopThread
    {
        public LoopCallBackDelegate callback;
        public deviceItem dev;
        public proxyItem proxy;
        public Panel vm_panel;
        public script spt;
        public static string MSG_COMPLETE = "COMPLETE";
        public static string MSG_STORE = "MSG_STORE";
        public static string MSG_BREAK = "MSG_BREAK";
        public static string MSG_TIME_OUT = "MSG_TIME_OUT";//  
        public static string MSG_KILL = "MSG_KILL";// 游戏点击 
        public string msg = MSG_TIME_OUT;
        public bool gameFlag = false;
        vm item;
        public string states = "";
        public string vm_temp = "";

        public string name = "";
        public bool exeStatus = false;
        public int killTime = 0;
        public bool check_cmd = false;
        private bool check_code_status = false;


        public int first_level = 2;
        public int to_level;

        public string game_package;


        Process click = null;
        int clickid = 0;


        Process hack = null;
        Process start = null;
        Process dogame = null;
        Process check = null;
        Process read = null;
        Process clear = null;
        Process delete = null;
        Process shut = null;
        Process sys = null;
        Process pull = null;
        Process push = null;
        Process stop = null;
        Process list = null;
        Process apk_check = null;


        int apk_checkid = 0;
        int listid = 0;
        int stopid = 0;
        int hackid = 0;
        int sysid = 0;
        int startid = 0;
        int shutid = 0;
        int dogameid = 0;
        int checkid = 0;
        int clearid = 0;
        int pullid = 0;
        int pushid = 0;
        int deleteid = 0;
        int readid = 0;
        public bool go_flag = true;
        public bool hack_test = false;

        public void kill_clear()
        {
            try
            {
                msg = MSG_KILL;
                startProcess(stop, stopid, item.host + "  " + spt.apk_package, "apk_clear.bat");
                startProcess(stop, stopid, item.host + "  " + game_package, "apk_clear.bat");
                vm_panel.Controls["label_status"].Text = "kill_cmd";
                KillCMD();
                vm_panel.Controls["label_status"].Text = "完成";
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + "  kill_clear: " + ex.StackTrace);
            } 
        }

        public void KillCMD()
        {
            go_flag = false;
            msg = MSG_KILL;
            if (clickid != 0)
            {
                kill(clickid, "do_anjian_click");
            }
            if (pushid != 0)
            {
                kill(pushid, "push");
            }
            if (hackid != 0)
            {
                kill(hackid, "hack");
            }
            if (startid != 0)
            {
                kill(startid, "start");
            }
            if (dogameid != 0)
            {
                kill(dogameid, "dogame");
            }
            if (pullid != 0)
            {
                kill(hackid, "pull");
            }
            if (deleteid != 0)
            {
                kill(deleteid, "delete");
            }
            if (listid != 0)
            {
                kill(listid, "list");
            }
            if (checkid != 0)
            {
                kill(checkid, "check");
            }
            if (readid != 0)
            {
                kill(readid, "read");
            }
            if (clearid != 0)
            {
                kill(checkid, "clear");
            }
            if (shutid != 0)
            {
                kill(shutid, "shut");
            }
            if (stopid != 0)
            {
                kill(stopid, "stop");
            }
            if (apk_checkid != 0)
            {
                kill(apk_checkid, "apk_check");
            }
        }
        public void kill(int pid, string method)
        {
            try
            {
                Process kill = new Process();
                int killid = 0;
                startProcess(kill, killid, pid + "", "kill_t.bat");
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + "  " + method + "_kill_error: " + ex.StackTrace);
            }
        }
          
        public void doWork()
        {
            exeStatus = true;
            // 此代码段异常，保存device
            int index;
            index = UIHelper.getInt(dev.device_id, spt.game_packages.Count);

            //Random randrom = new Random((int)DateTime.Now.Ticks);
            //index=randrom.Next(spt.game_packages.Count);


            if(spt.game_code=="dddld"&&index==4){
                callback(MSG_BREAK, vm_panel.Name, dev);
                return;
            }
            Console.WriteLine(dev.packageName);
            if (dev.packageName == null||dev.packageName=="")
            {            
                game_package = spt.game_packages[index];
                dev.packageName = game_package;
            }
            else
            {
                game_package = dev.packageName;
            }
           
            try
            {
                msg = MSG_STORE;
                item = (vm)vm_panel.Tag;
                vm_panel.Controls["text_device_id"].Text = dev.device_type+"--"+dev.device_id;  
                vm_panel.Controls["text_proxy"].Text = proxy.proxyIp + ";" + proxy.proxyPort;
                vm_panel.Controls["text_game_package"].Text = game_package; 

                vm_temp = item.code;
                int i = 0;
                // 启动按键精灵
                if (!checkApp(spt.apk_activity)||vm_panel.Controls["text_script"].Text != spt.game_platform || vm_panel.Controls["text_ghost_status"].Text == "off")
                {
                    if (vm_panel.Controls["text_script"].Text != "")
                    {
                        string apk_package = get_apk_package(vm_panel.Controls["text_script"].Text);
                        shut_apk(apk_package);
                    }

                    while (!restartApp(spt.apk_package, spt.apk_activity) && go_flag)
                    {
                        if (i > 10)
                        {
                            FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + "  restart_anjian_error: > 10");
                            callback(msg, vm_panel.Name, dev);
                            go_flag = false;
                            return;
                        }
                        i++;
                    }
                    // 授权按键精灵
                    do_click();
                }
                vm_panel.Controls["text_script"].Text = spt.game_platform;
                vm_panel.Controls["text_ghost_status"].Text = "on";
                clear_vm_file();
                
                if (!setDevice()) {
                    callback(msg, vm_panel.Name, dev);
                    return;
                }
              
                if (spt.game_code == "xjwqz"||spt.game_code == "tlbb"||spt.game_code=="dddld")
                {
                    setSystem();
                } 
                setLevel(dev.level);

                //dev.uname = "1152951531";
                //dev.password = "wzciamaman";

                string content = dev.uname + "\n" + dev.password + "\n" + spt.game_code + "\n" + dev.level + "\n" + to_level + "\n" + dev.active + "\n" + dev.server_id;
                push_vm_file("game_config.txt", content);

                i = 0;
                //              shut_apk("com.lyhd.hook"); 
                while (go_flag && !list_vm_file("sdcard", "game_config.txt"))
                {
                    push_vm_file("game_config.txt", content);
                    if (i > 3)
                    {
                        go_flag = false;
                        FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + " data: " + dev.ToJson() + "  game_config.txt_null: >10");
                        callback(MSG_BREAK, vm_panel.Name, dev);
                        return;
                    }
                    i++;
                } 
                vm_panel.Controls["text_apk_remark"].Text = "active:" + dev.active + ";" + dev.level + "-" + to_level;
                if (hack_test) return;
                i = 0; 
  //              shut_apk("com.lyhd.hook"); 
                while (go_flag && !restartApp(game_package, spt.game_activity))
                {
                    if (i > 10)
                    {
                        go_flag = false;
                        FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + " data: " + dev.ToJson() + "  restart_game_error: >10");
                        callback(msg, vm_panel.Name, dev);
                        return;
                    }
                    i++;
                }
                i = 0;

                if (!checkApp(spt.apk_activity))
                {
                    callback(msg, vm_panel.Name, dev);
                    return;
                }

                anjian_ghost();

                i = 0;
                while (go_flag && !list_vm_file("sdcard","game_start.txt"))
                {
                    anjian_ghost();
                    if (i >= spt.click_count)
                    {
                        dev.isError = true; 
                        callback(msg, vm_panel.Name, dev);
                        return;
                    }
                    i++;
                }

            }
            catch (Exception ex)
            {
                go_flag = false;
                FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + "   MSG_STORE   data:" + dev.ToJson() + " " + ex.StackTrace);
                callback(msg, vm_panel.Name, dev);
                return;
            }
            // 此代码段异常，device 作废;

            try
            {
                msg = MSG_TIME_OUT;
                int t = 0;
                if (spt.isLogin_code)
                {
                    msg = MSG_STORE;
                    while (go_flag && !check_vm_file("game_code_over.txt"))
                    {
                        if (go_flag && t % 5 == 0 && !checkApp(spt.apk_activity) && !checkApp(spt.game_activity))
                        {
                            go_flag = false; 
                            callback(msg, vm_panel.Name, dev);
                            return;
                        }
                        t++;
                    }
                    if (check_code_status)
                    {
                        check_code();
                    }
                                      
                } 
                 t = 0;
                //检测游戏结束  
                while (go_flag && !check_vm_file("game_over.txt"))
                {
                    if (go_flag && t % 5 == 0 && !checkApp(spt.apk_activity) && !checkApp(spt.game_activity))
                    {
                        go_flag = false; 
                        callback(msg, vm_panel.Name, dev);
                        return;
                    }
                    t++;
                }
               
                dev.old_level = dev.level;
                dev.level = to_level;
                if (go_flag && spt.isRecord_server && dev.active == 0)
                {
                    read_server_id();
                }
               
            }
            catch (Exception ex)
            {
                go_flag = false;
                dev.isError = true;
                FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + " MSG_TIME_OUT data:  " + dev.ToJson() + " " + ex.StackTrace);
            }

            // 此代码段清缓存异常，继续执行;
            try
            {
             //   clear_vm_file();
                shut_apk(game_package); 

            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "loopThead.txt", item.code + "  shut_game data: " + dev.ToJson() + "  " + ex.StackTrace);
            }
            exeStatus = false;
            if (go_flag)
            {
                msg = MSG_COMPLETE;
            }
            callback(msg, item.code, dev);
        }
        private int list_time = 0;
        private bool code_result = false;
        public void check_code()
        {
           
            if (!go_flag) return;
            if (!login()) return;

            string local = LoopConfig.temp + vm_temp + "_game_code.png";
            int t = 0;
            while (go_flag && !list_vm_file("sdcard", "game_code.png"))
            {

                if (go_flag && t % 4 == 0 && !checkApp(spt.apk_activity) && !checkApp(spt.game_activity))
                {
                    go_flag = false;
                    dev.isError = true; 
                    return;
                }
                t++; 
            }
            list_time++;
            pull_vm_file("game_code.png", local);
            StringBuilder pCodeResult = new StringBuilder(new string(' ', 30)); // 分配30个字节存放识别结果   
            int nCaptchaId = YDMWrapper.YDM_DecodeByPath(local, 1004, pCodeResult);
            Console.WriteLine(nCaptchaId + "");
             
            delete_vm_file("game_code.png");
            if (nCaptchaId > 0)
            {
                string content = 1 + "\n" + pCodeResult.ToString();
                Console.WriteLine(content);
                push_vm_file("game_code.txt", content);
                int i = 0;
                while (go_flag && !check_vm_file( "game_code_result.txt"))
                { 
                    if (i > 5)
                    {
                        go_flag = false;
                        return;
                    }
                    i++;

                } 
                delete_vm_file("game_code_result.txt");
                if (!code_result)
                { 
                    check_code();
                    return;
                }  
            }
            else
            {
                string content = 0 + "\n";
                push_vm_file("game_code.txt", content);
                if (list_time < 6)
                {
                    check_code();
                }
                else
                {
                    go_flag = false;
                }

            }
        }

        public void do_click()
        {
            if (!go_flag) return;
            string title = item.code + "--" + "anjian_click";
            startProcess(click, clickid, item.host + "  " + title, "anjian_click.bat");
        }

        public void setSystem()
        {
            if (!go_flag) return;
            startProcess(sys, sysid, item.host + "  " + dev.mac + " " + dev.imei + " " + dev.sim_iccid, "vm_sys.bat");
        } 
        public bool setDevice()
        {
            if (!go_flag) return false; 
            setDevIp(); 
            string filter = "";
            if (spt.filter != "")
            {
                filter = spt.filter; 
            }
            else
            {
                filter = proxy.proxyFilter;
            } 
            string param = "brand=" + dev.brand + "&device=" + dev.device + "&product=" + dev.product + "&board=" + dev.board + "&hardware=" + dev.hardware + "&operator_id=" + dev.operator_id + "&operator_name=" + dev.operator_name + "&android_id=" + dev.android_id + "&imei=" + dev.imei + "&mac=" + dev.mac + "&phone=" + dev.phone + "&sim_iccid=" + dev.sim_iccid + "&sim_state=" + dev.sim_state + "&sim_imsi=" + dev.sim_imsi + "&device_id=" + dev.device_id + "&device_type=" + dev.device_type + "&wifi_ssid=" + dev.wifi_ssid + "&wifi_bssid=" + dev.wifi_bssid + "&ip=" + dev.ip +
                "&proxyIp=" + proxy.proxyIp + "&proxyPort=" + proxy.proxyPort + "&proxyFilter=" + filter + "&gateway=" + dev.gateway + "&gamePackageName=" + game_package;

            //param = "brand=&device=&product=&board=&hardware=&operator_id=46001&operator_name=中国联通&android_id=19ec005af7c4f73a&imei=869818065776709&mac=ec:e3:a2:73:fc:34&phone=13980888463&sim_iccid=89860101888370512110&sim_state=5&sim_imsi=460013341028049&device_id=1002178&device_type=OPPO R7sm&wifi_ssid=pJKa4JW1&wifi_bssid=bb:6b:21:00:6d:ee&ip=192.168.2.11&proxyIp=10.10.10.107&proxyPort=9999&proxyFilter=&gateway=192.168.2.1&gamePackageName=com.telecom.video.ikan4g" + "&proxyFilter=" + filter;

            if(dev.game=="yhzg"){
                param = "brand=" + dev.brand + "&device=" + dev.device + "&product=" + dev.product + "&board=" + dev.board + "&hardware=" + dev.hardware + "&operator_id=" + dev.operator_id + "&operator_name=" + dev.operator_name + "&android_id=" + dev.android_id + "&imei=" + dev.imei + "&mac=" + dev.mac + "&phone=" + dev.phone + "&sim_iccid=" + dev.sim_iccid + "&sim_state=" + dev.sim_state + "&sim_imsi=" + dev.sim_imsi + "&device_id=" + dev.device_id + "&device_type=" + dev.device_type + "&wifi_ssid=" + dev.wifi_ssid + "&wifi_bssid=" + dev.wifi_bssid + "&ip=" + dev.ip +
               "&gateway=" + dev.gateway + "&gamePackageName=" + game_package;

            }



            vm_panel.Controls["label_status"].Text = " 游戏包名 " + "  " + game_package;

            if (game_package=="")
            {
                return false ;
            }

            Console.WriteLine(param);
            string js = System.Web.HttpUtility.UrlEncode(param);
            startProcess(hack, hackid, item.host + "  " + js, "hack.bat");
            return true;
             
        }
        public void push_vm_file(string filename, string content)
        {
            if (!go_flag) return;
            string local = LoopConfig.temp + vm_temp + "_game_temp.txt";
            if (dev.uname == null || dev.uname == "") dev.uname = "test_unname";
            if (dev.password == null || dev.password == "") dev.password = "test_password";
            FileHelper.writeContent(local, content);
            startProcess(push, pushid, item.host + "  " + local + "  " + filename, "vm_push_file.bat");
        }
        public void pull_vm_file(string filename, string local)
        {
            if (!go_flag) return;
            if (File.Exists(local))
            {
                File.Delete(local);
            }
            startProcess(pull, pushid, item.host + " " + filename + "  " + local, "vm_pull_file.bat");
        }

        public void clear_vm_file()
        {
            if (!go_flag) return;
            startProcess(clear, clearid, item.host, "vm_clear_file.bat");
            string game_bat = "vm_clear_file_"+spt.game_code+".bat";
            string clear_game = LoopConfig.bat + game_bat;
            if(File.Exists(clear_game)){
                clear_vm_file(game_bat);
            } 
            if (spt.isLogin_qq)
            {
                shut_apk(LoopConfig.qq_package);
            }

        }
        public void clear_vm_file(string clear_game)
        {
            if (!go_flag) return;
            startProcess(clear, clearid, item.host, clear_game);
        }
        public void delete_vm_file(string filename)
        {
            if (!go_flag) return;
            startProcess(delete, deleteid, item.host + "  " + filename, "vm_delete_file.bat");
        }

        public void anjian_ghost()
        {
            if (!go_flag) return;
            string title = "-" + item.code + "-anjian_ghost.bat";  
            int delay = spt.click_delay;

            startProcess(dogame, dogameid, item.host + " " + title + "  " + delay, "anjian_ghost.bat", check_cmd);
        }


        public void shut_apk(string package)
        {
            if (!go_flag || package == "") return;
            startProcess(shut, shutid, item.host + "  " + package, "apk_clear.bat");
            vm_panel.Controls["label_status"].Text = "apk_clear.bat" + "  " + package;
        }



        public void startProcess(Process process, int id, string args, string bat_name, bool cmd = false)
        {
            process = new Process();
            if (!cmd)
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
            }
            string temp_proc = LoopConfig.bat_temp + vm_temp + getName() + "_" + bat_name;
            File.Copy(LoopConfig.bat + bat_name, temp_proc, true);
            process.StartInfo.FileName = temp_proc;
            process.StartInfo.Arguments = args;
            vm_panel.Controls["label_status"].Text = bat_name+" "+args;
            process.Start();
            id = process.Id;
            process.WaitForExit();
            process.Close();
            process = null;
            id = 0;
            if (File.Exists(temp_proc)) File.Delete(temp_proc);

        }

        public bool list_vm_file(string path, string filename)
        {
            bool result = false;
            string temp_start = "";
            list = new Process();
            list.StartInfo.UseShellExecute = false;
            list.StartInfo.CreateNoWindow = true;
            list.StartInfo.RedirectStandardInput = true;
            list.StartInfo.RedirectStandardOutput = true;
            temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_list_file.bat";
            File.Copy(LoopConfig.bat + "vm_list_file.bat", temp_start, true);
            list.StartInfo.FileName = temp_start;
            list.StartInfo.Arguments = item.host + "  " + path;
            vm_panel.Controls["label_status"].Text = "list_" + path+" "+filename;
            list.Start();
            listid = list.Id;
            StreamReader output_reader = list.StandardOutput;//截取输出流         
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                //      Console.WriteLine(output); 
                if (output == filename)
                {
                    Console.WriteLine(output);
                    result = true;
                }
            }
            output_reader.Close();
            list.WaitForExit();
            list.Close();
            list = null;
            listid = 0;
            if (File.Exists(temp_start)) File.Delete(temp_start);
            return result;

        }


        public void read_server_id()
        {
            string file = "game_server.txt";
            int serverid = 0;
            string temp_start = "";
            read = new Process();
            read.StartInfo.UseShellExecute = false;
            read.StartInfo.CreateNoWindow = true;
            read.StartInfo.RedirectStandardInput = true;
            read.StartInfo.RedirectStandardOutput = true;
            temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_check_file.bat";
            File.Copy(LoopConfig.bat + "vm_check_file.bat", temp_start, true);
            read.StartInfo.FileName = temp_start;
            read.StartInfo.Arguments = item.host + "  " + file;
            vm_panel.Controls["label_status"].Text = "read_" + file;
            read.Start();
            readid = read.Id;
            StreamReader output_reader = read.StandardOutput;//截取输出流         
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                Console.WriteLine(output); 
                try
                {
                    serverid = Convert.ToInt32(output);
                }
                catch (Exception)
                { 
                }
                vm_panel.Controls["label_status"].Text = "read_" + file + " " + serverid;   
            }
            output_reader.Close();
            read.WaitForExit();
            read.Close();
            read = null;
            readid = 0;
            if (File.Exists(temp_start)) File.Delete(temp_start);
            dev.server_id = serverid; 
        } 

        public bool check_vm_file(string file)
        {
            bool result = false;
            string temp_start = "";
            check = new Process();
            check.StartInfo.UseShellExecute = false;
            check.StartInfo.CreateNoWindow = true;
            check.StartInfo.RedirectStandardInput = true;
            check.StartInfo.RedirectStandardOutput = true;
            temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_check_file.bat";
            File.Copy(LoopConfig.bat + "vm_check_file.bat", temp_start, true);
            check.StartInfo.FileName = temp_start;
            check.StartInfo.Arguments = item.host + "  " + file;
            vm_panel.Controls["label_status"].Text = "check_" + file;
            check.Start();
            checkid = check.Id;
            StreamReader output_reader = check.StandardOutput;//截取输出流         
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                Console.WriteLine(output);

                if (output.StartsWith("1"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file+" 1";
                    result = true;
                    //
                    code_result = true ;
                    check_code_status = true;
                }
                // game_over 异常 数据作废
                if (output.StartsWith("2"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file + " 2";
                    msg = MSG_COMPLETE; 
                    result = true;
                    dev.isError = true;
                    go_flag = false;
                }
                // 验证码 检测回收
                if (output.StartsWith("3"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file + " 3";
                    result = true;
                    go_flag = false; 
                }
                // 验证码 不用检测
                if (output.StartsWith("4"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file + " 4";
                    result = true;
                    check_code_status = false; 
                }
                // 检测失败 继续获取
                if (output.StartsWith("5"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file + " 5";
                    result = true;
                    code_result = false; 
                }
                // game_over 检测回收
                if (output.StartsWith("6"))
                {
                    vm_panel.Controls["label_status"].Text = "check_" + file + " 6";
                    result = true;
                    go_flag = false;
                }
            }
            output_reader.Close();
            check.WaitForExit();
            check.Close();
            check = null;
            checkid = 0;
            if (File.Exists(temp_start)) File.Delete(temp_start);
            return result;
        }
        public bool restartApp(string package, string activity)
        {
            if (!go_flag) return true;
            bool flag = false;

            start = new Process();
            start.StartInfo.UseShellExecute = false;
            start.StartInfo.CreateNoWindow = true;
            start.StartInfo.RedirectStandardInput = true;
            start.StartInfo.RedirectStandardOutput = true;
            string temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_apk_restart.bat";
            File.Copy(LoopConfig.bat + "apk_restart.bat", temp_start, true);
            start.StartInfo.FileName = temp_start;  
            start.StartInfo.Arguments = item.host + "  " + package + "  " + package + @"/" + activity;
            start.Start();
            vm_panel.Controls["label_status"].Text = "apk_restart.bat：" + package;
            StreamReader output_reader = start.StandardOutput;//截取输出流         
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                Console.WriteLine(output);
                if (output.StartsWith("Status"))
                {
                    Console.WriteLine(item.host + "  " + item.code + "  " + package + "  " + output);
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
            startid = start.Id;
            start.WaitForExit();
            start.Close();
            start = null;
            startid = 0;
            if (File.Exists(temp_start)) File.Delete(temp_start);

            return flag;

        }



        public bool checkApp(string activity)
        {
            if (!go_flag) return true;
            bool flag = false;

            apk_check = new Process();
            apk_check.StartInfo.UseShellExecute = false;
            apk_check.StartInfo.CreateNoWindow = true;
            apk_check.StartInfo.RedirectStandardInput = true;
            apk_check.StartInfo.RedirectStandardOutput = true;
            string temp_start = LoopConfig.bat_temp + vm_temp + getName() + "_apk_check.bat";
            File.Copy(LoopConfig.bat + "apk_check.bat", temp_start, true);
            apk_check.StartInfo.FileName = temp_start; 
            apk_check.StartInfo.Arguments = item.host + "  " + activity;
            apk_check.Start();
            StreamReader output_reader = apk_check.StandardOutput;//截取输出流     
            int i = 0;
            while (!output_reader.EndOfStream)
            {
                string output = output_reader.ReadLine();
                Console.WriteLine(output);
                if (output.StartsWith("TASK"))
                {
                    flag = true;
                    break;
                }
               
            }
            output_reader.Close();
            apk_checkid = apk_check.Id;
            apk_check.WaitForExit();
            apk_check.Close();
            apk_check = null;
            apk_checkid = 0;
            if (File.Exists(temp_start)) File.Delete(temp_start); 
            return flag; 
        }
         
        private string get_apk_package(string game_platform)
        {
            List<script> script_list = XmlHelper.xmlToList<script>(LoopConfig.scriptXml);
            foreach (script spt in script_list)
            {
                if (spt.game_platform == game_platform)
                    return spt.apk_package;
            }
            return null;
        }
        public void setLevel(int level)
        {
            Random randrom = new Random((int)DateTime.Now.Ticks);
            // 1:10 的几率 提升0 级
            if (level != 0 && (level >= spt.apk_level || randrom.Next(0, 10) == 0))
            {
                to_level = level;
            }
            if (level >= 5)
            {
                to_level = level + 1;
            }
            else
            {
                to_level = level + randrom.Next(1, first_level + 1);
                if (to_level >= spt.apk_level)
                {
                    to_level = spt.apk_level;
                }
            }
        }
        static string getName()
        {
            string str = "";
            Random randrom = new Random((int)DateTime.Now.Ticks);
            string chars = "0123456789";
            for (int i = 0; i < 3; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
        }


         private  void  setDevIp()
        { 
            Random randrom = new Random((int)DateTime.Now.Ticks);
            int ip= randrom.Next(0, 4); 
            dev.gateway="192.168."+ip+".1";
            dev.ip="192.168."+ip+"."+getIpLast();
        }
         
        private  int getIpLast(){

            Random randrom = new Random((int)DateTime.Now.Ticks);
            int ip= randrom.Next(3, 20); 
            int ip2= randrom.Next(100, 150);

            if( randrom.Next(0, 2)==0){
                 return ip; 

            } else{
                 return ip2; 
            }
            

        }



        private bool login()
        {
            int nAppId;         // 软件ＩＤ，开发者分成必要参数。登录开发者后台【我的软件】获得！
            string lpAppKey;    // 软件密钥，开发者分成必要参数。登录开发者后台【我的软件】获得！ 
            nAppId = 1;
            lpAppKey = "22cc5376925e9387a23cf797cb9ba745";
            YDMWrapper.YDM_SetAppInfo(nAppId, lpAppKey);
            int ret;
            ret = YDMWrapper.YDM_Login("kongpinde", "123qweasdzxc");
            YDMWrapper.YDM_SetTimeOut(30);

            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }


    }
}
