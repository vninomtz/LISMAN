using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host {
    class Program {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(LismanService.LismanService))){
                LismanService.LismanService service = new LismanService.LismanService();                
                host.Open();
                Console.WriteLine("Server is running:  " + DateTime.Now);
                Console.ReadKey();
            }
        }
    }
}
