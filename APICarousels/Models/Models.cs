using R2Core.Carousels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICarousels.Models
{
    public class APICarouselsSessionIdAllCarouselsFlag
    {
        public String SessionId;
        public bool  AllCarouselsFlag;
    }

    public class APICarouselsSessionIdCId
    {
        public String SessionId;
        public Int64  CId;
    }

    public class APICarouselsSessionIdCarousel
    {
        public String SessionId;
        public R2CoreCarousel  RawCarousel;
    }

    public class APICarouselsSessionIdCIdActiveStatus
    {
        public String SessionId;
        public Int64 CId;
        public bool ActiveStatus;
    }


}