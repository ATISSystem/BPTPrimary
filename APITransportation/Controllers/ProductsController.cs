using APICommon;
using APICommon.Models;
using APITransportation.Models;
using APITransportation.Models.Products;
using APITransportation.Models.ProvincesAndCities;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.ProvincesAndCities;
using R2CoreTransportationAndLoadNotification.Products;
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
    public class ProductsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetProducts")]
        public HttpResponseMessage GetProducts()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APICommonSessionIdSearchString>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceProducts = new R2CoreTransportationAndLoadNotificationProductsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceProducts.GetProducts_SearchIntroCharacters(SearchString, true), Encoding.UTF8, "application/json");
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
        [Route("api/ChangeActivateStatusOfProductType")]
        public HttpResponseMessage ChangeActivateStatusOfProductType()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdProductTypeIdProductTypeActive>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var ProductTypeId = Content.ProductTypeId;
                var ProductTypeActive = Content.ProductTypeActive;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceProducts = new R2CoreTransportationAndLoadNotificationProductsManager();
                InstanceProducts.ChangeActiveStatusOfProductType(ProductTypeId, ProductTypeActive);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeActivateStatusOfProduct")]
        public HttpResponseMessage ChangeActivateStatusOfProduct()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdProductIdProductActive>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var ProductId = Content.ProductId;
                var ProductActive = Content.ProductActive;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceProducts = new R2CoreTransportationAndLoadNotificationProductsManager();
                InstanceProducts.ChangeActiveStatusOfProduct(ProductId, ProductActive);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (DataBaseException ex)
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
