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
    public partial class NewForm : Form
    {

        private List<Thread> taskThreadList = new List<Thread>();
        private List<LoopThread> loopThreadList = new List<LoopThread>();
        public List<script> script_list;
        int request_time;
        int start_limit = 10;
        bool closing = false;

        public NewForm()
        {
            InitializeComponent();
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;// 控件，其他线程调用    
            init_script_config();

            if (Directory.Exists(LoopConfig.temp))
            {
                Directory.Delete(LoopConfig.temp, true);
            }
            Directory.CreateDirectory(LoopConfig.temp);

            string _ComputName = System.Net.Dns.GetHostName();
            request_pc_code.Text = _ComputName;
            initDevlistView();
            initVmlist();
            initProxylistView();

        //    LoopConfig.hmVersion = text_hm_version.Text;
            List<deviceItem> devices;

            devices = XmlHelper.xmlToList<deviceItem>(LoopConfig.devicesXml);
            if (devices != null)
            {
                if (devices.Count > 0)
                {
                    initDevData(devices);
                }
                else
                {
                    request();
                }
            }
            label_start.Text = DateTime.Now.ToString();
            init_combox_game();
            init_combox_hm();
              
        }
        private bool proxy_flag = true;
        private void getProxy()
        {
            if (!check_proxy.Checked) return;

            if (!proxy_flag) return;
            proxy_flag = false;
            string str_start_hour = text_stop_from_time.Text.ToString();
            int start_hour = Convert.ToInt32(str_start_hour);
            int now = DateTime.Now.Hour;
            if (now < start_hour)
            {
                return;
            }

            ProxyThread thread = new ProxyThread();
            thread.callback = ProxyCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            tWorkingThread.Start();

        }
        private void ProxyCallBackDelegate(string msg, proxyItem proxy)
        {
            GC.Collect();

            if (msg == ProxyThread.MSG_COMPLETE)
            {
                addProxyData(proxy);
            }
            proxy_flag = true;

        }
        public void init_combox_game()
        {

            List<script> list = XmlHelper.xmlToList<script>(LoopConfig.scriptXml);
            script spt = new script();
            spt.game_name = "随机";
            spt.game_code = "";
            list.Insert(0, spt);
            this.comboBox_game.DataSource = list; 
        }
        public void init_combox_hm()
        { 
            List<string> list = new List<string>();
            list.Insert(0, "0.10.4 Beta");
            list.Insert(0, "0.10.3 Beta");
            list.Insert(0, "0.10.2 Beta");
            list.Insert(0, "0.8.7 Beta");
            this.comboBox_hm_version.DataSource = list; 
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        public void request()
        {
            if (this.request_check.Checked) return;
            string str_from_hour = text_stop_from_time.Text.ToString(); // 2
            string str_to_hour = text_stop_to_time.Text.ToString(); // 7 
            int from_hour = Convert.ToInt32(str_from_hour);
            int to_hour = Convert.ToInt32(str_to_hour);
            int now = DateTime.Now.Hour;
            if (now >= from_hour && now < to_hour)  //2.3.4.5.6
            {
                return;
            }
            
            GC.Collect();
            label_request_time.Text = request_time++ + "";



            MsgJson msg;
            List<deviceItem> lst = new List<deviceItem>();
            List<deviceItem> devlst = new List<deviceItem>();
            string request_url = LoopConfig.get_url;
            string request_game = comboBox_game.SelectedValue.ToString();


            if (check_request_test.Checked)
            {// 测试url
                request_url = LoopConfig.get_test_url;
                if (request_pc_code.Text != "")
                {
                    request_url = request_url + "&pc=" + request_pc_code.Text;
                }
            }
            else
            {
                if (request_game != "")
                {
                    request_url = request_url + "?game=" + request_game;

                    if (request_pc_code.Text != "")
                    {
                        request_url = request_url + "&pc=" + request_pc_code.Text;
                    }
                }
                else
                {
                    if (request_pc_code.Text != "")
                    {
                        request_url = request_url + "?pc=" + request_pc_code.Text;
                    }
                }
            }

            try
            {
                string res = HttpHelper.SendGet(request_url);
                msg = JsonHelper.FromJson<MsgJson>(res);
                string game = msg.data.game;
                lst = msg.data.devices;
                foreach (deviceItem item in lst)
                {
                    item.game = game;
                    devlst.Add(item);
                }

            }
            catch (Exception)
            {
                return;
            }

            initDevData(devlst);

        }
        /// <summary>
        /// 初始化模拟器列表
        /// </summary>
        public void initVmlist()
        {

            this.flowVm.Controls.Clear();
            List<vm> vmlist = XmlHelper.xmlToList<vm>(LoopConfig.vmsXml);
            if (vmlist.Count <= 0) return;
            foreach (vm item in vmlist)
            {
                if (item.isOk == "1")
                {
                    addPanel(item);
                }
            }
        }

        public void addPanel(vm item)
        {
            foreach (Control control in flowVm.Controls)
            {
                if (item.code == control.Name)
                    return;

            }
            Panel panel = new Panel();
            panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_MouseClick);
            UIHelper.add_vm_panel(flowVm, panel, item, start_limit);
            start_limit += 1;
            add_panel_button(panel, item);

        }



        /// <summary>
        /// 初始化代理列表
        /// </summary> 
        public void initProxylistView()
        {
            GC.Collect();
            proxylist.Clear();
            proxylist.BackColor = Color.FromArgb(239, 248, 250); //默认背景色 
            proxylist.Columns.Add("序号", 50, HorizontalAlignment.Left); //一步添加   
            proxylist.Columns.Add("IP", 150, HorizontalAlignment.Left); //一步添加             
            proxylist.Columns.Add("端口", 80, HorizontalAlignment.Left); //一步添加      
        }

        public void addProxyData(proxyItem citem)
        {
            proxylist.BeginUpdate();
            int i = proxylist.Items.Count;
            ListViewItem item = new ListViewItem();
            item.Tag = citem;//-----------------
            i++;
            item.Text = i + "";
            item.SubItems.Add(citem.proxyIp + "");
            item.SubItems.Add(citem.proxyPort + "");
            item.Tag = citem;
            proxylist.Items.Add(item);
            proxylist.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }


        /// <summary>
        /// 初始化hook列表
        /// </summary> 
        public void initDevlistView()
        {
            GC.Collect();
            devlist.Clear();
            devlist.BackColor = Color.FromArgb(239, 248, 250); //默认背景色 
            devlist.Columns.Add("index", 50, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("active", 50, HorizontalAlignment.Left); //一步添加             
            devlist.Columns.Add("game", 80, HorizontalAlignment.Left); //一步添加     
            devlist.Columns.Add("platform", 80, HorizontalAlignment.Left); //一步添加    
            devlist.Columns.Add("level", 50, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("device_id", 100, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("device_type", 90, HorizontalAlignment.Left); //一步添加   
            devlist.Columns.Add("uname", 90, HorizontalAlignment.Left); //一步添加  
            devlist.Columns.Add("password", 90, HorizontalAlignment.Left); //一步添加       


            devlist.Columns.Add("imei", 120, HorizontalAlignment.Left); //一步添加       
            devlist.Columns.Add("android_id", 120, HorizontalAlignment.Left); //一步添加       
            devlist.Columns.Add("iccid", 120, HorizontalAlignment.Left); //一步添加       
            devlist.Columns.Add("sim_imsi", 120, HorizontalAlignment.Left); //一步添加       

        }

        public void initDevData(List<deviceItem> datalist)
        {
            devlist.BeginUpdate();
            int i = devlist.Items.Count;
            foreach (deviceItem citem in datalist)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = citem;//-----------------
                i++;
                item.Text = i + "";
                item.SubItems.Add(citem.active + "");
                item.SubItems.Add(citem.game + "");
                item.SubItems.Add(citem.platform);
                item.SubItems.Add(citem.level + "");
                item.SubItems.Add(citem.device_id);
                item.SubItems.Add(citem.device_type);
                item.SubItems.Add(citem.uname);
                item.SubItems.Add(citem.password);

                item.SubItems.Add(citem.imei);
                item.SubItems.Add(citem.android_id);
                item.SubItems.Add(citem.sim_iccid);
                item.SubItems.Add(citem.sim_imsi); 


                item.Tag = citem;
                devlist.Items.Add(item);
            }
            devlist.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }
        private void devlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GC.Collect();
            if (devlist.SelectedItems.Count > 0)
            {

                foreach (ListViewItem item in devlist.Items)
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.FromArgb(239, 248, 250); //恢复默认背景色 

                }
                //修改选中项颜色
                devlist.SelectedItems[0].SubItems[0].ForeColor = Color.SaddleBrown;
                devlist.SelectedItems[0].BackColor = Color.Silver;
                devlist.SelectedItems[0].Selected = false;// 会引发第二次 该方法的调用
                devlist.SelectedItems.Clear();
            }
        }
        public void addDevItem(deviceItem citem, string code)
        {
            devlist.BeginUpdate();
            ListViewItem item = new ListViewItem();
            item.Tag = citem;//-----------------  
            item.Text = "回收" + code;
            item.SubItems.Add(citem.active + "");
            item.SubItems.Add(citem.game + "");
            item.SubItems.Add(citem.platform);
            item.SubItems.Add(citem.level + "");
            item.SubItems.Add(citem.device_id);
            item.SubItems.Add(citem.device_type);
            item.SubItems.Add(citem.uname);
            item.SubItems.Add(citem.password);
            item.Tag = citem;
            devlist.Items.Add(item);
            devlist.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }

        private void doStartVm(Panel panel, bool isRestart = false)
        {
            if (panel.Controls["text_host_status"].Text == "loading") return;
            panel.Controls["text_host_status"].Text = "loading";
            string code = panel.Controls["text_code"].Text.ToString();
            panel.Controls["text_script"].Text = "";
            TextBox text_count_time = (TextBox)panel.Controls["text_count_time"];
            text_count_time.Enabled = false;

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

                TextBox text_complete = (TextBox)panel.Controls["text_complete"];
                TextBox text_restart_limit = (TextBox)panel.Controls["text_restart_limit"];


                int complete = Convert.ToInt32(text_complete.Text.ToString());
                int limit_num = 10;
                try
                {
                    limit_num = Convert.ToInt32(text_restart_count_limit.Text.ToString());
                }
                catch (Exception)
                {
                }

                text_restart_limit.Text = complete + limit_num + "";

            }
            VmStartThread thread = new VmStartThread();
            int sleep = Convert.ToInt32(text_restart_init.Text.ToString());
            thread.vm_panel = panel;
            thread.isRestart = isRestart;
            thread.sleep = sleep * 1000;
            thread.callback = VmStartCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "restart_" + panel.Controls["text_code"].Text.ToString();
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
                try
                {
                    vm item = (vm)flowVm.Controls[p_name].Tag;
                    panel.Controls["text_process_id"].Text = item.pid;
                    panel.Controls["text_host_status"].Text = "on";
                    panel.Controls["text_ghost_status"].Text = "off";
                    string text_num = panel.Controls["text_restart_time"].Text;// 重启数  
                    int num = Convert.ToInt32(text_num);
                    num++;
                    panel.Controls["text_restart_time"].Text = num + "";
                    flowVm.Controls[p_name].Controls["label_status"].Text = "完成";
                }
                catch (Exception)
                {

                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + " VmStartCallBackDelegate " + msg);
                }

                doWork(panel);
            }
            else
            {
                FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + " " + msg);
                panel.Controls["text_host_status"].Text = "off";
                if (panel.Controls["text_exe_status"].Text != "stop")
                {
                    panel.Controls["label_status"].Text = "等待重启..";
                }
            }
            GC.Collect();
        }
        public bool validate(Panel panel)
        {
            bool isDo = false;
            if (panel == null || closing) return isDo;
            //   if (panel.Controls["text_ghost_status"].Text == "off") return isDo; 
            if (devlist.Items.Count <= 0)
            {
                request();
            }

            //if (check_proxy.Checked)
            //{
            //    if (proxylist.Items.Count <= 0)
            //    {
            //        getProxy(); 
            //    } 
            //}


            if (devlist.Items.Count <= 0) return isDo;
            // 重启或者还在执行中
            TextBox text_count_time = (TextBox)panel.Controls["text_count_time"];
            if (panel.Controls["text_host_status"].Text.ToString() != "on" || panel.Controls["text_exe_status"].Text != "off") return isDo;

            TextBox vm_limit = (TextBox)panel.Controls["text_restart_limit"];
            TextBox vm_num = (TextBox)panel.Controls["text_complete"];

            if (vm_limit.Text.ToString() != "" && vm_limit.Text.ToString() != "0")
            {
                int last = Convert.ToInt32(vm_num.Text.ToString());
                int limit = Convert.ToInt32(vm_limit.Text.ToString());
                if (last >= limit)
                {
                    text_count_time.Enabled = false;
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + "  vm_num: " + last + "  num_limit: " + limit);
                    panel.Controls["text_host_status"].Text = "off";
                    if (panel.Controls["text_exe_status"].Text != "stop")
                    {
                        panel.Controls["label_status"].Text = "等待重启..";
                    }
                    return isDo;
                }
            }
            return true;
        }
        private script getScript(deviceItem dev)
        {
            script spt_new = new script();
            string game = dev.game;
            string platform = dev.platform;
            string game_platform = game + "_" + platform;
            foreach (script spt in script_list)
            {
                if (spt.game_platform == game_platform)
                { 
                    return spt;
                }
                   
            }
            return null;
        }
        int flag = 0;
        private void doWork(Panel panel)
        {
            // 不能dowork 
            if (!validate(panel)) return;
            TextBox text_count_time = (TextBox)panel.Controls["text_count_time"];
            TextBox text_exe_status = (TextBox)panel.Controls["text_exe_status"];
            text_count_time.Text = "0";
            text_count_time.Enabled = true;
            text_exe_status.Text = "on";// 开始执行 
            LoopThread thread = new LoopThread();
            if (flag == 1)
                Thread.Sleep(1000);
            flag = 1;
            deviceItem dev = null;
            proxyItem proxy = null;
            script spt = null;
            try
            {
                dev = (deviceItem)devlist.Items[0].Tag;
                spt = getScript(dev);
                if (spt == null)
                {
                    MessageBox.Show("请配置" + dev.game + "_" + dev.platform + "脚本");
                    if (text_exe_status.Text != "stop")
                    {
                        text_exe_status.Text = "off";
                    }
                    text_count_time.Enabled = false;
                    return;
                }
                string filter = text_proxy_filter.Text.ToString().Trim();
                devlist.Items[0].Remove();
                if (check_proxy.Checked && proxylist.Items.Count > 0)
                {
                    proxy = (proxyItem)proxylist.Items[0].Tag;
                    proxy.proxyFilter = filter;
                    proxylist.Items[0].Remove();
                }
                else
                {
                    string text = text_proxy_ip_port.Text.ToString().Trim();
                    proxy = new proxyItem(text);
                    proxy.proxyFilter = filter;
                }

            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "dowork.txt", panel.Controls["text_code"].Text.ToString() + " doWork:" + ex.StackTrace);
                if (text_exe_status.Text != "stop")
                {
                    text_exe_status.Text = "off";
                }
                text_count_time.Enabled = false;
                return;
            }
            flag = 0;

            if (dev == null)
            {
                MessageBox.Show("执行数据错误 dev=null ");
                if (text_exe_status.Text != "stop")
                {
                    text_exe_status.Text = "off";
                }
                text_count_time.Enabled = false;
                return;
            }
            string code = panel.Controls["text_code"].Text.ToString();
            thread.dev = dev;
            thread.proxy = proxy;
            thread.spt = spt;
            thread.name = code;
            thread.vm_panel = panel;
            thread.callback = LoopCallBackDelegate;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "loop_thread_" + panel.Controls["text_code"].Text.ToString();
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
            try
            {
                Panel panel = (Panel)flowVm.Controls[code];
                if (panel == null) return;
                panel.Controls["text_count_time"].Enabled = false;
                panel.Controls["text_count_time"].Text = "0";
                panel.Controls["label_status"].Text = "完成";

                if (panel.Controls["text_exe_status"].Text != "stop")
                {
                    panel.Controls["text_exe_status"].Text = "off";
                }

                if (msg == LoopThread.MSG_COMPLETE)
                {
                    string n = panel.Controls["text_complete"].Text;
                    int num = Convert.ToInt32(n);
                    num++;
                    panel.Controls["text_complete"].Text = num + "";
                    doWork(panel);
                    insertDev(dev);
                }
                if (msg == LoopThread.MSG_STORE)
                {
                    //panel.Controls["text_script"].Text = "";
                    //addDevItem(dev, code);
                    //stop_now(panel);

                    //panel.Controls["text_exe_status"].Text = "off";
                    //panel.Controls["label_status"].Text = "";
                    //doWork(panel);

                    addDevItem(dev, code);
                    panel.Controls["text_host_status"].Text = "off";
                    if (panel.Controls["text_exe_status"].Text != "stop")
                    {
                        panel.Controls["label_status"].Text = "等待重启..";
                    }


                }
                if (msg == LoopThread.MSG_BREAK)
                {
                    //panel.Controls["text_script"].Text = "";
                    ////    addDevItem(dev, code);
                    //stop_now(panel);
                    //panel.Controls["text_exe_status"].Text = "off";
                    //panel.Controls["label_status"].Text = "";
                    doWork(panel);

                }
                if (msg == LoopThread.MSG_TIME_OUT)
                {
                    panel.Controls["text_host_status"].Text = "off";
                    if (panel.Controls["text_exe_status"].Text != "stop")
                    {
                        panel.Controls["label_status"].Text = "等待重启..";
                    }
                }
                if (msg == LoopThread.MSG_KILL)
                {
                    //   panel.Controls["label_status"].Text = "终止";
                }
            }
            catch (Exception)
            {
            }




            GC.Collect();
        }
        public void insertDev(deviceItem dev)
        {
            InsertThread thread = new InsertThread();
            thread.dev = dev;
            Thread tWorkingThread = new Thread(thread.doWork);
            tWorkingThread.Name = "insert_" + dev.device_id;
            tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
            taskThreadList.Add(tWorkingThread);
            tWorkingThread.Start();
        }


        public void add_panel_button(Panel panel, vm item)
        {
            Button but_stop_next = new Button();
            Button but_restart = new Button();
            Button but_stop_now = new Button();
            Button but_start = new Button();
            Button but_host_status = new Button();
            Button but_remove = new Button();

            panel.Controls.Add(but_stop_next);
            panel.Controls.Add(but_restart);
            panel.Controls.Add(but_stop_now);
            panel.Controls.Add(but_start);
            panel.Controls.Add(but_host_status);
            panel.Controls.Add(but_remove);
            // 
            // Button but_stop_next
            // 
            but_stop_next.Location = new System.Drawing.Point(758, 40);
            but_stop_next.Name = "Button but_stop_next";
            but_stop_next.Size = new System.Drawing.Size(75, 23);
            but_stop_next.TabIndex = 57;
            but_stop_next.Text = "停止";
            but_stop_next.UseVisualStyleBackColor = true;
            // 
            //but_restart
            // 
            but_restart.Location = new System.Drawing.Point(851, 40);
            but_restart.Name = "Button but_restart";
            but_restart.Size = new System.Drawing.Size(75, 23);
            but_restart.TabIndex = 56;
            but_restart.Text = "重启";
            but_restart.UseVisualStyleBackColor = true;
            // 
            // but_host_status
            // 
            but_host_status.Location = new System.Drawing.Point(851, 71);
            but_host_status.Name = "but_host_status";
            but_host_status.Size = new System.Drawing.Size(75, 23);
            but_host_status.TabIndex = 65;
            but_host_status.Text = "刷新状态";
            but_host_status.UseVisualStyleBackColor = true;
            // 
            //but_stop_now
            // 
            but_stop_now.Location = new System.Drawing.Point(851, 11);
            but_stop_now.Name = "Button but_stop_now";
            but_stop_now.Size = new System.Drawing.Size(75, 23);
            but_stop_now.TabIndex = 55;
            but_stop_now.Text = "终止";
            but_stop_now.UseVisualStyleBackColor = true;
            // 
            //but_start
            // 
            but_start.Location = new System.Drawing.Point(758, 11);
            but_start.Name = "Button but_start";
            but_start.Size = new System.Drawing.Size(75, 23);
            but_start.TabIndex = 54;
            but_start.Text = "开始";
            but_start.UseVisualStyleBackColor = true;

            // 
            // but_remove
            // 
            but_remove.Location = new System.Drawing.Point(758, 71);
            but_remove.Name = "but_remove";
            but_remove.Size = new System.Drawing.Size(75, 23);
            but_remove.TabIndex = 66;
            but_remove.Text = "移除";
            but_remove.UseVisualStyleBackColor = true;

            but_start.Click += new System.EventHandler(this.but_start_Click);
            but_stop_now.Click += new System.EventHandler(this.but_stop_now_Click);
            but_stop_next.Click += new System.EventHandler(this.but_stop_next_Click);
            but_restart.Click += new System.EventHandler(this.but_restart_Click);
            but_host_status.Click += new System.EventHandler(this.but_host_status_Click);
            but_remove.Click += new System.EventHandler(this.but_remove_Click);

        }

        /// <summary>
        /// 开始
        /// </summary> 
        public void but_start_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;

            string exe_status = panel.Controls["text_exe_status"].Text.ToString();
            string host_status = panel.Controls["text_host_status"].Text.ToString();
            string ghost_status = panel.Controls["text_ghost_status"].Text.ToString();
            if (host_status == "off")
            {
                doStartVm((Panel)panel);

            }
            else if (host_status == "on" && exe_status != "on")
            {
                if (panel.Controls["text_exe_status"].Text == "stop" && (panel.Controls["label_status"].Text == "完成" || panel.Controls["label_status"].Text == ""))
                {
                    panel.Controls["text_exe_status"].Text = "off";// 开始执行  
                }
                doWork((Panel)panel);
            }

        }
        /// <summary>
        /// 终止
        /// </summary> 
        public void but_stop_now_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            stop_now(panel);

        }
        public void stop_now(Panel panel)
        {
            if (panel.Controls["text_host_status"].Text != "on") return;
            string code = panel.Controls["text_code"].Text.ToString();
            panel.Controls["text_count_time"].Enabled = false;
            panel.Controls["text_exe_status"].Text = "stop";
            panel.Controls["text_ghost_status"].Text = "off";
            panel.Controls["text_script"].Text = "";
            panel.Controls["label_status"].Text = "终止..";
            foreach (LoopThread loop in loopThreadList)
            {
                if (loop.name == code)
                {
                    Thread tWorkingThread = new Thread(loop.kill_clear);
                    tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
                    taskThreadList.Add(tWorkingThread);
                    tWorkingThread.Start();
                    break;
                }
            }
            foreach (Thread tThread in taskThreadList)
            {
                if (tThread.Name == "loop_thread_" + code)
                {
                    try
                    {
                        tThread.Abort();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                }
            }

        }
        /// <summary>
        /// 下条停止
        /// </summary> 
        public void but_stop_next_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            panel.Controls["text_exe_status"].Text = "stop";

        }
        /// <summary>
        /// 重启
        /// </summary> 
        public void but_restart_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", " but_restart_Click ");
            panel.Controls["text_exe_status"].Text = "stop";
            doStartVm(panel, true);


        }
        /// <summary>
        /// 主机状态
        /// </summary> 
        public void but_host_status_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            checkVmStatus(panel,true);
        }
        /// <summary>
        /// 移除
        /// </summary> 
        public void but_remove_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Panel panel = (Panel)b.Parent;
            stop_now(panel);
            foreach (Control control in flowVm.Controls)
            {
                if (control.Name == panel.Name)
                {
                    flowVm.Controls.Remove(control);

                }
            }
        }


        /// <summary>
        /// 1000 ms
        /// </summary> 
        private void timer1_Tick(object sender, EventArgs e)
        {
            label_now.Text = DateTime.Now.ToString();


            foreach (Control control in flowVm.Controls)
            {
                Panel panel = (Panel)control;
                TextBox text_count_time = (TextBox)panel.Controls["text_count_time"];
                TextBox text_limit_time = (TextBox)panel.Controls["text_limit_time"];

                if (text_count_time.Enabled)
                {
                    int last = 0;
                    int limit = 0;
                    try
                    {
                        last = Convert.ToInt32(text_count_time.Text.ToString());
                        limit = Convert.ToInt32(text_time_limit.Text.ToString());
                        last++;
                    }
                    catch (Exception)
                    {
                        last = 0;
                    }

                    if (last > limit)
                    {
                        text_count_time.Enabled = false;
                        FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + "  " + panel.Controls["label_status"].Text + "  timer1_Tick  count_time: " + last + "  time_limit: " + limit);
                        panel.Controls["text_host_status"].Text = "off";
                        if (panel.Controls["text_exe_status"].Text != "stop")
                        {
                            panel.Controls["text_exe_status"].Text = "off";
                            panel.Controls["label_status"].Text = "等待重启..";
                        }
                    }
                    else
                    {
                        text_count_time.Text = last + "";
                    }
                }

                if (panel.Controls["text_host_status"].Text == "on" && panel.Controls["text_exe_status"].Text == "off" && devlist.Items.Count > 0)
                {
                    doWork(panel);
                }

                if (panel.Controls["text_host_status"].Text == "off" && panel.Controls["text_exe_status"].Text != "stop")
                {
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + "  timer1 check offline restart!");
                    doStartVm(panel, true);
                }

            }

        }
        /// <summary>
        /// 30s
        /// </summary> 
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (proxylist.Items.Count <= 0)
            {
                getProxy();
            }
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
        /// 1 hour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        { 

            string kill = text_kill_hour.Text.ToString();
            int kill_time = 0;


            Console.WriteLine(DateTime.Now.Hour.ToString());
            if (kill != "")
            { 
                try
                {
                    kill_time = Convert.ToInt32(text_kill_hour.Text.ToString());
                }
                catch (Exception)
                {
                    return;
                }
            }
            if (DateTime.Now.Hour == kill_time)
            {
                startProcess("kill_task.bat", true); 
            }
             
        }
         
        public void checkVm()
        {  
            foreach (Control c in flowVm.Controls)
            { 
                Panel p = (Panel)c;
                checkVmStatus(p); 
            }
        }
        public void checkVmStatus(Panel panel,bool check_btn=false)
        {
            string status = panel.Controls["text_host_status"].Text.ToString();
            System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcesses();
            foreach (Process proc in list)
            {
                if (proc.ProcessName == "Droid4X")
                {
                    if (panel.Controls["text_code"].Text.Trim() + LoopConfig.hmVersion == proc.MainWindowTitle)
                    {
                        if (status == "on"||status=="loading")
                        {
                            if (check_btn)
                            {
                                panel.Controls["text_process_id"].Text = proc.Id.ToString();
                                panel.Controls["text_host_status"].Text = "on";
                                panel.Controls["text_script"].Text = "";
                                panel.Controls["label_status"].Text = "完成"; 
                            }
                            return;
                        }
                       
                    }
                }
            }
            panel.Controls["text_host_status"].Text = "off";
        }
        public void checkVmbyPid()
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
                string status = p.Controls["text_host_status"].Text.ToString();
                if (status == "on")
                {
                    string pid = p.Controls["text_process_id"].Text.ToString();
                    if (pid == "" || pids.Contains(pid))
                    {
                        p.Controls["text_host_status"].Text = "on";
                        continue;
                    }
                    FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", p.Controls["text_code"].Text.ToString() + "  pid:" + pid + " host_status: " + p.Controls["text_host_status"].Text);
                    p.Controls["text_host_status"].Text = "off";
                    if (p.Controls["text_exe_status"].Text != "stop")
                    {
                        p.Controls["label_status"].Text = "等待重启..";
                    }
                }
            }
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    try
                    {
                        tWorkingThread.Abort();
                    }
                    catch (Exception ex)
                    {
                        FileHelper.writeLogs(LoopConfig.log + "closing_error.txt", "error : " + ex.StackTrace);
                    }
                }
            }
            List<deviceItem> devices = new List<deviceItem>();
            foreach (ListViewItem item in devlist.Items)
            {
                deviceItem dev = (deviceItem)item.Tag;
                devices.Add(dev);
            }
            data data = new data();
            try
            {
                XmlHelper.objToXml(devices, LoopConfig.devicesXml);
            }
            catch (Exception ex)
            {
                FileHelper.writeLogs(LoopConfig.log + "closing_error.txt", "error : " + ex.StackTrace);
                return;
            }

            //List<proxyItem> proxys = new List<proxyItem>();
            //foreach (ListViewItem item in proxylist.Items)
            //{
            //    proxyItem dev = (proxyItem)item.Tag;
            //    proxys.Add(dev);
            //} 
            //try
            //{
            //    XmlHelper.objToXml(proxys, LoopConfig.proxysXml);
            //}
            //catch (Exception ex)
            //{
            //    FileHelper.writeLogs(LoopConfig.log + "closing_error.txt", "error : " + ex.StackTrace);
            //    return;
            //}



            Thread.Sleep(2000);//  
        }


        public string getApk()
        {
            GC.Collect();
            string path = "";
            string dir = LoopConfig.apks;
            getApkDialog.InitialDirectory = dir;
            if (getApkDialog.ShowDialog() == DialogResult.OK)
            {
                path = getApkDialog.FileName.ToString();
            }
            getApkDialog.Dispose();
            return path;
        }

        private void but_dowork_all_Click(object sender, EventArgs e)
        {
            doWorkAll();
        }


        public void doWorkAll()
        {
            foreach (Control panel in flowVm.Controls)
            {
                string status = panel.Controls["text_host_status"].Text.ToString();
                if (panel.Controls["text_exe_status"].Text == "stop")
                {
                    panel.Controls["text_exe_status"].Text = "off";// 开始执行  
                }
                if (status == "on")
                {
                    doWork((Panel)panel);
                }
                if (status == "off")
                {
                    panel.Controls["text_exe_status"].Text = "off";
                    doStartVm((Panel)panel);
                }
                FileHelper.writeLogs(LoopConfig.log + "doStartVm.txt", panel.Controls["text_code"].Text.ToString() + " " + panel.Controls["text_host_status"].Text + "  doWorkAll 开始");
            }
        }

        public void stopWorkAll()
        {
            foreach (Control panel in flowVm.Controls)
            {
                stop_now((Panel)panel);

            }
        }
        private void but_all_stop_Click(object sender, EventArgs e)
        {
            foreach (Control panel in flowVm.Controls)
            {
                panel.Controls["text_exe_status"].Text = "stop";

            }
        }
        private void flowVm_MouseClick(object sender, MouseEventArgs e)
        {
            flowVm.Focus();
        }
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            flowVm.Focus();
        }

        private void but_request_Click(object sender, EventArgs e)
        {
            request();
        }

        private void but_apk_config_Click(object sender, EventArgs e)
        {
            show_apk_config();
        }

        private void but_vm_config_Click(object sender, EventArgs e)
        {
            show_vm_config();
        }


        private void show_apk_config()
        {
            GC.Collect();
            ApkConfig config = new ApkConfig();
            config.StartPosition = FormStartPosition.CenterScreen;
            config.ShowDialog();
            if (config.isModify)
            {
                init_script_config();
                init_combox_game();
            }
            GC.Collect();

        }

        public void init_script_config()
        {
            script_list = XmlHelper.xmlToList<script>(LoopConfig.scriptXml);

        }
        private void show_vm_config()
        {
            GC.Collect();
            VmConfig config = new VmConfig();
            config.StartPosition = FormStartPosition.CenterScreen;
            config.ShowDialog();
            if (config.DialogResult == DialogResult.Yes)
            {
                addPanel(config.selItem);
                // timer1.Enabled = true;
                // doWork((Panel)flowVm.Controls[config.selItem.code]); 
            }
            GC.Collect();
        }

        private void but_clear_ache_Click(object sender, EventArgs e)
        {
            if (File.Exists(LoopConfig.devicesXml)) File.Delete(LoopConfig.devicesXml);
            devlist.Items.Clear();
        }

        private void but_all_stop_now_Click(object sender, EventArgs e)
        {
            stopWorkAll();
        }

        private void but_uninstall_Click(object sender, EventArgs e)
        {
            string uninstall_package = text_uninstall_package.Text.ToString();

            if (uninstall_package == "") return;

            foreach (Control control in flowVm.Controls)
            {
                Panel panel = (Panel)control;
                if (panel.Controls["text_host_status"].Text == "on")
                {
                    panel.Controls["text_script"].Text = "";
                    InstallThread thread = new InstallThread();
                    thread.uninstall_package = uninstall_package;
                    thread.vm_panel = panel;
                    thread.cmd = check_cmd.Checked;
                    thread.callback = InstallCallBackDelegate;
                    Thread tWorkingThread = new Thread(thread.doUinstall);
                    tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
                    taskThreadList.Add(tWorkingThread);
                    tWorkingThread.Start();
                }
            }

        }

        private void but_install_apk_Click(object sender, EventArgs e)
        {

            string apk_path = getApk();
            if (apk_path == "") return;
            foreach (Control control in flowVm.Controls)
            {
                Panel panel = (Panel)control;

                if (panel.Controls["text_host_status"].Text == "on")
                {
                    panel.Controls["text_script"].Text = "";
                    InstallThread thread = new InstallThread();
                    thread.apk_path = apk_path;
                    thread.vm_panel = panel;
                    thread.cmd = check_cmd.Checked;
                    thread.callback = InstallCallBackDelegate;
                    Thread tWorkingThread = new Thread(thread.doInstall);
                    tWorkingThread.SetApartmentState(ApartmentState.STA); // 单线程
                    taskThreadList.Add(tWorkingThread);
                    tWorkingThread.Start();

                }
            }
        }
        /// <summary>
        /// 安装回调
        /// </summary> 
        ///  
        private void InstallCallBackDelegate(string msg, Panel panel)
        {
            GC.Collect();

            if (msg == InstallThread.MSG_INSTALL_COMPLETE)
            {
                panel.Controls["label_status"].Text = "完成";

            }
            if (msg == InstallThread.MSG_UNINSTALL_COMPLETE)
            {
                panel.Controls["label_status"].Text = "完成";
            }
            if (msg == InstallThread.MSG_ERROR)
            {
                panel.Controls["label_status"].Text = "安装或者卸载失败";

            }
        }

        private void but_update_vm_Click(object sender, EventArgs e)
        {
         //   LoopConfig.hmVersion = text_hm_version.Text;
            initVmlist();
            GC.Collect();
        }

        private void but_all_restart_Click(object sender, EventArgs e)
        {
            foreach (Control panel in flowVm.Controls)
            {
                doStartVm((Panel)panel, true);
                panel.Controls["text_exe_status"].Text = "stop";
                panel.Controls["text_script"].Text = "";
                panel.Controls["label_status"].Text = "完成";
            }
        }

        private void comboBox_game_SelectedIndexChanged(object sender, EventArgs e)
        {
            GC.Collect();

        }

        private void check_proxy_CheckedChanged(object sender, EventArgs e)
        {
            proxylist.Visible = check_proxy.Checked;
            but_proxy.Visible = check_proxy.Checked;
        }

        private void but_proxy_Click(object sender, EventArgs e)
        {
            getProxy();
        }

        private void but_clear_task_Click(object sender, EventArgs e)
        {
            startProcess("kill_task.bat", true);
        }
        public void startProcess(string bat_name, bool cmd = false)
        {
            Process process = new Process();
            if (!cmd)
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
            }
            string temp_proc = LoopConfig.bat_temp + "temp" + "_" + bat_name;
            File.Copy(LoopConfig.bat + bat_name, temp_proc, true);
            process.StartInfo.FileName = temp_proc;
            process.Start();
            process.WaitForExit();
            process.Close();
            process = null;
            if (File.Exists(temp_proc)) File.Delete(temp_proc);

        }

        private void comboBox_hm_version_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoopConfig.hmVersion = " "+comboBox_hm_version.SelectedValue.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            text_proxy_ip_port.Text = "10.10.10.107:9999";
        }





    }
}
