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
using R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.TravelTime;
using Newtonsoft.Json.Linq;
using System.EnterpriseServices.Internal;
using R2CoreTransportationAndLoadNotification.TransportTariffs;
using R2CoreTransportationAndLoadNotification.Announcements;
using R2CoreTransportationAndLoadNotification.Turns.SequentialTurns;

namespace APITransportation.Models
{
    public class APITransportationSessionIdSoftwareUserId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
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
            public Int64 TruckId;
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

        public class APITransportationSessionIdTruckIdSequentialTurnId
        {
            public String SessionId;
            public Int64 TruckId;
            public Int64 SequentialTurnId;
        }

        public class APITransportationSessionIdTruckIdSequentialTurnIdDescription
        {
            public String SessionId;
            public Int64 TruckId;
            public Int64 SequentialTurnId;
            public String Description;
        }

        public class APITransportationSessionIdTruckIdSequentialTurnIdDateTime
        {
            public String SessionId;
            public Int64 TruckId;
            public Int64 SequentialTurnId;
            public String ShamsiDate;
            public String Time;
        }




    }

    namespace SequentialTurns
    {
        public class APITransportationSessionIdSequentialTurn
        {
            public String SessionId;
            public R2CoreTransportationAndLoadNotificationSequentialTurn RawSequentialTurn;
        }

        public class APITransportationSessionIdSequentialTurnId
        {
            public String SessionId;
            public Int64  SequentialTurnId ;
        }

        public class APITransportationSessionIdSequentialTurnIdLoaderTypeId
        {
            public String SessionId;
            public Int64 SequentialTurnId;
            public Int64 LoaderTypeId;
        }

        public class APITransportationSessionIdSequentialTurnIdAnnouncementSGId
        {
            public String SessionId;
            public Int64 SequentialTurnId;
            public Int64 AnnouncementSGId;
        }

    }

    namespace TransportCompanies
    {

        public class APITransportationSessionIdTransportCompanyId

        {
            public String SessionId;
            public Int64 TransportCompanyId;
        }

        public class APITransportationSessionIdRawTransportCompany

        {
            public String SessionId;
            public RawTransportCompany RawTransportCompany;
        }
    }

    namespace LoadingAndDischargingPlaces
    {
        public class APITransportationSessionIdLADPlaceId

        {
            public String SessionId;
            public Int64 LADPlaceId;
        }

        public class APITransportationSessionIdRawLADPlaceInf
        {
            public String SessionId;
            public RawLoadingAndDischargingPlace RawLADPlaceInf;

        }
    }

    namespace ProvincesAndCities
    {
        public class APITransportationSessionIdProvinceIdProvinceActive
        {
            public string SessionId;
            public Int64 ProvinceId;
            public bool  ProvinceActive;
        }
        public class APITransportationSessionIdCityIdCityActive
        {
            public string SessionId;
            public Int64 CityId;
            public bool  CityActive;
        }
    }

    namespace Products
    {
        public class APITransportationSessionIdProductTypeIdProductTypeActive
        {
            public string SessionId;
            public Int64 ProductTypeId;
            public bool  ProductTypeActive;
        }
        public class APITransportationSessionIdProductIdProductActive
        {
            public string SessionId;
            public Int64 ProductId;
            public bool  ProductActive;
        }
    }

    namespace FactoriesAndProductionCenters
    {
        public class APITransportationSessionIdFactoryAndProductionCenterId

        {
            public String SessionId;
            public Int64 FPCId;
        }

        public class APITransportationSessionIdRawFactoryAndProductionCenter

        {
            public String SessionId;
            public RawFactoryAndProductionCenter RawFPC;
        }


    }

    namespace LoadCapacitor
    {
        public class APITransportationSessionIdLoadId
        {
            public String SessionId;
            public Int64 LoadId;
        }

    }

    namespace LoaderTypes
    {
        public class APITransportationSessionIdLoaderTypeId
        {
            public String SessionId;
            public Int64 LoaderTypeId;
        }

    }

    namespace TravelTimes
    {
        public class APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityId
        {
            public String SessionId;
            public Int64 LoaderTypeId;
            public Int64 SourceCityId;
            public Int64 TargetCityId;
        }

        public class APITransportationSessionIdTravelTime
        {
            public String SessionId;
            public R2CoreTransportationAndLoadNotificationTravelTime TravelTime;
        }



    }

    namespace Tarrif
    {
        public class APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityIdGoodId
        {
            public String SessionId;
            public Int64 LoaderTypeId;
            public Int64 SourceCityId;
            public Int64 TargetCityId;
            public Int64 GoodId;
        }

        public class APITransportationSessionIdTariffs
        {
            public String SessionId;
            public List<R2CoreTransportationAndLoadNotificationTransportTariff> Tariffs;
        }

        public class APITransportationSessionIdTariffsWithAddPercentage
        {
            public String SessionId;
            public List<R2CoreTransportationAndLoadNotificationTransportTariff> Tariffs;
            public Double  AddPercentage;
        }

    }

    namespace Announcements
    {
        public class APITransportationSessionIdAnnouncement
        {
            public String SessionId;
            public R2CoreTransportationAndLoadNotificationAnnouncement RawAnnouncement;
        }

        public class APITransportationSessionIdAnnouncementId
        {
            public String SessionId;
            public Int64  AnnouncementId;
        }

        public class APITransportationSessionIdAnnouncementSubGroup
        {
            public String SessionId;
            public R2CoreTransportationAndLoadNotificationAnnouncementSubGroup RawAnnouncementSubGroup;
        }

        public class APITransportationSessionIdAnnouncementSubGroupId
        {
            public String SessionId;
            public Int64 AnnouncementSGId;
        }

        public class APITransportationSessionIdAnnouncementIdAnnouncementSubGroupId
        {
            public String SessionId;
            public Int64 AnnouncementId;
            public Int64 AnnouncementSGId;
        }

    }

    namespace TWS
    {
        public class APITransportationSessionIdTruckPelakTruckSerial
        {
            public String SessionId;
            public string  TruckPelak;
            public string  TruckSerial;
        }

    }
}