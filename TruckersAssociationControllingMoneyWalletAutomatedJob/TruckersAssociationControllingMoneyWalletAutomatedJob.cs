using PayanehClassLibrary.ConfigurationManagement;
using PayanehClassLibrary.TruckersAssociationControllingMoneyWallet;
using R2Core.ConfigurationManagement;
using R2Core.DateTimeProvider;
using R2Core.SMS.SMSSendRecive;
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

namespace TruckersAssociationControllingMoneyWalletAutomatedJob
{
    public partial class TruckersAssociationControllingMoneyWalletAutomatedJob : ServiceBase
    {
        private R2DateTimeService _DateTimeService;
        private R2CoreSoftwareUsersManager InstanceSoftwareUsers;
        private System.Timers.Timer _AutomatedJobsTimer = new System.Timers.Timer();
        private bool _FailStatus = true;
        private R2CoreSoftwareUser _SystemUser;
        private TruckersAssociationControllingMoneyWalletManager InstanceTruckersAssociationControllingMoneyWallet;

        public TruckersAssociationControllingMoneyWalletAutomatedJob()
        {
            InitializeComponent();
            _DateTimeService = new R2DateTimeService();
            InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            _AutomatedJobsTimer.Elapsed += _AutomatedJobsTimer_Elapsed;
            _SystemUser = InstanceSoftwareUsers.GetSystemUser();
            InstanceTruckersAssociationControllingMoneyWallet = new TruckersAssociationControllingMoneyWalletManager(_DateTimeService);
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
                        _AutomatedJobsTimer.Interval = InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 8) * 1000;
                        _FailStatus = false;
                        EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "TruckersAssociationControllingMoneyWalletAutomatedJob.Interval=" + _AutomatedJobsTimer.Interval.ToString(), EventLogEntryType.SuccessAudit);
                    }
                    catch (Exception ex)
                    {
                        _FailStatus = true;
                        EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "TruckersAssociationControllingMoneyWalletAutomatedJob.Interval Setting Failed", EventLogEntryType.SuccessAudit);
                        System.Threading.Thread.Sleep(15000);
                    }
                }

                try
                { InstanceTruckersAssociationControllingMoneyWallet.ControllingMoneyWalletAccounting(_SystemUser.UserId ); }
                catch (Exception ex)
                { EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "ControllingMoneyWalletAccounting:" + ex.Message.ToString(), EventLogEntryType.Error); }

            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "_AutomatedJobsTimer_Elapsed:" + ex.Message.ToString(), EventLogEntryType.Error); }

            _AutomatedJobsTimer.Enabled = true;
            _AutomatedJobsTimer.Start();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (EventLog.SourceExists("TruckersAssociationControllingMoneyWalletAutomatedJob"))
                { }
                else
                { EventLog.CreateEventSource("TruckersAssociationControllingMoneyWalletAutomatedJob", "TruckersAssociationControllingMoneyWalletAutomatedJob"); }

                _AutomatedJobsTimer.Interval = 1000;
                _AutomatedJobsTimer.Enabled = true;
                _AutomatedJobsTimer.Start();

                EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "TruckersAssociationControllingMoneyWalletAutomatedJob Start ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "OnStart():" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        protected override void OnStop()
        {
            try
            {
                _AutomatedJobsTimer.Enabled = false;
                _AutomatedJobsTimer.Stop();
                _AutomatedJobsTimer = null;
                EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "TruckersAssociationControllingMoneyWalletAutomatedJob Stop ...", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckersAssociationControllingMoneyWalletAutomatedJob", "OnStop()." + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
