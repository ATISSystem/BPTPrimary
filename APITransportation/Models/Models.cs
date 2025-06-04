using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using R2Core.R2PrimaryWS;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.Trucks;
using R2CoreParkingSystem.TrafficCardsManagement;
using R2CoreTransportationAndLoadNotification.TransportCompanies;

namespace APITransportation.Models
{
    public class APITransportationSessionIdSoftwareUserId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
    }

    public class APITransportationSessionIdSearchString
    {
        public String SessionId;
        public string SearchString;
    }

    namespace TruckDriver
    {
        public class APITransportationSessionIdTruckDriverNationalCode
        {
            public string SessionId;
            public string TruckDriverNationalCode;
        }

        public class APITransportationSessionIdTruckDriverId
        {
            public string SessionId;
            public Int64 TruckDriverId;
        }

        public class APITransportationSessionIdTruckDriverIdMobileNumber
        {
            public string SessionId;
            public Int64 TruckDriverId;
            public string MobileNumber;
        }

    }

    namespace Truck
    {
        public class APITransportationSessionIdSmartCardNo
        {
            public string SessionId;
            public string SmartCardNo;
        }

        public class APITransportationSessionIdTruckId
        {
            public string SessionId;
            public Int64  TruckId;
        }

        public class APITransportationSessionIdTruckIdTruckNativenessExpireDate
        {
            public string SessionId;
            public Int64 TruckId;
            public string TruckNativenessExpireDate;
        }

        public class APITransportationSessionIdTruckIdTruckDriverIdTurnIdMoneyWalletId
        {
            public string SessionId;
            public Int64 TruckId;
            public Int64 TruckDriverId;
            public Int64 TurnId;
            public Int64 MoneyWalletId;
        }

    }

    namespace Turn
    {
        public class APITransportationSessionIdTurnId
        {
            public String SessionId;
            public Int64 TurnId;
        }

    }

    namespace TransportCompanies
    {

        public class APITransportationSessionIdTransportCompanyId

        {
            public String SessionId;
            public Int64 TransportCompanyId;
        }

        public class APITransportationSessionIdTransportCompany

        {
            public String SessionId;
            public TransportCompany TransportCompany;
        }
    }




}