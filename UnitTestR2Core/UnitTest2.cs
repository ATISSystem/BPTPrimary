using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PayanehClassLibrary.CarTrucksManagement;
using PayanehClassLibrary.DriverTrucksManagement;
using R2Core.DateAndTimeManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.SMS.SMSOwners;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using System;
using System.Net;
using System.Web.Services.Protocols;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void GetSoftwareUserIdfromTruckDriverIdTest()
        {
            try
            {
                try
                {
                    var x = new R2CoreTransportationAndLoadNotification.TruckDrivers.R2CoreTransportationAndLoadNotificationTruckDriversManager();
                    var y=x.GetSoftwareUserIdfromTruckDriverId(1);
                    PayanehClassLibraryMClassCarTrucksManagement.GetNSSTruckBySmartCardNoWithUpdatingfromRMTO("4267898");

                    R2CoreTransportationAndLoadNotification.Rmto.RmtoWebService.GetNSSTruck("4267898");
                }
                catch (WebException ex)
                {
                    var x = 2;
                }
                catch (SoapException ex)
                {
                    var x = 2;
                }
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                PayanehWebService.PayanehWebService _WS = new PayanehWebService.PayanehWebService();
                var Truck = _WS.WebMethodGetTruckBySmartCarNofromRMTO("123", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));

                var X = 3;
            }
            catch (SoapException ex)
            {
                var x = ex.Message;
                var Y = 3;
            }
            catch (Exception ex)
            { var Y = 3; }
        }
        [TestMethod]
        public void GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode()
        {
            try
            {
             var x=   PayanehClassLibraryMClassDriverTrucksManagement.GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode("5759871382");
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
        }
        [TestMethod]
        public void RegisteringTruckDriverMobileNumber()
        {
            try
            {
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();

                InstanceTruckDrivers.RegisteringTruckDriverMobileNumber(1, "09133155865");
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
        }

        [TestMethod]
        public void TestJsonNothing()
        {
            try
            {
                R2CoreTransportationAndLoadNotification.Trucks.R2CoreTransportationAndLoadNotificationTruck  d = null;
                var x = JsonConvert.SerializeObject(d);
                var y = 2;
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
        }

        [TestMethod]
        public void RmtoWebService()
        {
            try
            {   try
                {
                PayanehClassLibraryMClassCarTrucksManagement.GetNSSTruckBySmartCardNoWithUpdatingfromRMTO("4267898");
             
                    R2CoreTransportationAndLoadNotification.Rmto.RmtoWebService.GetNSSTruck("4267898");
                }
                catch (WebException ex)
                {
                    var x = 2;
                }
                catch (SoapException ex)
                {
                    var x = 2;
                }
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                PayanehWebService.PayanehWebService _WS = new PayanehWebService.PayanehWebService();
                var Truck = _WS.WebMethodGetTruckBySmartCarNofromRMTO("123", _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));

                var X = 3;
            }
            catch (SoapException ex)
            {
                var x = ex.Message;
                var Y = 3;
            }
            catch(Exception ex)
            { var Y = 3; }
        }

        [TestMethod]
        public void EditTransportCompany()
        {
            try
            {               
                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanceTransportCompanies.TransportCompanyChangeActiveStatus(21306);

                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(21), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(InstanceTransportCompanies.GetSoftwareUserIdfromTransportCompanyId(21306, true));


                InstanceTransportCompanies.EditTransportCompany(new RawTransportCompany { TCId= 21306 , TCTitle = "ا مانت  بار", TCOrganizationCode="", TCTel="03134721838", TCManagerNameFamily="مرتضی شاهمرادی", TCManagerMobileNumber="09122002000", TCCityTitle="", EmailAddress="", Active=true} );
                var y = 2;
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
            catch (Exception ex)
            { var Y = 2; }
        }

        [TestMethod]
        public void FactoriesAndProductionCenters()
        {
            try
            {
                var Instance = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                Instance.EditFactoryAndProductionCenter(new RawFactoryAndProductionCenter {  FPCId=1015,FPCTitle="ertet",FPCTel="",FPCAddress="",FPCManagerMobileNumber= "09152043175", FPCManagerNameFamily="", EmailAddress="", Active=false } );
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
            catch (Exception ex)
            { var Y = 2; }
        }

        [TestMethod]
        public void Trucks()
        {
            try
            {
                var Instance = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTrucks = new R2CoreTransportationAndLoadNotification.Trucks.R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService ());
                InstanceTrucks.SetComposedTruckInf(5,1,15,-1);
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
            catch (Exception ex)
            { var Y = 2; }
        }

        [TestMethod]
        public void TravelTimes()
        {
            try
            {
                var InstanceTravelTime = new R2CoreTransportationAndLoadNotification.TravelTime.R2CoreTransportationAndLoadNotificationTravelTimeManager();

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanceTravelTime.GetTravelTimes(505,21310000,-1,true );
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
            catch (Exception ex)
            { var Y = 2; }
        }
        
    }
}
