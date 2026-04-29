using APICommon.Models;
using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SMS.Exceptions;
using R2Core.SMS.GeneralAnnounceSMS;
using R2Core.SMS.SMSHandling;
using R2Core.SMS.SMSHandling.Exceptions;
using R2Core.SMS.SMSTypes;
using R2Core.SMS.SMSTypes.Exceptions;
using R2Core.WebProcessesManagement;
using R2CoreTransportationAndLoadNotification.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class SMSController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public SMSController()
        {
            try
            {
                _DateTimeService = new R2DateTimeService();
                _loggerService = new R2Core.LoggingManagement.R2CorenLogService();
                _Networking = new Networking();
            }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetSMSTypes")]
        public HttpResponseMessage GetSMSTypes([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceSMSTypes = new R2CoreSMSTypesManager(_DateTimeService); ;
                var SMSTypes = InstanceSMSTypes.GetSMSTypesJSON(SearchString, false);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(SMSTypes, Encoding.UTF8, "application/json");
                return response;
            }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SendGeneralAnnounceSMS")]
        public HttpResponseMessage SendGeneralAnnounceSMS([FromBody] APIKernelTasksSessionIdSoftwareUserTypeIdMessage Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var SoftwareUserTypeId = Content.SoftwareUserTypeId;
                var Message = Content.Message;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceGeneralAnnounceSMS = new R2CoreGeneralAnnounceSMSManager(_DateTimeService);
                var GAMId = InstanceGeneralAnnounceSMS.RequestGeneralAnnounceSMS(SoftwareUserTypeId, Message);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.SendGeneralAnnounceSMS, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(SoftwareUserTypeId) + ":" + SoftwareUserTypeId,MessageDetail2= nameof(Message) + ":" + Message, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { GAMId = GAMId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SMSTypeIdNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SmsSystemIsDisabledException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetProgressInformation")]
        public HttpResponseMessage GetProgressInformation([FromBody] APIKernelTasksSessionIdGAMId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();

                var SessionId = Content.SessionId;
                var GAMId = Content.GAMId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceGeneralAnnounceSMS = new R2CoreGeneralAnnounceSMSManager(_DateTimeService);
                var ProgressInf = InstanceGeneralAnnounceSMS.GetProgressInf(GAMId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(ProgressInf), Encoding.UTF8, "application/json");
                return response;
            }
            catch (CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SMSTypeIdNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SmsSystemIsDisabledException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
