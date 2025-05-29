using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayanehClassLibrary.DriverTrucksManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using System;
using System.Web.Services.Protocols;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode()
        {
            try
            {
                PayanehClassLibraryMClassDriverTrucksManagement.GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode("5759871382");
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
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();

                InstanceTruckDrivers.RegisteringTruckDriverMobileNumber(1, "09133155865");
            }
            catch (SoapException ex)
            {
                var x = 2;
            }
        }

    }
}
