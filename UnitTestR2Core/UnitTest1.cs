
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            var actual=(new R2CoreSessionManager()).ConfirmSession(expected);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetWebProcesses()
        {
            //arrange
            //apikey
            var expected = "b02cd4d58a4188ff039ac33df314f948%OwUE(6D~kJA"; 
            //act
            var actual = (new R2CoreWebProcessesManager()).GetWebProcesses(expected);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task RegisteringSoftwareUser()
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

    }
}
