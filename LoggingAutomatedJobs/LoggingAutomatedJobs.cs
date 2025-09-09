using Newtonsoft.Json;
using R2Core.CachHelper;
using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.LoggingManagement;
using R2Core.PubSubMessaging;
using R2Core.SoftwareUserManagement;
using StackExchange.Redis;
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


namespace LoggingAutomatedJobs
{
    public partial class LoggingAutomatedJobs : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private ISubscriber _Subscriber = null;


        public LoggingAutomatedJobs()
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
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.LoggingAutomatedJobsSetting, 0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("LoggingAutomatedJobs", "LoggingAutomatedJobs.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("LoggingAutomatedJobs", "LoggingAutomatedJobs.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                }
                catch (Exception ex)
                { EventLog.WriteEntry("LoggingAutomatedJobs", "xxx" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoggingAutomatedJobs", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        void Logger(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue value)
        {
            try
            { R2CoreLoggingManager.Logger(value); }
            catch (Exception ex)
            { throw ex; }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("LoggingAutomatedJobs"))
                { }
                else
                { EventLog.CreateEventSource("LoggingAutomatedJobs", "LoggingAutomatedJobs"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                _Subscriber = RedisConnectorHelper.Connection.GetSubscriber();
                _Subscriber.Subscribe(R2CorePubSubChannels.Logging, new Action<StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue>(Logger));

                EventLog.WriteEntry("LoggingAutomatedJobs", "LoggingAutomatedJobs Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoggingAutomatedJobs", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("LoggingAutomatedJobs", "LoggingAutomatedJobs Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoggingAutomatedJobs", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
