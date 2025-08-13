using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayanehClassLibrary.LoadAllocations;
using R2Core.DateAndTimeManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation;
using R2CoreTransportationAndLoadNotification.LoadPermission;
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
            //var x = new R2CoreTransportationAndLoadNotificationLoadManipulationManager (new R2DateTimeService() );
            var z = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));
            //R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.R2CoreTransportationAndLoadNotificationLoad load = 
            //    new R2CoreTransportationAndLoadNotificationLoad {LoadId=21,AnnounceDate="",AnnounceTime="",TransportCompanyId=21306,TransportCompanyTitle="",
            //        GoodId= 2520001,GoodTitle="",AnnouncementGroupId= 2,AnnouncementGroupTitle="",AnnouncementSubGroupId= 7,AnnouncementSubGroupTitle="",SourceCityId= 21310000,
            //        SourceCityTitle="",TargetCityId=11320000,TargetCityTitle="",LoadingPlaceId= 1001,LoadingPlaceTitle="",DischargingPlaceId= 1001,DischargingPlaceTitle="",
            //        TotalNumber=50,Tonaj=25,Tariff=0,Recipient="",Address="",Description="AADAA",LoadStatusId=2,LoadStatusTitle="",RegisteringUserName="",TPTParams= "137:1;141:1;145:1;149:0;153:0;159:0;32:0;165:0;171:0;177:0;183:0;189:0;195:0;201:0;207:0;213:0;219:0;225:0;231:0;237:0",
            //        TPTParamsJoint="" }; 
            //x.LoadSedimenting(22, z.GetUser(7046, true));

            //var x = new R2CoreTransportationAndLoadNotificationLoadPermissionManager(new R2DateTimeService());
            //x.LoadPersmissionCancelling(30027, "دلم می خواهد",true ,true ,z.GetUser(7046, true));

            //var InstanceLoadPermission = new R2CoreTransportationAndLoadNotificationLoadPermissionManager(new R2DateTimeService());
            //InstanceLoadPermission.GetLoadPermissions(22);
            var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
            var Turn = InstanceLoadAllocation.LoadAllocateToOther(30027, R2CoreTransportationAndLoadNotificationRequesters.LoadAllocationController_LoadAllocateToOther, z.GetUser(7046, true));

        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var x = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                var y = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));
                var z=new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager (new R2DateTimeService(),new SoftwareUserService (21));

                x.LoadAllocationCancelling(10024,4,0,z.GetUser(7003,true ));
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "SetTruckDriversSeqTIdNativenessTypeId:" + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
