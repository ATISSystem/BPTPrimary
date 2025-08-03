using APICommon.Models;
using APITransportation.LoadAllocation.Models.LoadAllocation;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.LoadAllocation;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.LoadAllocation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadAllocationController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/LoadAllocationRegisteringforTruckDriver")]
        public HttpResponseMessage LoadAllocationRegisteringforTruckDriver([FromBody] APITransportationLoadAllocationSessionIdLoadId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                InstanceLoadAllocation.LoadAllocationRegisteringForTruckDriver(User, LoadId,R2CoreTransportationAndLoadNotificationRequesters. LoadAllocationController_LoadAllocationRegisteringforTruckDriver);

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
        [Route("api/GetTruckDriverLoadAllocationsForRepriority")]
        public HttpResponseMessage GetTruckDriverLoadAllocationsForRepriority([FromBody] APICommonSessionId  Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoadAllocation.GetTruckDriverLoadAllocationsForRepriority(User), Encoding.UTF8, "application/json");
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
        [Route("api/LoadAllocationCancelling")]
        public HttpResponseMessage LoadAllocationCancelling([FromBody] APITransportationLoadAllocationSessionIdLAId  Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LAId = Content.LAId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                InstanceLoadAllocation.LoadAllocationCancelling(LAId,R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.CancelledUser ,User);
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
        [Route("api/LoadAllocationDecreasePriority")]
        public HttpResponseMessage LoadAllocationDecreasePriority([FromBody] APITransportationLoadAllocationSessionIdLAId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LAId = Content.LAId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                InstanceLoadAllocation.DecreasePriority(LAId);
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
        [Route("api/LoadAllocationIncreasePriority")]
        public HttpResponseMessage LoadAllocationIncreasePriority([FromBody] APITransportationLoadAllocationSessionIdLAId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LAId = Content.LAId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                InstanceLoadAllocation.IncreasePriority (LAId);
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
        [Route("api/GetTravelTimeforLoadAllocation")]
        public HttpResponseMessage GetTravelTimeforLoadAllocation([FromBody] APITransportationLoadAllocationSessionIdLAId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LAId = Content.LAId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAllocation = new R2CoreTransportationAndLoadNotificationLoadAllocationManager(new R2DateTimeService());
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { TravelTime = InstanceLoadAllocation.GetTravelTimeforLoadAllocation(User.UserId, LAId) } ), Encoding.UTF8, "application/json");
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
