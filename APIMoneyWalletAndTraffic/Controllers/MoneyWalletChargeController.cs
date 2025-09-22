using APICommon.Models;
using APIMoneyWalletAndTraffic.Models;
using Newtonsoft.Json;
using R2Core.ConfigurationManagement;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.MonetaryCreditSupplySources;
using R2Core.MoneyWallet.PaymentRequests;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.AccountingManagement;
using R2CoreParkingSystem.MoneyWalletChargeManagement;
using R2CoreParkingSystem.MoneyWalletChargeManagement.Exceptions;
using R2CoreParkingSystem.MoneyWalletManagement;
using R2CoreTransportationAndLoadNotification.TerraficCardsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APIMoneyWalletAndTraffic.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class MoneyWalletChargeController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public MoneyWalletChargeController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/PaymentRequest")]
        public HttpResponseMessage PaymentRequest([FromBody] APIMoneyWalletAndTrafficSessionIdAmount Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var Amount = Content.Amount;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceMoneyWalletCharge.PaymentRequest(Amount, User.UserId), Encoding.UTF8, "application/json");
                return response;
            }
            catch (PaymentWebServiceConnectingException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/GetDefaultAmounts")]
        public HttpResponseMessage GetDefaultAmounts([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceMoneyWalletCharge.GetDefaultAmounts(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (PaymentWebServiceConnectingException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/GetMoneyWalletChargeRecords")]
        public HttpResponseMessage GetMoneyWalletChargeRecords([FromBody] APIMoneyWalletAndTrafficSessionIdMoneyWalletId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var MoneyWalletId = Content.MoneyWalletId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceMoneyWalletCharge.GetMoneyWalletChargeRecords(MoneyWalletId), Encoding.UTF8, "application/json");
                return response;
            }
            catch (PaymentWebServiceConnectingException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/GetTotalAmountofUserFunction")]
        public HttpResponseMessage GetTotalAmountofUserFunction([FromBody] APIMoneyWalletAndTrafficSessionIdDateTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var ShamsiDate1 = Content.StartDate;
                var ShamsiDate2 = Content.EndDate;
                var Time1 = Content.StartTime;
                var Time2 = Content.EndTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Total = InstanceMoneyWalletCharge.GetTotalAmountofUserFunction(User.UserId, ShamsiDate1, ShamsiDate2, Time1, Time2) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (PaymentWebServiceConnectingException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/GetUserChargingFunction")]
        public HttpResponseMessage GetUserChargingFunction([FromBody] APIMoneyWalletAndTrafficSessionIdDateTime Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var ShamsiDate1 = Content.StartDate;
                var ShamsiDate2 = Content.EndDate;
                var Time1 = Content.StartTime;
                var Time2 = Content.EndTime;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceMoneyWalletCharge.GetUserChargeFunction(User.UserId, ShamsiDate1, ShamsiDate2, Time1, Time2), Encoding.UTF8, "application/json");
                return response;
            }
            catch (PaymentWebServiceConnectingException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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

        //این متد مخصوص آقای پرداخت است
        public async Task OnGetCallbackasync(string transid, string cardnumber, string tracking_number)
        {
            try
            {
                Int64 MonetarySupplySource = R2CoreMonetaryCreditSupplySources.AqayepardakhtPaymentGate;
                string Authority = transid;

                var InstanceTrafficCards = new R2CoreTransportationAndLoadNotificationInstanceTerraficCardsManager();
                var InstanceMoneyWallets = new R2CoreParkingSystemInstanceMoneyWalletManager();
                var InstanceMoneyWalletCharge = new R2CoreParkingSystemInstanceMoneyWalletChargeManager(_DateTimeService);
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);

                if (Authority != "" && Authority != null)
                {
                    var WS = new R2Core.R2PrimaryWS.R2PrimaryWebService();
                    long PayId = long.MinValue;
                    PayId = WS.WebMethodVerificationRequest(R2CoreMonetaryCreditSupplySources.AqayepardakhtPaymentGate, Authority, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword));

                    var InstancePaymentRequests = new R2CoreInstansePaymentRequestsManager(_DateTimeService);
                    var NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId);
                    while ((NSSPaymentRequest.RefId == string.Empty) & (NSSPaymentRequest.VerificationErrors == string.Empty))
                    { System.Threading.Thread.Sleep(500); NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId); }
                    if (NSSPaymentRequest.RefId != string.Empty)
                    {
                        var InstanceAES = new AESAlgorithmsManager();
                        var InstanceConfiguration = new R2CoreInstanceConfigurationManager(_DateTimeService);
                        var NSSSoftwareUser = InstanceSoftwareUsers.GetNSSUser(NSSPaymentRequest.SoftwareUserId);
                        var NSSTrafficCard = InstanceTrafficCards.GetNSSTerafficCard(NSSSoftwareUser);
                        Int64 CurrentCharge = InstanceMoneyWallets.GetMoneyWalletCharge(NSSTrafficCard);
                        InstanceMoneyWallets.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.AddMoney, NSSPaymentRequest.Amount, R2CoreParkingSystemAccountings.ChargeType, NSSSoftwareUser);


                        if ((NSSPaymentRequest.Amount == 200000) || (NSSPaymentRequest.Amount == 300000))
                        { InstanceMoneyWalletCharge.SabtCharge(new R2StandardMoneyWalletChargeStructure(NSSTrafficCard, NSSPaymentRequest.Amount, InstanceSoftwareUsers.GetNSSSelfGoverningChargingSoftwareUser().UserId, "", _DateTimeService.GetCurrentDateTimeMilladi(), _DateTimeService.GetCurrentShamsiDate(), NSSPaymentRequest.Amount + CurrentCharge, 0, _DateTimeService.GetCurrentTime())); }
                        else
                        { InstanceMoneyWalletCharge.SabtCharge(new R2StandardMoneyWalletChargeStructure(NSSTrafficCard, NSSPaymentRequest.Amount, InstanceSoftwareUsers.GetNSSSystemUser().UserId, "", _DateTimeService.GetCurrentDateTimeMilladi(), _DateTimeService.GetCurrentShamsiDate(), NSSPaymentRequest.Amount + CurrentCharge, 0, _DateTimeService.GetCurrentTime())); }

                        Int64 LastCharge = InstanceMoneyWallets.GetMoneyWalletCharge(NSSTrafficCard);
                        //ViewBag.IsSuccess = true; ViewBag.RefId = NSSPaymentRequest.RefId;
                        //ViewBag.Message1 = NSSTrafficCard.CardNo + "  شاخص کیف پول ";
                        //ViewBag.Message2 = CurrentCharge.ToString() + "  موجودی قبلی ";
                        //ViewBag.Message3 = NSSPaymentRequest.Amount.ToString() + "  مبلغ شارژ ";
                        //ViewBag.Message4 = LastCharge.ToString() + "  موجودی نهایی ";
                    }
                    else
                    { /*ViewBag.IsSuccess = false; ViewBag.Message = NSSPaymentRequest.VerificationErrors;*/ }
                }
                else
                {/* ViewBag.IsSuccess = false; ViewBag.Message = "Invalid Input";*/ }
            }
            catch (Exception ex)
            { /*ViewBag.IsSuccess = false; ViewBag.Message = ex.Message;*/ }
            return;
        }

        //public ActionResult PaymentVerification()
        //{

        //    try
        //    {
        //        //تشخیص درگاه پرداخت و منبع تامین اعتبار
        //        Int64 MonetarySupplySource = R2CoreMonetaryCreditSupplySources.None;
        //        string Authority = string.Empty;
        //        try
        //        {//زرین پال
        //            if (Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
        //            { MonetarySupplySource = R2CoreMonetaryCreditSupplySources.ZarrinPalPaymentGate; Authority = Request.QueryString["Authority"]; }
        //        }
        //        catch (Exception ex) { }
        //        try
        //        {//شپا
        //            if (Request.QueryString["token"] != "" && Request.QueryString["token"] != null)
        //            { MonetarySupplySource = R2CoreMonetaryCreditSupplySources.ShepaPaymentGate; Authority = Request.QueryString["token"]; }
        //        }
        //        catch (Exception ex) { }

        //        if (MonetarySupplySource == R2CoreMonetaryCreditSupplySources.None)
        //        { throw new WebApiClientPaymentVerificationException("PaymentVerificationLocation1"); }

        //        //تایید اعتبار کلاینت
        //        WebAPi.AuthenticateClientPaymentVerification(Request, Authority);
        //        var InstanceTrafficCards = new R2CoreTransportationAndLoadNotificationInstanceTerraficCardsManager();
        //        var InstanceMoneyWallets = new R2CoreParkingSystemInstanceMoneyWalletManager();
        //        var InstanceMoneyWalletCharge = new R2CoreParkingSystemInstanceMoneyWalletChargeManager();
        //        var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager();

        //        ViewBag.Title = "سامانه آتیس";
        //        if (Authority != "" && Authority != null)
        //        {
        //            var WS = new R2Core.R2PrimaryWS.R2PrimaryWebService();
        //            long PayId = long.MinValue;
        //            if (MonetarySupplySource == R2CoreMonetaryCreditSupplySources.ZarrinPalPaymentGate)
        //            { PayId = WS.WebMethodVerificationRequest(R2CoreMonetaryCreditSupplySources.ZarrinPalPaymentGate, Authority, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword)); }
        //            else if (MonetarySupplySource == R2CoreMonetaryCreditSupplySources.ShepaPaymentGate)
        //            { PayId = WS.WebMethodVerificationRequest(R2CoreMonetaryCreditSupplySources.ShepaPaymentGate, Authority, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword)); }
        //            else if (MonetarySupplySource == R2CoreMonetaryCreditSupplySources.AqayepardakhtPaymentGate)
        //            { PayId = WS.WebMethodVerificationRequest(R2CoreMonetaryCreditSupplySources.AqayepardakhtPaymentGate, Authority, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword)); }
        //            else { throw new WebApiClientPaymentVerificationException("PaymentVerificationLocation2"); }

        //            var InstancePaymentRequests = new R2CoreInstansePaymentRequestsManager();
        //            var NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId);
        //            while ((NSSPaymentRequest.RefId == string.Empty) & (NSSPaymentRequest.VerificationErrors == string.Empty))
        //            { System.Threading.Thread.Sleep(500); NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId); }
        //            if (NSSPaymentRequest.RefId != string.Empty)
        //            {
        //                var InstanceAES = new AESAlgorithmsManager();
        //                var InstanceConfiguration = new R2CoreInstanceConfigurationManager();
        //                var NSSSoftwareUser = InstanceSoftwareUsers.GetNSSUser(NSSPaymentRequest.SoftwareUserId);
        //                var NSSTrafficCard = InstanceTrafficCards.GetNSSTerafficCard(NSSSoftwareUser);
        //                Int64 CurrentCharge = InstanceMoneyWallets.GetMoneyWalletCharge(NSSTrafficCard);
        //                InstanceMoneyWallets.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.AddMoney, NSSPaymentRequest.Amount, R2CoreParkingSystemAccountings.ChargeType, NSSSoftwareUser);


        //                if ((NSSPaymentRequest.Amount == 1000000))
        //                { InstanceMoneyWalletCharge.SabtCharge(new R2StandardMoneyWalletChargeStructure(NSSTrafficCard, NSSPaymentRequest.Amount, InstanceSoftwareUsers.GetNSSSelfGoverningChargingSoftwareUser().UserId, "", _R2DateTime.GetCurrentDateTimeMilladi(), _R2DateTime.GetCurrentShamsiDate(), NSSPaymentRequest.Amount + CurrentCharge, 0, _R2DateTime.GetCurrentTime())); }
        //                else
        //                { InstanceMoneyWalletCharge.SabtCharge(new R2StandardMoneyWalletChargeStructure(NSSTrafficCard, NSSPaymentRequest.Amount, InstanceSoftwareUsers.GetNSSSystemUser().UserId, "", _R2DateTime.GetCurrentDateTimeMilladi(), _R2DateTime.GetCurrentShamsiDate(), NSSPaymentRequest.Amount + CurrentCharge, 0, _R2DateTime.GetCurrentTime())); }

        //                //{ InstanceMoneyWalletCharge.SabtCharge(new R2StandardMoneyWalletChargeStructure(NSSTrafficCard, NSSPaymentRequest.Amount, InstanceSoftwareUsers.GetNSSSystemUser().UserId, "", _R2DateTime.GetCurrentDateTimeMilladi(), _R2DateTime.GetCurrentShamsiDate(), NSSPaymentRequest.Amount + CurrentCharge, 0, _R2DateTime.GetCurrentTime())); }

        //                Int64 LastCharge = InstanceMoneyWallets.GetMoneyWalletCharge(NSSTrafficCard);
        //                ViewBag.IsSuccess = true; ViewBag.RefId = NSSPaymentRequest.RefId;
        //                ViewBag.Message1 = NSSTrafficCard.CardNo + "  شاخص کیف پول ";
        //                ViewBag.Message2 = CurrentCharge.ToString() + "  موجودی قبلی ";
        //                ViewBag.Message3 = NSSPaymentRequest.Amount.ToString() + "  مبلغ شارژ ";
        //                ViewBag.Message4 = LastCharge.ToString() + "  موجودی نهایی ";
        //            }
        //            else
        //            { ViewBag.IsSuccess = false; ViewBag.Message = NSSPaymentRequest.VerificationErrors; }
        //        }
        //        else
        //        { ViewBag.IsSuccess = false; ViewBag.Message = "Invalid Input"; }
        //    }
        //    catch (Exception ex)
        //    { ViewBag.IsSuccess = false; ViewBag.Message = ex.Message; }
        //    return View();
        //}

    }
}
