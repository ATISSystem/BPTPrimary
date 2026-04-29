using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using System.Text;
using System.Web.Http.Cors;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.DatabaseManagement;
using R2Core.SessionManagement;
using R2Core.Devices;
using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.PredefinedMessagesManagement;
using R2Core.LoggingManagement;
using R2CoreTransportationAndLoadNotification.Logging;
using System.Web;

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class DevicesController : ApiController
    {
        private APICommon.APICommon _APICommon;
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;


        public DevicesController()
        {
            try
            {
                _APICommon = new APICommon.APICommon();
                _DateTimeService = new R2DateTimeService();
                _loggerService = new R2Core.LoggingManagement.R2CorenLogService();
                _Networking = new Networking();
            }
            catch (FileNotExistException ex)
            { _loggerService.WriteErrorLog(ex); }
            catch (Exception ex)
            { _loggerService.WriteErrorLog(ex); }
        }

        [HttpPost]
        [Route("api/GetAllDevices")]
        public HttpResponseMessage GetAllDevices([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceDevice = new R2CoreDeviceManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceDevice.GetAllDevices(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (AggregateException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (FileNotExistException ex)
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

        [HttpPost]
        [Route("api/DeviceRegistering")]
        public HttpResponseMessage DeviceRegistering([FromBody] APIKernelTasksSessionIdRawDevice Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawDevice = Content.RawDevice;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceDevice = new R2CoreDeviceManager(_DateTimeService);
                InstanceDevice.DeviceRegistering(RawDevice);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.DeviceRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawDevice.DeviceTitle) + ":" + RawDevice.DeviceTitle, MessageDetail2 = nameof(RawDevice.DeviceLocation) + ":" + RawDevice.DeviceLocation, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (AggregateException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (FileNotExistException ex)
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

        [HttpPost]
        [Route("api/DeviceEditing")]
        public HttpResponseMessage DeviceEditing([FromBody] APIKernelTasksSessionIdRawDevice Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawDevice = Content.RawDevice;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceDevice = new R2CoreDeviceManager(_DateTimeService);
                InstanceDevice.DeviceEditing(RawDevice);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.DeviceEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawDevice.DeviceId) + ":" + RawDevice.DeviceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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

        [HttpPost]
        [Route("api/DeviceDeleting")]
        public HttpResponseMessage DeviceDeleting([FromBody] APIKernelTasksSessionIdDeviceId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var DeviceId = Content.DeviceId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceDevice = new R2CoreDeviceManager(_DateTimeService);
                InstanceDevice.DeviceDeleting(DeviceId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.DeviceDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(DeviceId) + ":" + DeviceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
