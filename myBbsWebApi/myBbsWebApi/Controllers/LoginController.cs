using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using myBbsWebApi.Core;
using myBbsWebApi.Dal;
using myBbsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//引入程序集

namespace myBbsWebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public string Get(string UserName, string pwd)
        {
            UserDal userDal = new UserDal();
            bool hasUser= userDal.GetUserByUserNameAndPassword(UserName, pwd);
             if (hasUser)
            {
                return "登录成功";
            }
            else
            {
                return "登录失败";
            }
        }

        [HttpPost]
        public string Insert(string userno,string username,string userlevel, string pwd)
        {
            UserDal userDal = new UserDal();
            int rows = userDal.AddUser(userno,username,userlevel,pwd);
            if (rows > 0)
            {
                return "数据添加成功";
            }
            else
            {
                return "数据添加失败";
            }
        }

        [HttpDelete]
        public string Remove(string UserNo)
        {
            UserDal userDal = new UserDal();
            int rows= userDal.removeUser(UserNo);
            if (rows > 0)
            {
                return "数据删除成功";
            }
            else
            {
                return "数据删除失败";
            }
        }

        [HttpPut]
        public string Update(string UserNo,string userName,string password,string? userLevel)
        {
            UserDal userDal = new UserDal();
            int rows= userDal.updateUser(UserNo, userName, password, userLevel);
            if (rows>0)
            {
                return "数据修改成功";
            }
            else
            {
                return "数据修改失败";
            }
        }
    }
}
