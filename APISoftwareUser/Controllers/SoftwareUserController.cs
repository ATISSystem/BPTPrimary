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
using R2Core.WebProcessesManagement.Exceptions;
using Swashbuckle.Swagger;
using System.Web.Services.Description;
using System.Web;
using R2Core.SoftwareUserManagement.Exceptions;
using StackExchange.Redis;
using R2Core.DateTimeProvider;
using System.Reflection;
using R2Core.SecurityAlgorithmsManagement.Exceptions;
using R2Core.LoggingManagement;
using R2Core.Caching;
using R2Core.SQLInjectionPrevention;



namespace APISoftwareUser.Controllers
{



    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class SoftwareUserController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;
        private ISoftwareUserService _SoftwareUserService;

        public SoftwareUserController()
        {
            try
            {
                _DateTimeService = new R2DateTimeService();
                _SoftwareUserService = new SoftwareUserService();
                _loggerService = new R2CorenLogService();
                _Networking = new Networking();
            }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/urlparse")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage urlparse()
        {
            try
            {
                var x = Request.Headers.GetCookies();
                throw new Exception(x[0]["SessionId"].Value);
            }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpGet]
        [Route("api/GetCaptcha")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage GetCaptcha()
        {
            try
            {
                var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
                var SessionStartBox = InstanceSession.StartSession();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SessionStartBox), Encoding.UTF8, "application/json");
                return response;

                //var InstanceSession = new R2Core.SessionManagement.R2CoreSessionManager();
                //var SessionStartBox = InstanceSession.StartSession();
                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                //response.Content = new StringContent(JsonConvert.SerializeObject(new { Captcha = SessionStartBox.Captcha }), Encoding.UTF8, "application/json");
                //var SessionIdCookie = new System.Net.Http.Headers.CookieHeaderValue("SessionId", SessionStartBox.SessionId)
                //{ Expires = DateTimeOffset.Now.AddDays(1), Path = "/" };
                //response.Headers.AddCookies(new[] { SessionIdCookie });
                //return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AuthUser")]
        public HttpResponseMessage AuthUser([FromBody] APISoftwareUserAuthUserStructure Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                try
                {
                    var User = InstanceSession.ConfirmSession(SessionId);
                    throw new UserHasAlreadyBeenAuthenticated();
                }
                catch (UserHasAlreadyBeenAuthenticated ex)
                { throw ex; }
                catch (Exception ex)
                { }

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
                var SoftwareUser = InstanceSoftwareUsers.ConfirmUser(Content.SessionId, Content.UserShenaseh, Content.UserPassword, Content.Captcha);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = SoftwareUser.UserName, UserId = SoftwareUser.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APICommonSessionId { SessionId = Content.SessionId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (SqlInjectionException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (CaptchaWordNotCorrectException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (FileNotExistException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (CacheTypeNotFoundException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (SoftWareUserNotFoundException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (UserIsNotActiveException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (RedisException ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
            catch (UserHasAlreadyBeenAuthenticated ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            {
                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.UserUnSuccessLogin, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Content.UserShenaseh) + ":" + Content.UserShenaseh, MessageDetail2 = nameof(Content.UserPassword) + ":" + Content.UserPassword, MessageDetail3 = nameof(ex.Message) + ":" + ex.Message });
                return _APICommon.CreateErrorContentMessage(ex);
            }
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
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
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
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetVeyUsefulWebProcesses")]
        public HttpResponseMessage GetVeyUsefulWebProcesses([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceWebProcesses = new R2CoreWebProcessesManager();
                var WebProccesses = InstanceWebProcesses.GetVeryUsefulWebProcesses(User);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(WebProccesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoftwareUserHasNotAnyVeryUsefulWebProcessPermissionException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTaskBarWebProcesses")]
        public HttpResponseMessage GetTaskBarWebProcesses([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();
                var WebProccesses = InstanceWebProcesses.GetTaskBarWebProcesses(User);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(WebProccesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoftwareUserHasNotAnyTaskBarWebProcessPermissionException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                R2CoreSoftWareUserSecurity SoftwareUserSecurity = null;
                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, _SoftwareUserService);
                var SoftwareUserId = InstanceSoftwareUsers.RegisteringSoftwareUser(RawSoftwareUser, true, User.UserId, ref SoftwareUserSecurity);
                var SoftwareUser = InstanceSoftwareUsers.GetUser(SoftwareUserId, true);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.RegisteringSoftwareUser, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUser.UserId) + ":" + SoftwareUser.UserId, MessageDetail2 = nameof(SoftwareUser.UserName) + ":" + SoftwareUser.UserName, MessageDetail3 = nameof(SoftwareUser.MobileNumber) + ":" + SoftwareUser.MobileNumber, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftwareUserSecurity), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SoftwareUserForgetPassword")]
        public HttpResponseMessage SoftwareUserForgetPassword([FromBody] APISoftwareUserSessionIdSoftwareUserMobileNumberCaptcha Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(1));
                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.SoftwareUserMobileNumber;
                var Captcha = Content.Captcha;

                InstanceSession.ConfirmSession(SessionId, Captcha);
                InstanseSoftwareUsers.SoftwareUserForgetPassword(SessionId, SoftwareUserMobileNumber);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.SoftwareUserForgetPassword, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SessionId) + ":" + SessionId, MessageDetail2 = nameof(SoftwareUserMobileNumber) + ":" + SoftwareUserMobileNumber, MessageDetail3 = nameof(Captcha) + ":" + Captcha });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PleaseEnterOTPCode).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (CaptchaInvalidException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/VerifySoftwareUserOTPCode")]
        public HttpResponseMessage VerifySoftwareUserOTPCode([FromBody] APISoftwareUserSessionIdOTPCode Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                var SessionId = Content.SessionId;
                var OTPCode = Content.OTPCode;

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(1));
                InstanseSoftwareUsers.VerifySoftwareUserOTPCode(SessionId, OTPCode);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.NewUserPasswordSentSuccessfully).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (OTPCodeInvalidException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPasswordforMe(SoftwareUserId, User.UserId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ResetSoftwareUserPassword, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftWareUserSecurity), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var UserTypes = InstanceSoftwareUsers.GetSoftwareUserTypes(true);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(UserTypes, Encoding.UTF8, "application/json");
                return response;
            }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var RawSoftwareUser = Content.RawSoftwareUser;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.EditSoftwareUser(RawSoftwareUser);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.EditSoftwareUser, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawSoftwareUser.UserId) + ":" + RawSoftwareUser.UserId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.EditingSoftWareUserSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceSMSOwners = new R2CoreMClassSMSOwnersManager(_DateTimeService);

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var SMSOwnerCurrentState = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SMSOwnerCurrentState), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), _DateTimeService);
                InstanceSMSOwners.ChangeSMSOwnerCurrentState(SoftwareUserId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ChangeSMSOwnerCurrentStatus, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), _DateTimeService);
                InstanceSMSOwners.ActivateSMSOwner(SoftwareUserId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ActivateSMSOwner, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ActivateSMSOwnerSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                var InstanceSMSOwners = new R2CoreMClassSMSOwnersManager(_DateTimeService);
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var WantedSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(SoftwareUserMobileNumber));
                var RawSoftwareUser = new R2CoreRawSoftwareUserStructure { UserId = WantedSoftwareUser.UserId, UserName = WantedSoftwareUser.UserName, MobileNumber = WantedSoftwareUser.MobileNumber, UserTypeId = WantedSoftwareUser.UserTypeId, UserActive = WantedSoftwareUser.UserActive, SMSOwnerActive = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(WantedSoftwareUser).IsSendingActive };

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(RawSoftwareUser), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var SoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(Content.SoftwareUserMobileNumber));

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();

                var AllOfWebProcessGroupsWebProcesses = InstanceWebProcesses.GetAllOfWebProcessGroupsWebProcesses(SoftwareUser.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(AllOfWebProcessGroupsWebProcesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.PGId;
                var WPGAccess = Content.PGAccess;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId, WPGAccess);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ChangeSoftwareUserWebProcessGroupAccess, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, MessageDetail2 = nameof(WPGId) + ":" + WPGId, MessageDetail3 = nameof(WPGAccess) + ":" + WPGAccess, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ChangeSoftwareUserWebProcessGroupAccessSuccessed).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.PId;
                var WPAccess = Content.PAccess;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(User.UserId));
                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessAccess(SoftwareUserId, WPId, WPAccess);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ChangeSoftwareUserWebProcessAccess, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, MessageDetail2 = nameof(WPId) + ":" + WPId, MessageDetail3 = nameof(WPAccess) + ":" + WPAccess, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ChangeSoftwareUserWebProcessAccess).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPGId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, PGId = WPGId, PGAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

