using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2Core.Carousels;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using System;

namespace UnitTestR2Core
{
    [TestClass]
    public class Carousels
    {
        [TestMethod]
        public void TestMethod1()
        {
            var z = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(),new SoftwareUserService(21));
            var x = new R2CoreCarouselsManager(new R2DateTimeService ());
            //x.CarouselsCachePreparing(z.GetSystemUser());
            var zz=x.GetCarouselsForViewing();
            var sd = 3;

        }

        [TestMethod]
        public void TestMethod2()
        {
            //var InstanceSession = new R2CoreSessionManager();
            //InstanceSession.ConfirmSession("0db914feda6176caa68a909ab54f3eb1L~(kJCXa28q~");

            var z = new R2Core.SoftwareUserManagement.R2CoreSoftwareUsersManager(new R2DateTimeService(), new SoftwareUserService(21));
            var x = new R2CoreCarouselsManager(new R2DateTimeService());
            x.CarouselsCachePreparing(z.GetSystemUser());
            var zz = x.GetCarousels(false);
            var sd = 3;

        }
        
    }
}
