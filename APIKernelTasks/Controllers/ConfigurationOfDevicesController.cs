using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.ConfigurationOfDevices;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.Devices;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
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
    public class ConfigurationOfDevicesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;


        public ConfigurationOfDevicesController()
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
        [Route("api/GetAllConfigurationOfDevices")]
        public HttpResponseMessage GetAllConfigurationOfDevices([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfDevices = new R2CoreConfigurationOfDevicesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurationOfDevices.GetAllConfigurationOfDevices(true), Encoding.UTF8, "application/json");
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
        [Route("api/ConfigurationOfDeviceEditing")]
        public HttpResponseMessage ConfigurationOfDeviceEditing([FromBody] APIKernelTasksSessionIdRawConfigurationOfDevice Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfDevice = Content.RawConfigurationOfDevice;

                var User = InstanceSession.ConfirmSession(SessionId);
                var InstanceConfigurationOfDevices = new R2CoreConfigurationOfDevicesManager(_DateTimeService);
                InstanceConfigurationOfDevices.ConfigurationOfDeviceEditing(RawConfigurationOfDevice);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ConfigurationOfDeviceEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawConfigurationOfDevice.CODId) + ":" + RawConfigurationOfDevice.CODId, MessageDetail2 = nameof(RawConfigurationOfDevice.CODIndex) + ":" + RawConfigurationOfDevice.CODIndex, MessageDetail3 = nameof(RawConfigurationOfDevice.DeviceId) + ":" + RawConfigurationOfDevice.DeviceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/ConfigurationOfDeviceRegistering")]
        public HttpResponseMessage ConfigurationOfDeviceRegistering([FromBody] APIKernelTasksSessionIdRawConfigurationOfDevice Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfDevice = Content.RawConfigurationOfDevice;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfDevices = new R2CoreConfigurationOfDevicesManager(_DateTimeService);
                InstanceConfigurationOfDevices.ConfigurationOfDeviceRegistering(RawConfigurationOfDevice);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ConfigurationOfDeviceRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawConfigurationOfDevice.CODId) + ":" + RawConfigurationOfDevice.CODId, MessageDetail2 = nameof(RawConfigurationOfDevice.CODIndex) + ":" + RawConfigurationOfDevice.CODIndex, MessageDetail3 = nameof(RawConfigurationOfDevice.DeviceId) + ":" + RawConfigurationOfDevice.DeviceId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/ConfigurationOfDeviceDeleting")]
        public HttpResponseMessage ConfigurationOfDeviceDeleting([FromBody] APIKernelTasksSessionIdRawConfigurationOfDevice Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawConfigurationOfDevice = Content.RawConfigurationOfDevice;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurationOfDevices = new R2CoreConfigurationOfDevicesManager(_DateTimeService);
                InstanceConfigurationOfDevices.ConfigurationOfDeviceDeleting(RawConfigurationOfDevice);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.ConfigurationOfDeviceDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawConfigurationOfDevice.CODId ) + ":" + RawConfigurationOfDevice.CODId , MessageDetail2= nameof(RawConfigurationOfDevice.CODIndex  ) + ":" + RawConfigurationOfDevice.CODIndex , MessageDetail3= nameof(RawConfigurationOfDevice.DeviceId  ) + ":" + RawConfigurationOfDevice.DeviceId , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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

    public class Networking
    {
        public Networking() { }

        public string GetClientIpAddress(System.Web.HttpContext YourHttpContext)
        { return HttpContext.Current.Request.UserHostAddress; }

    }

}
