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
            Type t = typeof(RecipeService);
            Uri http = new Uri("http://localhost:8000/RecipeService");

            ServiceHost sh = new ServiceHost(t, http);
            sh.Open();
            Console.WriteLine("Publishes");
            Console.ReadLine();
            sh.Close();
        }
    }
}
//GetAllRecipes
