using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.LoaderTypes;
using APITransportation.Models.ProvincesAndCities;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreTransportationAndLoadNotification.LoaderTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoaderTypeController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

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

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager();

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

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject (InstanceLoaderTypes.GetLoaderTypeBySoftwareUser(User.UserId)), Encoding.UTF8, "application/json");
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
        [Route("api/ChangeActivateStatusOfLoaderType")]
        public HttpResponseMessage ChangeActivateStatusOfLoaderType()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdLoaderTypeId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoaderTypes = new R2CoreTransportationAndLoadNotificationLoaderTypesManager(); 
                InstanceLoaderTypes.LoaderTypeChangeActivate(LoaderTypeId); 
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
