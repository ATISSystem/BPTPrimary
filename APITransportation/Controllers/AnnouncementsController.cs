using APICommon.Models;
using APITransportation.Models.Announcements;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreTransportationAndLoadNotification.Announcements;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.Turns.SequentialTurns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers

{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class AnnouncementsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public AnnouncementsController()
        {
            try
            {
                _DateTimeService = new R2DateTimeService();
                _loggerService = new R2Core.LoggingManagement.R2CorenLogService();
                _Networking = new Networking();
            }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetAnnouncements")]
        public HttpResponseMessage GetAnnouncements([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAnnouncementsJSON(SearchString, true), Encoding.UTF8, "application/json");
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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawAnnouncement = Content.RawAnnouncement;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementRegistering(RawAnnouncement);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawAnnouncement.AnnouncementTitle ) + ":" + RawAnnouncement.AnnouncementTitle , UserId = User.UserId });

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawAnnouncement = Content.RawAnnouncement;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementEditing(RawAnnouncement);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawAnnouncement.AnnouncementId ) + ":" + RawAnnouncement.AnnouncementId , UserId = User.UserId });

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementDeleting(AnnouncementId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(AnnouncementId) + ":" + AnnouncementId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/GetAnnouncementSubGroups")]
        public HttpResponseMessage GetAnnouncementSubGroups([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);

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
        [Route("api/GetAnnouncementSubGroupsByAnnouncementId")]
        public HttpResponseMessage GetAnnouncementSubGroupsByAnnouncementId([FromBody] APITransportationSessionIdAnnouncementId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAnnouncementSGsByAnnouncementIdJSON(AnnouncementId, false), Encoding.UTF8, "application/json");
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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawAnnouncementSubGroup = Content.RawAnnouncementSubGroup;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementSGRegistering(RawAnnouncementSubGroup);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementSubGroupRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawAnnouncementSubGroup.AnnouncementSGTitle) + ":" + RawAnnouncementSubGroup.AnnouncementSGTitle, UserId = User.UserId });

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawAnnouncementSubGroup = Content.RawAnnouncementSubGroup;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementSGEditing(RawAnnouncementSubGroup);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementSubGroupEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawAnnouncementSubGroup.AnnouncementSGId) + ":" + RawAnnouncementSubGroup.AnnouncementSGId, UserId = User.UserId });

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementSGDeleting(AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementSubGroupDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

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
                var AnnouncementId = Content.AnnouncementId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementRelationAnnouncementSubGroupDeleting(AnnouncementId, AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementRelationAnnouncementSubGroupDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(AnnouncementId) + ":" + AnnouncementId, MessageDetail2 = nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementRelationAnnouncementSubGroupRegistering(AnnouncementId, AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementRelationAnnouncementSubGroupRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(AnnouncementId) + ":" + AnnouncementId, MessageDetail2 = nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

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
        [Route("api/GetAllAnnouncementRelationProvinces")]
        public HttpResponseMessage GetAllAnnouncementRelationProvinces([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAnnouncements.GetAllAnnouncementRelationProvinces(true), Encoding.UTF8, "application/json");
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
        [Route("api/AnnouncementSubGroupRelationProvinceRegistering")]
        public HttpResponseMessage AnnouncementSubGroupRelationProvinceRegistering([FromBody] APITransportationSessionIdProvinceIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var ProvinceId = Content.ProvinceId;
                var AnnouncementSGId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementSubGroupRelationProvinceRegistering(ProvinceId, AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementSubGroupRelationProvinceRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(ProvinceId) + ":" + ProvinceId, MessageDetail2 = nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

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
        [Route("api/AnnouncementSubGroupRelationProvinceDeleting")]
        public HttpResponseMessage AnnouncementSubGroupRelationProvinceDeleting([FromBody] APITransportationSessionIdProvinceIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var ProvinceId = Content.ProvinceId;
                var AnnouncementSGId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAnnouncements = new R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService);
                InstanceAnnouncements.AnnouncementSubGroupRelationProvinceDeleting(ProvinceId, AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.AnnouncementSubGroupRelationProvinceDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(ProvinceId) + ":" + ProvinceId, MessageDetail2 = nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

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

    }
}
