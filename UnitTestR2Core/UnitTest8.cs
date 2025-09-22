using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PayanehClassLibrary.LoadAllocations;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.LoggingManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadOtherThanManipulation;
using R2CoreTransportationAndLoadNotification.LoadPermission;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.Turns;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web.Http.ModelBinding.Binders;
using R2Core.ConfigurationManagement;
using R2Core.SMS.SMSHandling;
using R2Core.DatabaseManagement;
using R2Core.PredefinedMessagesManagement;
using System.Web.Http.Results;
using System.Threading.Tasks;
using R2CoreParkingSystem.SoftwareUsersManagement;
using R2Core.PubSubMessaging;
using R2Core.SMS.SMSSendRecive;
using R2Core.SMS.SMSOwners;

namespace LoadCapacitor
{
    [TestClass]
    public class UnitTest8
    {
        [TestMethod]
        public void TestMethod1()
        {
            var InstanceSMSOwners = new R2CoreSMSOwnersManager(new R2DateTimeService(),null  );
            InstanceSMSOwners.SendSMSOwnersPleaseCharge();

            R2DateTimeService _DateTimeService = new R2DateTimeService();
            var ddTE = _DateTimeService.GetTickofTime(DateTime.Now )  ;
            var ddd = ddTE;

            var sms2 = new R2CoreSMSRecivingManager(new R2DateTimeService());
            sms2.Reciving();
            //var sms1 = new R2CoreSMSSendingManager(new R2DateTimeService());
            //sms1.Sending();

            var xxx = new R2CoreRawLog { LogTypeId = 0, Description = "22", MessageDetail1 = "AnnouncementGroupId", MessageDetail2 = "", MessageDetail3 = "", UserId = 21 };
            R2CoreLoggingManager.RegisterLog(R2CorePubSubChannels.Logging ,new Exception("654321"));

            var InstanceSoftwareUsers = new R2CoreParkingSystemSoftwareUsersManager(new R2DateTimeService() , new SoftwareUserService(21));
            var Instance = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService (),null );
            var SoftwareUserComposedInf = InstanceSoftwareUsers.GetSoftwareUserComposedInf(Instance.GetUser(21,false ));
             SoftwareUserComposedInf = InstanceSoftwareUsers.GetSoftwareUserComposedInf(Instance.GetUser(21, false));

            //var InstanceAPICaller = new R2CoreDateTimeProviderAPICaller();
            //Task<DateTime> TaskResult   = InstanceAPICaller.GetCurrentDateTimeMilladi();
            //var xcx = TaskResult.Result;

            
            var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(new R2DateTimeService ());
            var ErrorMessageCode = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.None).MsgId;

            R2CoreSqlDataBOXManager InstanceSqlDataBOX = new R2CoreSqlDataBOXManager(new R2DateTimeService ());


            var ins1 = new R2CoreAPIHelperManager();
            ins1.GetAPIDateTime();


            var InstanceSMSHandler = new R2CoreSMSHandlerManager(new R2DateTimeService(), null);
            InstanceSMSHandler.SMSGarbageCollector();

            var InstanceConfiguration = new R2CoreInstanceConfigurationManager(new R2DateTimeService());
            var x6 = InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 19) * 1000;

            //var iNSTANCEAPIHelper = new R2CoreAPIHelperManager();
            //iNSTANCEAPIHelper.GetAPIDateTime();


            var InstanceConectionStrings = new R2Core.ConfigurationManagement.R2CoreConectionStringsManager();
            //InstanceConectionStrings.GetSMSDBSubscriptionConnectionString();

            //var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
            //var SessionStartBox = InstanceSession.StartSession();

            R2Core.LoggingManagement.R2CoreLoggingManager.RegisterLog(R2Core.PubSubMessaging.R2CorePubSubChannels.Logging, new R2CoreRawLog { LogTypeId = 0, Description = "14040623", MessageDetail1 = "1", MessageDetail2 = "2", MessageDetail3 = "3", UserId = 21 });
            return;

            //var y = new R2DateTime();
            //y.CheckShamsiDateSyntax("1404/06/22");
            var x3 = new R2Core.DateTimeProvider.R2DateTimeService();
            var x4 = x3.Get6ZeroTime();

            var x5 = x4;

            var x1 = new R2CoreTransportationAndLoadNotification.Turns.R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());
            x1.TempTurnsCancellation();

            var xxxx = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService());
            //xxxx.UpdateLoadLists();
            var zzz = xxxx.GetLoadsforTruckDriver(5, new R2CoreSoftwareUser { UserId = 7003 }, 3, 1);
            //xxxx.CacheLoadsforTruckDriver(JsonConvert.SerializeObject( new  {Query = "Select Provinces.ProvinceName,Provinces.ProvinceId,MyLoads.* from \r\n                           (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCTel as TCTel,Products.strGoodName as GoodTitle,\r\n                                   Loads.nTonaj as Tonaj,LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,\r\n                                   LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,\r\n                                   Loads.nCarNumKol as Total,Loads.StrPriceSug as Tariff,\r\n\t                               Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,\r\n                                   Loads.TPTParamsJoint as TPTParamsJoint,LoadsforTurnCancellation.Description as LoadColor\r\n                            from DBTransport.dbo.TbElam as Loads\r\n                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId \r\n                              Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode \r\n                              Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode\r\n                              Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode \r\n                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId \r\n                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId \r\n                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.tblLoadsViewConditions as LoadsViewConditions On Loads.AHSGId=LoadsViewConditions.AHSGId\r\n                              Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation as LoadsforTurnCancellation on Loads.nEstelamID=LoadsforTurnCancellation.nEstelamID\r\n                            Where Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE()) and Loads.LoadStatus=1 and LoadsViewConditions.LoadStatusId=1 and \r\n                                  LoadsViewConditions.RequesterId=5 and Loads.nCarNum>0 and Loads.AHSGId=8 and LoadsViewConditions.SeqTId=2 and LoadsViewConditions.NativenessTypeId=1) as myLoads\r\n                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  \r\n\t\t\t\t\t\t Order By ProvinceName,MyLoads.TargetCityTitle FOR JSON AUTO", CacheKey ="123" }));


            //var xxx = new R2CoreRawLog { LogTypeId = 0, Description = "22", MessageDetail1 = "AnnouncementGroupId", MessageDetail2 = "", MessageDetail3 = "", UserId = 21 };
            //R2CoreLoggingManager.WriteInfLog(xxx);


            var xx = new R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService());
            xx.LoadTonajValidate(new R2CoreTransportationAndLoadNotificationLoad { AnnouncementGroupId = 2, AnnouncementSubGroupId = 7, Tonaj = 29 });

            //var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
            //var sessionStartBox = InstanceSession.StartSession();



            var x = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());
            x.TurnResuscitationByUser(14, 21);
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
            //var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
            //var Turn = InstanceLoadAllocation.LoadAllocateToOther(30027, R2CoreTransportationAndLoadNotificationRequesters.LoadAllocationController_LoadAllocateToOther, z.GetUser(7046, true));

        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var x = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                var y = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));
                var z = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));

                x.LoadAllocationCancelling(10024, 4, 0, z.GetUser(7003, true));
            }
            catch (Exception ex)
            { EventLog.WriteEntry("TruckDriversAutomatedJobs", "SetTruckDriversSeqTIdNativenessTypeId:" + ex.Message.ToString(), EventLogEntryType.Error); }

        }
    }
}
