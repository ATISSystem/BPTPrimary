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



namespace APISoftwareUser.Controllers
{



    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class SoftwareUserController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

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
            { return  _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AuthUser")]
        public HttpResponseMessage AuthUser()
        {
            try
            {
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APISoftwareUserAuthUserStructure>(Request.Content.ReadAsStringAsync().Result);
                InstanceSoftwareUsers.ConfirmUser(Content.SessionId, Content.UserShenaseh, Content.UserPassword, Content.Captcha);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreSessionIdStructure { SessionId = Content.SessionId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AuthUser/ISSessionLive")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage ISSessionLive()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

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
        public HttpResponseMessage GetWebProcesses()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;

                var UserId = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();
                var WebProccesses = InstanceWebProcesses.GetWebProcesses(UserId);

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
        public HttpResponseMessage RegisteringSoftwareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdRawSoftwareUserStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSoftwareUser = Content.RawSoftwareUser;

                var UserId = InstanceSession.ConfirmSession(SessionId);
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftwareUserId = InstanceSoftwareUsers.RegisteringSoftwareUser(RawSoftwareUser, UserId);

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
        public HttpResponseMessage SoftwareUserForgetPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserMobileNumber>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.SoftwareUserMobileNumber;

                var UserId = InstanceSession.ConfirmSession(SessionId);

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
        public HttpResponseMessage VerifySoftwareUserVerificationCode()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserVerificationCode>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserVerificationCode = Content.VerificationCode;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
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
        public HttpResponseMessage ResetSoftwareUserPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(SoftwareUserId, UserId);

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
        public HttpResponseMessage GetUserTypes()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;

                InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
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
        public HttpResponseMessage EditSoftwareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdRawSoftwareUserStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSaftwareUser = Content.RawSoftwareUser;

                InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanceSoftwareUsers.EditSoftwareUser(RawSaftwareUser);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSMSOwnerCurrentStatus")]
        public HttpResponseMessage GetSMSOwnerCurrentStatus()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceSMSOwners = new R2CoreMClassSMSOwnersManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                InstanceSession.ConfirmSession(SessionId);

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
        public HttpResponseMessage ChangeSMSOwnerCurrentStatus()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                var SMSOwnerCurrentState = InstanceSMSOwners.ChangeSMSOwnerCurrentState(SoftwareUserId);

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
        [Route("api/ActivateSMSOwner")]
        public HttpResponseMessage ActivateSMSOwner()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(SoftwareUserId, UserId);

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
        [Route("api/GetSoftWareUser")]
        public HttpResponseMessage GetSoftWareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserMobileNumber>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.SoftwareUserMobileNumber;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var WantedSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(SoftwareUserMobileNumber));
                var RawSoftwareUser = new R2CoreRawSoftwareUserStructure { UserId = WantedSoftwareUser.UserId, UserName = WantedSoftwareUser.UserName, MobileNumber = WantedSoftwareUser.MobileNumber, UserTypeId = WantedSoftwareUser.UserTypeId };

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
        [Route("api/GetSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessGroupAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserIdWPGId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.WPGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPGId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, WPGId = WPGId, WPGAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserWebProcessAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserIdWPId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.WPId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new APISoftwareUserSessionIdSoftwareUserIdWPId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, WPId = WPId, WPAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessAccess(SoftwareUserId, WPId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessGroupAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserIdWPGId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.WPGId;
                var WPGAess = Content.WPGAccess;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId, WPGAess);

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
        [Route("api/ChangeSoftwareUserWebProcessAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<APISoftwareUserSessionIdSoftwareUserIdWPId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.WPId;
                var WPAess = Content.WPAccess;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                InstanceSoftwareUsers.ChangeSoftwareUserWebProcessAccess(SoftwareUserId, WPId, WPAess);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
