using System;
using NetMQ;

namespace PriceTickerService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var p = new Publisher();
                p.Start();
                Console.ReadLine();
            }
            finally
            {
                NetMQConfig.Cleanup();
            }
        }
    }
}
