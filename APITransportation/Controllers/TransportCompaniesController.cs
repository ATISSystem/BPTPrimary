using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.TransportCompanies;
using APITransportation.Models.Truck;
using APITransportation.Models.TruckDriver;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
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
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetTransportCompanies")]
        public HttpResponseMessage GetTransportCompanies([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTransportCompanies.GetTransportCompanies_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetTransportCompany")]
        public HttpResponseMessage GetTransportCompany([FromBody] APITransportationSessionIdTransportCompanyId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var TCId = Content.TransportCompanyId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTransportCompanies.GetTransportCompany(TCId, true)), Encoding.UTF8, "application/json");
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
        [Route("api/GetTransportCompanyfromSoftwareUser")]
        public HttpResponseMessage GetTransportCompanyfromSoftwareUser([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService );

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject( InstanceTransportCompanies.GetTransportCompanyfromSoftwareUser(User, false)), Encoding.UTF8, "application/json");
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
        [Route("api/EditTransportCompany")]
        public HttpResponseMessage EditTransportCompany([FromBody] APITransportationSessionIdRawTransportCompany Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var TC = Content.RawTransportCompany;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);
                InstanceTransportCompanies.EditTransportCompany(TC);
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
        [Route("api/ActivateTransportCompanySMSOwner")]
        public HttpResponseMessage ActivateTransportCompanySMSOwner([FromBody] APITransportationSessionIdTransportCompanyId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);
                var InstanceSMSOwners = new R2CoreParkingSystemSMSOwnersManager(new SoftwareUserService(User.UserId), _DateTimeService);
                InstanceSMSOwners.ActivateSMSOwner(InstanceTransportCompanies.GetSoftwareUserIdfromTransportCompanyId(TransportCompanyId, true));

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
        [Route("api/ResetTransportCompanyUserPassword")]
        public HttpResponseMessage ResetTransportCompanyUserPassword([FromBody] APITransportationSessionIdTransportCompanyId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);
                var InstanseSoftwareUsers = new R2CoreSoftwareUsersManager(_DateTimeService, new SoftwareUserService(User.UserId));
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceTransportCompanies.GetSoftwareUserIdfromTransportCompanyId(TransportCompanyId, true), User.UserId);

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
        [Route("api/TransportCompanyChangeActiveStatus")]
        public HttpResponseMessage TransportCompanyChangeActiveStatus([FromBody] APITransportationSessionIdTransportCompanyId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var TransportCompanyId = Content.TransportCompanyId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportCompanies = new R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService);
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                InstanceTransportCompanies.TransportCompanyChangeActiveStatus(TransportCompanyId);

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
