using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoopClient
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String hs = textBox1.Text.ToString();

            deviceItem item = new deviceItem();
            item.device_id = hs;

        //    int code = asc(hs);
            string str1 =hs.Substring(0,1);
            string str2 = hs.Substring(1,1);



            int code = item.device_id.GetHashCode();

          //  int code = getInt(str1)+getInt(str2) + hs.Length; 
            int index = code % 5;
            if (index < 0)
            {
                index = -index;
            }

            textBox2.Text = code + "";
            textBox3.Text = index + "";
        }


        public int getInt(string str)
        { 
            return asc(str);

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
