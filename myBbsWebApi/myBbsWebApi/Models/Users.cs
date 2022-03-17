using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBbsWebApi.Models
{
    public class Users
    {
        //数据库字段要和模型相对应
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string UserLevel { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
    }
}
