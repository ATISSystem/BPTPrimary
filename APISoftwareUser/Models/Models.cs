using R2Core.SoftwareUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APISoftwareUser.Models
{
    public class APISoftwareUserAuthUserStructure
    {
        public string SessionId;
        public string Captcha;
        public string UserShenaseh;
        public string UserPassword;
    }

    public class APISoftwareUserSessionIdRawSoftwareUserStructure
    {
        public string SessionId;
        public R2CoreRawSoftwareUserStructure RawSoftwareUser;
    }

    public class APISoftwareUserSessionIdSoftwareUserId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
    }

    public class APISoftwareUserSessionIdSoftwareUserVerificationCode
    {
        public String SessionId;
        public string VerificationCode;
    }

    public class APISoftwareUserSessionIdSoftwareUserIdWPGId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
        public Int64 PGId;
        public bool PGAccess;
    }

    public class APISoftwareUserSessionIdSoftwareUserIdWPId
    {
        public String SessionId;
        public Int64 SoftwareUserId;
        public Int64 PId;
        public bool PAccess;
    }

    public class APISoftwareUserSessionIdSoftwareUserMobileNumber
    {
        public String SessionId;
        public string SoftwareUserMobileNumber;
    }
}