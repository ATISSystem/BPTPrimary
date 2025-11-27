using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITransportation.LoadAllocation.Models
{
    namespace LoadAllocation
    {
        public class APITransportationLoadAllocationSessionIdLoadId
        {
            public string SessionId;
            public Int64 LoadId;
        }

        public class APITransportationLoadAllocationSessionIdLAId
        {
            public string SessionId;
            public Int64 LAId;

        }

        public class LAIdPriority
        {
            public Int64 LAId;
            public Int64 Priority;
        }

        public class APITransportationLoadAllocationSessionIdLAIdPrioritys
        {
            public string SessionId;
            public LAIdPriority[] LAIdPrioritys;
        }

        public class APITransportationLoadAllocationSessionIdLAIdLoadId
        {
            public string SessionId;
            public Int64 LAId;
            public Int64 LoadId;
        }

        public class APITransportationLoadAllocationSessionIdTruckIdTruckDriverIdLoadId
        {
            public string SessionId;
            public Int64 TruckId;
            public Int64 TruckDriverId;
            public Int64 LoadId;

        }

    }
}