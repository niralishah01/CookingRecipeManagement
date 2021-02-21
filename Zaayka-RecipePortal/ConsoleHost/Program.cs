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
            Console.WriteLine("AccountService is hosted successfully.");
            ServiceHost host1 = new ServiceHost(typeof(CommentService), new Uri("http://localhost:8080/CommentService"));
            host1.Open();
            Console.WriteLine("CommentService is hosted successfully.");
            ServiceHost host2 = new ServiceHost(typeof(RecipeService), new Uri("http://localhost:8090/RecipeService"));
            host2.Open();
            Console.WriteLine("RecipeService is hosted successfully.");
            Console.ReadLine();
            host.Close();
            host1.Close();
            host2.Close();
        }
    }
}
