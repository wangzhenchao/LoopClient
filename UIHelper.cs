using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace LoopClient
{
    class UIHelper
    {
        public static void add_vm_panel(FlowLayoutPanel flowlayout, Panel panel, vm item, int limit = 10)
        {
            TextBox text_process_id = new TextBox();
            Label label_process_id = new Label();
            TextBox text_device_id = new TextBox();
            Label label_device_id = new Label();
            Label label_status = new Label();
            TextBox text_script = new TextBox();
            Label label_script = new Label();
            Label label_proxy = new Label();
            TextBox text_proxy = new TextBox();
            TextBox text_start_time = new TextBox();
            Label label_start_time = new Label();
            TextBox text_count_time = new TextBox();
            Label label_count_time = new Label();
            TextBox text_ghost_status = new TextBox();
            Label label_ghost_status = new Label();
            TextBox text_exe_status = new TextBox();
            Label label_exe_status = new Label();
            TextBox text_host_status = new TextBox();
            Label label_host_status = new Label();
            TextBox text_restart_limit = new TextBox();
            Label label_restart_limit = new Label();
            TextBox text_restart_time = new TextBox();
            Label label_restart_time = new Label();
            TextBox text_complete = new TextBox();
            Label label_complete = new Label();
            TextBox text_droid = new TextBox();
            Label label_droid = new Label();
            TextBox text_code = new TextBox();
            Label label_code = new Label();
            TextBox text_host = new TextBox();
            Label label_host = new Label();

            TextBox text_game_package = new TextBox();
            Label label_game_package = new Label();



            panel.BackColor = System.Drawing.SystemColors.ScrollBar;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.Controls.Add(text_process_id);
            panel.Controls.Add(label_process_id);
            panel.Controls.Add(text_device_id);
            panel.Controls.Add(label_device_id);
            panel.Controls.Add(label_status);
            panel.Controls.Add(text_script);
            panel.Controls.Add(label_script);

            panel.Controls.Add(text_start_time);
            panel.Controls.Add(label_start_time);
            panel.Controls.Add(text_count_time);
            panel.Controls.Add(label_count_time);
            panel.Controls.Add(text_ghost_status);
            panel.Controls.Add(label_ghost_status);
            panel.Controls.Add(text_exe_status);
            panel.Controls.Add(label_exe_status);
            panel.Controls.Add(text_host_status);
            panel.Controls.Add(label_host_status);
            panel.Controls.Add(text_restart_limit);
            panel.Controls.Add(label_restart_limit);
            panel.Controls.Add(text_restart_time);
            panel.Controls.Add(label_restart_time);
            panel.Controls.Add(text_complete);
            panel.Controls.Add(label_complete);
            panel.Controls.Add(text_droid);
            panel.Controls.Add(label_droid);
            panel.Controls.Add(text_code);
            panel.Controls.Add(label_code);
            panel.Controls.Add(text_host);
            panel.Controls.Add(label_host);
            panel.Controls.Add(label_proxy);
            panel.Controls.Add(text_proxy);

            panel.Controls.Add(text_game_package);
            panel.Controls.Add(label_game_package);


            panel.Location = new System.Drawing.Point(3, 3);
            panel.Size = new System.Drawing.Size(943, 133);
            // 
            // TextBox text_count_time
            // 
            text_count_time.Location = new System.Drawing.Point(639, 43);
            text_count_time.Name = "text_count_time";
            text_count_time.Enabled = false;
            text_count_time.Size = new System.Drawing.Size(100, 21);
            text_count_time.TabIndex = 64;
            // 
            // label_count_time
            // 
            label_count_time.AutoSize = true;
            label_count_time.Location = new System.Drawing.Point(586, 46);
            label_count_time.Name = "label_count_time";
            label_count_time.Size = new System.Drawing.Size(41, 12);
            label_count_time.TabIndex = 63;
            label_count_time.Text = "计时";
            // 
            //  text_process_id
            //  
            text_process_id.Location = new System.Drawing.Point(463, 13);
            text_process_id.Name = "text_process_id";
            text_process_id.ReadOnly = true;
            text_process_id.Size = new System.Drawing.Size(100, 21);
            text_process_id.TabIndex = 51;
            // 
            // label_process_id
            // 
            label_process_id.AutoSize = true;
            label_process_id.Location = new System.Drawing.Point(428, 16);
            label_process_id.Name = "label_process_id";
            label_process_id.Size = new System.Drawing.Size(29, 12);
            label_process_id.TabIndex = 50;
            label_process_id.Text = "进程";
            // 
            //  text_device_id
            // 
            text_device_id.Location = new System.Drawing.Point(639, 13);
            text_device_id.Name = "text_device_id";
            text_device_id.ReadOnly = true;
            text_device_id.Size = new System.Drawing.Size(100, 21);
            text_device_id.TabIndex = 62;
            // 
            // label_device_id
            // 
            label_device_id.AutoSize = true;
            label_device_id.Location = new System.Drawing.Point(586, 16);
            label_device_id.Name = "label_device_id";
            label_device_id.Size = new System.Drawing.Size(41, 12);
            label_device_id.TabIndex = 61;
            label_device_id.Text = "设备Id";

            // 
            //  text_script
            // 
            text_script.Location = new System.Drawing.Point(463, 43);
            text_script.Name = "text_script";
            text_script.ReadOnly = true;
            text_script.Size = new System.Drawing.Size(100, 21);
            text_script.TabIndex = 59;
            // 
            // label_script
            // 
            label_script.AutoSize = true;
            label_script.Location = new System.Drawing.Point(428, 46);
            label_script.Name = "label_script";
            label_script.Size = new System.Drawing.Size(29, 12);
            label_script.TabIndex = 58;
            label_script.Text = "脚本";
            // 
            //  text_apk_remark
            // 
            text_start_time.Location = new System.Drawing.Point(463, 73);
            text_start_time.Name = "text_apk_remark";
            text_start_time.ReadOnly = true;
            text_start_time.Size = new System.Drawing.Size(100, 21);
            text_start_time.TabIndex = 53;
            // 
            // label_apk_remark
            // 
            label_start_time.AutoSize = true;
            label_start_time.Location = new System.Drawing.Point(428, 76);
            label_start_time.Name = "label_apk_remark";
            label_start_time.Size = new System.Drawing.Size(29, 12);
            label_start_time.TabIndex = 52;
            label_start_time.Text = "描述";

            // 
            //  text_ghost
            // 
            text_ghost_status.Location = new System.Drawing.Point(349, 73);
            text_ghost_status.Name = "text_ghost_status";
            text_ghost_status.ReadOnly = true;
            text_ghost_status.Size = new System.Drawing.Size(57, 21);
            text_ghost_status.TabIndex = 49;
            // 
            // label_ghost_status
            // 
            label_ghost_status.AutoSize = true;
            label_ghost_status.Location = new System.Drawing.Point(290, 76);
            label_ghost_status.Name = "label_ghost_status";
            label_ghost_status.Size = new System.Drawing.Size(53, 12);
            label_ghost_status.TabIndex = 48;
            label_ghost_status.Text = "按键精灵";
            // 
            //  text_exe_status
            // 
            text_exe_status.Location = new System.Drawing.Point(349, 43);
            text_exe_status.Name = "text_exe_status";
            text_exe_status.ReadOnly = true;
            text_exe_status.Size = new System.Drawing.Size(57, 21);
            text_exe_status.TabIndex = 47;
            // 
            // label_exe_status
            // 
            label_exe_status.AutoSize = true;
            label_exe_status.Location = new System.Drawing.Point(290, 46);
            label_exe_status.Name = "label_exe_status";
            label_exe_status.Size = new System.Drawing.Size(53, 12);
            label_exe_status.TabIndex = 46;
            label_exe_status.Text = "执行状态";
            // 
            //  text_host_status
            // 
            text_host_status.Location = new System.Drawing.Point(349, 13);
            text_host_status.Name = "text_host_status";
            text_host_status.ReadOnly = true;
            text_host_status.Size = new System.Drawing.Size(57, 21);
            text_host_status.TabIndex = 45;
            // 
            // label_host_status
            // 
            label_host_status.AutoSize = true;
            label_host_status.Location = new System.Drawing.Point(290, 16);
            label_host_status.Name = "label_host_status";
            label_host_status.Size = new System.Drawing.Size(53, 12);
            label_host_status.TabIndex = 44;
            label_host_status.Text = "主机状态";
            // 
            //  text_restart_limit
            // 
            text_restart_limit.Location = new System.Drawing.Point(209, 73);
            text_restart_limit.Name = "text_restart_limit";
            text_restart_limit.Size = new System.Drawing.Size(57, 21);
            text_restart_limit.TabIndex = 43;
            // 
            // label_restart_limit
            // 
            label_restart_limit.AutoSize = true;
            label_restart_limit.Location = new System.Drawing.Point(162, 76);
            label_restart_limit.Name = "label_restart_limit";
            label_restart_limit.Size = new System.Drawing.Size(41, 12);
            label_restart_limit.TabIndex = 42;
            label_restart_limit.Text = "重启点";
            // 
            //  text_restart_time
            // 
            text_restart_time.Location = new System.Drawing.Point(209, 43);
            text_restart_time.Name = "text_restart_time";
            text_restart_time.ReadOnly = true;
            text_restart_time.Size = new System.Drawing.Size(57, 21);
            text_restart_time.TabIndex = 41;
            // 
            // label_restart_time
            // 
            label_restart_time.AutoSize = true;
            label_restart_time.Location = new System.Drawing.Point(162, 46);
            label_restart_time.Name = "label_restart_time";
            label_restart_time.Size = new System.Drawing.Size(41, 12);
            label_restart_time.TabIndex = 40;
            label_restart_time.Text = "重启数";
            // 
            //  text_complete
            // 
            text_complete.Location = new System.Drawing.Point(209, 13);
            text_complete.Name = "text_complete";
            text_complete.ReadOnly = true;
            text_complete.Size = new System.Drawing.Size(57, 21);
            text_complete.TabIndex = 39;
            // 
            // label_complete
            // 
            label_complete.AutoSize = true;
            label_complete.Location = new System.Drawing.Point(162, 16);
            label_complete.Name = "label_complete";
            label_complete.Size = new System.Drawing.Size(41, 12);
            label_complete.TabIndex = 38;
            label_complete.Text = "完成数";
            // 
            //  text_droid
            // 
            text_droid.Location = new System.Drawing.Point(44, 73);
            text_droid.Name = "text_droid";
            text_droid.ReadOnly = true;
            text_droid.Size = new System.Drawing.Size(100, 21);
            text_droid.TabIndex = 37;
            // 
            // label_droid
            // 
            label_droid.AutoSize = true;
            label_droid.Location = new System.Drawing.Point(8, 76);
            label_droid.Name = "label_droid";
            label_droid.Size = new System.Drawing.Size(35, 12);
            label_droid.TabIndex = 36;
            label_droid.Text = "droid";
            // 
            //  text_code
            // 
            text_code.Location = new System.Drawing.Point(44, 43);
            text_code.Name = "text_code";
            text_code.ReadOnly = true;
            text_code.Size = new System.Drawing.Size(100, 21);
            text_code.TabIndex = 35;
            // 
            // label_code
            // 
            label_code.AutoSize = true;
            label_code.Location = new System.Drawing.Point(8, 46);
            label_code.Name = "label_code";
            label_code.Size = new System.Drawing.Size(29, 12);
            label_code.TabIndex = 34;
            label_code.Text = "编号";
            // 
            //  text_host
            // 
            text_host.Location = new System.Drawing.Point(44, 13);
            text_host.Name = "text_host";
            text_host.ReadOnly = true;
            text_host.Size = new System.Drawing.Size(100, 21);
            text_host.TabIndex = 33;
            // 
            // label_host
            // 
            label_host.AutoSize = true;
            label_host.Location = new System.Drawing.Point(8, 16);
            label_host.Name = "label_host";
            label_host.Size = new System.Drawing.Size(29, 12);
            label_host.TabIndex = 0;
            label_host.Text = "主机";
            // 
            // text_proxy
            // 
            text_proxy.Location = new System.Drawing.Point(639, 73);
            text_proxy.Name = "text_proxy";
            text_proxy.ReadOnly = true;
            text_proxy.Size = new System.Drawing.Size(100, 21);
            text_proxy.TabIndex = 68;
            // 
            // label_proxy
            // 
            label_proxy.AutoSize = true;
            label_proxy.Location = new System.Drawing.Point(586, 76);
            label_proxy.Name = "label_proxy";
            label_proxy.Size = new System.Drawing.Size(29, 12);
            label_proxy.TabIndex = 67;
            label_proxy.Text = "代理";


            // 
            // text_game_package
            // 
            text_game_package.Location = new System.Drawing.Point(44, 105);
            text_game_package.Name = "text_game_package";
            text_game_package.ReadOnly = true;
            text_game_package.Size = new System.Drawing.Size(222, 21);
            text_game_package.TabIndex = 70;
            // 
            // label30
            // 
            label_game_package.AutoSize = true;
            label_game_package.Location = new System.Drawing.Point(8, 108);
            label_game_package.Name = "label30";
            label_game_package.Size = new System.Drawing.Size(29, 12);
            label_game_package.TabIndex = 69;
            label_game_package.Text = "游戏";




            // 
            // label_script_process
            // 
            label_status.AutoSize = true;
            label_status.Location = new System.Drawing.Point(290, 108);
            label_status.Name = "label_status";
            label_status.Size = new System.Drawing.Size(53, 12);
            label_status.TabIndex = 60;
            label_status.Text = "执行进度";
            //

            text_process_id.Text = item.pid;
            text_device_id.Text = "";
            label_status.Text = "";
            text_script.Text = "";
            text_start_time.Text = "";
            text_count_time.Text = "0";
            text_ghost_status.Text = "off";
            text_exe_status.Text = "stop";
            text_host_status.Text = "off";
            text_restart_limit.Text = limit + "";
            text_restart_time.Text = "0";
            text_complete.Text = "0";
            text_droid.Text = item.droid;
            text_code.Text = item.code;
            text_host.Text = item.host;


            //text_restart_limit.Text = "重启点";
            //text_restart_time.Text = "重启数";
            //text_complete.Text = "完成数";


            panel.Tag = item;
            panel.Name = item.code;


            if (!checkPidForInit(panel))
            {
                text_host_status.Text = "off";
            }

            flowlayout.Controls.Add(panel);
        }

        private static bool checkPidForInit(Panel panel)
        {
            Process[] list = Process.GetProcesses();
            vm item = (vm)panel.Tag;
            foreach (Process proc in list)
            {
                if (proc.ProcessName == "Droid4X" && item.code.Trim() + LoopConfig.hmVersion == proc.MainWindowTitle)
                {
                    item.pid = proc.Id + "";
                    panel.Controls["text_host_status"].Text = "on";
                    panel.Controls["text_process_id"].Text = proc.Id + "";
                    panel.Tag = item;
                    return true;
                }

            }
            return false;
        }

        public static int getIntByHash(string dev, int count)
        {
            try
            {
                int code = dev.GetHashCode();
                int index = code % count;
                if (index < 0)
                {
                    index = -index;
                }
                return index;
            }
            catch (Exception)
            {
                return 0;
            } 
        }


        public static int getInt(string dev,int count)
        {
            try
            {
                string str1 = dev.Substring(0, 1);
                string str2 = dev.Substring(1, 1);
                int code = asc(str1) + asc(str2) + dev.Length;
                int index = code % count;
                if (index < 0)
                {
                    index = -index;
                }
                return index;
            }
            catch (Exception)
            {
                return 0;
            }


        }

        //字符转ASCII码：
        public static int asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                return 1;
            }
        }

    }
}
