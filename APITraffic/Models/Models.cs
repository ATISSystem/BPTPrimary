using R2CoreParkingSystem.TrafficCardsManagement;
using R2CoreParkingSystem.TrafficCosts;
using R2CoreParkingSystem.TrafficGates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITraffic.Models
{
    public class APITrafficSessionIdTrafficCardNoTrafficCardTypeIdTrafficCardTempTypeId
    {
        public string SessionId;
        public string TrafficCardNo;
        public long TrafficCardTypeId;
        public Int16 TrafficCardTempTypeId;
    }

    public class APITrafficSessionIdTrafficCardNo
    {
        public string SessionId;
        public string TrafficCardNo;
    }

    public class APITrafficSessionIdRawTrafficCard
    {
        public string SessionId;
        public R2CoreParkingSystemTrafficCard RawTrafficCard;
    }

    public class APITrafficSessionIdTrafficCardTypeTitle
    {
        public string SessionId;
        public string TrafficCardTypeTitle;
    }

    public class APITrafficSessionIdRawTrafficCost
    {
        public string SessionId;
        public R2CoreParkingSystemTrafficCost RawTrafficCost;
    }

    public class APITrafficSessionIdRawTrafficCardType
    {
        public string SessionId;
        public R2CoreParkingSystemTrafficCardType RawTrafficCardType;
    }

    public class APITrafficSessionIdRawTrafficGate
    {
        public string SessionId;
        public R2CoreParkingSystemTrafficGate RawTrafficGate;
    }

    public class APITrafficSessionIdTrafficGateId
    {
        public string SessionId;
        public Int64 TrafficGateId ;
    }

    public class APITrafficSessionIdTrafficGateIdTrafficCardNoTrafficPicture
    {
        public string SessionId;
        public Int64 TrafficGateId;
        public string TrafficCardNo;
        public Byte[] TrafficPicture;
    }

    public class APITrafficSessionIdTrafficCardId
    {
        public string SessionId;
        public Int64 TrafficCardId;
    }

    
}