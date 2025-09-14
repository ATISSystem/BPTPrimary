using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadOtherThanManipulation;
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

namespace ChangingStatusOfTommorowLoadsAutomatedJob
{
    public partial class ChangingStatusOfTommorowLoadsAutomatedJob : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _User;

        public ChangingStatusOfTommorowLoadsAutomatedJob()
        {
            InitializeComponent();
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(1));
            _User = InstanceSoftwareUsers.GetSystemUser();
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
                        var InstanceConfiguration = new R2CoreConfigurationsManager();
                        var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(1));

                        InstanceSoftwareUsers.AuthenticationUserByPinCode(_User);
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, 3) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                    var InstanceChangingStatusOfTommorowLoads = new R2CoreTransportationAndLoadNotificationChangingStatusOfTommorowLoadsManager(new R2DateTimeService());
                    InstanceChangingStatusOfTommorowLoads.ChangingStatusOfTommorowLoads(_User.UserId);
                }
                catch (Exception ex)
                { EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob:" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }


        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("ChangingStatusOfTommorowLoadsAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }


        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "ChangingStatusOfTommorowLoadsAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("ChangingStatusOfTommorowLoadsAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }


        }
    }
}
