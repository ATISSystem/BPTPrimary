
using R2Core.CachHelper;
using R2Core.Caching;
using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PubSubMessaging;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.PubSubMessaging;
using R2CoreTransportationAndLoadNotification.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.Turns.TurnInfo;
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

namespace TruckDriversAutomatedJobs
{
    public partial class TruckDriversAutomatedJobs : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private ISubscriber _Subscriber = null;

        public TruckDriversAutomatedJobs()
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
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.TruckDriversAutomatedJobsService, 0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("TruckDriversAutomatedJobs", "TruckDriversAutomatedJobs.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("TruckDriversAutomatedJobs", "TruckDriversAutomatedJobs.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                }
                catch (Exception ex)
                { EventLog.WriteEntry("TruckDriversAutomatedJobs", "SetTruckDriversSeqTIdNativenessTypeId:" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        void InitializeTurnInfo(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue value)
        {
            try
            {
                var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnInfoManager();
                InstanceTurns.InitializeTurnInfo(value);
            }
            catch (AnyNotFoundException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
        }

        void SetTurnInfo(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue value)
        {
            try
            {
                var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnInfoManager();
                InstanceTurns.SetTurnInfo(value);
            }
            catch (DataEntryException ex)
            { throw ex; }
            catch (CacheNotFoundException ex)
            { throw ex; }
            catch (AnyNotFoundException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("TruckDriversAutomatedJobs"))
                { }
                else
                { EventLog.CreateEventSource("TruckDriversAutomatedJobs", "TruckDriversAutomatedJobs"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                _Subscriber = RedisConnectorHelper.Connection.GetSubscriber();
                _Subscriber.Subscribe(R2CorePubSubChannels.UserAuthenticated, new Action<StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue>(InitializeTurnInfo));
                _Subscriber.Subscribe(R2CoreTransportationAndLoadNotificationPubSubChannels.TurnInfo, new Action<StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue>(SetTurnInfo));
                EventLog.WriteEntry("TruckDriversAutomatedJobs", "TruckDriversAutomatedJobs Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }


        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("TruckDriversAutomatedJobs", "TruckDriversAutomatedJobs Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }


        }




    }
}
