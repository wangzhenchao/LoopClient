namespace LoopClient
{
    partial class LoopForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowVm = new System.Windows.Forms.FlowLayoutPanel();
            this.but_refresh_vms = new System.Windows.Forms.Button();
            this.but_config = new System.Windows.Forms.Button();
            this.but_request = new System.Windows.Forms.Button();
            this.lab_activity = new System.Windows.Forms.Label();
            this.lab_package = new System.Windows.Forms.Label();
            this.lab_game = new System.Windows.Forms.Label();
            this.devlist = new System.Windows.Forms.ListView();
            this.but_begin = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.text_time_limit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_num_limit = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.text_sleep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_start_time = new System.Windows.Forms.Label();
            this.lab_current_time = new System.Windows.Forms.Label();
            this.lab_request_time = new System.Windows.Forms.Label();
            this.but_add_vm = new System.Windows.Forms.Button();
            this.text_allsecs = new System.Windows.Forms.Label();
            this.check_quest = new System.Windows.Forms.CheckBox();
            this.check_cmd = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_top_level = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.text_anjian_package = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_anjian_activity = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.text_first_level = new System.Windows.Forms.TextBox();
            this.text_time_over = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label_now_hour = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.but_apk_config = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.text_request_game = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // flowVm
            // 
            this.flowVm.AutoScroll = true;
            this.flowVm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowVm.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowVm.Location = new System.Drawing.Point(13, 73);
            this.flowVm.Margin = new System.Windows.Forms.Padding(10);
            this.flowVm.Name = "flowVm";
            this.flowVm.Size = new System.Drawing.Size(724, 374);
            this.flowVm.TabIndex = 4;
            this.flowVm.WrapContents = false;
            // 
            // but_refresh_vms
            // 
            this.but_refresh_vms.Location = new System.Drawing.Point(16, 41);
            this.but_refresh_vms.Name = "but_refresh_vms";
            this.but_refresh_vms.Size = new System.Drawing.Size(82, 26);
            this.but_refresh_vms.TabIndex = 5;
            this.but_refresh_vms.Text = "刷新配置";
            this.but_refresh_vms.UseVisualStyleBackColor = true;
            this.but_refresh_vms.Click += new System.EventHandler(this.but_refresh_vms_Click);
            // 
            // but_config
            // 
            this.but_config.Location = new System.Drawing.Point(16, 5);
            this.but_config.Name = "but_config";
            this.but_config.Size = new System.Drawing.Size(82, 26);
            this.but_config.TabIndex = 6;
            this.but_config.Text = "列表配置";
            this.but_config.UseVisualStyleBackColor = true;
            this.but_config.Click += new System.EventHandler(this.but_config_Click);
            // 
            // but_request
            // 
            this.but_request.Location = new System.Drawing.Point(563, 595);
            this.but_request.Name = "but_request";
            this.but_request.Size = new System.Drawing.Size(75, 23);
            this.but_request.TabIndex = 49;
            this.but_request.Text = "请求";
            this.but_request.UseVisualStyleBackColor = true;
            this.but_request.Click += new System.EventHandler(this.but_request_Click);
            // 
            // lab_activity
            // 
            this.lab_activity.AutoSize = true;
            this.lab_activity.Location = new System.Drawing.Point(14, 508);
            this.lab_activity.Name = "lab_activity";
            this.lab_activity.Size = new System.Drawing.Size(77, 12);
            this.lab_activity.TabIndex = 48;
            this.lab_activity.Text = "lab_activity";
            // 
            // lab_package
            // 
            this.lab_package.AutoSize = true;
            this.lab_package.Location = new System.Drawing.Point(14, 485);
            this.lab_package.Name = "lab_package";
            this.lab_package.Size = new System.Drawing.Size(71, 12);
            this.lab_package.TabIndex = 47;
            this.lab_package.Text = "lab_package";
            // 
            // lab_game
            // 
            this.lab_game.AutoSize = true;
            this.lab_game.Location = new System.Drawing.Point(14, 458);
            this.lab_game.Name = "lab_game";
            this.lab_game.Size = new System.Drawing.Size(29, 12);
            this.lab_game.TabIndex = 46;
            this.lab_game.Text = "game";
            // 
            // devlist
            // 
            this.devlist.FullRowSelect = true;
            this.devlist.Location = new System.Drawing.Point(13, 529);
            this.devlist.MultiSelect = false;
            this.devlist.Name = "devlist";
            this.devlist.Size = new System.Drawing.Size(524, 214);
            this.devlist.TabIndex = 45;
            this.devlist.UseCompatibleStateImageBehavior = false;
            this.devlist.View = System.Windows.Forms.View.Details;
            // 
            // but_begin
            // 
            this.but_begin.Location = new System.Drawing.Point(122, 5);
            this.but_begin.Name = "but_begin";
            this.but_begin.Size = new System.Drawing.Size(82, 26);
            this.but_begin.TabIndex = 50;
            this.but_begin.Text = "全部开始";
            this.but_begin.UseVisualStyleBackColor = true;
            this.but_begin.Click += new System.EventHandler(this.but_begin_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // text_time_limit
            // 
            this.text_time_limit.Location = new System.Drawing.Point(484, 10);
            this.text_time_limit.Name = "text_time_limit";
            this.text_time_limit.Size = new System.Drawing.Size(44, 21);
            this.text_time_limit.TabIndex = 51;
            this.text_time_limit.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(401, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "执行超时(秒)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "重启间隔次数";
            // 
            // text_num_limit
            // 
            this.text_num_limit.Location = new System.Drawing.Point(335, 46);
            this.text_num_limit.Name = "text_num_limit";
            this.text_num_limit.Size = new System.Drawing.Size(44, 21);
            this.text_num_limit.TabIndex = 53;
            this.text_num_limit.Text = "10";
            // 
            // timer2
            // 
            this.timer2.Interval = 30000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // text_sleep
            // 
            this.text_sleep.Location = new System.Drawing.Point(484, 46);
            this.text_sleep.Name = "text_sleep";
            this.text_sleep.Size = new System.Drawing.Size(44, 21);
            this.text_sleep.TabIndex = 54;
            this.text_sleep.Text = "120";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(401, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 55;
            this.label3.Text = "重启加载(秒)";
            // 
            // lab_start_time
            // 
            this.lab_start_time.AutoSize = true;
            this.lab_start_time.Location = new System.Drawing.Point(555, 13);
            this.lab_start_time.Name = "lab_start_time";
            this.lab_start_time.Size = new System.Drawing.Size(89, 12);
            this.lab_start_time.TabIndex = 56;
            this.lab_start_time.Text = "lab_start_time";
            // 
            // lab_current_time
            // 
            this.lab_current_time.AutoSize = true;
            this.lab_current_time.Location = new System.Drawing.Point(555, 49);
            this.lab_current_time.Name = "lab_current_time";
            this.lab_current_time.Size = new System.Drawing.Size(101, 12);
            this.lab_current_time.TabIndex = 57;
            this.lab_current_time.Text = "lab_current_time";
            // 
            // lab_request_time
            // 
            this.lab_request_time.AutoSize = true;
            this.lab_request_time.Location = new System.Drawing.Point(565, 639);
            this.lab_request_time.Name = "lab_request_time";
            this.lab_request_time.Size = new System.Drawing.Size(53, 12);
            this.lab_request_time.TabIndex = 58;
            this.lab_request_time.Text = "请求次数";
            // 
            // but_add_vm
            // 
            this.but_add_vm.Location = new System.Drawing.Point(122, 41);
            this.but_add_vm.Name = "but_add_vm";
            this.but_add_vm.Size = new System.Drawing.Size(82, 26);
            this.but_add_vm.TabIndex = 63;
            this.but_add_vm.Text = "添加";
            this.but_add_vm.UseVisualStyleBackColor = true;
            this.but_add_vm.Click += new System.EventHandler(this.but_add_vm_Click);
            // 
            // text_allsecs
            // 
            this.text_allsecs.AutoSize = true;
            this.text_allsecs.Location = new System.Drawing.Point(555, 31);
            this.text_allsecs.Name = "text_allsecs";
            this.text_allsecs.Size = new System.Drawing.Size(11, 12);
            this.text_allsecs.TabIndex = 66;
            this.text_allsecs.Text = "0";
            // 
            // check_quest
            // 
            this.check_quest.AutoSize = true;
            this.check_quest.Location = new System.Drawing.Point(665, 602);
            this.check_quest.Name = "check_quest";
            this.check_quest.Size = new System.Drawing.Size(72, 16);
            this.check_quest.TabIndex = 67;
            this.check_quest.Text = "停止请求";
            this.check_quest.UseVisualStyleBackColor = true;
            // 
            // check_cmd
            // 
            this.check_cmd.AutoSize = true;
            this.check_cmd.Location = new System.Drawing.Point(245, 11);
            this.check_cmd.Name = "check_cmd";
            this.check_cmd.Size = new System.Drawing.Size(66, 16);
            this.check_cmd.TabIndex = 68;
            this.check_cmd.Text = "显示CMD";
            this.check_cmd.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(588, 469);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 69;
            this.label5.Text = "脚本等级";
            // 
            // text_top_level
            // 
            this.text_top_level.Location = new System.Drawing.Point(659, 465);
            this.text_top_level.Name = "text_top_level";
            this.text_top_level.Size = new System.Drawing.Size(44, 21);
            this.text_top_level.TabIndex = 70;
            this.text_top_level.Text = "8";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(567, 718);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 71;
            this.button1.Text = "cest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(221, 495);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 72;
            this.label6.Text = "按键包名";
            // 
            // text_anjian_package
            // 
            this.text_anjian_package.Location = new System.Drawing.Point(293, 492);
            this.text_anjian_package.Name = "text_anjian_package";
            this.text_anjian_package.Size = new System.Drawing.Size(244, 21);
            this.text_anjian_package.TabIndex = 73;
            this.text_anjian_package.Text = "com.mbmfmemnngmpmemkml.xjwqz.waifang";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 469);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 74;
            this.label7.Text = "按键activity";
            // 
            // text_anjian_activity
            // 
            this.text_anjian_activity.Location = new System.Drawing.Point(292, 466);
            this.text_anjian_activity.Name = "text_anjian_activity";
            this.text_anjian_activity.Size = new System.Drawing.Size(244, 21);
            this.text_anjian_activity.TabIndex = 75;
            this.text_anjian_activity.Text = "com.cyjh.elfin.activity.MainActivity";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(670, 718);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 76;
            this.button2.Text = "hack";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(558, 495);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 77;
            this.label8.Text = "1-5级随机升级Max";
            // 
            // text_first_level
            // 
            this.text_first_level.Location = new System.Drawing.Point(659, 492);
            this.text_first_level.Name = "text_first_level";
            this.text_first_level.Size = new System.Drawing.Size(44, 21);
            this.text_first_level.TabIndex = 78;
            this.text_first_level.Text = "2";
            // 
            // text_time_over
            // 
            this.text_time_over.Location = new System.Drawing.Point(670, 674);
            this.text_time_over.Name = "text_time_over";
            this.text_time_over.Size = new System.Drawing.Size(44, 21);
            this.text_time_over.TabIndex = 64;
            this.text_time_over.Text = "9";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(558, 677);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 65;
            this.label4.Text = "几点开始请求数据";
            // 
            // label_now_hour
            // 
            this.label_now_hour.AutoSize = true;
            this.label_now_hour.Location = new System.Drawing.Point(755, 677);
            this.label_now_hour.Name = "label_now_hour";
            this.label_now_hour.Size = new System.Drawing.Size(11, 12);
            this.label_now_hour.TabIndex = 79;
            this.label_now_hour.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(720, 677);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "当前";
            // 
            // but_apk_config
            // 
            this.but_apk_config.Location = new System.Drawing.Point(670, 20);
            this.but_apk_config.Name = "but_apk_config";
            this.but_apk_config.Size = new System.Drawing.Size(75, 23);
            this.but_apk_config.TabIndex = 81;
            this.but_apk_config.Text = "脚本配置";
            this.but_apk_config.UseVisualStyleBackColor = true;
            this.but_apk_config.Click += new System.EventHandler(this.but_apk_config_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(565, 563);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 83;
            this.label10.Text = "请求游戏";
            // 
            // text_request_game
            // 
            this.text_request_game.Location = new System.Drawing.Point(659, 560);
            this.text_request_game.Name = "text_request_game";
            this.text_request_game.Size = new System.Drawing.Size(78, 21);
            this.text_request_game.TabIndex = 82;
            // 
            // LoopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 761);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.text_request_game);
            this.Controls.Add(this.but_apk_config);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_now_hour);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.text_first_level);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_anjian_activity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.text_anjian_package);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.text_top_level);
            this.Controls.Add(this.check_cmd);
            this.Controls.Add(this.check_quest);
            this.Controls.Add(this.text_allsecs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_time_over);
            this.Controls.Add(this.but_add_vm);
            this.Controls.Add(this.lab_request_time);
            this.Controls.Add(this.lab_current_time);
            this.Controls.Add(this.lab_start_time);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_sleep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_num_limit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_time_limit);
            this.Controls.Add(this.but_begin);
            this.Controls.Add(this.but_request);
            this.Controls.Add(this.lab_activity);
            this.Controls.Add(this.lab_package);
            this.Controls.Add(this.lab_game);
            this.Controls.Add(this.devlist);
            this.Controls.Add(this.but_config);
            this.Controls.Add(this.but_refresh_vms);
            this.Controls.Add(this.flowVm);
            this.Name = "LoopForm";
            this.Text = "LoopForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoopForm_FormClosed);
            this.Load += new System.EventHandler(this.LoopForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowVm;
        private System.Windows.Forms.Button but_refresh_vms;
        private System.Windows.Forms.Button but_config;
        private System.Windows.Forms.Button but_request;
        private System.Windows.Forms.Label lab_activity;
        private System.Windows.Forms.Label lab_package;
        private System.Windows.Forms.Label lab_game;
        private System.Windows.Forms.ListView devlist;
        private System.Windows.Forms.Button but_begin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox text_time_limit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_num_limit;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox text_sleep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_start_time;
        private System.Windows.Forms.Label lab_current_time;
        private System.Windows.Forms.Label lab_request_time;
        private System.Windows.Forms.Button but_add_vm;
        private System.Windows.Forms.Label text_allsecs;
        private System.Windows.Forms.CheckBox check_quest;
        private System.Windows.Forms.CheckBox check_cmd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_top_level;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_anjian_package;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_anjian_activity;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox text_first_level;
        private System.Windows.Forms.TextBox text_time_over;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_now_hour;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button but_apk_config;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox text_request_game;
    }
}