using APITransportation.Models;
using APITransportation.Models.TransportCompanies;
using APITransportation.Models.Truck;
using APITransportation.Models.TruckDriver;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.SMS.SMSOwners;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
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
    public class TransportCompaniesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetTransportCompanies")]
        public HttpResponseMessage GetTransportCompanies()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTransportCompanies.GetTransportCompanies_SearchIntroCharacters(SearchString,true ), Encoding.UTF8, "application/json");
                return response;
            }
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
        [Route("api/GetTransportCompany")]
        public HttpResponseMessage GetTransportCompany()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTransportCompanyId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TCId = Content.TransportCompanyId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTransportCompanies.GetTransportCompany(TCId,true )), Encoding.UTF8, "application/json");
                return response;
            }
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
        [Route("api/EditTransportCompany")]
        public HttpResponseMessage EditTransportCompany()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdRawTransportCompany>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TC = Content.RawTransportCompany;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();
                InstanceTransportCompanies.EditTransportCompany(TC);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ActivateTransportCompanySMSOwner")]
        public HttpResponseMessage ActivateTransportCompanySMSOwner()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTransportCompanyId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();
                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(InstanceTransportCompanies.GetSoftwareUserIdfromTransportCompanyId(TransportCompanyId,true ));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ResetTransportCompanyUserPassword")]
        public HttpResponseMessage ResetTransportCompanyUserPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTransportCompanyId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceTransportCompanies.GetSoftwareUserIdfromTransportCompanyId(TransportCompanyId,true ), UserId);

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
        [Route("api/TransportCompanyChangeActiveStatus")]
        public HttpResponseMessage TransportCompanyChangeActiveStatus()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTransportCompanyId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                 InstanceTransportCompanies.TransportCompanyChangeActiveStatus(TransportCompanyId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed ).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

















    }


}
