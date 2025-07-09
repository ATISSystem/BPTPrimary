using APICommon.Models;
using APITransportation.Models.Announcements;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreTransportationAndLoadNotification.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers

{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class AnnouncementsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetAnnouncements")]
        public HttpResponseMessage GetAnnouncements([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAnnouncements(SearchString, true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementRegistering")]
        public HttpResponseMessage AnnouncementRegistering([FromBody] APITransportationSessionIdAnnouncement Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var RawAnnouncement = Content.RawAnnouncement;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementRegistering(RawAnnouncement);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementEditing")]
        public HttpResponseMessage AnnouncementEditing([FromBody] APITransportationSessionIdAnnouncement Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var RawAnnouncement = Content.RawAnnouncement;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementEditing(RawAnnouncement);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementDeleting")]
        public HttpResponseMessage AnnouncementDeleting([FromBody] APITransportationSessionIdAnnouncementId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementDeleting(AnnouncementId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetAnnouncementSubGroups")]
        public HttpResponseMessage GetAnnouncementSubGroups([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAnnouncementSGs(SearchString, true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementSubGroupRegistering")]
        public HttpResponseMessage AnnouncementSubGroupRegistering([FromBody] APITransportationSessionIdAnnouncementSubGroup Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var RawAnnouncementSubGroup = Content.RawAnnouncementSubGroup;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementSGRegistering(RawAnnouncementSubGroup);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementSubGroupEditing")]
        public HttpResponseMessage AnnouncementSubGroupEditing([FromBody] APITransportationSessionIdAnnouncementSubGroup Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var RawAnnouncementSubGroup = Content.RawAnnouncementSubGroup;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementSGEditing(RawAnnouncementSubGroup);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementSubGroupDeleting")]
        public HttpResponseMessage AnnouncementSubGroupDeleting([FromBody] APITransportationSessionIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementSGDeleting(AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetAnnouncementRelationAnnouncementSubGroups")]
        public HttpResponseMessage GetAnnouncementRelationAnnouncementSubGroups([FromBody] APITransportationSessionIdAnnouncementId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId ;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAnnouncementRelationAnnouncementSubGroups(AnnouncementId, true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementRelationAnnouncementSubGroupDeleting")]
        public HttpResponseMessage AnnouncementRelationAnnouncementSubGroupDeleting([FromBody] APITransportationSessionIdAnnouncementIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementRelationAnnouncementSubGroupDeleting(AnnouncementId,AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AnnouncementRelationAnnouncementSubGroupRegistering")]
        public HttpResponseMessage AnnouncementRelationAnnouncementSubGroupRegistering([FromBody] APITransportationSessionIdAnnouncementIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager();
                InstanceAnnouncements.AnnouncementRelationAnnouncementSubGroupRegistering (AnnouncementId, AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed ).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
