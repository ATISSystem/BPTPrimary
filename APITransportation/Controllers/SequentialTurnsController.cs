using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.LoaderTypes;
using APITransportation.Models.SequentialTurns;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreTransportationAndLoadNotification.Turns.SequentialTurns;
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
    public class SequentialTurnsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetSequentialTurns")]
        public HttpResponseMessage GetSequentialTurns()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APICommonSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceSequentialTurns.GetSequentialTurns(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetSequentialTurnsByLoaderType")]
        public HttpResponseMessage GetSequentialTurnsByLoaderType()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdLoaderTypeId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var LoaderTypeId = Content.LoaderTypeId ;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceSequentialTurns.GetSequentialTurnsByLoaderTypeId(LoaderTypeId, true), Encoding.UTF8, "application/json");
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
        [Route("api/SequentialTurnRegistering")]
        public HttpResponseMessage SequentialTurnRegistering()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurn>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSequentialTurn = Content.RawSequentialTurn;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnRegistering(RawSequentialTurn);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnEditing")]
        public HttpResponseMessage SequentialTurnEditing()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurn>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var RawSequentialTurn = Content.RawSequentialTurn;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnEditing(RawSequentialTurn);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnDeleting")]
        public HttpResponseMessage SequentialTurnDeleting()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnDeleting(SequentialTurnId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/GetSequentialTurnsRelationLoaderTypes")]
        public HttpResponseMessage GetSequentialTurnsRelationLoaderTypes()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceSequentialTurns.GetSequentialTurnsRelationLoaderTypes(SequentialTurnId, true), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnRelationLoaderTypeDeleting")]
        public HttpResponseMessage SequentialTurnRelationLoaderTypeDeleting()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnIdLoaderTypeId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;
                var LoaderTypeId = Content.LoaderTypeId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnRelationLoaderTypeDeleting(SequentialTurnId, LoaderTypeId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnRelationLoaderTypeRegistering")]
        public HttpResponseMessage SequentialTurnRelationLoaderTypeRegistering()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnIdLoaderTypeId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;
                var LoaderTypeId = Content.LoaderTypeId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnRelationLoaderTypeRegistering(SequentialTurnId, LoaderTypeId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed ).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/GetSequentialTurnRelationAnnouncementSubGroups")]
        public HttpResponseMessage GetSequentialTurnRelationAnnouncementSubGroups()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceSequentialTurns.GetSequentialTurnRelationAnnouncementSubGroups(SequentialTurnId,true ), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnRelationAnnouncementSubGroupDeleting")]
        public HttpResponseMessage SequentialTurnRelationAnnouncementSubGroupDeleting()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnIdAnnouncementSGId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;
                var AnnouncementSGId= Content.AnnouncementSGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnRelationAnnouncementSubGroupDeleting(SequentialTurnId, AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/SequentialTurnRelationAnnouncementSubGroupRegistering")]
        public HttpResponseMessage SequentialTurnRelationAnnouncementSubGroupRegistering()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdSequentialTurnIdAnnouncementSGId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SequentialTurnId = Content.SequentialTurnId;
                var AnnouncementSGId = Content.AnnouncementSGId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceSequentialTurns = new R2CoreTransportationAndLoadNotificationSequentialTurnsManager();
                InstanceSequentialTurns.SequentialTurnRelationAnnouncementSubGroupRegistering(SequentialTurnId, AnnouncementSGId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed ).MsgContent), Encoding.UTF8, "application/json"); return response;
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
