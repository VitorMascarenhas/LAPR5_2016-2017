using Porto.Go.Monitor.Service.Dto;
using Porto.Go.Monitor.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Porto.Go.Monitor.Service
{
    public class UserManagement
    {
        private readonly IApiService apiService;
        private readonly string path;
        private readonly string domainName;

        private readonly IActiveDirectoryManagement adManagement;

        public UserManagement(string path, 
                              string domainName, 
                              string domainContainer, 
                              IApiService apiService,
                              IActiveDirectoryManagement activeDirectoryManagement)
        {
            this.path = path;
            this.domainName = domainName;

            //adManagement = new ActiveDirectoryManagement(domainName, domainContainer);
            this.adManagement = activeDirectoryManagement;
            this.apiService = apiService;
        }

        public void CreateUserAndShare(UserDto u)
        {
            //string userFolder = string.Empty;

            string defaultPassword = "Password01!";

            if (!adManagement.UserExists(u.UserName))
            {
                adManagement.CreateUser(u.UserName, u.DisplayName, defaultPassword);
            }


            if (!Directory.Exists(string.Format("{0}{1}", this.path, u.UserName))) //se a pasta n existe trata da criação e partilha
            {

                DirectoryInfo dInfo = Directory.CreateDirectory(string.Format("{0}{1}", this.path, u.UserName)); //assume-se que o caminho já termina com \

                CreateShare(dInfo.FullName.TrimEnd('\\'), string.Format("Share {0}", u.DisplayName), "", domainName, u.UserName);
            }

        }

        public void DeleteUserAndShare(string samaccountname)
        {
            string userFolder = string.Format("{0}{1}", path, samaccountname);

            if (Directory.Exists(userFolder))
            {
                var di = new DirectoryInfo(userFolder);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                //delete share
                DeleteShare(di.FullName);

                Directory.Delete(userFolder);
            }

            if (adManagement.UserExists(samaccountname))
            {
                adManagement.DeleteUser(samaccountname);
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            try
            {
                return this.apiService.GetUsers();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<string> UsersToDelete()
        {
            var result = new List<string>();

            List<string> usersFromDb = GetUsers().Select(x => x.UserName).ToList();

            result = adManagement.GetUsersFromActiveDirectory().Except(usersFromDb).ToList(); //users de AD qforam removidos da bd

            return result;
        }

        private static ManagementBaseObject SecurityDescriptor(string domain, string samaccountname)
        {
            var account = new NTAccount(domain, samaccountname);
            var sid = (SecurityIdentifier) account.Translate(typeof(SecurityIdentifier));

            var sidArray = new byte[sid.BinaryLength];
            sid.GetBinaryForm(sidArray, 0);

            ManagementObject trustee = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            trustee["Domain"] = domain;
            trustee["Name"] = samaccountname;
            trustee["SID"] = sidArray;

            ManagementObject adminAce = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            adminAce["AccessMask"] = 2032127;
            adminAce["AceFlags"] = 3;
            adminAce["AceType"] = 0;
            adminAce["Trustee"] = trustee;

            var everyone = new NTAccount("NT Authority", "Everyone");
            var sidEveryone = (SecurityIdentifier) everyone.Translate(typeof(SecurityIdentifier));
            var sidArrayEveryone = new byte[sidEveryone.BinaryLength];
            sidEveryone.GetBinaryForm(sidArrayEveryone, 0);

            ManagementObject trusteeEveryone = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            trusteeEveryone["Domain"] = "NT Authority";
            trusteeEveryone["Name"] = "Everyone";
            trusteeEveryone["SID"] = sidArrayEveryone;

            ManagementObject everyoneAce = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            everyoneAce["AccessMask"] = 1179817;
            everyoneAce["AceFlags"] = 3;
            everyoneAce["AceType"] = 0;
            everyoneAce["Trustee"] = trusteeEveryone;

            ManagementObject securityDescriptor = new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
            securityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT
            securityDescriptor["DACL"] = new object[] { adminAce, everyoneAce };
            return securityDescriptor;
        }

        public static void CreateShare(string folderPath, string shareName, string description, string domain, string samaccountname)
        {
            //create a ManagementClass object
            var managementClass = new ManagementClass("Win32_Share");

            //create ManagementBaseObjects for in and out parameters
            ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
            ManagementBaseObject outParams;

            // Set the input parameters
            inParams["Description"] = description;
            inParams["Name"] = shareName;
            inParams["Path"] = folderPath;
            inParams["Type"] = 0x0; // Disk Drive
            inParams["Access"] = SecurityDescriptor(domain, samaccountname);

            //invoke the method
            outParams = managementClass.InvokeMethod("Create", inParams, null);

            //check to see if the method invocation was successful
            var rVal = (uint) (outParams.Properties["ReturnValue"].Value);
            if (rVal != 0 && rVal != 22) //ok if it already exists
            {
                throw new Exception(string.Format("Unable to share directory. \nReturn Code: {0} \nDescription: {1}", rVal, GetCreateShareReturnCodeDescription(rVal)));
            }
        }

        public static void DeleteShare(string folder)
        {
            var searcher = new ManagementObjectSearcher("select * from win32_share");
            ManagementBaseObject outParams;
            var mc = new ManagementClass("Win32_Share");

            foreach (ManagementObject share in searcher.Get())
            {
                string type = share["Type"].ToString();
                if (type == "0")
                {
                    string path = share["Path"].ToString();

                    if (path == folder)
                    {
                        outParams = share.InvokeMethod("delete", null, null);

                        //check to see if the method invocation was successful
                        var rVal = (uint) (outParams.Properties["ReturnValue"].Value);
                        if (rVal != 0 && rVal != 22) //ok if it already exists
                        {
                            throw new Exception(string.Format("Unable to unshare directory. \nReturn Code: {0} \nDescription: {1}", rVal, GetCreateShareReturnCodeDescription(rVal)));
                        }
                    }
                }
            }
        }

        private static string GetCreateShareReturnCodeDescription(uint returnValue)
        {
            switch (returnValue)
            {
                case 0:
                    return "Success";
                case 2:
                    return "Access Denied";
                case 8:
                    return "Unknown Failure";
                case 9:
                    return "Invalid Name";
                case 10:
                    return "Invalid Level";
                case 21:
                    return "Invalid Parameter";
                case 22:
                    return "Duplicate Share";
                case 23:
                    return "Redirect Path";
                case 24:
                    return "Unknown Device or Directory";
                case 25:
                    return "Net Name Not Found";
            }
            return "Unknown";
        }
    }
}
