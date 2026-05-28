
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using R2Core.DateTimeProvider;
using R2Core.GeneralConfiguration;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.GeneralConfiguration;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.LoadPermission;


namespace LoadPermissionsIssueingAutomatedJobs
{
    public partial class LoadPermissionsIssueingAutomatedJobs : ServiceBase
    {
        private R2DateTimeService _DateTimeService;
        private R2CoreSoftwareUsersManager InstanceSoftwareUsers;
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _SystemUser;

        public LoadPermissionsIssueingAutomatedJobs()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _SystemUser = InstanceSoftwareUsers.GetSystemUser();
        }

        private void _AutomatedJobsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();

                while (_FailStatus)
                {
                    try
                    {
                        var InstanceGeneralConfiguration = new R2CoreGeneralConfigurationManager(_DateTimeService);
                        InstanceSoftwareUsers.AuthenticationUserByPinCode(_SystemUser);
                        _AutomatedJobsTimer.Interval = InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.LoadPermissionsIssueingAutomatedJobs,0 ) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "LoadPermissionsIssueingAutomatedJobs.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "LoadPermissionsIssueingAutomatedJobs.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                    var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService );
                    InstanceLoadAllocation.LoadAllocationsLoadPermissionRegistering(_SystemUser);
                }
                catch (Exception ex)
                { EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "LoadAllocationsLoadPermissionRegistering:" + ex.Message.ToString(), EventLogEntryType.Error); }

            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }


        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("LoadPermissionsIssueingAutomatedJobs"))
                { }
                else
                { EventLog.CreateEventSource("LoadPermissionsIssueingAutomatedJobs", "LoadPermissionsIssueingAutomatedJobs"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "LoadPermissionsIssueingAutomatedJobs Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "LoadPermissionsIssueingAutomatedJobs Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadPermissionsIssueingAutomatedJobs", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
