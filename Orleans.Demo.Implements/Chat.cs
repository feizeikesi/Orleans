using System;
using Orleans.Demo.Interfaces;

namespace Orleans.Demo.Implements
{
    public class Chat:IChat
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}