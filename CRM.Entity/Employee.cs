using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entity
{
    public class Employee
    {
        public Int64? EmpID { get; set; }

        public string EmpName { get; set; }

        public string EmpTLId { get; set; }

        public string EmpID_N { get; set; }
        public string Martialstatus { get; set; }
        public string EmpDOJ { get; set; }
        public string EmpDOB { get; set; }
        public string EmpDOA { get; set; }
        public string EmpToday { get; set; }

        public string EmpPhoto { get; set; }

        public string EmpContactNo { get; set; }

        public string OfficialMailId { get; set; }

        public string PersonalEmailId { get; set; }

        public string EmpGender { get; set; }

        public string SalesOrgID { get; set; }


        public EmpImageCapture ImageInfo { get; set; }


        public string EmpQualification { get; set; }

        public string LastLogin { get; set; }
        public string ESSRegistrationDate { get; set; }

        public int EmpNoticePeriod { get; set; }
    }
}
