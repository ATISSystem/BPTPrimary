using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.FactoriesAndProductionCenters;
using APITransportation.Models.TransportCompanies;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.SMS.SMSOwners;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
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
    public class FactoriesAndProductionCentersController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetFPCs")]
        public HttpResponseMessage GetFactoriesAndProductionCenters()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APICommonSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
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
        public HttpResponseMessage GetFactoryAndProductionCenter()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdFactoryAndProductionCenterId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceFactoriesAndProductionCenters.GetFactoryAndProductionCenter(FPCId, true )), Encoding.UTF8, "application/json");
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
        public HttpResponseMessage FactoryAndProductionCenterRegistering()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdRawFactoryAndProductionCenter>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FactoryAndProductionCenter = Content.RawFPC;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.FactoryAndProductionCenterRegister (FactoryAndProductionCenter);
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
        public HttpResponseMessage EditFactoryAndProductionCenter()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdRawFactoryAndProductionCenter>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FactoryAndProductionCenter = Content.RawFPC;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.EditFactoryAndProductionCenter(FactoryAndProductionCenter);
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
        public HttpResponseMessage DeleteFactoryAndProductionCenter()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdFactoryAndProductionCenterId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                InstanceFactoriesAndProductionCenters.DeleteFactoryAndProductionCenter(FPCId);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed ).MsgContent), Encoding.UTF8, "application/json");
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
        public HttpResponseMessage ActivateFactoryAndProductionCenterSMSOwner()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdFactoryAndProductionCenterId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(InstanceFactoriesAndProductionCenters.GetSoftwareUserIdfromFactoryAndProductionCenterId(FPCId, true));

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
        public HttpResponseMessage ResetFactoryAndProductionCenterUserPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdFactoryAndProductionCenterId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(new R2DateTimeService(),new SoftwareUserService(User.UserId ));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceFactoriesAndProductionCenters.GetSoftwareUserIdfromFactoryAndProductionCenterId(FPCId, true), User.UserId);

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
        public HttpResponseMessage FactoryAndProductionCenterChangeActiveStatus()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdFactoryAndProductionCenterId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var FPCId = Content.FPCId ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceFactoriesAndProductionCenters = new R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanceFactoriesAndProductionCenters.FactoryAndProductionCenterChangeActiveStatus(FPCId);

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
