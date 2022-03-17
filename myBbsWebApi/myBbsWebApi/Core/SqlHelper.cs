using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace myBbsWebApi.Core
{
    public class SqlHelper
    {
        //封装数据
        //先准备数据库的连接字符串，创建一个属性来存储我们的连接字符串
        //静态方法中必须引用静态对象
        public static string ConnectionString { get; set; } = "server=.;database=MyBBSDb;Integrated Security = true; Encrypt=True;TrustServerCertificate=True";
        //做一个获取数据表的封装,加上params这个参数可以不传
        public static DataTable ExecuteTable(string cmdText,params SqlParameter[] sqlParameters)
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            //打开数据库
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddRange(sqlParameters);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //准备卡车
            DataSet ds = new DataSet();
            //推车填满卡车
            sda.Fill(ds);
            return ds.Tables[0];
        }
        //对数据进行增删改的操作
    public static int ExecuteNonQuery(string cmdText, params SqlParameter[] sqlParameters)
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddRange(sqlParameters);
            return cmd.ExecuteNonQuery();
        }

    }
}
