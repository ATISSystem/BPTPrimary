using APICommon.Models;
using APIMoneyWalletAndTraffic.Models;
using Newtonsoft.Json;
using PayanehClassLibrary.ConfigurationManagement;
using R2Core.ConfigurationManagement;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.MoneyWallet.MoneyWallet;
using R2Core.SessionManagement;
using R2CoreParkingSystem.AccountingManagement;
using R2CoreParkingSystem.MoneyWalletManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APIMoneyWalletAndTraffic.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class MoneyWalletController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetUserMoneyWallet")]
        public HttpResponseMessage GetUserMoneyWallet([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWallet = new R2CoreParkingSystemMoneyWalletManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWallet(User)), Encoding.UTF8, "application/json");
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
        [Route("api/GetMoneyWalletBalance")]
        public HttpResponseMessage GetMoneyWalletBalance([FromBody] APIMoneyWalletAndTrafficSessionIdMoneyWalletId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var MoneyWalletId = Content.MoneyWalletId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWallet = new R2CoreParkingSystemMoneyWalletManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Balance = InstanceMoneyWallet.GetMoneyWalletCharge(MoneyWalletId) }), Encoding.UTF8, "application/json");
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
        [Route("api/GetMoneyWalletTransactions")]
        public HttpResponseMessage GetMoneyWalletTransactions([FromBody] APIMoneyWalletAndTrafficSessionIdMoneyWalletId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var MoneyWalletId = Content.MoneyWalletId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceAccounting = new R2CoreParkingSystemAccountingManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceAccounting.GetMoneyWalletTransactions(MoneyWalletId), Encoding.UTF8, "application/json");
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
        [Route("api/GetTruckMoneyWallet")]
        public HttpResponseMessage GetTruckMoneyWallet([FromBody] APIMoneyWalletAndTrafficSessionIdTruckId Content)
        {
            try
            {
                var InstanceMoneyWallet = new R2CoreParkingSystemMoneyWalletManager(new R2DateTimeService());
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWalletfromCarId(TruckId, false)), Encoding.UTF8, "application/json");
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
        [Route("api/GetTransportCompanyMoneyWallet")]
        public HttpResponseMessage GetTransportCompanyMoneyWallet([FromBody] APIMoneyWalletAndTrafficSessionIdTransportCompanyId Content)
        {
            try
            {
                var InstanceMoneyWallet = new R2CoreParkingSystemMoneyWalletManager(new R2DateTimeService());
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TransportCompanyId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWalletfromTransportCompanyId(TruckId)), Encoding.UTF8, "application/json");
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
        [Route("api/GetTruckersAssociationMoneyWallet")]
        public HttpResponseMessage GetTruckersAssociationMoneyWallet([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceConfiguration = new R2CoreInstanceConfigurationManager();
                var InstanceMoneyWallet = new R2CoreMoneyWalletManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var MoneyWalletId = InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 4);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWallet(MoneyWalletId, false)), Encoding.UTF8, "application/json");
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
        [Route("api/GetSMSMoneyWallet")]
        public HttpResponseMessage GetSMSMoneyWallet([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceConfiguration = new R2CoreInstanceConfigurationManager();
                var InstanceMoneyWallet = new R2CoreMoneyWalletManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var MoneyWalletId = InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 8);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceMoneyWallet.GetMoneyWallet(MoneyWalletId, false)), Encoding.UTF8, "application/json");
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
    }
}