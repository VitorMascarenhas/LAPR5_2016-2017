using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porto.Go.Monitor.Service
{
    public interface IActiveDirectoryManagement
    {
        void CreateUser(string samaccountname, string displayName, string password);

        void DeleteUser(string samaccountname);

        bool UserExists(string samaccountname);

        IList<string> GetUsersFromActiveDirectory();
    }
}
