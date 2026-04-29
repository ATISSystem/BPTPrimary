using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.FactoriesAndProductionCenters;
using APITransportation.Models.TransportCompanies;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.SMS.SMSOwners;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
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
    public class FactoriesAndProductionCentersController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public FactoriesAndProductionCentersController()
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
        [Route("api/GetFPCs")]
        public HttpResponseMessage GetFactoriesAndProductionCenters([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceFactoriesAndProductionCenters.GetFactoriesAndProductionCenters_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetFPC")]
        public HttpResponseMessage GetFactoryAndProductionCenter([FromBody] APITransportationSessionIdFactoryAndProductionCenterId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceFactoriesAndProductionCenters.GetFactoryAndProductionCenter(FPCId, true)), Encoding.UTF8, "application/json");
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
        [Route("api/FPCRegistering")]
        public HttpResponseMessage FactoryAndProductionCenterRegistering([FromBody] APITransportationSessionIdRawFactoryAndProductionCenter Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawFactoryAndProductionCenter = Content.RawFPC;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.FactoryAndProductionCenterRegister(RawFactoryAndProductionCenter);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.FactoryAndProductionCenterRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawFactoryAndProductionCenter.FPCTitle ) + ":" + RawFactoryAndProductionCenter.FPCTitle , MessageDetail2= nameof(RawFactoryAndProductionCenter.FPCManagerMobileNumber ) + ":" + RawFactoryAndProductionCenter.FPCManagerMobileNumber , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/EditFPC")]
        public HttpResponseMessage EditFactoryAndProductionCenter([FromBody] APITransportationSessionIdRawFactoryAndProductionCenter Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawFactoryAndProductionCenter = Content.RawFPC;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.EditFactoryAndProductionCenter(RawFactoryAndProductionCenter);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.EditFactoryAndProductionCenter, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawFactoryAndProductionCenter.FPCId) + ":" + RawFactoryAndProductionCenter.FPCId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/DeleteFPC")]
        public HttpResponseMessage DeleteFactoryAndProductionCenter([FromBody] APITransportationSessionIdFactoryAndProductionCenterId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.DeleteFactoryAndProductionCenter(FPCId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.DeleteFactoryAndProductionCenter, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(FPCId) + ":" + FPCId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/ActivateFPCSMSOwner")]
        public HttpResponseMessage ActivateFactoryAndProductionCenterSMSOwner([FromBody] APITransportationSessionIdFactoryAndProductionCenterId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), _DateTimeService);
                InstanceSMSOwners.ActivateSMSOwner(InstanceFactoriesAndProductionCenters.GetSoftwareUserIdfromFactoryAndProductionCenterId(FPCId, true));

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ActivateFactoryAndProductionCenterSMSOwner, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(FPCId) + ":" + FPCId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/ResetFPCUserPassword")]
        public HttpResponseMessage ResetFactoryAndProductionCenterUserPassword([FromBody] APITransportationSessionIdFactoryAndProductionCenterId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceFactoriesAndProductionCenters.GetSoftwareUserIdfromFactoryAndProductionCenterId(FPCId, true), User.UserId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.ResetFactoryAndProductionCenterUserPassword, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(FPCId) + ":" + FPCId, UserId = User.UserId });

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
        [Route("api/FPCChangeActiveStatus")]
        public HttpResponseMessage FactoryAndProductionCenterChangeActiveStatus([FromBody] APITransportationSessionIdFactoryAndProductionCenterId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.FactoryAndProductionCenterChangeActiveStatus(FPCId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.FactoryAndProductionCenterChangeActiveStatus, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(FPCId) + ":" + FPCId,UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
