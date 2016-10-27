namespace LoopClient
{
    partial class VmConfig
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
            this.panel19 = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.text_code = new System.Windows.Forms.TextBox();
            this.text_host = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_droid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_orientation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.but_save = new System.Windows.Forms.Button();
            this.but_close = new System.Windows.Forms.Button();
            this.but_del = new System.Windows.Forms.Button();
            this.text_isOk = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.but_stop = new System.Windows.Forms.Button();
            this.but_start = new System.Windows.Forms.Button();
            this.text_game = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.but_add = new System.Windows.Forms.Button();
            this.panel19.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel19
            // 
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel19.Controls.Add(this.listView);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(257, 440);
            this.panel19.TabIndex = 2;
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(255, 438);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "编号";
            // 
            // text_code
            // 
            this.text_code.Location = new System.Drawing.Point(337, 15);
            this.text_code.Name = "text_code";
            this.text_code.Size = new System.Drawing.Size(201, 21);
            this.text_code.TabIndex = 4;
            // 
            // text_host
            // 
            this.text_host.Location = new System.Drawing.Point(337, 48);
            this.text_host.Name = "text_host";
            this.text_host.Size = new System.Drawing.Size(201, 21);
            this.text_host.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "主机";
            // 
            // text_droid
            // 
            this.text_droid.Location = new System.Drawing.Point(337, 81);
            this.text_droid.Name = "text_droid";
            this.text_droid.Size = new System.Drawing.Size(201, 21);
            this.text_droid.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Droid";
            // 
            // text_orientation
            // 
            this.text_orientation.Location = new System.Drawing.Point(337, 114);
            this.text_orientation.Name = "text_orientation";
            this.text_orientation.Size = new System.Drawing.Size(201, 21);
            this.text_orientation.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "方向";
            // 
            // but_save
            // 
            this.but_save.Location = new System.Drawing.Point(337, 186);
            this.but_save.Name = "but_save";
            this.but_save.Size = new System.Drawing.Size(75, 23);
            this.but_save.TabIndex = 11;
            this.but_save.Text = "保存";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new System.EventHandler(this.but_save_Click);
            // 
            // but_close
            // 
            this.but_close.Location = new System.Drawing.Point(337, 348);
            this.but_close.Name = "but_close";
            this.but_close.Size = new System.Drawing.Size(75, 23);
            this.but_close.TabIndex = 13;
            this.but_close.Text = "关闭";
            this.but_close.UseVisualStyleBackColor = true;
            this.but_close.Click += new System.EventHandler(this.but_close_Click);
            // 
            // but_del
            // 
            this.but_del.Location = new System.Drawing.Point(463, 186);
            this.but_del.Name = "but_del";
            this.but_del.Size = new System.Drawing.Size(75, 23);
            this.but_del.TabIndex = 14;
            this.but_del.Text = "删除";
            this.but_del.UseVisualStyleBackColor = true;
            this.but_del.Click += new System.EventHandler(this.but_del_Click);
            // 
            // text_isOk
            // 
            this.text_isOk.Location = new System.Drawing.Point(337, 147);
            this.text_isOk.Name = "text_isOk";
            this.text_isOk.Size = new System.Drawing.Size(201, 21);
            this.text_isOk.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "状态";
            // 
            // txtLevel
            // 
            this.txtLevel.Location = new System.Drawing.Point(466, 263);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(72, 21);
            this.txtLevel.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(431, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "等级";
            // 
            // but_stop
            // 
            this.but_stop.Location = new System.Drawing.Point(463, 293);
            this.but_stop.Name = "but_stop";
            this.but_stop.Size = new System.Drawing.Size(75, 23);
            this.but_stop.TabIndex = 20;
            this.but_stop.Text = "stop录制";
            this.but_stop.UseVisualStyleBackColor = true;
            this.but_stop.Click += new System.EventHandler(this.but_stop_Click);
            // 
            // but_start
            // 
            this.but_start.Location = new System.Drawing.Point(337, 293);
            this.but_start.Name = "but_start";
            this.but_start.Size = new System.Drawing.Size(75, 23);
            this.but_start.TabIndex = 21;
            this.but_start.Text = "开始录制";
            this.but_start.UseVisualStyleBackColor = true;
            this.but_start.Click += new System.EventHandler(this.but_start_Click);
            // 
            // text_game
            // 
            this.text_game.Location = new System.Drawing.Point(337, 263);
            this.text_game.Name = "text_game";
            this.text_game.Size = new System.Drawing.Size(72, 21);
            this.text_game.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "游戏";
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(337, 224);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(75, 23);
            this.but_add.TabIndex = 24;
            this.but_add.Text = "添加到列表";
            this.but_add.UseVisualStyleBackColor = true;
            this.but_add.Click += new System.EventHandler(this.but_add_Click);
            // 
            // VmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 440);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.text_game);
            this.Controls.Add(this.but_start);
            this.Controls.Add(this.but_stop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtLevel);
            this.Controls.Add(this.text_isOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.but_del);
            this.Controls.Add(this.but_close);
            this.Controls.Add(this.but_save);
            this.Controls.Add(this.text_orientation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_droid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_host);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_code);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel19);
            this.Name = "VmConfig";
            this.Text = "模拟器配置";
            this.Load += new System.EventHandler(this.VmConfig_Load);
            this.panel19.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_code;
        private System.Windows.Forms.TextBox text_host;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_droid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_orientation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button but_save;
        private System.Windows.Forms.Button but_close;
        private System.Windows.Forms.Button but_del;
        private System.Windows.Forms.TextBox text_isOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button but_stop;
        private System.Windows.Forms.Button but_start;
        private System.Windows.Forms.TextBox text_game;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button but_add;
    }
}