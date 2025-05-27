
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.MobileProcessesManagement.Exceptions;
using R2Core.SessionManagement;
using R2Core.WebProcessesManagement;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding.Binders;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StartSession__null__SessionIDCaptcha()
        {
            //arrange
            var expected = "b02cd4d58a4188ff039ac33df314f948%OwUE(6D~kJA";
            //act
            var actual = (new R2CoreSessionManager()).StartSession();
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConfirmSession()
        {
            //arrange
            var expected = "b02cd4d58a4188ff039ac33df314f948%OwUE(6D~kJA";
            //act
            var actual = (new R2CoreSessionManager()).ConfirmSession(expected);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetWebProcesses()
        {
            //arrange
            //apikey
            var expected = 21;
            //act
            var InstanceSession = new R2CoreSessionManager();
            //var UserId = InstanceSession.ConfirmSession("");

            var actual = (new R2CoreWebProcessesManager()).GetWebProcesses(expected);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task RegisteringSoftwareUserhttp()
        {

            HttpClient _HttpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Http://127.0.0.1:81/api/RegisteringSoftwareUser");
            request.Content = new StringContent(JsonConvert.SerializeObject("167d6b15e0d99b92649080538b582f10r~jfYpjA0Pxe;123"), Encoding.UTF8, "application/json");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpResponseMessage response = await _HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var myTruckDriver = JsonConvert.DeserializeObject<string>(content);
            }
            else
            {
                var x = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
            }

        }

        [TestMethod]
        public void RegisteringSoftwareUser()
        {
            try
            {
                var Instance = new R2Core.SoftwareUserManagement.R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var x = Instance.RegisteringSoftwareUser(new R2Core.SoftwareUserManagement.R2CoreRawSoftwareUserStructure() { UserId = 0, MobileNumber = "09138031812", UserName = "خسروی", UserTypeId = 1 }, 21);
                Console.WriteLine(x);
            }
            catch (DataBaseException ex)
            { var x = ex.Message; }
            catch (Exception ex)
            { }
        }


        [TestMethod]
        public void ResetSoftwareUserPassword()
        {
            try
            {
                var Instance = new R2Core.SoftwareUserManagement.R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                Instance.ResetSoftwareUserPassword(30,21);
                
            }
            catch (DataBaseException ex)
            { var x = ex.Message; }
            catch (Exception ex)
            { }
        }

        [TestMethod]
        public void ChangeSMSOwnerCurrentState()
        {
            try
            {
                var Instance = new R2CoreParkingSystem.SMS.SMSOwners.R2CoreParkingSystemMClassSMSOwnersManager(new R2Core.SoftwareUserManagement.SoftwareUserService(21) ,new  R2DateTimeService() );
               var x= Instance.ChangeSMSOwnerCurrentState(21);
                var d = 12;
            }
            catch (DataBaseException ex)
            { var x = ex.Message; }
            catch (Exception ex)
            { }
        }

        [TestMethod]
        public void ActivateSMSOwner()
        {
            try
            {
                var Instance = new R2CoreParkingSystem.SMS.SMSOwners.R2CoreParkingSystemMClassSMSOwnersManager(new R2Core.SoftwareUserManagement.SoftwareUserService(21), new R2DateTimeService());
                Instance.ActivateSMSOwner (21,21);
                var d = 12;
            }
            catch (DataBaseException ex)
            { var x = ex.Message; }
            catch (Exception ex)
            { }
        }

        [TestMethod]
        public void RmtoWebServiceTest()
        {
            try
            {
                R2CoreTransportationAndLoadNotification.Rmto.RmtoWebService.GetNSSTruckDriver("5759871382");
            }
            catch (DataBaseException ex)
            { var x = ex.Message; }
            catch (Exception ex)
            { }
        }


    }
}
