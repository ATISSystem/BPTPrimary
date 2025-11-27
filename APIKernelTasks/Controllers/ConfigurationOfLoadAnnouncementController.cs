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
using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.PredefinedMessagesManagement;

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class ConfigurationOfLoadAnnouncementController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public ConfigurationOfLoadAnnouncementController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfLoadAnnouncement = Content.RawConfigurationOfLoadAnnouncement ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementEditing(RawConfigurationOfLoadAnnouncement);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfLoadAnnouncement = Content.RawConfigurationOfLoadAnnouncement;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementRegistering (RawConfigurationOfLoadAnnouncement);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var COLAId = Content.COLAId ;
                var COLAIndex = Content.COLAIndex;
                var AnnouncementId = Content.AnnouncementId;
                var AnnouncementSGId = Content.AnnouncementSGId;

               var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfLoadAnnouncement = new R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager(_DateTimeService);
                InstanceConfigurationOfLoadAnnouncement.ConfigurationOfLoadAnnouncementDeleting (COLAId, COLAIndex, AnnouncementId, AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
