using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Demo.Interfaces
{
    public interface IUserService:IGrainWithStringKey
    {
        Task<bool> Exist(string mobileNumber);
        Task<bool> Exist();
    }
}
