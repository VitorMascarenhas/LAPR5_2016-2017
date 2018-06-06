using Porto.Go.Monitor.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porto.Go.Monitor.TestService
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new ApiService();

            svc.GetUsers();
        }
    }
}
