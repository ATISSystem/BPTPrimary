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
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.Turns.SequentialTurns;
using PayanehClassLibrary.TurnRegisterRequest;
using R2CoreTransportationAndLoadNotification.TurnRegisterRequest;
using R2Core.DateTimeProvider;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void Announcements()
        {
            var x = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(new R2DateTimeService());
            x.AnnouncementRelationAnnouncementSubGroupDeleting(2, 7);
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
            var InstanceTurnRegisterRequestTransportation = new R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(new R2DateTimeService());
            var InstanceTurnRegisterRequestPayaneh = new PayanehClassLibraryTurnRegisterRequestManager(new R2DateTimeService());
            var TRR = InstanceTurnRegisterRequestTransportation.GetTurnRegisteringRequestWithReservedDateTime(new R2CoreDateAndTime { DateTimeMilladi=DateTime.Now, ShamsiDate="1404/04/25", Time="00:00:00" }, true);
            Int64 TurnId = 0;
            InstanceTurnRegisterRequestPayaneh.ResuscitationReserveTurn(2, TRR.TRRId, 85183, R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_ResuscitationReserveTurn, TurnType.Permanent, 21);

            //var InstanceTurnRegisterRequest = new PayanehClassLibraryTurnRegisterRequestManager(new R2DateTimeService());
            //Int64 TurnId = 0;
            //InstanceTurnRegisterRequest.EmergencyTurnRegisterRequest(2, 85183, ref TurnId, "jhgjh", R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_EmergencyTurnRegisterRequest, TurnType.Permanent, 21);


            var zz = 3;
        }




    }
}
