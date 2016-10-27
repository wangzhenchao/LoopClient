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
    class PostThread
    {
        
        
        deviceItem dev;
        string vm_temp = "temp";
        Process start;
        int startid;
        public void doWork()
        {
             
            GC.Collect(); 
        }
          
    }
}
