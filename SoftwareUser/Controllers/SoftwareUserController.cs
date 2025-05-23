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


namespace SoftwareUser.Controllers
{
    public class R2CoreAuthUserStructure
    {
        public string SessionId;
        public string Captcha;
        public string UserShenaseh;
        public string UserPassword;
    }

    public class R2CoreSessionIdRawSoftwareUserStructure
    {
        public string SessionId;
        public R2CoreRawSoftwareUserStructure RawSoftwareUser;
    }

    public class R2CoreSessionIdSoftwareUserId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
    }

    public class R2CoreSessionIdSoftwareUserIdWPGId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
        public Int64 WPGId;
        public bool WPGAccess;
    }

    public class R2CoreSessionIdSoftwareUserIdWPId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
        public Int64 WPId;
        public bool WPAccess;
    }

    public class R2CoreSessionIdSoftwareUserMobileNumber
    {
        public String SessionId;
        public string MobileNumber;
    }


    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class SoftwareUserController : ApiController
    {
        private SoftwareUserManager _InstanceSoftwareUserManager = new SoftwareUserManager();

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
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/AuthUser")]
        [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
        public HttpResponseMessage AuthUser()
        {
            try
            {
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<R2CoreAuthUserStructure>(Request.Content.ReadAsStringAsync().Result);
                InstanceSoftwareUsers.ConfirmUser(Content.SessionId, Content.UserShenaseh, Content.UserPassword, Content.Captcha);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreSessionIdStructure { SessionId = Content.SessionId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
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
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/RegisteringSoftwareUser")]
        public HttpResponseMessage RegisteringSoftwareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdRawSoftwareUserStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSoftwareUser = Content.RawSoftwareUser;

                var UserId = InstanceSession.ConfirmSession(SessionId);
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftwareUserId = InstanceSoftwareUsers.RegisteringSoftwareUser(RawSoftwareUser, UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { SoftwareUserId = SoftwareUserId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ResetSoftwareUserPassword")]
        public HttpResponseMessage ResetSoftwareUserPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(SoftwareUserId, UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SoftWareUserSecurity), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
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
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/EditSoftwareUser")]
        public HttpResponseMessage EditSoftwareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdRawSoftwareUserStructure>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSaftwareUser = Content.RawSoftwareUser;

                InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanceSoftwareUsers.EditSoftwareUser(RawSaftwareUser);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
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

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                InstanceSession.ConfirmSession(SessionId);

                var SMSOwnerCurrentState = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SMSOwnerCurrentState), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSMSOwnerCurrentStatus")]
        public HttpResponseMessage ChangeSMSOwnerCurrentStatus()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                var SMSOwnerCurrentState = InstanceSMSOwners.ChangeSMSOwnerCurrentState(SoftwareUserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SMSOwnerCurrentState), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ActivateSMSOwner")]
        public HttpResponseMessage ActivateSMSOwner()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(SoftwareUserId, UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftWareUser")]
        public HttpResponseMessage GetSoftWareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserMobileNumber>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserMobileNumber = Content.MobileNumber;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var WantedSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(new R2CoreSoftwareUserMobile(SoftwareUserMobileNumber));
                var RawSoftwareUser = new R2CoreRawSoftwareUserStructure { UserId = WantedSoftwareUser.UserId, UserName = WantedSoftwareUser.UserName, MobileNumber = WantedSoftwareUser.MobileNumber, UserTypeId = WantedSoftwareUser.UserTypeId };

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(RawSoftwareUser), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessGroupAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserIdWPGId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPGId = Content.WPGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreSessionIdSoftwareUserIdWPGId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, WPGId = WPGId, WPGAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessGroupAccess(SoftwareUserId, WPGId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetSoftwareUserWebProcessAccess")]
        public HttpResponseMessage GetSoftwareUserWebProcessAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserIdWPId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SoftwareUserId = Content.SoftwareUserId;
                var WPId = Content.WPId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreSessionIdSoftwareUserIdWPId { SessionId = SessionId, SoftwareUserId = SoftwareUserId, WPId = WPId, WPAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessAccess(SoftwareUserId, WPId) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSoftwareUserWebProcessGroupAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessGroupAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserIdWPGId>(Request.Content.ReadAsStringAsync().Result);
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
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeSoftwareUserWebProcessAccess")]
        public HttpResponseMessage ChangeSoftwareUserWebProcessAccess()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());

                var Content = JsonConvert.DeserializeObject<R2CoreSessionIdSoftwareUserIdWPId>(Request.Content.ReadAsStringAsync().Result);
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
            catch (Exception ex)
            { return _InstanceSoftwareUserManager.CreateErrorContentMessage(ex); }
        }

    }
}
