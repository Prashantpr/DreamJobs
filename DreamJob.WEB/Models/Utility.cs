using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.WEB.Models
{
    public class Utility
    {
        public  enum VerificationStatus
        { 
        Pending=0,
            Verified=1,
            Discarded=2
        }

        public enum ViewMode
        {
            View = 1,
            Edit = 2,
            Update = 3
        }
    }
}