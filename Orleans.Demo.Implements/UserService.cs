using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Demo.Interfaces;

namespace Orleans.Demo.Implements
{
    public class UserService:Grain,IUserService
    {
        public Task<bool> Exist(string mobileNumber)
        {
            return Task.FromResult(mobileNumber == "13812345678");
        }

        public Task<bool> Exist()
        {

            Console.WriteLine("{0} 我处理了一个请求", this.RuntimeIdentity);
            return Task.FromResult(this.GetPrimaryKeyString() == "13812345678");
        }
    }
}
