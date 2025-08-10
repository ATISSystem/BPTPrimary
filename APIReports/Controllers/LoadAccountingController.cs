using APIReports.Models.LoadPermissions;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorAccounting;
using R2CoreTransportationAndLoadNotification.LoadPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APIReports.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadAccountingController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetLoadAccountingRecords")]
        public HttpResponseMessage GetLoadAccountingRecords([FromBody] APIReportsSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId ;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAccounting = new R2CoreTransportationAndLoadNotificationLoadAccountingManager(_DateTimeService); 

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoadAccounting.GetLoadAccountingRecords(LoadId ,true ), Encoding.UTF8, "application/json");
                return response;
            }
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
