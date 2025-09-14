using APITransportation.Models;
using APITransportation.Models.TravelTimes;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.PublicProc;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
using R2CoreTransportationAndLoadNotification.TravelTime;
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
    public class TravelTimesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetTravelTimes")]
        public HttpResponseMessage GetTravelTimes([FromBody] APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                Int64 LoaderTypeId = Content.LoaderTypeId == 0 ? -1 : Content.LoaderTypeId;
                Int64 SourceCityId = Content.SourceCityId == 0 ? -1 : Content.SourceCityId;
                Int64 TargetCityId = Content.TargetCityId == 0 ? -1 : Content.TargetCityId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTravelTime.GetTravelTimes(LoaderTypeId, SourceCityId, TargetCityId, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetTravelTime")]
        public HttpResponseMessage GetTravelTime([FromBody] APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId;
                var SourceCityId = Content.SourceCityId;
                var TargetCityId = Content.TargetCityId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTravelTime.GetTravelTime(LoaderTypeId, SourceCityId, TargetCityId, true)), Encoding.UTF8, "application/json");
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
        [Route("api/TravelTimeRegistering")]
        public HttpResponseMessage TravelTimeRegistering([FromBody] APITransportationSessionIdTravelTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var TravelTime = Content.TravelTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();
                InstanceTravelTime.TravelTimeRegistering(TravelTime);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TravelTimeDeleting")]
        public HttpResponseMessage TravelTimeDeleting([FromBody] APITransportationSessionIdTravelTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var TravelTime = Content.TravelTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();
                InstanceTravelTime.TravelTimeDeleteting(TravelTime);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TravelTimeEditing")]
        public HttpResponseMessage TravelTimeEditing([FromBody] APITransportationSessionIdTravelTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var TravelTime = Content.TravelTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();
                InstanceTravelTime.TravelTimeEditing(TravelTime);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TravelTimeChangeActivateStatus")]
        public HttpResponseMessage TravelTimeChangeActivateStatus([FromBody] APITransportationSessionIdTravelTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var TravelTime = Content.TravelTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTravelTime = new R2CoreTransportationAndLoadNotificationTravelTimeManager();
                InstanceTravelTime.TravelTimeChangeActivateStatus(TravelTime);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
