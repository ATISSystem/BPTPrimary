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


namespace SoftwareUser.Controllers
{
    public class SoftwareUserController : ApiController
    {
        [HttpPost]
        [Route("api/GetCaptcha")]
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
            { throw ex; }
        }

        [HttpPost]
        [Route("api/AuthUser")]
        public HttpResponseMessage AuthUser()
        {
            try
            {
                var InstanceSoftwareUsers = new R2Core.SoftwareUserManagement.R2CoreInstanseSoftwareUsersManager();
                var Content = JsonConvert.DeserializeObject<string>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.Split(';')[0];
                var Captcha = Content.Split(';')[1];
                var UserShenaseh = Content.Split(';')[2];
                var UserPassword = Content.Split(';')[3];

                InstanceSoftwareUsers.ConfirmUser(SessionId, UserShenaseh, UserPassword, Captcha);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(SessionId), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { throw ex; }
        }

        [HttpPost]
        [Route("api/GetWebProcesses")]
        public HttpResponseMessage GetWebProcesses()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<string>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.Split(';')[0];

                var APIKey = InstanceSession.ConfirmSession(SessionId);
                var InstanceWebProcesses = new R2CoreWebProcessesManager();
                var WebProccesses = InstanceWebProcesses.GetWebProcesses(APIKey);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(WebProccesses, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { throw ex; }
        }

        [HttpPost]
        [Route("api/RegisteringSoftwareUser")]
        public HttpResponseMessage RegisteringSoftwareUser()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<string>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.Split(';')[0];
                var RawSaftwareUser = JsonConvert.DeserializeObject<R2CoreRawSoftwareUser>(Content.Split(';')[1]);

                var APIKey = InstanceSession.ConfirmSession(SessionId);
                var InstanceSoftwareUsers = new R2Core.SoftwareUserManagement.R2CoreInstanseSoftwareUsersManager();
                var UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(RawSaftwareUser, APIKey);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(UserId.ToString() , Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { throw ex; }
        }

        
    }
}
