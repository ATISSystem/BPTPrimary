using APICommon;
using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.TruckDriver;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2Core.WebProcessesManagement;
using R2CoreParkingSystem.Drivers;
using R2CoreParkingSystem.SMS.SMSOwners;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.Rmto;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions;
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
    public class TruckDriverController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public TruckDriverController()
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
        [Route("api/GetTruckDriverfromRMTO")]
        public HttpResponseMessage GetTruckDriverfromRMTO([FromBody] APITransportationSessionIdTruckDriverNationalCode Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckDriverNationalCode = Content.TruckDriverNationalCode;

                var User = InstanceSession.ConfirmSession(SessionId);

                try
                {
                    PayanehClassLibrary.PayanehWS.PayanehWebService _WS = new PayanehClassLibrary.PayanehWS.PayanehWebService();
                    var TruckDriver = _WS.WebMethodGetTruckDriverByNationalCodefromRMTO(TruckDriverNationalCode, _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(TruckDriver), Encoding.UTF8, "application/json");
                    return response;

                }
                catch (RMTOWebServiceSmartCardLimitationException ex)
                { throw ex; }
                catch (Exception ex)
                { throw new Exception("PayanehWS:" + ex.Message); }

            }
            catch (RMTOWebServiceSmartCardLimitationException ex)
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
        [Route("api/GetTruckDriverfromWebSite")]
        public HttpResponseMessage GetTruckDriverfromWebSite([FromBody] APITransportationSessionIdTruckDriverNationalCode Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckDriverNationalCode = Content.TruckDriverNationalCode;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTruckDrivers.GetTruckDriverfromNationalCode(TruckDriverNationalCode, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (TruckDriverNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/GetTruckDriverBySoftwareUser")]
        public HttpResponseMessage GetTruckDriverBySoftwareUser([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTruckDrivers.GetTruckDriverBySoftwareUser(User.UserId, false)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (TruckDriverNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/TruckDriverRegisteringMobileNumber")]
        public HttpResponseMessage TruckDriverRegisteringMobileNumber([FromBody] APITransportationSessionIdTruckDriverIdMobileNumber Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckDriverId = Content.TruckDriverId;
                var MobileNumber = Content.MobileNumber;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTruckDrivers.RegisteringTruckDriverMobileNumber(TruckDriverId, MobileNumber);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TruckDriverRegisteringMobileNumber, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(TruckDriverId) + ":" + TruckDriverId, MessageDetail2 = nameof(MobileNumber) + ":" + MobileNumber, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/ActivateTruckDriverSMSOwner")]
        public HttpResponseMessage ActivateTruckDriverSMSOwner([FromBody] APITransportationSessionIdTruckDriverId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), _DateTimeService);
                InstanceSMSOwners.ActivateSMSOwner(InstanceTruckDrivers.GetSoftwareUserIdfromTruckDriverId(TruckDriverId, true));

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ActivateTruckDriverSMSOwner, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(TruckDriverId) + ":" + TruckDriverId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ResetTruckDriverUserPassword")]
        public HttpResponseMessage ResetTruckDriverUserPassword([FromBody] APITransportationSessionIdTruckDriverId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceTruckDrivers.GetSoftwareUserIdfromTruckDriverId(TruckDriverId, true), User.UserId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ResetTruckDriverUserPassword, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(TruckDriverId) + ":" + TruckDriverId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftWareUserSecurity), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SendWebSiteLink")]
        public HttpResponseMessage SendWebSiteLink([FromBody] APITransportationSessionIdTruckDriverId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationTruckDriversManager();
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                InstanseSoftwareUsers.SendWebSiteLink(InstanceTruckDrivers.GetSoftwareUserfromTruckDriverId(TruckDriverId));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
