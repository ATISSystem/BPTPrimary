using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using R2CoreTransportationAndLoadNotification.Announcements;
using R2CoreTransportationAndLoadNotification.TravelTime;
using R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces;
using System.Net.Http;
using System.Net;
using System.Text;
using R2CoreTransportationAndLoadNotification.Turns;
using R2Core.DateAndTimeManagement;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void Announcements()
        {
            var x = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
            x.AnnouncementEditing(new R2CoreTransportationAndLoadNotificationAnnouncement { AnnouncementId = 1, AnnouncementTitle = "عمومی", Active = true });
        }

        [TestMethod]
        public void GetTravelTimes()
        {
            var x = new R2CoreTransportationAndLoadNotificationTravelTimeManager();
            x.GetTravelTimes(505, 21310000, -1, true);
        }


        [TestMethod]
        public void LoadingAndDischargingPlaces()
        {
            var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());


             InstanceLoadingAndDischargingPlaces.TurnCancellationByUser (1, true,21);
            var zz = 3;
        }




    }
}
