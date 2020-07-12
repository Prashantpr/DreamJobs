using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entity
{
    public class CampaignMappingVM
    {
        public List<ServiceMaster> ServiceMasterList { get; set; }
        public List<DataSourceProfileMaster> DataSourceProfileMasterList { get; set; }
        public List<CampaignMapping> CampaignMappingList { get; set; }

    }

    public class CampaignMapping
    {
        public long AllocationID { get; set; }
        public User user { get; set; }
        public ServiceMaster serviceMaster { get; set; }
        public DataSourceProfileMaster dataSourceProfileMaster { get; set; }

    }
}
