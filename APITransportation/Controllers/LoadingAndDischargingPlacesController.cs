using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.LoadingAndDischargingPlaces;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification;
using R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces;
using R2CoreTransportationAndLoadNotification.Logging;

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
    public class LoadingAndDischargingPlacesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public LoadingAndDischargingPlacesController()
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
        [Route("api/GetLADPlaces")]
        public HttpResponseMessage GetLADPlaces()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var Content = JsonConvert.DeserializeObject<APICommonSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoadingAndDischargingPlaces.GetLoadingAndDischargingPlaces_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetLADPlace")]
        public HttpResponseMessage GetLADPlace()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdLADPlaceId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var LADPlaceId = Content.LADPlaceId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceLoadingAndDischargingPlaces.GetLoadingAndDischargingPlace(LADPlaceId, true)), Encoding.UTF8, "application/json");
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
        [Route("api/LADPlaceRegister")]
        public HttpResponseMessage LADPlaceRegister()
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdRawLADPlaceInf>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawLADPlaceInf = Content.RawLADPlaceInf;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();
                var LADPlaceId = InstanceLoadingAndDischargingPlaces.LoadingAndDischargingPlaceRegister(RawLADPlaceInf);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LADPlaceRegister, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawLADPlaceInf.LADPlaceTitle ) + ":" + RawLADPlaceInf.LADPlaceTitle , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/LADPlaceUpdate")]
        public HttpResponseMessage LADPlaceUpdate([FromBody] APITransportationSessionIdRawLADPlaceInf Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawLADPlaceInf = Content.RawLADPlaceInf;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();
                InstanceLoadingAndDischargingPlaces.LoadingAndDischargingPlaceUpdating(RawLADPlaceInf);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LADPlaceUpdate, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawLADPlaceInf.LADPlaceId) + ":" + RawLADPlaceInf.LADPlaceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/LADPlaceDelete")]
        public HttpResponseMessage LADPlaceDelete([FromBody] APITransportationSessionIdLADPlaceId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LADPlaceId = Content.LADPlaceId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();
                InstanceLoadingAndDischargingPlaces.LoadingAndDischargingPlaceDelete(LADPlaceId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LADPlaceDelete, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LADPlaceId) + ":" + LADPlaceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/LoadingPlaceChangeActiveStatus")]
        public HttpResponseMessage LoadingPlaceChangeActiveStatus([FromBody] APITransportationSessionIdLADPlaceId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LADPlaceId = Content.LADPlaceId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();
                InstanceLoadingAndDischargingPlaces.LoadingPlaceChangeActiveStatus(LADPlaceId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoadingPlaceChangeActiveStatus, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LADPlaceId) + ":" + LADPlaceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/DischargingPlaceChangeActiveStatus")]
        public HttpResponseMessage DischargingPlaceChangeActiveStatus([FromBody] APITransportationSessionIdLADPlaceId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var LADPlaceId = Content.LADPlaceId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadingAndDischargingPlaces = new R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager();
                InstanceLoadingAndDischargingPlaces.DischargingPlaceChangeActiveStatus(LADPlaceId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.DischargingPlaceChangeActiveStatus, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LADPlaceId) + ":" + LADPlaceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
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
