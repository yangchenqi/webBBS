using myBbsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBbsWebApi.Bll.Interfaces
{
    public interface IuserBll
    {
        //接口里面不能包含方法体
         List<Users> GetAll();
         Users CheckLogin(string userNo, string password);
    }
}
