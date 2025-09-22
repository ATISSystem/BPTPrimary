using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.SoftwareUserManagement;
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

namespace CarouselsPreparingAutomatedJob
{
    public partial class CarouselsPreparingAutomatedJob : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _User;
        private R2DateTimeService _DateTimeService;

        public CarouselsPreparingAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService,new SoftwareUserService(1));
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
                        var InstanceConfiguration = new R2CoreConfigurationsManager(_DateTimeService);
                        var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(1));

                        InstanceSoftwareUsers.AuthenticationUserByPinCode(_User);
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.Carousels , 0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "CarouselsPreparingAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "CarouselsPreparingAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                    var InstanceCarousels = new R2Core.Carousels.R2CoreCarouselsManager(_DateTimeService );
                    var Instance = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(_DateTimeService ,new SoftwareUserService(1));
                    InstanceCarousels.CarouselsCachePreparing(_User );
                }
                catch (Exception ex)
                { EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "CarouselsCachePreparing:" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }


        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("CarouselsPreparingAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("CarouselsPreparingAutomatedJob", "CarouselsPreparingAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "CarouselsPreparingAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }


        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "CarouselsPreparingAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("CarouselsPreparingAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }


        }
    }
}
