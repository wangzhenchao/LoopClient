using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace LoopClient
{
    public partial class VmConfig : Form
    {
        List<vm> list;
        Process proc = new Process();
        public vm selItem = null;
        public VmConfig()
        {
            InitializeComponent();
            list = XmlHelper.xmlToList<vm>(LoopConfig.vmsXml);
            initListView();

        }
        private void initListView()
        {
            listView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大
            listView.Clear();
            listView.Columns.Add("序号", 50, HorizontalAlignment.Left); //一步添加  
            listView.Columns.Add("编号", 50, HorizontalAlignment.Left); //一步添加  
            listView.Columns.Add("主机", 120, HorizontalAlignment.Left); //一步添加    
            listView.Columns.Add("状态", 120, HorizontalAlignment.Left); //一步添加    
            int i = 0;
            foreach (vm citem in list)
            { 
                ListViewItem item = new ListViewItem();
                item.Tag = citem;//-----------------
                item.Text = ++i + ""; 
                item.SubItems.Add(citem.code);
                item.SubItems.Add(citem.host);
                item.SubItems.Add(citem.isOk);
                item.Tag = citem;
                listView.Items.Add(item);
            }

            listView.Sort();

            listView.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }
        private void but_save_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(LoopConfig.xmlfolder)) Directory.CreateDirectory(LoopConfig.xmlfolder);
            string host = text_host.Text.ToString();
            string orientation = text_orientation.Text;
            string code = text_code.Text;
            string droid = text_droid.Text;
            string isOk = text_isOk.Text;
            vm v = new vm();
            v.host = host;
            v.orientation = orientation;
            v.code = code;
            v.droid = droid;
            v.isOk = isOk; 

           
            for (int i = 0; i <listView.Items.Count ; i++)
            {
                vm item = (vm)listView.Items[i].Tag; 
                if (item.code == v.code)
                {
                    listView.Items[i].Remove(); 
                }  
            }
            list.Clear(); 
            for (int i = 0; i < listView.Items.Count; i++)
            {
                vm item = (vm)listView.Items[i].Tag;
                list.Add(item); 
            }

            if (host == "" || code == "" || droid == "")
            {
                //MessageBox.Show("信息不全");
                //return;
            }
            else
            {
                list.Add(v);
            }
             
            XmlHelper.objToXml(list, LoopConfig.vmsXml);
            initListView();

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            GC.Collect();
            if (listView.SelectedItems.Count > 0)
            {
                selItem = (vm)listView.SelectedItems[0].Tag;

                foreach (ListViewItem item in listView.Items)
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.FromArgb(239, 248, 250); //恢复默认背景色 
                   
                }
                //修改选中项颜色
                listView.SelectedItems[0].SubItems[0].ForeColor = Color.SaddleBrown;
                listView.SelectedItems[0].BackColor = Color.Silver;

                text_host.Text = "";
                text_orientation.Text = "竖屏";
                text_code.Text = "";
                text_droid.Text = "droid4x";

                text_host.Text = selItem.host;
                text_orientation.Text = selItem.orientation;
                text_code.Text = selItem.code;
                text_droid.Text = selItem.droid;
                text_isOk.Text = selItem.isOk; 
                //去掉选中项背景
                listView.SelectedItems[0].Selected = false;// 会引发第二次 该方法的调用
                listView.SelectedItems.Clear();
            }
        }


        private void but_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].code == text_code.Text)
                {
                    list.RemoveAt(i);
                }
            }

            XmlHelper.objToXml(list, LoopConfig.vmsXml);
            initListView();
        }

        int pid;
        private void but_start_Click(object sender, EventArgs e)
        {
            but_start.Enabled = false;
            string path = LoopConfig.bat_temp +"base-"+ txtLevel.Text.ToString() + ".txt";

            string temp_start = LoopConfig.bat + @"startRec.bat";
            proc.StartInfo.FileName = temp_start;
            proc.StartInfo.Arguments = path;
            proc.Start();
            pid = proc.Id;
        }

        private void but_stop_Click(object sender, EventArgs e)
        {
            if (proc == null) return;
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = LoopConfig.bat + "kill_t.bat";
                p.StartInfo.Arguments = pid + "";
                p.Start();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception)
            {
                throw;
            } 
            proc = null;
            Thread.Sleep(1000);
            string tmpBat = "\n";
            Int32 t = 0;
            string path = LoopConfig.bat_temp + "base-" + txtLevel.Text.ToString() + ".txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("录制错误:"+path );
                return;
            }

            string[] readText = File.ReadAllLines(path);
            foreach (string str in readText)
            {

                if (str.ToString().Trim().Length > 0)
                {
                    string s = str.ToString();
                    string[] arr = s.Split(' ');
                    if (s.Contains("014a"))
                    {
                        Int32 ti = Convert.ToInt32(arr[0].Substring(1, 10));
                        if (t != 0)
                        {
                            Int32 tt = ti - t;
                            if (tt == 0)
                                tt = 1;
                            if (tt > 0)
                            {
                                tmpBat = tmpBat + " adb shell sleep " + tt + "\n";
                            }
                        }
                        t = ti;
                    }
                    tmpBat = tmpBat + " adb shell sendevent /dev/input/event8  " + Convert.ToInt32(arr[1]) + " $((0x" + arr[2] + "))  $((0x" + arr[3] + "))\n";
                }
            }
            //        MessageBox.Show(tmpBat); 
            string folder = LoopConfig.bat+text_game.Text.ToString()+@"\rec\";
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder); 
            FileHelper.writeContent(folder+"base-" + txtLevel.Text.ToString() + ".bat", tmpBat);


            DialogResult dr = MessageBox.Show("录制完毕,是否需要打开文件目录！", "提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            { 
                if (Directory.Exists(folder))
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                    psi.Arguments = "/e,/open, " + folder;
                    System.Diagnostics.Process.Start(psi);
                } 
            }                
             
            but_start.Enabled = true;
        }

        private void but_add_Click(object sender, EventArgs e)
        {
            if (selItem != null)
            {
                this.DialogResult = DialogResult.Yes;
            }
        }

        private void VmConfig_Load(object sender, EventArgs e)
        {
            this.listView.ListViewItemSorter = new ListViewColumnSorter();
            this.listView.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
        }
    }
}
