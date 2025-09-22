using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Text;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.LoadAnnouncementPlaces;
using System.Web.Services.Protocols;
using R2Core.ExceptionManagement;
using R2Core.DatabaseManagement;
using R2CoreTransportationAndLoadNotification.Exceptions;
using R2Core.DateTimeProvider;
using System.Reflection;



namespace APICommonCentralSystem.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadAnnouncementPlacesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;

        public LoadAnnouncementPlacesController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpGet]
        //[HttpPost]
        [Route("api/GetLoadAnnouncementPlaces")]
        public HttpResponseMessage GetLoadAnnouncementPlaces()
        //public HttpResponseMessage GetLoadAnnouncementPlaces([FromBody] Int64 ProvinceId)
        {
            try
            {
                var InstanceLoadAnnouncementPlaces = new R2CoreTransportationAndLoadNotificationLoadAnnouncementPlacesManager();
                var LoadAnnouncementPlaces = InstanceLoadAnnouncementPlaces.GetLoadAnnouncementPlaces(0);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(LoadAnnouncementPlaces, Encoding.UTF8, "application/json");
                return response;
            }
            catch (LoadAnnouncementPlacesForThisProvinceNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }


        }

    }
}
