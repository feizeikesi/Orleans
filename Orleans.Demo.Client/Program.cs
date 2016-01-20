using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Demo.Interfaces;
using Orleans.Demo.Interfaces.EmployeesAndManagers;

namespace Orleans.Demo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GrainClient.Initialize();

            //for (int i = 0; i < 100000; i++)
            //{
            //     var mobileNumber = Guid.NewGuid().ToString();
            //    var userService = GrainClient.GrainFactory.GetGrain<IUserService>(i.ToString());

            //    Console.WriteLine("用户{0},{1}", i, (userService.Exist().Result ? "已经存在" : "不存在"));
            //}

            var grainFactory = GrainClient.GrainFactory;
            var e0 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e1 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e2 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e3 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());
            var e4 = grainFactory.GetGrain<IEmployee>(Guid.NewGuid());

            var m0 = grainFactory.GetGrain<IManager>(Guid.NewGuid());
            var m1 = grainFactory.GetGrain<IManager>(Guid.NewGuid());
            var m0e = m0.AsEmployee().Result;
            var m1e = m1.AsEmployee().Result;

            m0e.Promote(10);
            m1e.Promote(11);

            m0.AddDirectReport(e0).Wait();
            m0.AddDirectReport(e1).Wait();
            m0.AddDirectReport(e2).Wait();

            m1.AddDirectReport(m0e).Wait();
            m1.AddDirectReport(e3).Wait();

            m1.AddDirectReport(e4).Wait();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();
        }
    }
}
