using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggingApplication
{
    public class DateHitInfo : BaseHitInfo
    {
        public DateHitInfo() : base() { }

        // Fields...
        private DateInfoType _HitInfoType;

        public DateInfoType HitInfoType
        {
            get { return _HitInfoType; }
            set { _HitInfoType = value; }
        }
    }
}
