
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
using R2Core.SMS.SMSSendRecive;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationOfLoadAnnouncement;
using R2CoreTransportationAndLoadNotification.GeneralConfiguration;
using R2CoreTransportationAndLoadNotification.LoadSedimentation;

namespace LoadSedimentingAutomatedJob
{
    public partial class LoadSedimentingAutomatedJob : ServiceBase
    {
        private R2DateTimeService _DateTimeService;
        private R2CoreSoftwareUsersManager InstanceSoftwareUsers;
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _SystemUser;
        private R2CoreTransportationAndLoadNotificationLoadSedimentationManager InstanceLoadSedimentation;

        public LoadSedimentingAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _SystemUser = InstanceSoftwareUsers.GetSystemUser();
            InstanceLoadSedimentation = new R2CoreTransportationAndLoadNotificationLoadSedimentationManager(_DateTimeService);
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
                        _AutomatedJobsTimer.Interval = InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.LoadSedimentationAutomatedJob, 0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("LoadSedimentingAutomatedJob", "LoadSedimentingAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("LoadSedimentingAutomatedJob", "LoadSedimentingAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                { InstanceLoadSedimentation.SedimentingProcess(_SystemUser.UserId); }
                catch (Exception ex)
                { EventLog.WriteEntry("LoadSedimentingAutomatedJob", "SedimentingProcess:" + ex.Message.ToString(), EventLogEntryType.Error); }

            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadSedimentingAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }


        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("LoadSedimentingAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("LoadSedimentingAutomatedJob", "LoadSedimentingAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("LoadSedimentingAutomatedJob", "LoadSedimentingAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadSedimentingAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("LoadSedimentingAutomatedJob", "LoadSedimentingAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadSedimentingAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
