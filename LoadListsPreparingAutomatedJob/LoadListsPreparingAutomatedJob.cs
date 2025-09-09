using R2Core.CachHelper;
using R2Core.Caching;
using R2Core.ConfigurationManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PubSubMessaging;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.PubSubMessaging;
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

namespace LoadListsPreparingAutomatedJob
{
    public partial class LoadListsPreparingAutomatedJob : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private ISubscriber _Subscriber = null;

        public LoadListsPreparingAutomatedJob()
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
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.LoadListsPreparingAutomatedJob, 0) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "LoadListsPreparingAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "LoadListsPreparingAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                    var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService() );
                    InstanceLoad.UpdateLoadLists();
                }
                catch (Exception ex)
                { EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "xxx" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        void PreparingLoadsList(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue value)
        {
            try
            {
                var InstanceLoads = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService ());
                InstanceLoads.CacheLoads(value);
                EventLog.WriteEntry("LoadListsPreparingAutomatedJob", " InstanceLoads.CacheLoads(value)" , EventLogEntryType.Error);
            
            }
            catch (RedisException ex)
            { throw ex; }
            catch (Exception ex)
            {/* throw ex;*/
                EventLog.WriteEntry("LoadListsPreparingAutomatedJob", ex.Message , EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("LoadListsPreparingAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("LoadListsPreparingAutomatedJob", "LoadListsPreparingAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                _Subscriber = RedisConnectorHelper.Connection.GetSubscriber();
                _Subscriber.Subscribe(R2CoreTransportationAndLoadNotificationPubSubChannels.LoadsForTruckDrivers, new Action<StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue>(PreparingLoadsList));
                EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "LoadListsPreparingAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "LoadListsPreparingAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("LoadListsPreparingAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
