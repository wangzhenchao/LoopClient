using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Data;  

namespace LoopClient
{
    class DemoThread
    { 
         
        public void doWork()
        { 

            GC.Collect(); 
        }
        public void proc()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.FileName = LoopConfig.bat + "vm_start.bat";
            p.StartInfo.Arguments = "";
            p.Start();

           
            p.WaitForExit();
            p.Close();
             
        }

    }
}
