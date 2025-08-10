using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2Core.DateAndTimeManagement;
using R2CoreTransportationAndLoadNotification.TransportTariffs;
using System;
using System.Collections.Generic;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void Tariffs()
        {
            var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(new R2DateTimeService());
            R2CoreTransportationAndLoadNotificationTransportTariff f1 = new R2CoreTransportationAndLoadNotificationTransportTariff() { SourceCityId = 21310000, TargetCityId = 11320000, LoaderTypeId = 503, GoodId = 1861401, Tariff = 50000000, BaseTonnag = 19, CalculationReference = "CalculationReference", Active = true };
            R2CoreTransportationAndLoadNotificationTransportTariff f2 = new R2CoreTransportationAndLoadNotificationTransportTariff() { SourceCityId = 21310000, TargetCityId = 11320000, LoaderTypeId = 503, GoodId = 1861401, Tariff = 50000000, BaseTonnag = 19, CalculationReference = "CalculationReference", Active = true };
            R2CoreTransportationAndLoadNotificationTransportTariff f3 = new R2CoreTransportationAndLoadNotificationTransportTariff() { SourceCityId = 21310000, TargetCityId = 11320000, LoaderTypeId = 502, GoodId = 1861401, Tariff = 50000000, BaseTonnag = 19, CalculationReference = "CalculationReference", Active = true };
            List<R2CoreTransportationAndLoadNotificationTransportTariff> z=new List<R2CoreTransportationAndLoadNotificationTransportTariff>() ;
            z.Add(f1); z.Add(f2); z.Add(f3);
            InstanceTransportTariffs.TariffsRegistering(z,30);
            var y = 2;

        }

        [TestMethod]
        public void TURNS()
        {
            var X = new R2CoreTransportationAndLoadNotification.Turns.R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService() );
            X.TurnCancellationByUser(13, 21);
            var y = 2;

        }


    }
}
