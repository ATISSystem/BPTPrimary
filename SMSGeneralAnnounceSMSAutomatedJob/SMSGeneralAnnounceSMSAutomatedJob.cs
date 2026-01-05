using R2Core.CachHelper;
using R2Core.DateTimeProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using R2Core.GeneralConfiguration;
using R2Core.SoftwareUserManagement;
using System.Timers;
using R2Core.PubSubMessaging;
using Newtonsoft.Json;
using R2Core.SMS.GeneralAnnounceSMS;


namespace SMSGeneralAnnounceSMSAutomatedJob
{
    public partial class SMSGeneralAnnounceSMSAutomatedJob : ServiceBase
    {
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private ISubscriber _Subscriber = null;
        private R2DateTimeService _DateTimeService;
        private RedisConnectorHelper _RCH;

        public SMSGeneralAnnounceSMSAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _RCH = new RedisConnectorHelper();
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
                        var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(1));
                        InstanceSoftwareUsers.AuthenticationUserByPinCode(InstanceSoftwareUsers.GetSystemUser());
                        _AutomatedJobsTimer.Interval = InstanceGeneralConfiguration.GetInt64Configuration(R2CoreGeneralConfigurations.SmsSystemSetting, 25) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "SMSGeneralAnnounceSMSAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "SMSGeneralAnnounceSMSAutomatedJob.Interval Setting Failed:" + ex.Message, EventLogEntryType.Error);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                {
                }
                catch (Exception ex)
                { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "xxx:" + ex.Message.ToString(), EventLogEntryType.Error); }
            }
            catch (Exception ex)
            { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        void SMSSending(StackExchange.Redis.RedisChannel channel, StackExchange.Redis.RedisValue value)
        {
            try
            {
                var PubSubMessage = JsonConvert.DeserializeObject<GeneralAnnounceSMSPubSubMessage>(value);
                var InstanceGeneralAnnounceSMS = new R2CoreGeneralAnnounceSMSManager(_DateTimeService);
                var SMSResult = InstanceGeneralAnnounceSMS.SendGeneralAnnounceSMS(PubSubMessage.SoftwareUserTypeId, PubSubMessage.Message, PubSubMessage.GAMId);
                EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "GAMId:" + PubSubMessage.GAMId+" SMSResult:"+ SMSResult, EventLogEntryType.SuccessAudit);
            }
            catch (RedisException ex)
            { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", ex.Message, EventLogEntryType.Error); }
            catch (Exception ex)
            { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", ex.Message, EventLogEntryType.Error); }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("SMSGeneralAnnounceSMSAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("SMSGeneralAnnounceSMSAutomatedJob", "SMSGeneralAnnounceSMSAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                _Subscriber = _RCH.Connection.GetSubscriber();
                _Subscriber.Subscribe(R2CorePubSubChannels.GeneralAnnounceSMSRequest, new Action<StackExchange.Redis.RedisChannel, StackExchange.Redis.RedisValue>(SMSSending));
                EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "SMSGeneralAnnounceSMSAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "SMSGeneralAnnounceSMSAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("SMSGeneralAnnounceSMSAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
