using R2Core.ConfigurationOfDevices;
using R2Core.Devices;
using R2Core.GeneralConfiguration;
using R2CoreTransportationAndLoadNotification.ConfigurationOfLoadAnnouncement;
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
        public Int64  COLAId;
        public Int64 COLAIndex;
        public Int64 AnnouncementId;
        public Int64 AnnouncementSGId;
    }






}