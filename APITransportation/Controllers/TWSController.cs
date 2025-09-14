using APITransportation.Models.Turn;
using APITransportation.Models.TWS;
using Newtonsoft.Json;
using PayanehClassLibrary.TurnRegisterRequest;
using PayanehClassLibrary.TWSManagement;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.Turns;
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
    public class TWSController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetTWSTruckTrace")]
        public HttpResponseMessage GetTWSTruckTrace()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckPelakTruckSerial>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckPelak = Content.TruckPelak ;
                var TruckSerial = Content.TruckSerial ;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTWS = new PayanehClassLibraryTWSManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTWS.GetTruckTrace(TruckPelak ,TruckSerial)), Encoding.UTF8, "application/json"); return response;
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
