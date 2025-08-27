using APITransportation.Models.LoadPermissions;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.LoadPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadPermissionsController : ApiController
    {

        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();


        [HttpPost]
        [Route("api/LoadPersmissionCancelling")]
        public HttpResponseMessage LoadPermissionCancelling([FromBody] APITransportationSessionIdLAIdDescriptionTurnResuscitautionLoadResuscitaution Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadAllocationId = Content.LAId ;
                var Description = Content.Description ;
                var TurnResuscitaution = Content.TurnResuscitaution;
                var LoadResuscitaution = Content.LoadResuscitaution;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadPermission = new R2CoreTransportationAndLoadNotificationLoadPermissionManager(_DateTimeService);
                InstanceLoadPermission.LoadPersmissionCancelling(LoadAllocationId ,Description , TurnResuscitaution, LoadResuscitaution, User);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
