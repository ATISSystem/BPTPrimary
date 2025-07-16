using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using R2Core.SessionManagement;
using R2Core.WebProcessesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using R2Core.SoftwareUserManagement;
using R2Core.SMS.SMSOwners;
using R2CoreParkingSystem.SMS.SMSOwners;
using System.Web.Http.Cors;
using R2Core.DateAndTimeManagement;
using Microsoft.Ajax.Utilities;
using APISoftwareUser.Models;
using R2Core.PredefinedMessagesManagement;
using R2Core.DatabaseManagement;
using R2Core.ExceptionManagement;
using R2Core.MoneyWallet.MoneyWallet;
using R2Core.MoneyWallet.Exceptions;
using APICommon.Models;
using R2CoreParkingSystem.SoftwareUsersManagement;
using System.Web.Services.Protocols;



namespace APISoftwareUser.Controllers
{



    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class SoftwareUserController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public SoftwareUserController()
        { _DateTimeService = new R2DateTimeService(); }

        [HttpGet]
        [Route("api/GetCaptcha")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage GetCaptcha()
        {
            try
            {
                var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
                var sessionStartBox = InstanceSession.StartSession();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(sessionStartBox), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AuthUser")]
        public HttpResponseMessage AuthUser([FromBody] APISoftwareUserAuthUserStructure Content)
        {
            try
            {
                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), null);
                InstanceSoftwareUsers.ConfirmUser(Content.SessionId, Content.UserShenaseh, Content.UserPassword, Content.Captcha);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APICommonSessionId { SessionId = Content.SessionId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSessionSoftwareUser")]
        public HttpResponseMessage GetSessionSoftwareUser([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ISSessionLive")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage ISSessionLive([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { ISSessionLive = true }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { ISSessionLive = false }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetWebProcesses")]
        public HttpResponseMessage GetWebProcesses([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();
                var WebProccesses = InstanceWebProcesses.GetWebProcesses(User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(WebProccesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/RegisteringSoftwareUser")]
        public HttpResponseMessage RegisteringSoftwareUser([FromBody] APISoftwareUserSessionIdRawSoftwareUserStructure Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var RawSoftwareUser = Content.RawSoftwareUser;

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                var SoftwareUserId = InstanceSoftwareUsers.RegisteringSoftwareUser(RawSoftwareUser, true, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { SoftwareUserId = SoftwareUserId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SoftwareUserForgetPassword")]
        public HttpResponseMessage SoftwareUserForgetPassword([FromBody] APISoftwareUserSessionIdSoftwareUserMobileNumber Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.SoftwareUserMobileNumber;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanseSoftwareUsers.SoftwareUserForgetPassword(SessionId, SoftwareUserMobileNumber);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/VerifySoftwareUserVerificationCode")]
        public HttpResponseMessage VerifySoftwareUserVerificationCode([FromBody] APISoftwareUserSessionIdSoftwareUserVerificationCode Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserVerificationCode = Content.VerificationCode;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanseSoftwareUsers.VerifySoftwareUserVerificationCode(SessionId, SoftwareUserVerificationCode);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ResetSoftwareUserPassword")]
        public HttpResponseMessage ResetSoftwareUserPassword([FromBody] APISoftwareUserSessionIdSoftwareUserId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(SoftwareUserId, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftWareUserSecurity), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetUserTypes")]
        public HttpResponseMessage GetUserTypes([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                var UserTypes = InstanceSoftwareUsers.GetSoftwareUserTypes();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(UserTypes, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/EditSoftwareUser")]
        public HttpResponseMessage EditSoftwareUser([FromBody] APISoftwareUserSessionIdRawSoftwareUserStructure Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var RawSoftwareUser = Content.RawSoftwareUser;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.EditSoftwareUser(RawSoftwareUser);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.EditingSoftWareUserSuccessed).MsgContent }), Encoding.UTF8, "application/json");
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
        [Route("api/GetSMSOwnerCurrentStatus")]
        public HttpResponseMessage GetSMSOwnerCurrentStatus([FromBody] APISoftwareUserSessionIdSoftwareUserId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceSMSOwners = new R2CoreMClassSMSOwnersManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var SMSOwnerCurrentState = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SMSOwnerCurrentState), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSMSOwnerCurrentStatus")]
        public HttpResponseMessage ChangeSMSOwnerCurrentStatus([FromBody] APISoftwareUserSessionIdSoftwareUserId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), new R2DateTimeService());
                InstanceSMSOwners.ChangeSMSOwnerCurrentState(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ActivateSMSOwner")]
        public HttpResponseMessage ActivateSMSOwner([FromBody] APISoftwareUserSessionIdSoftwareUserId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ActivateSMSOwnerSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftWareUser")]
        public HttpResponseMessage GetSoftWareUser([FromBody] APISoftwareUserSessionIdSoftwareUserMobileNumber Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.SoftwareUserMobileNumber;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreMClassSMSOwnersManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var WantedSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(SoftwareUserMobileNumber));
                var RawSoftwareUser = new R2CoreRawSoftwareUserStructure { UserId = WantedSoftwareUser.UserId, UserName = WantedSoftwareUser.UserName, MobileNumber = WantedSoftwareUser.MobileNumber, UserTypeId = WantedSoftwareUser.UserTypeId, UserActive = WantedSoftwareUser.UserActive, SMSOwnerActive = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(WantedSoftwareUser).IsSendingActive };

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(RawSoftwareUser), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetAllOfWebProcessGroupsWebProcesses")]
        public HttpResponseMessage GetAllOfWebProcessGroupsWebProcesses([FromBody] APISoftwareUserSessionIdSoftwareUserMobileNumber Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var SoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(Content.SoftwareUserMobileNumber));

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();

                var AllOfWebProcessGroupsWebProcesses = InstanceWebProcesses.GetAllOfWebProcessGroupsWebProcesses(SoftwareUser.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(AllOfWebProcessGroupsWebProcesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessGroupAccess([FromBody] APISoftwareUserSessionIdSoftwareUserIdWPGId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.PGId;
                var WPGAccess = Content.PGAccess;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId, WPGAccess);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ChangeSoftwareUserWebProcessGroupAccessSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSoftwareUserWebProcessAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessAccess([FromBody] APISoftwareUserSessionIdSoftwareUserIdWPId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.PId;
                var WPAccess = Content.PAccess;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessAccess(SoftwareUserId, WPId, WPAccess);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ChangeSoftwareUserWebProcessAccess).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessGroupAccess([FromBody] APISoftwareUserSessionIdSoftwareUserIdWPGId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.PGId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPGId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, PGId = WPGId, PGAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserWebProcessAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessAccess([FromBody] APISoftwareUserSessionIdSoftwareUserIdWPId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.PId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, PId = WPId, PAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessAccess(SoftwareUserId, WPId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SendWebSiteLink")]
        public HttpResponseMessage SendWebSiteLink([FromBody] APISoftwareUserSessionIdSoftwareUserId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(),null );
                InstanseSoftwareUsers.SendWebSiteLink(InstanseSoftwareUsers.GetUser(SoftwareUserId, true));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.SendWebSiteLink).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetVirtualMoneyWallet")]
        public HttpResponseMessage GetVirtualMoneyWallet([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWallet = new R2CoreMoneyWalletManager();
                var MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWallet(MoneyWalletId, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (MoneyWalletNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserProfile")]
        public HttpResponseMessage GetSoftwareUserProfile([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreParkingSystemSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftwareUserComposedInf = InstanceSoftwareUsers.GetSoftwareUserComposedInf(User);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var x = new R2CoreRawSoftwareUserStructure { UserId = SoftwareUserComposedInf.SoftwareUserExtended.UserId, UserName = SoftwareUserComposedInf.SoftwareUserExtended.UserName, MobileNumber = SoftwareUserComposedInf.SoftwareUserExtended.MobileNumber, UserTypeId = SoftwareUserComposedInf.SoftwareUserExtended.UserTypeId, UserActive = SoftwareUserComposedInf.SoftwareUserExtended.UserActive, UserTypeTitle= SoftwareUserComposedInf.SoftwareUserExtended.SoftwareUserTypeTitle };
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftwareUserComposedInf), Encoding.UTF8, "application/json");
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


    }
}
