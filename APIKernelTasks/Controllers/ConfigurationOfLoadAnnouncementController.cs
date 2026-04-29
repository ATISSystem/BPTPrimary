using R2Core.ConfigurationOfDevices;
using R2CoreTransportationAndLoadNotification.ConfigurationOfLoadAnnouncement;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.SessionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using R2Core.PredefinedMessagesManagement;
using R2Core.LoggingManagement;
using System.Web;
using APIKernelTasks.Models;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.Announcements;
using System.Security.Claims;

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class ConfigurationOfLoadAnnouncementController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public ConfigurationOfLoadAnnouncementController()
        {
            try
            {
                _DateTimeService = new R2DateTimeService();
                _loggerService = new R2Core.LoggingManagement.R2CorenLogService();
                _Networking = new  Networking();
            }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetAllConfigurationOfLoadAnnouncement")]
        public HttpResponseMessage GetAllConfigurationLoadAnnouncement([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurationOfLoadAnnouncement.GetAllConfigurationOfLoadAnnouncement(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ConfigurationOfLoadAnnouncementEditing")]
        public HttpResponseMessage ConfigurationOfLoadAnnouncementEditing([FromBody] APIKernelTasksSessionIdRawConfigurationOfLoadAnnouncement Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfLoadAnnouncement = Content.RawConfigurationOfLoadAnnouncement;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementEditing(RawConfigurationOfLoadAnnouncement);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ConfigurationOfLoadAnnouncementEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawConfigurationOfLoadAnnouncement.COLAId) + ":" + RawConfigurationOfLoadAnnouncement.COLAId, MessageDetail2 = nameof(RawConfigurationOfLoadAnnouncement.COLAIndex) + ":" + RawConfigurationOfLoadAnnouncement.COLAIndex, MessageDetail3 = nameof(RawConfigurationOfLoadAnnouncement.AnnouncementId) + ":" + RawConfigurationOfLoadAnnouncement.AnnouncementId + " " + nameof(RawConfigurationOfLoadAnnouncement.AnnouncementSGId) + ":" + RawConfigurationOfLoadAnnouncement.AnnouncementSGId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ConfigurationOfLoadAnnouncementRegistering")]
        public HttpResponseMessage ConfigurationOfLoadAnnouncementRegistering([FromBody] APIKernelTasksSessionIdRawConfigurationOfLoadAnnouncement Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfLoadAnnouncement = Content.RawConfigurationOfLoadAnnouncement;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementRegistering(RawConfigurationOfLoadAnnouncement);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ConfigurationOfLoadAnnouncementRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawConfigurationOfLoadAnnouncement.COLAId) + ":" + RawConfigurationOfLoadAnnouncement.COLAId, MessageDetail2 = nameof(RawConfigurationOfLoadAnnouncement.COLAIndex) + ":" + RawConfigurationOfLoadAnnouncement.COLAIndex, MessageDetail3 = nameof(RawConfigurationOfLoadAnnouncement.AnnouncementId) + ":" + RawConfigurationOfLoadAnnouncement.AnnouncementId + " " + nameof(RawConfigurationOfLoadAnnouncement.AnnouncementSGId) + ":" + RawConfigurationOfLoadAnnouncement.AnnouncementSGId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ConfigurationOfLoadAnnouncementDeleting")]
        public HttpResponseMessage ConfigurationOfLoadAnnouncementDeleting([FromBody] APIKernelTasksSessionIdCOLAIdCOLAIndexAnnouncementIdAnnouncementSGId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var COLAId = Content.COLAId;
                var COLAIndex = Content.COLAIndex;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementDeleting(COLAId, COLAIndex, AnnouncementId, AnnouncementSGId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ConfigurationOfLoadAnnouncementDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(COLAId) + ":" + COLAId, MessageDetail2 = nameof(COLAIndex) + ":" + COLAIndex, MessageDetail3 = nameof(AnnouncementId) + ":" + AnnouncementId+" "+ nameof(AnnouncementSGId) + ":" + AnnouncementSGId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }


}
