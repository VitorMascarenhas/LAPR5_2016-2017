using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porto.Go.Monitor.Service
{
    public class ActiveDirectoryManagement : IActiveDirectoryManagement
    {
        private PrincipalContext principalContext;
        private readonly string domainName;
        private readonly string domainContainer;

        public ActiveDirectoryManagement(string domainName, string domainContainer)
        {
            if (string.IsNullOrEmpty(domainName))
            {
                throw new ArgumentNullException("domainName");
            }

            if (string.IsNullOrEmpty(domainContainer))
            {
                throw new ArgumentNullException("domainContainer");
            }

            this.domainName = domainName;
            this.domainContainer = domainContainer;
        }

        public void CreateUser(string samaccountname, string displayName, string password)
        {
            if (string.IsNullOrEmpty(samaccountname) || string.IsNullOrEmpty(password))
            {
                //os parametros são obrigatorios
                return;
            }

            principalContext = new PrincipalContext(ContextType.Domain, domainName, domainContainer);

            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, samaccountname);

            if (usr != null)
            {
                //O user já existe 
                return;
            }

            var userPrincipal = new UserPrincipal(principalContext);

            userPrincipal.SamAccountName = samaccountname;

            if (!string.IsNullOrEmpty(displayName))
            {
                userPrincipal.DisplayName = displayName;

                var names = displayName.Split(' ');

                if (names.Length > 1)
                {
                    userPrincipal.GivenName = names[0];

                    userPrincipal.Surname = displayName.Replace(names[0], "");
                }
                else
                {
                    userPrincipal.GivenName = names[0];

                    userPrincipal.Surname = names[0];
                }

                
            }

            userPrincipal.SetPassword(password);
            userPrincipal.Enabled = true;
            userPrincipal.PasswordNeverExpires = true;
            //userPrincipal.Manager = "db";

            userPrincipal.Save();
        }

        public void DeleteUser(string samaccountname)
        {
            if (string.IsNullOrEmpty(samaccountname))
            {
                //o parametro é obrigatorio
                return;
            }

            principalContext = new PrincipalContext(ContextType.Domain, domainName, domainContainer);

            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, samaccountname);

            if (usr != null)
            {
                usr.Delete();
            }
        }

        public bool UserExists(string samaccountname)
        {
            principalContext = new PrincipalContext(ContextType.Domain, domainName, domainContainer);

            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, samaccountname);

            if (usr != null)
            {
                return true;
            }

            return false;
        }

        public IList<string> GetUsersFromActiveDirectory()
        {
            var result = new List<string>();

            principalContext = new PrincipalContext(ContextType.Domain, domainName, domainContainer);

            var queryUser = new UserPrincipal(principalContext);

            var searcher = new PrincipalSearcher(queryUser);

            foreach (var found in searcher.FindAll())
            {
                result.Add(found.SamAccountName);
            }

            return result;
        }
    }
}
