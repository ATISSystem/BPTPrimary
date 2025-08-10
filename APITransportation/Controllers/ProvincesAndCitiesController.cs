using APITransportation.Models;
using APITransportation.Models.TruckDriver;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreParkingSystem.ProvincesAndCities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;
using APITransportation.Models.ProvincesAndCities;
using R2Core.PredefinedMessagesManagement;
using R2Core.ExceptionManagement;
using R2Core.DatabaseManagement;
using APICommon.Models;


namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class ProvincesAndCitiesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetCities")]
        public HttpResponseMessage GetCities()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APICommonSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceProvincesAndCities = new R2CoreParkingSystemProvincesAndCitiesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceProvincesAndCities.GetListOfCitys_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/ChangeActivateStatusOfProvince")]
        public HttpResponseMessage ChangeActivateStatusOfProvince()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdProvinceIdProvinceActive>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var ProvinceId = Content.ProvinceId;
                var ProvineActive = Content.ProvinceActive;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceProvincesAndCities = new R2CoreParkingSystemProvincesAndCitiesManager(_DateTimeService);
                InstanceProvincesAndCities.ChangeActiveStatusOfProvince(ProvinceId, ProvineActive);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/ChangeActivateStatusOfCity")]
        public HttpResponseMessage ChangeActivateStatusOfCity()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdCityIdCityActive>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var CityId = Content.CityId;
                var CityActive = Content.CityActive;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceProvincesAndCities = new R2CoreParkingSystemProvincesAndCitiesManager(_DateTimeService);
                InstanceProvincesAndCities.ChangeActiveStatusOfCity(CityId, CityActive);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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

    }
}
