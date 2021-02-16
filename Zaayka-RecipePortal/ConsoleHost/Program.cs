using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFServices;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(AccountService), new Uri("http://localhost:8000/AccountService"));
            host.Open();
            Console.WriteLine("Service is hosted successfully.");
            Console.ReadLine();
            host.Close();
        }
    }
}
