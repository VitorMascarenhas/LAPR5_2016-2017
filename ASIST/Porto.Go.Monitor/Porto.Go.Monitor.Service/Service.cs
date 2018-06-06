using Porto.Go.Monitor.Service.Dto;
using Porto.Go.Monitor.Service.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace Porto.Go.Monitor.Service
{
    public partial class Service : ServiceBase
    {
        private int pollingInterval;
        private string sharePath;

        private string domainName;
        private string domainContainer;

        private Timer serviceTimer = null;

        private UserManagement uManagement;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!EventLog.SourceExists("Porto.Go.Monitor.Service"))
                EventLog.CreateEventSource("Porto.Go.Monitor.Service", "Application");

            try
            {
                pollingInterval = Convert.ToInt32(ConfigurationManager.AppSettings["PollingInterval"]);
                sharePath = ConfigurationManager.AppSettings["SharePath"];

                if (string.IsNullOrEmpty(sharePath))
                {
                    sharePath = @"C:\Share\";
                }

                if (!sharePath.EndsWith(@"\"))
                {
                    sharePath += @"\";
                }

                domainName = ConfigurationManager.AppSettings["DomainName"];
                domainContainer = ConfigurationManager.AppSettings["DomainContainer"];

                LogEvent(string.Format("SharePath: {0}", sharePath), EventLogEntryType.Information);
                LogEvent(string.Format("Domain Name: {0}", domainName), EventLogEntryType.Information);
                LogEvent(string.Format("Domain Container: {0}", domainContainer), EventLogEntryType.Information);

                IApiService apiService = new ApiService();

                try
                {
                    apiService.Login();
                }
                catch (HttpException ex)
                {
                    LogEvent(ex.Message, EventLogEntryType.Error);
                    this.Stop();
                }

                IActiveDirectoryManagement adManagement = new ActiveDirectoryManagement(domainName, domainContainer);

                uManagement = new UserManagement(sharePath, domainName, domainContainer, apiService, adManagement);
            }
            catch (Exception ex)
            {
                pollingInterval = 2;
                LogEvent(ex.Message, EventLogEntryType.Error);
            }

            serviceTimer = new Timer(pollingInterval * 60000);

            serviceTimer.AutoReset = true;
            serviceTimer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            serviceTimer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IEnumerable<UserDto> result = uManagement.GetUsers();

            //handle user and share creation
            foreach (var u in result)
            {
                try
                {
                    uManagement.CreateUserAndShare(u);
                }
                catch (Exception ex)
                {
                    LogEvent(ex.Message, EventLogEntryType.Error);
                }
            }

            //handles user and share deletion
            IEnumerable<string> usersToDelete = uManagement.UsersToDelete();

            foreach (string u in usersToDelete)
            {
                try
                {
                    uManagement.DeleteUserAndShare(u);
                }
                catch (Exception ex)
                {
                    LogEvent(ex.Message, EventLogEntryType.Error);
                }
            }
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
            serviceTimer.Dispose();
            serviceTimer = null;
        }

        private void LogEvent(string message, EventLogEntryType entryType)
        {
            var eventLog = new EventLog();
            eventLog = new EventLog();
            eventLog.Source = "Porto.Go.Monitor.Service";
            eventLog.Log = "Application";
            eventLog.WriteEntry(message, entryType);
        }
    }
}
