using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayanehClassLibrary.CarTrucksManagement;
using PayanehClassLibrary.DriverTrucksManagement;
using R2Core.Caching;
using R2Core.Carousels;
using R2Core.ConfigurationOfDevices;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.PermissionManagement;
using R2Core.SMS.SMSSendRecive;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.MoneyWalletChargeManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreParkingSystem.Traffic;
using R2CoreTransportationAndLoadNotification.Announcements;
using R2CoreTransportationAndLoadNotification.ConfigurationOfLoadAnnouncement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.PermissionManagement;
using R2CoreTransportationAndLoadNotification.Trucks;
using StackExchange.Redis;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Text;
using R2Core.RFIDCards;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters;
using R2Core.GeneralConfiguration;
using R2Core.SMS.SMSHandling;
using R2Core.SMS.GeneralAnnounceSMS;
using System.Security.Cryptography;
using System.EnterpriseServices.Internal;
using R2Core.RawGroups;
using R2Core;
using R2Core.ExceptionManagement;
using R2Core.Devices;
using R2Core.PredefinedMessagesManagement;
using R2Core.SMS.SMSHandling.Exceptions;
using R2Core.BaseClasses;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest9
    {
        IDateTimeService _DateTimeService;
        R2CoreSqlDataBOXManager _InstanceSqlDataBOX;

        [TestMethod]
        public void TestMethod1()
        {
            _DateTimeService = new R2DateTimeService();
            _InstanceSqlDataBOX = new R2CoreSqlDataBOXManager(_DateTimeService);
            PayanehWebService.PayanehWebService _WSp = new PayanehWebService.PayanehWebService();
            var _WS = new R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService();
            var InstanceGeneralConfiguration = new R2Core.GeneralConfiguration.R2CoreGeneralConfigurationManager(_DateTimeService);
            var InstanceGeneralAnnounceSMS = new R2CoreGeneralAnnounceSMSManager(_DateTimeService);
            var InstanceSMSHandler = new R2CoreSMSHandlerManager(_DateTimeService, null);
            var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);
            var instanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            var RawGroupFileHolder = new R2CoreRawGroupFileHolder(R2Core.RawGroups.R2CoreRawGroups.Carousels, new R2Core.BaseClasses.R2CoreFile("1"), _DateTimeService);
            var InstanceTestAPIs = new TestAPIs();
            var InstanceDevice = new R2CoreDeviceManager(_DateTimeService);
            var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
            var InsatnceRawGroups = new R2CoreRawGroupsManager (_DateTimeService);
            var InstancePermissions = new R2CorePermissionsManager(_DateTimeService);
            var InstancePaymentRequests = new R2Core.PaymentRequests.R2CoreInstansePaymentRequestsManager(_DateTimeService);
            var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
            try
            {

                InstanceSession.StartSession();

                //R2CoreTransportationAndLoadNotification.Rmto.RmtoWebService.GetNSSTruckDriver("123");

                R2CoreTransportationAndLoadNotification.Rmto.RmtoWebService.GetNSSTruck("4178165");

                //PayanehClassLibraryMClassDriverTrucksManagement.GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode("4172014607");
                //var Truck = _WS.WebMethodGetTruckBySmartCarNofromRMTO("2548574", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));
                //var TruckDriver = _WS.WebMethodGetTruckDriverByNationalCodefromRMTO("1280336651", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));


                var MobileNumber = new MobileNumberInvallidException("09132043148");


                InstancePredefinedMessages.GetPredefinedMessage(2);
                //InstanceDevice.GetAllDevices(true);
                //var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                //var WantedSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile("09132043148"));
                try
                {
                    var xop = InstanceTestAPIs.TestGetAllDevices(true );
                }
                catch (AggregateException  ex)
                {
                    ;
                }


                var Da = new SqlDataAdapter();
                DataSet Ds = new DataSet();
                Da.SelectCommand = new SqlCommand("Select * from r2primary..tbldevice");
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection();
                Da.Fill(Ds);
            }
            catch (SqlException ex)
            {
                var xXx = R2CoreDatabaseManager.GetEquivalenceMessage(ex);
                throw xXx;
            }
            catch (Exception ex)
            {; }

            var RGFileHolder = new R2CoreRawGroupFileHolder(9, new R2CoreFile("1.Carousel"), _DateTimeService);
            Byte[] FileByteArray = null;
            RGFileHolder.GetFile(ref FileByteArray);

            var RawGroup = InsatnceRawGroups.GetRawGroup(9);

            var byt = _WS.WebMethodGetFile(R2Core.RawGroups.R2CoreRawGroups.Carousels, 1.ToString() + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.Pictures, 0), _WS.WebMethodLogin(instanceSoftwareUsers.GetUser(21, true).UserShenaseh, instanceSoftwareUsers.GetUser(21, true).UserPassword));
            _WS.WebMethodSaveFile(R2Core.RawGroups.R2CoreRawGroups.Carousels, 111.ToString() + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.Pictures, 0), byt, _WS.WebMethodLogin(instanceSoftwareUsers.GetUser(21, true).UserShenaseh, instanceSoftwareUsers.GetUser(21, true).UserPassword));


            var xcxc = instanceSoftwareUsers.GetUser("F49B1141", true);



            InstanceCarousels.CarouselsCachePreparing(instanceSoftwareUsers.GetUser(21, true));

            InstanceCarousels.CarouselRegistering(new R2CoreCarousel { CTitle = "", URL = "", Description = "", Picture = new byte[] { 1, 2, 3 } }, instanceSoftwareUsers.GetUser(21, true));
            InstanceCarousels.CarouselChangeActiveStatus(20007, true, instanceSoftwareUsers.GetUser(21, true));




            InstanceSMSHandler.SendSMSFree("09132043148", new SMSCreationData { Data1 = "123" }, 12);

            var SoftwareUserTypes = instanceSoftwareUsers.GetSoftwareUserTypes(true);


            InstanceCarousels.CarouselChangeActiveStatus(4, false, null);

            InstanceGeneralAnnounceSMS.RequestGeneralAnnounceSMS(3, "1234567890");
            var SMSResult = InstanceGeneralAnnounceSMS.SendGeneralAnnounceSMS(3, "1234567890", "88o6We~8H%@5*~r");
            var ProgressInf = InstanceGeneralAnnounceSMS.GetProgressInf("88o6We~8H%@5*~r");


            //var ins = new R2CoreSMSHandlerManager(_DateTimeService,null );
            //ins.SendBulkSMS(12,3,new SMSCreationData { Data1="123" });



            //var InstanceGeneralConfiguration = new R2CoreGeneralConfigurationManager(_DateTimeService);
            //var ConfigTime = TimeSpan.Parse(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreConfigurations.SmsSystemSetting, 9)).Ticks;

            var intsdf = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
            intsdf.TransportPriceTarrifParameterDetailRegistering(new R2CoreTransportationAndLoadNotificationTransportTariffsParametersDetail { TPTPDId = 0, TPTPId = 1, AnnouncementSGId = 7, Active = true, Cost = 0, AnnouncementSGTitle = "", TPTPTitle = "" });


            var xrf = new R2CoreSoftwareUsersManager(_DateTimeService, null);

            var xzcx = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
            xzcx.RegisteringTruckDriverMobileNumber(144543, "09130903148");


            xrf.RegisteringSoftwareUserPermissionsByUserType(25);

            var xcvf = new R2CoreRFIDCardsManager(_DateTimeService);
            xcvf.RFIDCardRegistering("WWWWWW01", 21);

            var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);
            InstanceMoneyWalletCharge.GetDefaultAmounts(true);

            var insss = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
            insss.ConfigurationOfLoadAnnouncementRegistering(new R2CoreTransportationAndLoadNotificationRawConfigurationOfLoadAnnouncement { COLAId = 1, COLAName = "LoadAnnounceTimeCycle", COLATitle = "زمانبندی اعلام بار", AnnouncementId = 3, AnnouncementSGId = 14, COLAIndex = 0, COLAIndexTitle = "ساعات", Description = "اعلام بار انباری برون شهری", COLAValue = "11:00:00-10:00:00-12:00:00-14:00:00" });


            var soft = new R2CoreSoftwareUsersManager(_DateTimeService, null);
            var cm = new R2CoreCarouselsManager(_DateTimeService);
            cm.CarouselEditing(new R2CoreCarousel { CId = 1, URL = "", Description = "", CTitle = "", Active = true, Picture = null }, soft.GetSystemUser());








            var xty = new R2CoreCacheManager(_DateTimeService);
            xty.GetCache("", 0);



            if (InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.LoadAllocationUseTimeHandlingByLoadStatus, 1, 0))
            { }


            var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(_DateTimeService);
            InstanceTrucks.SetComposedTruckInf(198707, 143545, 6, 0);


            try
            {
                R2CoreSMSSendingManager InstanceSMSSending = new R2CoreSMSSendingManager(_DateTimeService);
                InstanceSMSSending.Sending();
            }
            catch (Exception ex)
            { EventLog.WriteEntry("SMSSendingAutomatedJob", "Sending:" + ex.Message.ToString(), EventLogEntryType.Error); }

            var xcvb = new R2CoreParkingSystemTrafficManager(_DateTimeService);
            var y = xcvb.GetTrafficRecords(90, true);


            var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
            var SessionStartBox = InstanceSession.StartSession();

            var InstanceSqlDataBOX = new R2CoreSqlDataBOXManager(_DateTimeService);
            DataSet DS = null;
            bool bb = false;
            InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "Select Top 1 * from R2Primary.dbo.TblCacheTypes Where CacheTypeId=" + 1 + "", 10000, ref DS, ref bb);

            var xcv = new R2CoreTransportationAndLoadNotificationTrucksManager(_DateTimeService);
            xcv.GetTruckBySmartCardNo("4116459");

            PayanehClassLibraryMClassDriverTrucksManagement.GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode("4172014607");
            //var Truck = _WS.WebMethodGetTruckBySmartCarNofromRMTO("2548574", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));
            //var TruckDriver = _WS.WebMethodGetTruckDriverByNationalCodefromRMTO("1280336651", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));


            PayanehClassLibraryMClassCarTrucksManagement.GetNSSTruckBySmartCardNoWithUpdatingfromRMTO("2548574");


            var xxx = new R2CoreParkingSystemProvincesAndCitiesManager(new R2DateTimeService());
            xxx.GetCity(11320000);
            var xx = new R2CoreSqlDataBOXManager(new R2DateTimeService());
            //System.Data.DataSet DS = null;
            bool booo = false;
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 0, ref DS, ref booo);
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 1000, ref DS, ref booo);
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 0, ref DS, ref booo);
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 0, ref DS, ref booo);
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 1000, ref DS, ref booo);
            xx.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection(), "select * from tblsoftwareusers", 0, ref DS, ref booo);


            InstanceMoneyWalletCharge.PaymentRequest(15000, 21);
            //R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService  _WS=new R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService ();
            int x = 1;
            //_WS.WebMethodGetFile(R2Core.RawGroups.R2CoreRawGroups.Carousels, x.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 0), _WS.WebMethodLogin("1564b5d438dab9465bc641a79d7280d67e83b93acf1cd639f62897f1e9baf851", "1564b5d438dab9465bc641a79d7280d67e83b93acf1cd639f62897f1e9baf851"));

            var InstanceLoadSedimentation = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(new R2DateTimeService());
            InstanceLoadSedimentation.AnnouncementRegistering(new R2CoreTransportationAndLoadNotificationAnnouncement { AnnouncementId = 0, Active = true, AnnouncementTitle = "تريلي برون شهري - صادراتي" });



        }
    }
}
