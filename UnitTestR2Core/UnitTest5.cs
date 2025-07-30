using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2Core.DateAndTimeManagement;
using R2CoreParkingSystem.MoneyWalletChargeManagement;
using System;

namespace UnitTestR2Core
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void MoneyWalletAndTraffic_PaymentRequest()
        {
            var InstanceMoneyWalletCharge = new R2CoreParkingSystemMoneyWalletChargeManager(new R2DateTimeService());
            var x = InstanceMoneyWalletCharge.PaymentRequest(1500, 21);
            var y = 3;
        }
    }
}
