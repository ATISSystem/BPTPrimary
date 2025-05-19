
using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2Core.SessionManagement;
using R2Core.WebProcessesManagement;
using System;

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
            var expected = "b02cd4d58a4188ff039ac33df314f948%OwUE(6D~kJA";
            //act
            var actual = (new R2CoreWebProcessesManager()).GetWebProcesses(expected);
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
