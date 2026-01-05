using PayanehClassLibrary.TurnsCancellation;
using R2Core.ConfigurationManagement;
using R2Core.DateTimeProvider;
using R2Core.GeneralConfiguration;
using R2Core.SMS.SMSSendRecive;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.GeneralConfiguration;
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

namespace TurnsCancellationAutomatedJob
{
    public partial class TurnsCancellationAutomatedJob : ServiceBase
    {
        private R2DateTimeService _DateTimeService;
        private R2CoreSoftwareUsersManager InstanceSoftwareUsers;
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _SystemUser;
        private PayanehClassLibraryTurnsCancellationManager InstanceTurnsCancellation;

        public TurnsCancellationAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _SystemUser = InstanceSoftwareUsers.GetSystemUser();
            InstanceTurnsCancellation = new PayanehClassLibraryTurnsCancellationManager(_DateTimeService);

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
                        _AutomatedJobsTimer.Interval = InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 10) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("TurnsCancellationAutomatedJob", "TurnsCancellationAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("TurnsCancellationAutomatedJob", "TurnsCancellationAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                { InstanceTurnsCancellation.TurnsCancellation(_SystemUser.UserId); }
                catch (Exception ex)
                { EventLog.WriteEntry("TurnsCancellationAutomatedJob", "TurnsCancellation:" + ex.Message.ToString(), EventLogEntryType.Error); }

            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsCancellationAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }


        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("TurnsCancellationAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("TurnsCancellationAutomatedJob", "TurnsCancellationAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("TurnsCancellationAutomatedJob", "TurnsCancellationAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsCancellationAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("TurnsCancellationAutomatedJob", "TurnsCancellationAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsCancellationAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
