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
using System.Data.Common;
using System.Data.SQLite;
using SQLiteQueryBrowser;

namespace LoopClient
{
    class InsertThread
    {
        SQLiteDBHelper db;
        public deviceItem dev;
        public bool isTest=false;

        public void doWork()
        { 
            GC.Collect();

         

            CreateTable();
            InsertItemData(dev);
            if (dev.isError) return;
            string data =  HttpHelper.UrlEncode(dev.ToJson()); 
            PostSubmitter post = new PostSubmitter();
            post.Url = LoopConfig.post_url;
            //if (!isTest)
            //{
            //    post.Url = LoopConfig.post_url;
            //}
            //else
            //{
            //    post.Url = LoopConfig.post_test_url;
            //} 
            post.PostItems.Add("data", data);
            post.Type = PostSubmitter.PostTypeEnum.Post;
            try
            {
                string result = post.Post();
                Console.WriteLine("insertData  " + result); 
            }
            catch (Exception)
            {
                 
            }

            
        }
        public class postData
        {
           public  string data { get; set; }

        }
        ////////////////////
        public void CreateTable()
        {
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(LoopConfig.dev_db))
            {
                SQLiteDBHelper.CreateDB(LoopConfig.dev_db);
            }
            db = new SQLiteDBHelper(LoopConfig.dev_db);
            string sql = "CREATE TABLE IF NOT EXISTS  Devices(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,game varchar(15),platform varchar(10),androidId varchar(30),device_id varchar(30),device_type varchar(30),uname varchar(20),password varchar(20),old_level char(10),level char(10),active integer,isError BOOLEAN,addDate datetime)";
            db.ExecuteNonQuery(sql, null);
        }
        public void InsertItemData(deviceItem devData)
        {
            string sql = "INSERT INTO Devices(game,platform,androidId,device_id,device_type,uname,password,old_level,level,active,isError,addDate)values(@game,@platform,@androidId,@device_id,@device_type,@uname,@password,@old_level,@level,@active,@isError,@addDate)";
            db = new SQLiteDBHelper(LoopConfig.dev_db);
            SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                                new SQLiteParameter("@game",dev.game), 
                                                new SQLiteParameter("@platform",dev.platform), 
                                                new SQLiteParameter("@androidId",dev.android_id), 
                                                new SQLiteParameter("@device_id",dev.device_id), 
                                                new SQLiteParameter("@device_type",dev.device_type), 
                                                new SQLiteParameter("@uname",dev.uname), 
                                                new SQLiteParameter("@password",dev.password), 
                                                new SQLiteParameter("@old_level",dev.old_level), 
                                                new SQLiteParameter("@level",dev.level), 
                                                new SQLiteParameter("@active",dev.active),
                                                new SQLiteParameter("@isError",dev.isError),
                                                new SQLiteParameter("@addDate",DateTime.Now)
                                         };
            db.ExecuteNonQuery(sql, parameters);
        }

        public void InsertListData(List<deviceItem> devData)
        { 
            foreach (deviceItem item in devData)
            {
                InsertItemData(item);
            }
        }

      
        //        UPDATE table_name
        //SET column1 = value1, column2 = value2...., columnN = valueN
        //WHERE [condition];
        public void UpdateData(List<deviceItem> devData)
        {
            //string sql = "UPDATE Devices set active=@active,";
            //db = new SQLiteDBHelper(LoopConfig.dev_db);
            //foreach (deviceItem item in devData)
            //{
            //    SQLiteParameter[] parameters = new SQLiteParameter[]{ 
            //                                     new SQLiteParameter("@androidId",item.android_id), 
            //                             new SQLiteParameter("@uid",item.uname), 
            //                             new SQLiteParameter("@password",item.password), 
            //                             new SQLiteParameter("@active",item.active),
            //                              new SQLiteParameter("@addDate",DateTime.Now)
            //                             };
            //    db.ExecuteNonQuery(sql, parameters);
            // }
        }



        public void ShowData()
        {
            //查询从50条起的20条记录  
            string sql = "select * from Devices where androidId=@androidId";
            SQLiteParameter[] parameters = { new SQLiteParameter("@androidId", DbType.String) };
            using (SQLiteDataReader reader = db.ExecuteReader(sql, parameters))
            {
                Console.WriteLine("reader.Read");
                while (reader.Read())
                {
                    Console.WriteLine("ID:{0},androidId {1},uid {2},password {3},active {4},addDate {5}", reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetDateTime(5));
                }
            }
        }

        /////////////////

    }
}
