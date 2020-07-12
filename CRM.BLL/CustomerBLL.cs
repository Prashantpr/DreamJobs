using CRM.DAL;
using CRM.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL
{
    public class CustomerBLL
    {
        private CustomerDAL _CustomerDAL;
        public CustomerBLL()
        {
            _CustomerDAL = new CustomerDAL();
        }

        public DataSet uploadExccel(string SourceFileName, string xmlData, string UploadedBy, string ServiceID="0", string ProfileID="0")
        {
            return _CustomerDAL.uploadExccel(SourceFileName, xmlData, UploadedBy, ServiceID, ProfileID);
        }

        public DataSet getUploadedData(string SourceID, string UploadedBy)
        {
            return _CustomerDAL.getUploadedData(SourceID, UploadedBy);
        }

        public DataSet createUser(UserVM objUserVM, string UserID)
        {
            return _CustomerDAL.createUser(objUserVM, UserID);

        }
        public DataSet getCall(string UserID)
        {
            return _CustomerDAL.getCall(UserID);

        }

        public DataSet fillDropDown(string param0, string param1 = "", string param2 = "")
        {
            return _CustomerDAL.fillDropDown(param0, param1, param2);

        }

        public DataSet EndCall(TeleCallerVM objTeleCallerVM, string UserID, string userHostAddress)
        {
            return _CustomerDAL.EndCall(objTeleCallerVM, UserID, userHostAddress);

        }

        public List<CallHistory> getCallHistory(string PhoneNumber, string FromDate="", string ToDate="",string Outcome="")
        {
            return _CustomerDAL.getCallHistory(PhoneNumber,FromDate,ToDate,Outcome);

        }

        public DataSet getCallHistoryV2(string PhoneNumber, string FromDate = "", string ToDate = "", string Outcome = "", string ReportType = "", string CallID = "")
        {
            return _CustomerDAL.getCallHistoryV2(PhoneNumber, FromDate, ToDate, Outcome, ReportType,CallID);

        }

        public DataTable getAgentWiseCallStat(string FromDate = "", string ToDate = "", string UserId = "")
        {
            return _CustomerDAL.getAgentWiseCallStat(FromDate, ToDate, UserId);
        }

        public List<CallHistory> getCallBackData()
        {
            return _CustomerDAL.getCallBackData();
        }

        public TodaysStat getTodaysStat()
        {
            return _CustomerDAL.getTodaysStat();

        }

        public List<SourceMaster> getUploadHistory( string FromDate = "", string ToDate = "")
        {
            return _CustomerDAL.getUploadHistory(FromDate, ToDate);

        }

        public List<ServiceMaster> getServiceMaster()
        {
            return _CustomerDAL.getServiceMaster();
        }

        public List<DataSourceProfileMaster> getProfileMaster()
        {
            return _CustomerDAL.getProfileMaster();
        }

        public List<CampaignMapping> getCampaignMapping()
        {
            return _CustomerDAL.getCampaignMapping();
        }

        public DataSet UpdateCampaignMapping(string UserID, string ServiceID, string ProfileID)
        {
            return _CustomerDAL.UpdateCampaignMapping( UserID,  ServiceID,  ProfileID);
        }
    }
}
