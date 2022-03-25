using myBbsWebApi.Bll.Interfaces;
using myBbsWebApi.Dal;
using myBbsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBbsWebApi.Bll
{
    //规范开发，多态，层级引用
    //让userBll抽象出来，继承
    public class UserBll : IuserBll
    {
        UserDal userDal = new UserDal();
        public List<Users> GetAll()
        {
            return userDal.GetAll().FindAll(x => x.IsDelete != "1");
        }
        //业务逻辑层判断用户登录
        public Users CheckLogin(string userNo, string password)
        {
            List<Users> userList = userDal.GetUserByUserNameAndPassword(userNo, password);
            if (userList.Count <= 0)
                return default;
            else
            {
                Users user = userList.Find(m => m.IsDelete != "1");
                return user;
            }
        }
    }
}
