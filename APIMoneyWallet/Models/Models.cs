using R2Core.SoftwareUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMoneyWallet.Models
{
    public class APIMoneyWalletSessionIdMoneyWalletId
    {
        public string SessionId;
        public Int64 MoneyWalletId;
    }

    public class APIMoneyWalletSessionIdAmount
    {
        public string SessionId;
        public Int64 Amount;
    }

    public class APIMoneyWalletSessionIdTruckId
    {
        public string SessionId;
        public Int64 TruckId;
    }

    public class APIMoneyWalletSessionIdTransportCompanyId
    {
        public string SessionId;
        public Int64 TransportCompanyId;
    }

    public class APIMoneyWalletSessionIdDateTime
    {
        public string SessionId;
        public string StartDate;
        public string EndDate;
        public string StartTime;
        public string EndTime;
    }



}