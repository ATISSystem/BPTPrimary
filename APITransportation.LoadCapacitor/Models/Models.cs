using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITransportation.LoadCapacitor.Models
{
    namespace LoadCapacitor
    {
        public class APITransportationLoadCapacitorSessionIdLoadId
        {
            public String SessionId;
            public Int64 LoadId;
        }

        public class APITransportationLoadCapacitorSessionIdAnnouncementSGIdLoadStatusId
        {
            public String SessionId;
            public Int64 AnnouncementSGId;
            public Int64 LoadStatusId;
        }

        public class APITransportationLoadCapacitorSessionIdPlus8Parameters
        {
            public String SessionId;
            public Int64 TransportCompanyId;
            public Int64 AnnouncementGroupId;
            public Int64 AnnouncementSubGroupId;
            public bool Inventory;
            public string ShamsiDate;
            public Int64 LoadStatusId;
            public Int64 LoadSourceCityId;
            public Int64 LoadTargetCityId;

        }

        public class APITransportationLoadCapacitorSessionIdLoad
        {
            public String SessionId;
            public R2CoreTransportationAndLoadNotificationLoad Load;
        }

        public class APITransportationLoadCapacitorSessionIdTPTParams
        {
            public String SessionId;
            public string   TPTParams;
        }

        public class APITransportationLoadCapacitorSessionIdAnnouncementSGId
        {
            public String SessionId;
            public string AnnouncementSGId;
        }

        public class APITransportationLoadCapacitorSessionIdListofTransportTariffsParams
        {
            public String SessionId;
            public List<R2CoreTransportationAndLoadNotificationTPTParamsDetails> ListofTransportTariffsParams;
        }

        

    }
}