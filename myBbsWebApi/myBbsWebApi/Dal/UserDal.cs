using Microsoft.Data.SqlClient;
using myBbsWebApi.Core;
using myBbsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myBbsWebApi.Dal
{
    public class UserDal
    {
        public List<Users> GetAll()
        {
            DataTable res = SqlHelper.ExecuteTable("Select * from Users");
            List<Users> userList = ToModelList(res);
            return userList;
        }

        public List<Users> GetUserByUserNameAndPassword(string UserName, string Password)
        {
            DataTable res = SqlHelper.ExecuteTable("select * From Users where UserName=@UserName and Password =@Password",
                new SqlParameter("UserName", UserName),
                new SqlParameter("Password", Password)
                );
            //防止有多条数据，可能用户的名称和密码会有重复的，为了防止有重复的,for循环一下,然后把它放在user的集合里面
            //List<Users> userList = new List<Users>();
            //for(int i = 0; i < res.Rows.Count; i++)
            //{
            //    DataRow row = res.Rows[i];
            //    //userList.Add(new Users
            //    //{
            //    //    UserNo = row["UserNo"].ToString(),
            //    //    UserName = row["UserName"].ToString(),
            //    //    UserLevel = row["UserLevel"].ToString(),
            //    //    Password = row["Password"].ToString(),
            //    //});
            //  Users user = ToModel(row);
            //}
            List<Users> userList = ToModelList(res);
            return userList;
        }
        public Users GetUserById(string UserNo)
        {
            DataRow row = null;
            DataTable res = SqlHelper.ExecuteTable("select * From Users where UserNo =@UserNo",
                new SqlParameter("UserNo", UserNo));
            if (res.Rows.Count > 0)
            {
                row = res.Rows[0];
            }
            //Users user = new Users();
            //user.UserNo = row["UserNo"].ToString();
            //user.UserName = row["UserName"].ToString();
            //user.UserLevel = row["UserLevel"].ToString();
            //user.Password = row["Password"].ToString();
            //模型映射封装
            Users user = ToModel(row);
            return user;
        }

        public int AddUser(string userno, string username, string userlevel, string pwd)
        {
            return SqlHelper.ExecuteNonQuery(
                  "insert into Users(UserNo, UserName, UserLevel, Password) values (@UserNo, @UserName, @UserLevel, @Password)",
                  new SqlParameter("@UserNo", userno),
                  new SqlParameter("@UserName", username),
                  new SqlParameter("@UserLevel", userlevel),
                  new SqlParameter("@Password", pwd));
        }

        public int updateUser(string UserNo, string userName, string password, string? userLevel)
        {
            //从数据库中要先拿到原有的数据
            DataTable res = SqlHelper.ExecuteTable("select * from users where UserNo=@UserNo", new SqlParameter("@UserNo", UserNo));
            int rowCount = 0;//返回受影响的行数
            if (res.Rows.Count > 0)
            {
                DataRow row = res.Rows[0];
                Users user = new Users();
                user.UserNo = UserNo ?? row["UserNo"].ToString();
                user.UserName = userName ?? row["UserName"].ToString();
                user.UserLevel = userLevel ?? row["UserLevel"].ToString();
                user.Password = password ?? row["Password"].ToString();
                SqlHelper.ExecuteNonQuery(
                "update  Users set UserName=@UserName,UserLevel=@UserLevel, Password=@Password where UserNo=@UserNo",
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@UserLevel", user.UserLevel),
                new SqlParameter("@UserNo", user.UserNo)
                );
            }
            return rowCount;
        }

        public int removeUser(string UserNo)
        {
          return  SqlHelper.ExecuteNonQuery(
                "delete from Users where UserNo=@UserNo",
                new SqlParameter("@UserNo", UserNo)
                );
        }

        private Users ToModel(DataRow row)
        {
            Users user = new Users();
            user.UserNo = row["UserNo"].ToString();
            user.UserName = row["UserName"].ToString();
            user.UserLevel = row["UserLevel"].ToString();
            user.Password = row["Password"].ToString();
            //user.IsDelete = (bool)row["IsDelete"];
            user.IsDelete = row["IsDelete"].ToString();
            return user;
        }

        private List<Users> ToModelList(DataTable table)
        {
            List<Users> userList = new List<Users>();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                Users user = ToModel(row);
                userList.Add(user);
            }
            return userList;
        }
    }
}
