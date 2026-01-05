using R2Core.SoftwareUserManagement;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
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
    public class APISoftwareUserSessionIdSoftwareUserMobileNumber
    {
        public String SessionId;
        public string SoftwareUserMobileNumber;
    }

    public class APISoftwareUserSessionIdOTPCode
    {
        public String SessionId;
        public string OTPCode;
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

    public class APISoftwareUserSoftwareUserMobileNumber
    {
        public string SoftwareUserMobileNumber;
    }

    public class APISoftwareUserSessionIdSoftwareUserMobileNumberCaptcha
    {
        public String SessionId;
        public string SoftwareUserMobileNumber;
        public string Captcha;
    }

    public class APISoftwareUserSessionIdSoftwareUserIdOldPasswordNewPassword
    {
        public String SessionId;
        public Int64 SoftwareUserId;
        public String OldPassword;
        public String NewPassword;
    }


}