using PayanehClassLibrary.Turns;
using PayanehClassLibrary.TurnsCancellation;
using R2Core.ConfigurationManagement;
using R2Core.DateTimeProvider;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
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

namespace TurnsRegisteringAutomatedJob
{
    public partial class TurnsRegisteringAutomatedJob : ServiceBase
    {
        private R2DateTimeService _DateTimeService;
        private R2CoreSoftwareUsersManager InstanceSoftwareUsers;
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _SystemUser;
        private PayanehClassLibraryTurnManager InstanceTurn;

        public TurnsRegisteringAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _SystemUser = InstanceSoftwareUsers.GetSystemUser();
            InstanceTurn = new PayanehClassLibraryTurnManager(_DateTimeService);
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
                        var InstanceConfiguration = new R2CoreConfigurationsManager(_DateTimeService);
                        InstanceSoftwareUsers.AuthenticationUserByPinCode(_SystemUser);
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnRegisteringSetting,0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "TurnsRegisteringAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "TurnsRegisteringAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                { InstanceTurn.AutomaticTurnRegistering (); }
                catch (Exception ex)
                { EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "AutomaticTurnRegistering:" + ex.Message.ToString(), EventLogEntryType.Error); }

            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("TurnsRegisteringAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("TurnsRegisteringAutomatedJob", "TurnsRegisteringAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "TurnsRegisteringAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "TurnsRegisteringAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TurnsRegisteringAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
