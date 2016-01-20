using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Orleans.Demo.Interfaces;
using Orleans.Runtime.Host;

namespace Orleans.Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("请输入配置文件名称：");
                var configName = Console.ReadLine();
                configName = string.IsNullOrEmpty(configName) ? "OrleansConfiguration" : configName;
                var configFile = new FileInfo(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    string.Format("{0}.xml", configName)));
                if (!configFile.Exists)
                {
                    Console.WriteLine("配置文件：{0} 不存在", configFile.Name);
                    continue;
                }
                Console.WriteLine("请输入节点名称");
                var nodeName = Console.ReadLine() ?? string.Empty;

                using (var host = new SiloHost(nodeName, configFile))
                {
                    host.LoadOrleansConfig();
                    host.InitializeOrleansSilo();
                    host.StartOrleansSilo();

                    Console.WriteLine("已启动，按下任意键退出");
                    Console.ReadLine();
                    host.StopOrleansSilo();
                }
            }
           
            //AppDomain hostDomain=AppDomain.CreateDomain("OrleansHost",null,new AppDomainSetup()
            //{
            //    AppDomainInitializer =InitSilo
            //});

            //DoSomeClientWork();
            //Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            //Console.ReadLine();

            //hostDomain.DoCallBack(ShutdownSilo);
        }

        private static void DoSomeClientWork()
        {
           var clientconfig=new Orleans.Runtime.Configuration.ClientConfiguration();
            clientconfig.Gateways.Add(new IPEndPoint(IPAddress.Loopback, 30000));
            GrainClient.Initialize(clientconfig);
            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Console.WriteLine(result);
        }

        private static SiloHost siloHost;
        private static void InitSilo(string[] args)
        {
            siloHost=new SiloHost(System.Net.Dns.GetHostName());
            siloHost.ConfigFileName = "OrleansConfiguration.xml";
            siloHost.InitializeOrleansSilo();
            var startedOk = siloHost.StartOrleansSilo();
            if (!startedOk)
            {
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
            }
        }

        static void ShutdownSilo()
        {
            if (siloHost != null)
            {
                siloHost.Dispose();
                GC.SuppressFinalize(siloHost);
                siloHost = null;
            }
        }
    }
}
