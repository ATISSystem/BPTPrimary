using APICommon;
using APITransportation.Models;
using APITransportation.Models.TruckDriver;
using APITransportation.PayanehWebService;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2Core.WebProcessesManagement;
using R2CoreParkingSystem.Drivers;
using R2CoreParkingSystem.SMS.SMSOwners;
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
    public class TruckDriverController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetTruckDriverfromRMTO")]
        public HttpResponseMessage GetTruckDriverfromRMTO()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverNationalCode>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverNationalCode = Content.TruckDriverNationalCode;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                PayanehWebService.PayanehWebService _WS = new PayanehWebService.PayanehWebService();
                var TruckDriver = _WS.WebMethodGetTruckDriverByNationalCodefromRMTO(TruckDriverNationalCode, _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(TruckDriver), Encoding.UTF8, "application/json");
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
        [Route("api/GetTruckDriverfromWebSite")]
        public HttpResponseMessage GetTruckDriverfromWebSite()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var AES = new AESAlgorithmsManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverNationalCode>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverNationalCode = Content.TruckDriverNationalCode;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTruckDrivers.GetNSSTruckDriverfromNationalCode(TruckDriverNationalCode), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TruckDriverRegisteringMobileNumber")]
        public HttpResponseMessage TruckDriverRegisteringMobileNumber()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverIdMobileNumber>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverId = Content.TruckDriverId;
                var MobileNumber = Content.MobileNumber;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                InstanceTruckDrivers.RegisteringTruckDriverMobileNumber(TruckDriverId, MobileNumber);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed ).MsgContent ), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DriverNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ActivateTruckDriverSMSOwner")]
        public HttpResponseMessage ActivateTruckDriverSMSOwner()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var InstanceSMSOwners = new R2CoreParkingSystemMClassSMSOwnersManager(new SoftwareUserService(UserId), new R2DateTimeService());
                InstanceSMSOwners.ActivateSMSOwner(InstanceTruckDrivers.GetSoftwareUserIdfromTruckDriverId(TruckDriverId));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed ).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ResetTruckDriverUserPassword")]
        public HttpResponseMessage ResetTruckDriverUserPassword()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SoftWareUserSecurity = InstanseSoftwareUsers.ResetSoftwareUserPassword(InstanceTruckDrivers.GetSoftwareUserIdfromTruckDriverId(TruckDriverId), UserId);

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
        [Route("api/SendWebSiteLink")]
        public HttpResponseMessage SendWebSiteLink()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();

                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverId = Convert.ToInt64(Content.TruckDriverId);

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var InstanseSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                InstanseSoftwareUsers.SendWebSiteLink(InstanceTruckDrivers.GetNSSSoftwareUserfromTruckDriverId(TruckDriverId));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
