using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2Core.DateAndTimeManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.SoftwareUserManagement;
using System;
using System.Diagnostics;
using System.Web.Http.ModelBinding.Binders;

namespace LoadCapacitor
{
    [TestClass]
    public class UnitTest8
    {
        [TestMethod]
        public void TestMethod1()
        {
            // var x = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService() ,null);
            //var User= x.GetUser(new R2Core.SoftwareUserManagement.R2CoreSoftwareUserMobile("09133155865"), true);
            // var InstanceLoadCapacitorLoad = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService());
            // var Loads = InstanceLoadCapacitorLoad.GetLoadsforTruckDriver(R2CoreTransportationAndLoadNotificationRequesters.LoadCapacitorController_GetLoadsforTruckDriver, User, 7, 1);
            //try
            //{
            //    var InstanceSoftwareUsers = new R2CoreTransportationAndLoadNotificationSoftwareUsersManager();
            //    InstanceSoftwareUsers.SetTruckDriversSeqTIdNativenessTypeId();
            //}
            //catch (Exception ex)
            //{ EventLog.WriteEntry("TruckDriversAutomatedJobs", "SetTruckDriversSeqTIdNativenessTypeId:" + ex.Message.ToString(), EventLogEntryType.Error); }

        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var x = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                var y = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));
                var z=new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager (new R2DateTimeService(),new SoftwareUserService (21));

                x.LoadAllocationCancelling(10024,4,z.GetUser(7003,true ));
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "SetTruckDriversSeqTIdNativenessTypeId:" + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