                var InstanceSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, PId = WPId, PAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessAccess(SoftwareUserId, WPId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, null);
                InstanseSoftwareUsers.SendWebSiteLink(InstanseSoftwareUsers.GetUser(SoftwareUserId, true));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.SendWebSiteLink).MsgContent }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWallet = new R2CoreMoneyWalletManager(_DateTimeService);
                var MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWallet(MoneyWalletId, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreParkingSystemSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var InstanceSoftwareUser = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftwareUserComposedInf = InstanceSoftwareUsers.GetSoftwareUserComposedInf(User);
                var RawSoftwareUser = new R2CoreRawSoftwareUserStructure { UserId = SoftwareUserComposedInf.SoftwareUserExtended.UserId, UserName = SoftwareUserComposedInf.SoftwareUserExtended.UserName, MobileNumber = SoftwareUserComposedInf.SoftwareUserExtended.MobileNumber, UserTypeId = SoftwareUserComposedInf.SoftwareUserExtended.UserTypeId, UserActive = SoftwareUserComposedInf.SoftwareUserExtended.UserActive, SMSOwnerActive = InstanceSoftwareUser.GetRawSoftwareUser(User.UserId, false).SMSOwnerActive, UserTypeTitle = SoftwareUserComposedInf.SoftwareUserExtended.SoftwareUserTypeTitle };
                var TempComposedInf = new { RawSoftwareUser = RawSoftwareUser, MoneyWallet = SoftwareUserComposedInf.MoneyWallet };

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(TempComposedInf), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoftwareUserMoneyWalletNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/CustomizationSoftwareUserPassword")]
        public HttpResponseMessage CustomizationSoftwareUserPassword([FromBody] APISoftwareUserSessionIdSoftwareUserIdOldPasswordNewPassword Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var OldPassword = Content.OldPassword;
                var NewPassword = Content.NewPassword;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreParkingSystemSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));

                var InstanceSoftwareUser = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                InstanceSoftwareUser.CustomizationSoftwareUserPassword(SoftwareUserId, OldPassword, NewPassword);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.CustomizationSoftwareUserPassword, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserId) + ":" + SoftwareUserId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (InvalidSoftWareUserOldPasswordException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoftWareUserNewPasswordOldPasswordEqualException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (UserIdNotExistException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SqlInjectionException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (PasswordStrengthRejectedException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (RedisException ex)
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

    }

    public class Networking
    {
        public Networking() { }

        public string GetClientIpAddress(System.Web.HttpContext YourHttpContext)
        { return HttpContext.Current.Request.UserHostAddress; }

    }
}
