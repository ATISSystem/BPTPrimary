using APICarousels.Models;
using APICommon.Models;
using Newtonsoft.Json;
using R2Core.Carousels;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SecurityAlgorithmsManagement.Exceptions;
using R2Core.SessionManagement;
using R2Core.SQLInjectionPrevention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APICarousels.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class CarouselsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;


        public CarouselsController()
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
        [Route("api/GetCarousels")]
        public HttpResponseMessage GetCarousels([FromBody] APICarouselsSessionIdAllCarouselsFlag Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AllCarouselsFlag = Content.AllCarouselsFlag;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceCarousels.GetCarousels(AllCarouselsFlag), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SqlInjectionException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
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
        [Route("api/GetCarouselPicture")]
        public HttpResponseMessage GetCarouselPicture([FromBody] APICarouselsSessionIdCId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var CId = Content.CId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Picture = InstanceCarousels.GetCarousel(CId, User) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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
        [Route("api/CarouselRegistering")]
        public HttpResponseMessage CarouselRegistering([FromBody] APICarouselsSessionIdCarousel Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Carousel = Content.RawCarousel;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);
                InstanceCarousels.CarouselRegistering(Carousel, User);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.CarouselRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Carousel.CTitle) + ":" + Carousel.CTitle, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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
        [Route("api/CarouselEditing")]
        public HttpResponseMessage CarouselEditing([FromBody] APICarouselsSessionIdCarousel Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Carousel = Content.RawCarousel;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);
                InstanceCarousels.CarouselEditing(Carousel, User);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.CarouselEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Carousel.CId) + ":" + Carousel.CId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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
        [Route("api/CarouselDeleting")]
        public HttpResponseMessage CarouselDeleting([FromBody] APICarouselsSessionIdCId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var CId = Content.CId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);
                InstanceCarousels.CarouselDeleting(CId, User);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.CarouselDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(CId) + ":" + CId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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
        [Route("api/CarouselChangeActiveStatus")]
        public HttpResponseMessage CarouselChangeActiveStatus([FromBody] APICarouselsSessionIdCIdActiveStatus Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var CId = Content.CId;
                var ActiveStatus = Content.ActiveStatus;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);
                InstanceCarousels.CarouselChangeActiveStatus(CId, ActiveStatus, User);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreLogTypes.CarouselChangeActiveStatus, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(CId) + ":" + CId, MessageDetail2= nameof(ActiveStatus) + ":" + ActiveStatus, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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
        [Route("api/GetCarouselsForViewing")]
        public HttpResponseMessage GetCarouselsForViewing([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceCarousels = new R2CoreCarouselsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceCarousels.GetCarouselsForViewing(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
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

    public class Networking
    {
        public Networking() { }

        public string GetClientIpAddress(System.Web.HttpContext YourHttpContext)
        { return HttpContext.Current.Request.UserHostAddress; }

    }

}
