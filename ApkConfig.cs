using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoopClient
{
    public partial class ApkConfig : Form
    {
        List<script> list;
        public script selItem = null;
        public bool isModify = false;
        public ApkConfig()
        {
            InitializeComponent(); 
            list = XmlHelper.xmlToList<script>(LoopConfig.scriptXml);
            initListView();

        }
        private void initListView()
        {
            listView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大
            listView.Clear();
            listView.Columns.Add("序号", 40, HorizontalAlignment.Left); //一步添加  
            listView.Columns.Add("游戏", 90, HorizontalAlignment.Left); //一步添加  
            listView.Columns.Add("平台", 90, HorizontalAlignment.Left); //一步添加    
            listView.Columns.Add("等级", 50, HorizontalAlignment.Left); //一步添加    
            int i = 0;
            foreach (script citem in list)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = citem;//-----------------
                item.Text = ++i + "";
                item.SubItems.Add(citem.game_code);
                item.SubItems.Add(citem.platform_code);
                item.SubItems.Add(citem.apk_level.ToString());
                item.Tag = citem;
                listView.Items.Add(item);
            }
            listView.EndUpdate();  //结束数据处理，UI界面一次性绘制
            GC.Collect();
        }
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        { 
            GC.Collect();
            if (listView.SelectedItems.Count > 0)
            {
             
                foreach (ListViewItem item in listView.Items)
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.FromArgb(239, 248, 250); //恢复默认背景色  

                }
                //修改选中项颜色
                listView.SelectedItems[0].SubItems[0].ForeColor = Color.SaddleBrown;
                listView.SelectedItems[0].BackColor = Color.Silver;
                clear();
                selItem = (script)listView.SelectedItems[0].Tag;
               
                text_game_code.Text = selItem.game_code;
                text_game_package.Text = listToString(selItem.game_packages); 
                text_game_activity.Text = selItem.game_activity;
                text_game_name.Text = selItem.game_name; 
                text_platform_code.Text = selItem.platform_code;
                text_platform_name.Text = selItem.platform_name;
                text_apk_activity.Text = selItem.apk_activity;
                text_apk_package.Text = selItem.apk_package;


                text_apk_level.Text = selItem.apk_level.ToString();
                text_click_count.Text = selItem.click_count.ToString();
                text_click_delay.Text = selItem.click_delay.ToString();

                text_filter.Text = selItem.filter;
                check_login_code.Checked = selItem.isLogin_code;
                check_login_qq.Checked = selItem.isLogin_qq;
                check_server.Checked = selItem.isRecord_server;
                //去掉选中项背景
                listView.SelectedItems[0].Selected = false;// 会引发第二次 该方法的调用
                listView.SelectedItems.Clear();
            }
        }
        private string listToString(List<string> list)
        {
            if (list.Count == 0) return "";
            string s = "";
            if (list.Count == 1)
            {
                s = list[0];
                return s;

            }
            s = list[0];
            for(int i=1;i<list.Count ;i++){
                s+=","+list[i]; 
            }  
            return s; 
        }
        private void clear()
        {
           // selItem = null;
            text_game_code.Text = "";
            text_game_package.Text = "";
            text_game_activity.Text = "";
            text_game_name.Text = "";
            text_filter.Text = "";


            text_platform_code.Text = "";
            text_platform_name.Text = "";
            text_apk_activity.Text = "";
            text_apk_package.Text = "";
            text_apk_level.Text = "0";
            text_click_count.Text = "0";
            text_click_delay.Text = "10";
            check_login_code.Checked = false;
            check_login_qq.Checked = false;
            check_server.Checked = false;
        }

        private void but_save_Click(object sender, EventArgs e)
        {
            
            if (!Directory.Exists(LoopConfig.xmlfolder)) Directory.CreateDirectory(LoopConfig.xmlfolder);  
            string game_name = text_game_name.Text.ToString();
            string game_package = text_game_package.Text.ToString();
            string game_activity = text_game_activity.Text.ToString();
            string game_code = text_game_code.Text.ToString();

            string platform_code = text_platform_code.Text.ToString();
            string platform_name = text_platform_name.Text.ToString(); 
            string apk_package = text_apk_package.Text.ToString();
            string apk_level = text_apk_level.Text.ToString();
            string apk_activity = text_apk_activity.Text.ToString();
            string click_count = text_click_count.Text.ToString();
            string click_delay = text_click_delay.Text.ToString().Trim();


            string filter = text_filter.Text.ToString(); 

            if (game_code == "" || platform_code == "" || game_package == "" || game_activity == "" || apk_package == "" || apk_activity == "" || apk_level == "")
            {

                MessageBox.Show("必填信息不全,保存失败");
                return ;
            }
             
            script item = new script(); 
            item.game_activity = game_activity;
            item.game_code = game_code;
   //         item.game_package = game_package;
            item.game_name = game_name; 
            item.platform_code = platform_code;
            item.platform_name = platform_name; 
            item.game_platform = game_code + "_" + platform_code; 
            item.isLogin_code = check_login_code.Checked;
            item.isLogin_qq = check_login_qq.Checked;
            item.isRecord_server = check_server.Checked;
            item.filter = filter; 

            try
            {
                item.apk_level = Convert.ToInt32(apk_level);
            }
            catch (Exception)
            { 
                item.apk_level =0;
            } 
            try
            {
                item.click_count = Convert.ToInt32(click_count);
            }
            catch (Exception)
            {
                item.click_count = 5;
            }

            try
            {
                item.click_delay = Convert.ToInt32(click_delay);
            }
            catch (Exception)
            {
                item.click_delay = 10;
            }  
            item.apk_activity = apk_activity;
            item.apk_package = apk_package; 
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].game_platform == item.game_platform)
                {
                    list.RemoveAt(i);
                }
            }
            string[] sArray = game_package.Split(new char[1] { ',' });   
            List<string> game_packages = new List<string>(); 
            foreach (string i in sArray)
            {
                game_packages.Add(i); 
            }
            item.game_packages = game_packages; 
            list.Add(item);
            XmlHelper.objToXml(list, LoopConfig.scriptXml);
            initListView();
            isModify = true;
        }

        private void but_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void but_remove_Click(object sender, EventArgs e)
        {
            if (selItem == null) return;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].game_platform == selItem.game_platform)
                {
                    list.RemoveAt(i);
                    clear();
                }
            } 
            XmlHelper.objToXml(list, LoopConfig.scriptXml);
            initListView();
            isModify = true;
        }

        private void but_add_Click(object sender, EventArgs e)
        {
            clear(); 
        }
 
  
    }
}
