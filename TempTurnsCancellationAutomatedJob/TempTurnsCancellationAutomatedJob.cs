using R2Core.CachHelper;
using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.PubSubMessaging;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.Turns;
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

namespace TempTurnsCancellationAutomatedJob
{
    public partial class TempTurnsCancellationAutomatedJob : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;

        public TempTurnsCancellationAutomatedJob()
        {
            InitializeComponent();
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
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
                        var InstanceConfiguration = new R2CoreInstanceConfigurationManager();
                        var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(1));
                        InstanceSoftwareUsers.AuthenticationUserByPinCode(InstanceSoftwareUsers.GetSystemUser());
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.TurnsCancellationSetting  , 9) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "TempTurnsCancellationAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "TempTurnsCancellationAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                    var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());
                    InstanceTurns.TempTurnsCancellation();
                }
                catch (Exception ex)
                { EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "TempTurnsCancellation : " + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("TempTurnsCancellationAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("TempTurnsCancellationAutomatedJob", "TempTurnsCancellationAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();
                EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "TempTurnsCancellationAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "TempTurnsCancellationAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TempTurnsCancellationAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
