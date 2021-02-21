using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host1 = new ServiceHost(typeof(WCFServices.RecipeService));
            host1.Open();
            Console.WriteLine("Published Recipe Service");
            ServiceHost host2 = new ServiceHost(typeof(WCFServices.AccountService));
            host2.Open();
            Console.WriteLine("Published Account Service");
            ServiceHost host3 = new ServiceHost(typeof(WCFServices.CommentService));
            host3.Open();
            Console.WriteLine("Published Comment Service");
            Console.ReadLine();
            host1.Close();
            host2.Close();
            host3.Close();
        }
    }
}
