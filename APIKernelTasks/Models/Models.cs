using R2Core.ConfigurationOfDevices;
using R2Core.Devices;
using R2Core.GeneralConfiguration;
using R2Core.SMS.SMSHandling;
using R2CoreTransportationAndLoadNotification.ConfigurationOfLoadAnnouncement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace APIKernelTasks.Models
{
    public class APIKernelTasksSessionIdRawGeneralConfiguration
    {
        public String SessionId;
        public R2CoreRawGeneralConfiguration RawGeneralConfiguration;
    }

    public class APIKernelTasksSessionIdRawDevice
    {
        public String SessionId;
        public R2CoreRawDevice RawDevice;
    }

    public class APIKernelTasksSessionIdDeviceId
    {
        public String SessionId;
        public Int64 DeviceId;
    }

    public class APIKernelTasksSessionIdRawConfigurationOfDevice
    {
        public String SessionId;
        public R2CoreRawConfigurationOfDevice RawConfigurationOfDevice;
    }

    public class APIKernelTasksSessionIdRawConfigurationOfLoadAnnouncement
    {
        public String SessionId;
        public R2CoreTransportationAndLoadNotificationRawConfigurationOfLoadAnnouncement RawConfigurationOfLoadAnnouncement;
    }

    public class APIKernelTasksSessionIdCOLAIdCOLAIndexAnnouncementIdAnnouncementSGId
    {
        public String SessionId;
        public Int64 COLAId;
        public Int64 COLAIndex;
        public Int64 AnnouncementId;
        public Int64 AnnouncementSGId;
    }

    public class APIKernelTasksSessionIdRawLoadViewCondition
    {
        public String SessionId;
        public R2CoreTransportationAndLoadNotificationLoadViewCondition RawLoadViewCondition;
    }

    public class APIKernelTasksSessionIdLoadViewConditionId
    {
        public String SessionId;
        public Int64 LoadViewConditionId;
    }

    public class APIKernelTasksSessionIdRawLoadAllocationCondition
    {
        public String SessionId;
        public R2CoreTransportationAndLoadNotificationLoadAllocationCondition RawLoadAllocationCondition;
    }

    public class APIKernelTasksSessionIdLoadAllocationConditionId
    {
        public String SessionId;
        public Int64 LoadAllocationConditionId;
    }

    public class APIKernelTasksSessionIdSoftwareUserTypeIdMessage
    {
        public String SessionId;
        public Int64 SoftwareUserTypeId;
        public String Message;

    }

    public class APIKernelTasksSessionIdGAMId
    {
        public String SessionId;
        public string GAMId;
    }




}