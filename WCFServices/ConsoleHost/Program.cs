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
            Type r_t= typeof(RecipeService);
            Uri r_http = new Uri("http://localhost:8080/RecipeService");
            Type a_t = typeof(AccountService);
            Uri a_http = new Uri("http://localhost:8000/AccountService");
            Type c_t = typeof(CommentService);
            Uri c_http = new Uri("http://localhost:8090/CommentService");

            ServiceHost r_sh = new ServiceHost(r_t, r_http);
            r_sh.Open();
            Console.WriteLine("Publishes RecipeService");
            ServiceHost a_sh = new ServiceHost(a_t, a_http);
            a_sh.Open();
            Console.WriteLine("Publishes AccountService");
            ServiceHost c_sh = new ServiceHost(c_t, c_http);
            c_sh.Open();
            Console.WriteLine("Publishes CommentService");
            Console.ReadLine();
            r_sh.Close();
            a_sh.Close();
            c_sh.Close();
        }
    }
}
//GetAllRecipes
