using System;
using System.ServiceModel;

namespace Host {
    public static class Program {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(LismanService.LismanService))){             
                host.Open();
                Console.WriteLine("Server is running:  " + DateTime.Now);
                Console.ReadKey();
            }
        }

    }
}
