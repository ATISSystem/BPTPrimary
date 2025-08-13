using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace APIKernelTasks.Models
{
    public class APIKernelTasksSessionIdCId
    {
        public String SessionId;
        public Int64 CId;
    }

        public class APIKernelTasksSessionIdCIdCValue
    {
        public String SessionId;
        public Int64 CId;
        public string CValue;
    }

    public class APIKernelTasksSessionIdCIdMIdCValue
    {
        public String SessionId;
        public Int64 CId;
        public Int64 MId;
        public string CValue;
    }

    public class APIKernelTasksSessionIdCIdAnnouncementIdCValue
    {
        public String SessionId;
        public Int64 CId;
        public Int64 AnnouncementId;
        public string CValue;
    }

    

}