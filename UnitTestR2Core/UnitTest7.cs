using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2CoreTransportationAndLoadNotification.TransportCompanies;
using System;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest7
    {
        [TestMethod]
        public void EditTransportCompany()
        {
            var x = new R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager();
            x.EditTransportCompany(new RawTransportCompany { TCId = 21053, TCTel = "0313472183", TCTitle = "مرتضی شاهمرادی", Active = true , EmailAddress = "e123", TCCityTitle = "", TCManagerMobileNumber = "09132243148", TCManagerNameFamily = "رضا شاهمرادی", TCOrganizationCode = "" });
        }
    }
}
