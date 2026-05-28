using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.LoaderTypes;
using APITransportation.Models.ProvincesAndCities;
using APITransportation.Models.SequentialTurns;
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
using R2CoreTransportationAndLoadNotification.LoaderTypes;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.Trucks.Exceptions;
using R2CoreTransportationAndLoadNotification.Turns.SequentialTurns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoaderTypeController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public LoaderTypeController()
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
        [Route("api/GetLoaderTypes")]
        public HttpResponseMessage GetLoaderTypes([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoaderTypes.GetLoaderTypes_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetLoaderTypeBySoftwareUser")]
        public HttpResponseMessage GetLoaderTypeBySoftwareUser([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceLoaderTypes.GetLoaderTypeBySoftwareUser(User.UserId)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (TruckNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/ChangeActivateStatusOfLoaderType")]
        public HttpResponseMessage ChangeActivateStatusOfLoaderType([FromBody] APITransportationSessionIdLoaderTypeId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);
                InstanceLoaderTypes.LoaderTypeChangeActivate(LoaderTypeId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ChangeActivateStatusOfLoaderType, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LoaderTypeId) + ":" + LoaderTypeId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/GetLoaderTypeRelationAnnouncementSubGroups")]
        public HttpResponseMessage GetLoaderTypeRelationAnnouncementSubGroups([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoaderTypes.GetLoaderTypeRelationAnnouncementSubGroups(true), Encoding.UTF8, "application/json");
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
        [Route("api/LoaderTypeRelationAnnouncementSubGroupRegistering")]
        public HttpResponseMessage LoaderTypeRelationAnnouncementSubGroupRegistering([FromBody] APITransportationSessionIdLoaderTypeIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId;
                var AnnouncementSubGroupId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);
                InstanceLoaderTypes.LoaderTypeRelationAnnouncementSubGroupRegistering(LoaderTypeId, AnnouncementSubGroupId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoaderTypeRelationAnnouncementSubGroupRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LoaderTypeId) + ":" + LoaderTypeId, MessageDetail2 = nameof(AnnouncementSubGroupId) + ":" + AnnouncementSubGroupId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/LoaderTypeRelationAnnouncementSubGroupDeleting")]
        public HttpResponseMessage LoaderTypeRelationAnnouncementSubGroupDeleting([FromBody] APITransportationSessionIdLoaderTypeIdAnnouncementSubGroupId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId;
                var AnnouncementSubGroupId = Content.AnnouncementSubGroupId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService);
                InstanceLoaderTypes.LoaderTypeRelationAnnouncementSubGroupDeleting(LoaderTypeId, AnnouncementSubGroupId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoaderTypeRelationAnnouncementSubGroupDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LoaderTypeId) + ":" + LoaderTypeId, MessageDetail2 = nameof(AnnouncementSubGroupId) + ":" + AnnouncementSubGroupId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
