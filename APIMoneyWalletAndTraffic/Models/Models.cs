using R2Core.SoftwareUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMoneyWalletAndTraffic.Models
{
    public class APIMoneyWalletAndTrafficSessionIdMoneyWalletId
    {
        public string SessionId;
        public Int64 MoneyWalletId;
    }

    public class APIMoneyWalletAndTrafficSessionIdAmount
    {
        public string SessionId;
        public Int64 Amount;
    }

    public class APIMoneyWalletAndTrafficSessionIdTruckId
    {
        public string SessionId;
        public Int64 TruckId;
    }

    public class APIMoneyWalletAndTrafficSessionIdTransportCompanyId
    {
        public string SessionId;
        public Int64 TransportCompanyId;
    }

    public class APIMoneyWalletAndTrafficSessionIdDateTime
    {
        public string SessionId;
        public string StartDate;
        public string EndDate;
        public string StartTime;
        public string EndTime;
    }



}