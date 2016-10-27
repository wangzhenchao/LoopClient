using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace LoopClient
{
    class FileHelper
    {
        /// <summary>
        /// 写日志
        /// </summary>
        public static void writeContent(string path, string content)
        {
            GC.Collect();
            // File.WriteAllText(path, content,Encoding.UTF8);

            if (!File.Exists(path))
                File.Create(path).Close();
            File.SetAttributes(path, FileAttributes.Normal);
            FileStream _file = new FileStream(@path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            if (_file == null)
            {
                MessageBox.Show("文件写入失败，请重试！");
                return;
            }
            using (StreamWriter writer1 = new StreamWriter(_file))
            {
                writer1.Write(content);
                writer1.Flush();
                writer1.Close();
                _file.Close();
            }

        }
        public static void writeLogs(string path, string log)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "    " + log);
                }
                using (StreamWriter sw = new StreamWriter(LoopConfig.log + "log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "    " + log);
                }
            }
            catch (Exception)
            {

                return;
            }

           
        }


        /// <summary>
        /// 随机获取批处理
        /// </summary>
        public static string getBat(string directory)
        {
            string[] bats = Directory.GetFiles(directory, "*.bat"); 
            Random ra = new Random();
            int num = bats.Length;
            int chooes = ra.Next(num);
            string file = bats[chooes].ToString();
            Console.WriteLine(file);
            return file;

        }


    }
}
