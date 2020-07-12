using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Entity;

namespace CRM.WEB.Models
{
    public class DataUploadVM
    {
        public List<ServiceMaster> lstServiceMaster { get; set; }
        public List<DataSourceProfileMaster> lstDataSourceProfileMaster { get; set; }

        public System.Data.DataTable dtUploadedData { get; set; }
    }
}