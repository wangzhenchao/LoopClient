using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LoopClient
{

    public partial class LoopForm : Form
    {

        public int flag = 0;
        //  string url = "http://guanliying.repairsky.svc.linnyou.com/client/requestDevices";
        //   string url = "http://lijinling.repairsky.svc.linnyou.com/client/listDevices";
      
          string url = "http://guanliying.repairsky.svc.linnyou.com/client/listDevices?game=dhxy";

        string game = "";
        string package = "";
        string activity = "";
        int request_time = 0;
        int limit_num = 10;
        int allsecs = 0;

        int start_num = 15;
        bool closing = false;
        bool time_ok = true;
        public List<script> script_list;

        public LoopForm()
        {
            InitializeComponent();
        }

        private void LoopForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;// 控件，其他线程调用  
            initVmlist();
            initDevlistView();
            initConfig();

            data data = XmlHelper.xmlToData(LoopConfig.devicesXml);
            if (data != null && data.devices != null)
            {
                game = data.game;
                package = data.package;
                activity = data.activity;
                this.lab_game.Text = game;
                this.lab_package.Text = package;
                this.lab_activity.Text = activity;

                if (data.devices.Count > 0)
                {
                    initDevData(data.devices);
                }
                else
                {
                    request();
                }
            }
            lab_start_time.Text = DateTime.Now.ToString();
            timer2.Enabled = true;
        }
        public void initConfig()
        {
            script_list = XmlHelper.xmlToList<script>(LoopConfig.scriptXml);

        }
        public void initVmlist()
        {
            this.flowVm.Controls.Clear();
            List<vm> vmlist = XmlHelper.xmlToList<vm>(LoopConfig.vmsXml);
            if (vmlist.Count <= 0) return;
            foreach (vm item in vmlist)
            {
                if (item.isOk == "1")
                {
                    addtoPanel(item);
                }
            }

        }
        public void request()
        { 
            if (!time_ok) return;

            if (check_quest.Checked) return;

            GC.Collect();
            string request_game = text_request_game.Text.ToString(); 
            string res = HttpHelper.SendGet(url);
            List<deviceItem> lst;
            MsgJson msg;
            try
            {
                msg = JsonHelper.FromJson<MsgJson>(res);
                lst = msg.data.devices;

                if (msg.data.package.Contains("mumayi"))
                {
                    return;
                }
            }
            catch (Exception)
            {
                FileHelper.writeLogs(LoopConfig.log  + "log_request.txt", "request_time:" + request_time + "  data: " + res);
                return;
            }

            lab_request_time.Text = request_time++ + "";
            initDevData(lst);
            game = msg.data.game;
            package = msg.data.package;
            activity = msg.data.activity;

            this.lab_game.Text = game;
            this.lab_package.Text = package;
            this.lab_activity.Text = activity;

            //package = "com.cyou.mrd.xjqx5.ly.mumayi";
            //activity = "com.icee.activity.MainActivity";
            //game = "xjwqz";

        }

        public void initDevlistView()
        {
            GC.Collect();
            devlist.Clear();
            devlist.Columns.Add("index", 50, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("active", 50, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("level", 50, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("platform", 90, HorizontalAlignment.Left); //一步添加     
            devlist.Columns.Add("device_id", 120, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("device_type", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("uname", 90, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("password", 90, HorizontalAlignment.Left); //一步添加    
            devlist.Columns.Add("operator_id", 120, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("operator_name", 120, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("android_id", 120, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("ip", 90, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("imei", 100, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("mac", 100, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("phone", 90, HorizontalAlignment.Left); //一步添加    
            devlist.Columns.Add("sim_iccid", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("sim_imsi", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("sim_state", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("wifi_ssid", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("wifi_bssid", 90, HorizontalAlignment.Left); //一步添加    
        }

        public void initDevData(List<deviceItem> datalist)
        {
            devlist.BeginUpdate();
            int i = 0;
            foreach (deviceItem citem in datalist)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = citem;//-----------------
                i++;
                item.Text = i + "";
                item.SubItems.Add(citem.active + "");
                item.SubItems.Add(citem.level + "");
                item.SubItems.Add(citem.platform);
                item.SubItems.Add(citem.device_id);
                item.SubItems.Add(citem.device_type);
                item.SubItems.Add(citem.uname);
                item.SubItems.Add(citem.password);
                item.SubItems.Add(citem.operator_id);
                item.SubItems.Add(citem.operator_name);
                item.SubItems.Add(citem.android_id);
                item.SubItems.Add(citem.ip);
                item.SubItems.Add(citem.imei);
                item.SubItems.Add(citem.mac);
                item.SubItems.Add(citem.phone);
                item.SubItems.Add(citem.sim_iccid);
                item.SubItems.Add(citem.sim_imsi);
                item.SubItems.Add(citem.sim_state + "");
                item.SubItems.Add(citem.wifi_ssid);
                item.SubItems.Add(citem.wifi_bssid);
                item.Tag = citem;
                devlist.Items.Add(item);

            }
            devlist.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }
        public void addDevItem(deviceItem citem)
        {
            devlist.BeginUpdate();
            ListViewItem item = new ListViewItem();
            item.Tag = citem;//-----------------  
            item.Text = "0";
            item.SubItems.Add(citem.active + "");
            item.SubItems.Add(citem.android_id);
            item.SubItems.Add(citem.device_type);
            item.SubItems.Add(citem.operator_id);
            item.SubItems.Add(citem.operator_name);
            item.SubItems.Add(citem.device_id);
            item.SubItems.Add(citem.ip);
            item.SubItems.Add(citem.imei);
            item.SubItems.Add(citem.mac);
            item.SubItems.Add(citem.phone);
            item.SubItems.Add(citem.sim_iccid);
            item.SubItems.Add(citem.sim_imsi);
            item.SubItems.Add(citem.sim_state + "");
            item.SubItems.Add(citem.wifi_ssid);
            item.SubItems.Add(citem.wifi_bssid);
            item.Tag = citem;
            devlist.Items.Add(item);
            devlist.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }

        public void addtoPanel(vm vm)
        {
            Panel vm_panel = new Panel();
            Label vm_status = new Label();
            Label vm_restart = new Label();
            Label vm_seci = new Label();
            TextBox vm_sec = new TextBox();

            Label vm_dev = new Label();
            Label vm_num = new Label();
            Label vm_orientation = new Label();
            Label vm_host = new Label();
            Label vm_pid = new Label();
            Label vm_code = new Label();
            Label vm_bat = new Label();
            Label vm_droid = new Label();
            Label vm_start_status = new Label();
            Button vm_shut = new Button();
            Button vm_start = new Button();
            Button vm_re_start = new Button();
            Button vm_stop_now = new Button();
            
            Label vm_level = new Label();
            Label vm_anjian_status = new Label();
            vm_panel.BackColor = System.Drawing.Color.Silver;
            vm_panel.Controls.Add(vm_start_status);
            vm_panel.Controls.Add(vm_status);
            vm_panel.Controls.Add(vm_restart);
            vm_panel.Controls.Add(vm_seci);
            vm_panel.Controls.Add(vm_sec);
            vm_panel.Controls.Add(vm_dev);
            vm_panel.Controls.Add(vm_num);
            vm_panel.Controls.Add(vm_orientation);
            vm_panel.Controls.Add(vm_shut);
            vm_panel.Controls.Add(vm_start);
            vm_panel.Controls.Add(vm_re_start);
            vm_panel.Controls.Add(vm_droid);
            vm_panel.Controls.Add(vm_code);
            vm_panel.Controls.Add(vm_bat);
            vm_panel.Controls.Add(vm_pid);
            vm_panel.Controls.Add(vm_level);
            vm_panel.Controls.Add(vm_host);
            vm_panel.Controls.Add(vm_stop_now);
            vm_panel.Controls.Add(vm_anjian_status);
            vm_panel.Location = new System.Drawing.Point(10, 10);
            vm_panel.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            vm_panel.Name = vm.code;
            vm_panel.Size = new System.Drawing.Size(688, 73);
            vm_panel.TabIndex = 0;
            // 
            // lab_status
            // 
            vm_status.AutoSize = true;
            vm_status.Location = new System.Drawing.Point(150, 8);
            vm_status.Name = "vm_status";
            vm_status.Size = new System.Drawing.Size(47, 12);
            vm_status.TabIndex = 12;
            // 
            // lab_restart
            // 
            vm_restart.AutoSize = true;
            vm_restart.Location = new System.Drawing.Point(280, 50);
            vm_restart.Name = "vm_restart";
            vm_restart.Size = new System.Drawing.Size(29, 12);
            vm_restart.TabIndex = 11;
            // 
            // lab_seci
            // 
            vm_seci.AutoSize = true;
            vm_seci.Location = new System.Drawing.Point(433, 29);
            vm_seci.Name = "vm_seci";
            vm_seci.Size = new System.Drawing.Size(29, 12);
            vm_seci.TabIndex = 10;
            // 
            // lab_sec
            // 
            vm_sec.AutoSize = true;
            vm_sec.Location = new System.Drawing.Point(280, 29);
            vm_sec.Name = "vm_sec";
            vm_sec.Size = new System.Drawing.Size(29, 12);
            vm_sec.TabIndex = 9;
            // 
            // lab_dev
            // 
            vm_dev.AutoSize = true;
            vm_dev.Location = new System.Drawing.Point(433, 8);
            vm_dev.Name = "vm_dev";
            vm_dev.Size = new System.Drawing.Size(59, 12);
            vm_dev.TabIndex = 8;
            // 
            // lab_num
            // 
            vm_num.AutoSize = true;
            vm_num.Location = new System.Drawing.Point(280, 8);
            vm_num.Name = "vm_num";
            vm_num.Size = new System.Drawing.Size(29, 12);
            vm_num.TabIndex = 7;
            // 
            // lab_org
            // 
            vm_orientation.AutoSize = true;
            vm_orientation.Location = new System.Drawing.Point(13, 52);
            vm_orientation.Name = "vm_orientation";
            vm_orientation.Size = new System.Drawing.Size(29, 12);
            vm_orientation.TabIndex = 6;
            // 
            // lab_level
            // 
            vm_level.AutoSize = true;
            vm_level.Location = new System.Drawing.Point(75, 52);
            vm_level.Name = "vm_level";
            vm_level.Text = "执行等级";
            vm_level.Size = new System.Drawing.Size(29, 12);
            vm_level.TabIndex = 6;

            // 
            // but_shut
            // 
            vm_shut.Location = new System.Drawing.Point(605, 47);
            vm_shut.Name = "vm_shut";
            vm_shut.Size = new System.Drawing.Size(75, 23);
            vm_shut.TabIndex = 5;
            vm_shut.Text = "下条停止";
            vm_shut.UseVisualStyleBackColor = true;
            vm_shut.Click += new System.EventHandler(this.but_shut_Click);
            // 
            // but_start
            // 
            vm_start.Location = new System.Drawing.Point(605, 3);
            vm_start.Name = "vm_start";
            vm_start.Size = new System.Drawing.Size(75, 23);
            vm_start.TabIndex = 4;
            vm_start.Text = "开始";
            vm_start.UseVisualStyleBackColor = true;
            vm_start.Click += new System.EventHandler(this.but_start_Click);
            // 
            //  vm_re_start
            // 
            vm_re_start.Location = new System.Drawing.Point(605, 25);
            vm_re_start.Name = "vm_re_start";
            vm_re_start.Size = new System.Drawing.Size(75, 23);
            vm_re_start.TabIndex = 4;
            vm_re_start.Text = "重启";
            vm_re_start.UseVisualStyleBackColor = true;
            vm_re_start.Click += new System.EventHandler(this.but_re_start_Click);


            // 
            //  vm_stop_now
            // 
            vm_stop_now.Location = new System.Drawing.Point(505, 25);
            vm_stop_now.Name = "vm_stop_now";
            vm_stop_now.Size = new System.Drawing.Size(75, 23);
            vm_stop_now.TabIndex = 4;
            vm_stop_now.Text = "终止";
            vm_stop_now.UseVisualStyleBackColor = true;
            vm_stop_now.Click += new System.EventHandler(this.but_stop_now_Click);


            // 
            // lab_dname
            // 
            vm_droid.AutoSize = true;
            vm_droid.Location = new System.Drawing.Point(150, 52);
            vm_droid.Name = "vm_droid";
            vm_droid.Size = new System.Drawing.Size(59, 12);
            vm_droid.TabIndex = 3;
            // 
            // lab_code
            // 
            vm_code.AutoSize = true;
            vm_code.Location = new System.Drawing.Point(13, 30);
            vm_code.Name = "vm_code";
            vm_code.Size = new System.Drawing.Size(17, 12);
            vm_code.TabIndex = 2;
            // 
            // lab_bat
            // 
            vm_bat.AutoSize = true;
            vm_bat.Location = new System.Drawing.Point(53, 30);
            vm_bat.Name = "vm_bat";
            vm_bat.Size = new System.Drawing.Size(17, 12);
            vm_bat.Text = "执行脚本";

            // 
            // lab_pid
            // 
            vm_pid.AutoSize = true;
            vm_pid.Location = new System.Drawing.Point(150, 30);
            vm_pid.Name = "vm_pid";
            vm_pid.Size = new System.Drawing.Size(29, 12);
            vm_pid.TabIndex = 1;
            // 
            // lab_port
            // 
            vm_host.AutoSize = true;
            vm_host.Location = new System.Drawing.Point(13, 8);
            vm_host.Name = "vm_host";
            vm_host.Size = new System.Drawing.Size(95, 12);
            vm_host.TabIndex = 0;
            // 
            // lab_start_status
            // 
            vm_start_status.AutoSize = true;
            vm_start_status.Location = new System.Drawing.Point(433, 50);
            vm_start_status.Name = "vm_start_status";
            vm_start_status.Size = new System.Drawing.Size(77, 12);
            vm_start_status.TabIndex = 13;
            // 
            // lab_anjian_status
            // 
            vm_anjian_status.AutoSize = true;
            vm_anjian_status.Location = new System.Drawing.Point(550, 50);
            vm_anjian_status.Name = "vm_anjian_status";
            vm_anjian_status.Size = new System.Drawing.Size(77, 12);
            vm_anjian_status.TabIndex = 13;
            vm_anjian_status.Text = "off";

            vm_restart.Text = "0";
            vm_seci.Text = "0";
            vm_seci.Enabled = false;
            vm_sec.Text = start_num + "";
            start_num += 3;
            vm_num.Text = "0";
            vm_dev.Text = "";
            vm_orientation.Text = vm.orientation;
            vm_host.Text = vm.host;
            vm_pid.Text = vm.pid;
            vm_code.Text = vm.code;
            vm_droid.Text = vm.droid;
            vm_start_status.Text = "stop";
            vm_panel.Tag = vm;
            this.flowVm.Controls.Add(vm_panel);
            //   
            if (!checkPidForInit(vm_panel))
            {
                vm_status.Text = "offline";
            }

        }

        public void doWorkAll()
        {
            foreach (Control panel in flowVm.Controls)
            {
                string status = panel.Controls["vm_status"].Text.ToString();
                if (status == "online")
                {
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + " " + panel.Controls["vm_status"].Text + "  doWorkAll 开始");
                    Label vm_seci = (Label)panel.Controls["vm_seci"];
                    panel.Controls["vm_start_status"].Text = "启动成功";
                    vm_seci.Enabled = false;
                    vm_seci.Text = "0";
                    doWork((Panel)panel);
                }

                if (status == "offline")
                {
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + " " + panel.Controls["vm_status"].Text + "  doWorkAll 开始");
                    doStartVm((Panel)panel);
                }

            }
        }

        private List<Thread> taskThreadList = new List<Thread>();
        private List<LoopThread> loopThreadList = new List<LoopThread>();
        private void doStartVm(Panel panel, bool isRestart = false)
        {
            if (panel.Controls["vm_status"].Text == "initing") return;
            panel.Controls["vm_status"].Text = "initing";
            string code = panel.Controls["vm_code"].Text.ToString();
            Label vm_seci = (Label)panel.Controls["vm_seci"];
            vm_seci.Enabled = false;
            vm_seci.Text = "0";
            panel.Controls["vm_anjian_status"].Text = "off";
            if (isRestart)
            {
                foreach (LoopThread loop in loopThreadList)
                {
                    if (loop.name == code)
                    {
                        loop.KillCMD();
                        break;
                    }
                }
                foreach (Thread tThread in taskThreadList)
                {
                    if (tThread.Name == code)
                    {
                        tThread.Abort();
                        break;
                    }
                }
                Label vm_num = (Label)panel.Controls["vm_num"];
                TextBox vm_sec = (TextBox)panel.Controls["vm_sec"];
                if (vm_num.Text != null && vm_sec != null)
                {
                    int last = Convert.ToInt32(vm_num.Text.ToString());
                    if (text_num_limit.Text.ToString() != "" && text_num_limit.Text.ToString() != "0")
                    {
                        try
                        {
                            limit_num = Convert.ToInt32(text_num_limit.Text.ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    vm_sec.Text = last + limit_num + "";
                }

            }
            VmStartThread thread = new VmStartThread();
            int sleep = Convert.ToInt32(text_sleep.Text.ToString());
            thread.vm_panel = panel;
            thread.isRestart = isRestart;
            thread.sleep = sleep * 1000;
            thread.callback = VmStartCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "restart_" + panel.Controls["vm_code"].Text.ToString();
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();

        }

        /// <summary>
        /// 重启回调
        /// </summary> 
        ///  
        private void VmStartCallBackDelegate(string msg, string p_name)
        {
            GC.Collect();
            Panel panel = (Panel)flowVm.Controls[p_name];
            if (msg == VmStartThread.MSG_COMPLETE)
            {
                vm item = (vm)flowVm.Controls[p_name].Tag;
                panel.Controls["vm_pid"].Text = item.pid;
                panel.Controls["vm_start_status"].Text = "启动成功";
                panel.Controls["vm_status"].Text = "online";

                string n = panel.Controls["vm_restart"].Text;
                int num = Convert.ToInt32(n);
                num++;
                panel.Controls["vm_restart"].Text = num + "";
                anjianStart(panel);
            }
            else
            {
                FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + " " + panel.Controls["vm_status"].Text + " " + msg);
                panel.Controls["vm_status"].Text = "offline";
                if (flowVm.Controls[p_name].Controls["vm_start_status"].Text != "stop")
                {
                    flowVm.Controls[p_name].Controls["vm_start_status"].Text = "等待重启..";
                } 
            } 
            GC.Collect();
        }

        public bool validate(Panel panel)
        {
            bool isDo = false;
            Console.WriteLine(" time_ok " + time_ok.ToString());
            if (panel.Controls["vm_start_status"].Text == "stop") return isDo;
            if (panel.Controls["vm_anjian_status"].Text == "off") return isDo; 
            if (!time_ok) return isDo;
            if (devlist.Items.Count <= 0)
            {
                request();
            }
            if (devlist.Items.Count <= 0) return isDo;
            if (panel == null || closing) return isDo;

            // 重启或者还在执行中
            Label vm_seci = (Label)panel.Controls["vm_seci"];
            if (vm_seci.Enabled || panel.Controls["vm_status"].Text.ToString() != "online") return isDo; 

            TextBox vm_sec = (TextBox)panel.Controls["vm_sec"];
            Label vm_num = (Label)panel.Controls["vm_num"];
            if (vm_num.Text.ToString() != "" && vm_num.Text.ToString() != "0" && vm_sec.Text.ToString() != "" && vm_sec.Text.ToString() != "0")
            {
                int last = Convert.ToInt32(vm_num.Text.ToString());
                int limit = Convert.ToInt32(vm_sec.Text.ToString());
                if (last >= limit)
                {
                    vm_seci.Enabled = false;
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + "  " + panel.Controls["vm_status"].Text + "vm_num: " + last + "  num_limit: " + limit);
                    panel.Controls["vm_status"].Text = "offline";
                    if (panel.Controls["vm_start_status"].Text != "stop")
                    {
                        panel.Controls["vm_start_status"].Text = "等待重启..";
                    }
                    return isDo;
                }
            }
            return true;

        }
         
        private void doWork(Panel panel)
        {
            // 不能dowork 
            if (!validate(panel)) return;
            Label vm_seci = (Label)panel.Controls["vm_seci"];
            vm_seci.Text = "0";
            vm_seci.Enabled = true;
            LoopThread thread = new LoopThread();
            if (flag == 1)
                Thread.Sleep(1000);
            flag = 1;
            deviceItem dev = null;
            try
            {
                dev = (deviceItem)devlist.Items[0].Tag;
                devlist.Items[0].Remove();
                dev.game = game;
            }
            catch (Exception)
            {
                FileHelper.writeLogs(LoopConfig.log + "timeout_StartVm.txt", panel.Controls["vm_status"].Text + panel.Controls["vm_code"].Text.ToString() + " doWork，重启");
                panel.Controls["vm_bat"].Text = "执行脚本";
                panel.Controls["vm_status"].Text = "offline";
                if (panel.Controls["vm_start_status"].Text != "stop")
                {
                    panel.Controls["vm_start_status"].Text = "等待重启..";
                }
            }
            flag = 0;
            if (dev == null)
            {
                return;
            }

            //string[] arr = package.Split(new char[] { '.' });
            //dev.platform = arr[arr.Length - 1];  
            string code = panel.Controls["vm_code"].Text.ToString();
            thread.dev = dev;  
            thread.spt = getScript(game, dev.platform);
            thread.name = code; 
            thread.first_level = Convert.ToInt32(text_first_level.Text.ToString());

            thread.check_cmd = check_cmd.Checked;
            panel.Controls["vm_dev"].Text = dev.device_id;
            thread.vm_panel = panel;
            thread.callback = LoopCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "loop_thread_" + panel.Controls["vm_code"].Text.ToString();
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();
            for (int i = 0; i < loopThreadList.Count; i++)
            {
                if (loopThreadList[i].name == code)
                {
                    loopThreadList.RemoveAt(i);
                    break;
                }
            }
            loopThreadList.Add(thread);
        }
        /// <summary>
        /// 游戏回调
        /// </summary> 
        ///  
        private void LoopCallBackDelegate(string msg, string code, deviceItem dev)
        {
            GC.Collect();
            Panel panel = (Panel)flowVm.Controls[code];
            if (panel == null) return;
            panel.Controls["vm_seci"].Enabled = false;
            panel.Controls["vm_seci"].Text = "0";
            panel.Controls["vm_bat"].Text = "执行完成";
            panel.Controls["vm_level"].Text = "";
            if (msg == LoopThread.MSG_COMPLETE)
            {
                string n = panel.Controls["vm_num"].Text;
                int num = Convert.ToInt32(n);
                num++;
                panel.Controls["vm_num"].Text = num + "";
                doWork(panel);
                insertDev(dev);
            }
            if (msg == LoopThread.MSG_TIME_OUT)
            { 
                panel.Controls["vm_status"].Text = "offline";
                if (panel.Controls["vm_start_status"].Text != "stop")
                {
                    panel.Controls["vm_start_status"].Text = "等待重启..";
                } 
            }

            if (msg == LoopThread.MSG_KILL)
            {
                // 终止
                panel.Controls["vm_bat"].Text = "终止";

            } 
            GC.Collect();
        }


        public void anjianStart(Panel panel)
        {
            AnjianThread thread = new AnjianThread();
            thread.activity = text_anjian_activity.Text.ToString();
            thread.package = text_anjian_package.Text.ToString();
            thread.vm_panel = panel;
            thread.callback = AnjianStartCallBackDelegate;

            Thread tWorkingThread = new Thread(thread.doStart);
            tWorkingThread.Name = "anjian_" + panel.Controls["vm_code"].Text.ToString();
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();
        }

        /// <summary>
        /// 重启按键精灵
        /// </summary>
        /// <param name="dev"></param>
        /// 
        public void AnjianStartCallBackDelegate(string msg, Panel panel)
        {

            if (msg == AnjianThread.MSG_START_COMPLETE)
            {
                panel.Controls["vm_anjian_status"].Text = "on";
                doWork(panel);
            }
            if (msg == AnjianThread.MSG_ERROR)
            {
                FileHelper.writeLogs(LoopConfig.log + "Anjian_Ghost.txt", panel.Controls["vm_status"].Text + panel.Controls["vm_code"].Text.ToString() + " MSG_TIME_OUT，重启");
                panel.Controls["vm_bat"].Text = "执行脚本";
                panel.Controls["vm_status"].Text = "offline";
                if (panel.Controls["vm_start_status"].Text != "stop")
                {
                    panel.Controls["vm_start_status"].Text = "等待重启..";
                }
                panel.Controls["vm_anjian_status"].Text = "off";
            }

        }

        public void insertDev(deviceItem dev)
        {
            InsertThread thread = new InsertThread();
            thread.dev = dev;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "insert_" + dev.android_id;
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();
        }


        private void LoopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            closing = true;
            for (int i = 0; i < loopThreadList.Count; i++)
            {
                if (loopThreadList[i].exeStatus)
                    loopThreadList[i].KillCMD();
            }
            if (taskThreadList.Count > 0)
            {
                //编列自定义队列,将所有线程终止
                foreach (Thread tWorkingThread in taskThreadList)
                {
                    tWorkingThread.Abort();
                }
            }
            List<deviceItem> devData = new List<deviceItem>();
            foreach (ListViewItem item in devlist.Items)
            {
                deviceItem dev = (deviceItem)item.Tag;
                devData.Add(dev);
            }
            data data = new data();
            data.game = game;
            data.activity = activity;
            data.package = package;
            data.devices = devData;
            try
            {
                XmlHelper.objToXml(data, LoopConfig.devicesXml);
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "log_devs_stored.txt", "data : " + ex.StackTrace);

                return;
            }
            Thread.Sleep(3000);//  
        }

        public void checkVm()
        {
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses();
            List<string> pids = new List<string>();
            foreach (Process p in list)
            {
                if (p.ProcessName == "Droid4X")
                {
                    pids.Add(p.Id + "");
                }
            }

            foreach (Control c in flowVm.Controls)
            {
                Panel p = (Panel)c;
                string status = p.Controls["vm_status"].Text.ToString();
                if (status == "online")
                {
                    string pid = p.Controls["vm_pid"].Text.ToString();
                    if (pid == "" || pids.Contains(pid))
                    {
                        p.Controls["vm_status"].Text = "online";
                        continue;
                    }
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", p.Controls["vm_code"].Text.ToString() + "  pid:" + pid + " vm_status" + p.Controls["vm_status"].Text);
                    p.Controls["vm_status"].Text = "offline";
                    if (p.Controls["vm_start_status"].Text != "stop")
                    {
                        p.Controls["vm_start_status"].Text = "等待重启..";
                    }

                }
            }
        }

        private bool checkPidForInit(Panel panel)
        {
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses();
            vm item = (vm)panel.Tag;
            foreach (Process proc in list)
            {
                if (proc.ProcessName == "Droid4X" && item.code.Trim() + LoopConfig.hmVersion == proc.MainWindowTitle)
                {
                    item.pid = proc.Id + "";
                    panel.Controls["vm_status"].Text = "online";
                    //    panel.Controls["vm_start_status"].Text = "启动成功";
                    panel.Controls["vm_pid"].Text = proc.Id + "";
                    panel.Tag = item;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 30000ms 关闭更新弹框 ，检测pid offline  发送请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!time_ok) return;
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses();
            foreach (Process p in list)
            {
                if (p.ProcessName == "DXUpdate")
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;
                }
            }

            checkVm();
            if (devlist.Items.Count <= 0)
            {
                request();
            }

            GC.Collect();
        }
        /// <summary>
        /// 1000 ms 检查offline 重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            text_allsecs.Text = allsecs++ + "";
            lab_current_time.Text = DateTime.Now.ToString();
            string time = text_time_over.Text.ToString();
            int _int_time = Convert.ToInt32(time);
            int t = DateTime.Now.Hour; 
            label_now_hour.Text = t + ""; 
         //   Console.WriteLine("now" + t + "  time:" + _int_time); 
            if (t < _int_time)
            {
                time_ok = false;
                return;
            }
            else
            {
                time_ok = true;
            }

            //Console.WriteLine(" time_ok " + time_ok.ToString());

            foreach (Control c in flowVm.Controls)
            {
                Panel p = (Panel)c;
                Label vm_seci = (Label)p.Controls["vm_seci"];

                if (vm_seci != null && vm_seci.Enabled && text_time_limit.Text.ToString() != "" && text_time_limit.Text.ToString() != "0")
                {
                    int last = Convert.ToInt32(vm_seci.Text.ToString());
                    int limit = Convert.ToInt32(text_time_limit.Text.ToString());
                    last++;
                    if (last > limit)
                    {
                        vm_seci.Enabled = false;
                        FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", p.Controls["vm_code"].Text.ToString() + "  " + p.Controls["vm_status"].Text + "vm_seci: " + last + "  time_limit: " + limit);
                        p.Controls["vm_status"].Text = "offline";
                        if (p.Controls["vm_start_status"].Text != "stop")
                        {
                            p.Controls["vm_start_status"].Text = "等待重启..";
                        }
                    }
                    else
                    {
                        vm_seci.Text = last + "";
                    }
                }

                if (p.Controls["vm_status"].Text == "online" && p.Controls["vm_start_status"].Text == "启动成功" && p.Controls["vm_anjian_status"].Text == "on" && !vm_seci.Enabled && devlist.Items.Count > 0)
                {
                    doWork(p);

                }

                if (p.Controls["vm_status"].Text == "offline" && p.Controls["vm_start_status"].Text != "stop")
                {
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", p.Controls["vm_code"].Text.ToString() + "  timer1 check offline restart!");
                    doStartVm(p, true);
                }

            }
        }
       
        private void showVmConfig()
        {
            GC.Collect();
            VmConfig config = new VmConfig();
            config.StartPosition = FormStartPosition.CenterScreen;
            config.ShowDialog();
            if (config.DialogResult == DialogResult.Yes)
            {
                addtoPanel(config.selItem);
                // timer1.Enabled = true;
                // doWork((Panel)flowVm.Controls[config.selItem.code]); 
            }
            GC.Collect();

        }
        private void but_request_Click(object sender, EventArgs e)
        {
            request();
        }

        private void but_refresh_vms_Click(object sender, EventArgs e)
        {
            initVmlist();
            GC.Collect();
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_shut_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + "  " + panel.Controls["vm_status"].Text + " but_shut_Click ");
            string status = panel.Controls["vm_status"].Text.ToString();
            panel.Controls["vm_start_status"].Text = "stop";

        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            string status = panel.Controls["vm_status"].Text.ToString();
            panel.Controls["vm_anjian_status"].Text = "on";
            if (status == "online" && !panel.Controls["vm_seci"].Enabled)
            {
                FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + " " + panel.Controls["vm_status"].Text + "  but_start_now_Click 开始");
                Label vm_seci = (Label)panel.Controls["vm_seci"];
                panel.Controls["vm_start_status"].Text = "启动成功";
                vm_seci.Text = "0";
                doWork((Panel)panel);
            }

            if (status == "offline")
            {
                FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["vm_code"].Text.ToString() + " " + panel.Controls["vm_status"].Text + "  but_start_Click 开始");
                doStartVm((Panel)panel);
            }

        }

        private void but_stop_now_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;  
            if (panel.Controls["vm_status"].Text != "online") return; 

            string code = panel.Controls["vm_code"].Text.ToString();
            Label vm_seci = (Label)panel.Controls["vm_seci"];
            vm_seci.Enabled = false; 
            panel.Controls["vm_start_status"].Text = "stop"; 
            anjianStart(panel); 
            foreach (LoopThread loop in loopThreadList)
            {
                if (loop.name == code)
                {
                    loop.KillCMD();
                    break;
                }
            }
            foreach (Thread tThread in taskThreadList)
            {
                if (tThread.Name == code)
                {
                    tThread.Abort();
                    break;
                }
            }
            
        }

        private void but_re_start_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            doStartVm(panel, true);

        }

        private void but_add_vm_Click(object sender, EventArgs e)
        {
            showVmConfig();
        }

        private void but_config_Click(object sender, EventArgs e)
        {
            showVmConfig();
        }

        private void but_begin_Click(object sender, EventArgs e)
        {
            FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", "but_begin_Click 一键开启");
            timer1.Enabled = true;
            doWorkAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertDev((deviceItem)devlist.Items[0].Tag);

        }

        private void button2_Click(object sender, EventArgs e)
        {


            LoopThread thread = new LoopThread();
            deviceItem dev = (deviceItem)devlist.Items[0].Tag;
            devlist.Items[0].Remove();
            dev.game = game;
            string[] arr = package.Split(new char[] { '.' });
            dev.platform = arr[arr.Length - 1];

            thread.vm_panel = (Panel)flowVm.Controls["v0"];
            thread.hack_test = true;
            thread.dev = dev;
            //thread.activity = activity;
            //thread.game = game;
            //thread.package = package; 
            thread.check_cmd = check_cmd.Checked;
            thread.callback = LoopCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();
             
        }

        private void but_apk_config_Click(object sender, EventArgs e)
        {
            showApkConfig();
        }

        private void showApkConfig()
        {
            GC.Collect();
            ApkConfig config = new ApkConfig();
            config.StartPosition = FormStartPosition.CenterScreen;
            config.ShowDialog();
            if (config.DialogResult == DialogResult.Yes)
            {
                initConfig();
            }
            GC.Collect();

        }

        private script getScript(string game,string platform){

            string game_platform = game+"_"+platform;

            foreach(script spt in script_list){

                if(spt.game_platform ==game_platform)
                return  spt;
            }

            return null; 
        }



    }
}
