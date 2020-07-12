using CRM.Entity;
using CRM.DbBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CRM.DAL
{
    public class CustomerDAL
    {
        private SqlDbBridge _SqlDbBridge;
        public CustomerDAL()
        {
            _SqlDbBridge = new SqlDbBridge();
        }

        public DataSet fillDropDown(string param0, string param1 = "", string param2="")
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@param0", param0));
                _param.Add(new SqlParameter("@param1", param1));
                _param.Add(new SqlParameter("@param2", param2));
                return _SqlDbBridge.ExecuteDataSet("proc_FillDropDown", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public DataSet uploadExccel(string SourceFileName,string xmlData, string UploadedBy,string ServiceID,string ProfileID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@SourceFileName", SourceFileName));
                _param.Add(new SqlParameter("@xmlData", xmlData));
                _param.Add(new SqlParameter("@UploadedBy", UploadedBy));
                _param.Add(new SqlParameter("@ServiceID", ServiceID));
                _param.Add(new SqlParameter("@ProfileID", ProfileID));
                return _SqlDbBridge.ExecuteDataSet("proc_ImportExcel", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            
            
        }

        public DataSet getUploadedData(string SourceID, string UploadedBy)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@SourceID", SourceID));
                _param.Add(new SqlParameter("@UploadedBy", UploadedBy));
                return _SqlDbBridge.ExecuteDataSet("proc_ImportExcel", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }


        }

        public DataSet createUser(UserVM objUserVM, string UserID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@UserName", objUserVM.user.UserName));
                _param.Add(new SqlParameter("@FullName", objUserVM.user.FullName));
                _param.Add(new SqlParameter("@Password", objUserVM.user.Password));
                _param.Add(new SqlParameter("@UserEmailAddress",(object) objUserVM.user.UserEmailAddress??DBNull.Value));
                _param.Add(new SqlParameter("@RoleId", objUserVM.userRole.RoleId));
                _param.Add(new SqlParameter("@CreatedBy", UserID));
                return _SqlDbBridge.ExecuteDataSet("proc_createUser", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public DataSet getCall(string UserID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@UserID", UserID));
                return _SqlDbBridge.ExecuteDataSet("proc_getCall", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public DataSet EndCall(TeleCallerVM objTeleCallerVM, string UserID, string userHostAddress)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@CustomerName", (object)objTeleCallerVM.customer.CustomerName ?? DBNull.Value));

                _param.Add(new SqlParameter("@AlternateNo",(object) objTeleCallerVM.customer.AltContactNo ?? DBNull.Value));
                _param.Add(new SqlParameter("@Call_Type", objTeleCallerVM.callHistory.Call_Type));
                
                _param.Add(new SqlParameter("@UpdatedBy", UserID));
                _param.Add(new SqlParameter("@HostName", userHostAddress));

                _param.Add(new SqlParameter("@EmailID", (object)objTeleCallerVM.customer.EmailID ?? DBNull.Value));
                _param.Add(new SqlParameter("@LoanAmount", (object)objTeleCallerVM.customer.LoanAmount ?? DBNull.Value));
                _param.Add(new SqlParameter("@NatureOfLoanID",(object) objTeleCallerVM.customer.NatureOfLoanID ?? DBNull.Value));
                _param.Add(new SqlParameter("@BalanceTransferBankID", (object)objTeleCallerVM.customer.BalanceTransferBankID ?? DBNull.Value));
                _param.Add(new SqlParameter("@BalanceTransferOtherBankName", (object)objTeleCallerVM.customer.BalanceTransferOtherBankName ?? DBNull.Value));
                _param.Add(new SqlParameter("@ResalePropertyAddress", (object)objTeleCallerVM.customer.ResalePropertyAddress ?? DBNull.Value));
                _param.Add(new SqlParameter("@ResaleAuthorityType", (object)objTeleCallerVM.customer.ResaleAuthorityType ?? DBNull.Value));
                _param.Add(new SqlParameter("@ResaleAuthorityDetails", (object)objTeleCallerVM.customer.ResaleAuthorityDetails ?? DBNull.Value));
                _param.Add(new SqlParameter("@PlotPurchaseAddress", (object)objTeleCallerVM.customer.PlotPurchaseAddress ?? DBNull.Value));
                _param.Add(new SqlParameter("@MonthOfRegistration", (object)objTeleCallerVM.customer.MonthOfRegistration ?? DBNull.Value));
                _param.Add(new SqlParameter("@AnyConstruction", (object)objTeleCallerVM.customer.AnyConstruction ?? DBNull.Value));
                _param.Add(new SqlParameter("@ConstructionApprovedMap", (object)objTeleCallerVM.customer.ConstructionApprovedMap ?? DBNull.Value));
                _param.Add(new SqlParameter("@OriginalCostOfAllotment", (object)objTeleCallerVM.customer.OriginalCostOfAllotment ?? DBNull.Value));
                _param.Add(new SqlParameter("@PropertyDetailType", (object)objTeleCallerVM.customer.PropertyDetailType ?? DBNull.Value));
                _param.Add(new SqlParameter("@BuilderName", (object)objTeleCallerVM.customer.BuilderName ?? DBNull.Value));
                _param.Add(new SqlParameter("@BuilderProjectName", (object)objTeleCallerVM.customer.BuilderProjectName ?? DBNull.Value));
                _param.Add(new SqlParameter("@BuliderProjectAddress", (object)objTeleCallerVM.customer.BuliderProjectAddress ?? DBNull.Value));
                _param.Add(new SqlParameter("@CentralGovtProjectName", (object)objTeleCallerVM.customer.CentralGovtProjectName ?? DBNull.Value));
                _param.Add(new SqlParameter("@CentralGovtProjectAddress", (object)objTeleCallerVM.customer.CentralGovtProjectAddress ?? DBNull.Value));
                _param.Add(new SqlParameter("@IndependentMapApproved", (object)objTeleCallerVM.customer.IndependentMapApproved ?? DBNull.Value));
                _param.Add(new SqlParameter("@IndependentMapApprovedType", (object)objTeleCallerVM.customer.IndependentMapApprovedType ?? DBNull.Value));
                _param.Add(new SqlParameter("@PropertyType", (object)objTeleCallerVM.customer.PropertyType ?? DBNull.Value));
                _param.Add(new SqlParameter("@DoableMarketValue", (object)objTeleCallerVM.customer.DoableMarketValue ?? DBNull.Value));
                _param.Add(new SqlParameter("@DoableRegisteredValue", (object)objTeleCallerVM.customer.DoableRegisteredValue ?? DBNull.Value));
                _param.Add(new SqlParameter("@OwnershipOfProperty", (object)objTeleCallerVM.customer.OwnershipOfProperty ?? DBNull.Value));
                _param.Add(new SqlParameter("@IncomeSource", (object)objTeleCallerVM.customer.IncomeSource ?? DBNull.Value));
                _param.Add(new SqlParameter("@SelfEmploymentType", (object)objTeleCallerVM.customer.SelfEmploymentType ?? DBNull.Value));

                _param.Add(new SqlParameter("@CSEID", objTeleCallerVM.callHistory.CSEID));
                _param.Add(new SqlParameter("@CrmID", objTeleCallerVM.customer.CrmID));
                _param.Add(new SqlParameter("@CallStartDateTime", objTeleCallerVM.callHistory.CallDateTime));
                _param.Add(new SqlParameter("@CallEndDateTime", DateTime.Now));
                _param.Add(new SqlParameter("@CalledPhoneNo", objTeleCallerVM.customer.MobileNo));
                _param.Add(new SqlParameter("@SubOutcomeID", objTeleCallerVM.callHistory.subOutcomeMaster.SubOutcomeID));
                _param.Add(new SqlParameter("@Remarks", objTeleCallerVM.callHistory.Remarks));
                _param.Add(new SqlParameter("@CallBackDateTime", objTeleCallerVM.callHistory.subOutcomeMaster.SubOutcomeID == 2 ? (object)objTeleCallerVM.callHistory.CallBackDateTime : DBNull.Value));
                _param.Add(new SqlParameter("@OutcomeID", DBNull.Value));
                _param.Add(new SqlParameter("@CallbackType", objTeleCallerVM.callHistory.subOutcomeMaster.SubOutcomeID == 2 ? (object)objTeleCallerVM.callHistory.CallbackType : DBNull.Value));
                return _SqlDbBridge.ExecuteDataSet("proc_endCall", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public List<CallHistory> getCallHistory(string PhoneNumber, string FromDate = "", string ToDate = "", string Outcome = "")
        {
            var ch1 = new List< CallHistory>();
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                if (!string.IsNullOrEmpty(PhoneNumber))
                    _param.Add(new SqlParameter("@Phone_Number", PhoneNumber));
                if (!string.IsNullOrEmpty(FromDate))
                {
                    _param.Add(new SqlParameter("@CallDateFrom", FromDate));
                    _param.Add(new SqlParameter("@CallDateTo", ToDate));
                }
                if (!string.IsNullOrEmpty(Outcome))
                    _param.Add(new SqlParameter("@Outcome", Outcome));

                return _SqlDbBridge.ExecuteDataSet("proc_getCallHistory", _param).Tables[0].AsEnumerable().Select(
                    ch => new CallHistory
                    {

                        CrmID = (long)ch["CrmID"]
                        ,
                        CSEName = Convert.ToString(ch["AgentName"])
                        ,
                        customer = new Customer { SourceID = Convert.ToInt64(ch["SourceID"]), CustomerName = Convert.ToString(ch["CustomerName"]), MobileNo = Convert.ToString(ch["Phone_Number"]), SourceRef = Convert.ToString(ch["SourceRef"]) }
                        ,
                        subOutcomeMaster = new SubOutcomeMaster { SubOutcome = Convert.ToString(ch["SubOutcome"]), outcomeMaster = new OutcomeMaster { Outcome = Convert.ToString(ch["Outcome"]) } }
                        ,
                        Remarks = Convert.ToString(ch["Remarks"])
                        ,
                        CallDateTime = Convert.ToDateTime(ch["CallStartDateTime"])
                        ,
                        CallEndDateTime = Convert.ToDateTime(ch["CallEndDateTime"])
                    }
                    ).ToList();


            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            return ch1;
        }

        public DataSet getCallHistoryV2(string PhoneNumber, string FromDate = "", string ToDate = "", string Outcome = "", string ReportType = "", string CallID = "")
        {
            var dt = new DataSet();
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                if (!string.IsNullOrEmpty(PhoneNumber))
                    _param.Add(new SqlParameter("@Phone_Number", PhoneNumber));
                if (!string.IsNullOrEmpty(FromDate))
                {
                    _param.Add(new SqlParameter("@CallDateFrom", FromDate));
                    _param.Add(new SqlParameter("@CallDateTo", ToDate));
                }
                if (!string.IsNullOrEmpty(Outcome))
                    _param.Add(new SqlParameter("@Outcome", Outcome));

                if (ReportType == "DETAIL")
                {
                    _param.Add(new SqlParameter("@ReportType", ReportType));
                    _param.Add(new SqlParameter("@CallID", string.IsNullOrEmpty(CallID) ? DBNull.Value : (object)CallID));
                    return _SqlDbBridge.ExecuteDataSet("proc_getCallHistory", _param);
                }
                


            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            return dt;
        }


        public TodaysStat getTodaysStat()
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {

                return _SqlDbBridge.ExecuteDataSet("proc_TodaysStat", _param).Tables[0].AsEnumerable().Select(
                    ch => new TodaysStat
                    {
                        CallBackCount = Convert.ToInt32(ch["CallBackCount"]),
                        FreshCallCount = Convert.ToInt32(ch["FreshCallCount"])
                    }
                    ).Single();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public DataTable getAgentWiseCallStat(string FromDate = "", string ToDate = "", string UserId = "")
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(FromDate))
            {
                _param.Add(new SqlParameter("@DateFrom", FromDate));
                _param.Add(new SqlParameter("@DateTo", ToDate));
            }
            if (!string.IsNullOrEmpty(UserId))
                _param.Add(new SqlParameter("@UserId", UserId));
            _param.Add(new SqlParameter("@RptParam", "AgentWiseCallStat"));
            try
            {
                return _SqlDbBridge.ExecuteDataSet("proc_TodaysStat", _param).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public List<CallHistory> getCallBackData()
        {
            var ch1 = new List<CallHistory>();
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@RequiredData", "CallBack"));
                    return _SqlDbBridge.ExecuteDataSet("proc_TodaysStatDetail", _param).Tables[0].AsEnumerable().Select(
                    ch => new CallHistory
                    {

                        CrmID = (long)ch["CrmID"]
                        ,
                        CSEName = Convert.ToString(ch["AgentName"])
                        ,
                        customer = new Customer { SourceID = Convert.ToInt64(ch["SourceID"]), CustomerName = Convert.ToString(ch["CustomerName"]), MobileNo = Convert.ToString(ch["Phone_Number"]), SourceRef = Convert.ToString(ch["SourceRef"]) }
                        ,
                        CallID = (long)ch["CallID"]
                        ,
                        subOutcomeMaster = new SubOutcomeMaster { SubOutcome = Convert.ToString(ch["SubOutcome"]), outcomeMaster = new OutcomeMaster { Outcome = Convert.ToString(ch["Outcome"]) } }
                        ,
                        Remarks = Convert.ToString(ch["Remarks"])
                        ,
                        CallDateTime = Convert.ToDateTime(ch["CallStartDateTime"])
                        ,
                        CallEndDateTime = Convert.ToDateTime(ch["CallEndDateTime"])
                        ,
                        CallBackDateTime = Convert.ToDateTime(ch["CallBackDateTime"])
                        ,
                        CallbackType = Convert.ToString(ch["CallbackType"])
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            return ch1;
        }

        public List<SourceMaster> getUploadHistory( string FromDate = "", string ToDate = "")
        {
            var ch1 = new List<SourceMaster>();
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                
                if (!string.IsNullOrEmpty(FromDate))
                {
                    _param.Add(new SqlParameter("@DateFrom", FromDate));
                    _param.Add(new SqlParameter("@DateTo", ToDate));
                }
                return _SqlDbBridge.ExecuteDataSet("proc_getUploadHistory", _param).Tables[0].AsEnumerable().Select(
                    ch => new SourceMaster
                    {
                        SourceID = Convert.ToString(ch["SourceID"]),
                        SourceFileName = Convert.ToString(ch["SourceFileName"]),
                        ServiceID = Convert.ToString(ch["ServiceID"]),
                        TotalNoRecords = Convert.ToString(ch["TotalNoRecords"]),
                        TotalNoRecordsUploaded = Convert.ToString(ch["TotalNoRecordsUploaded"]),
                        TotalNoIncorrectRecords = Convert.ToString(ch["TotalNoIncorrectRecords"]),
                        FileUploadStartDateTime = Convert.ToString(ch["FileUploadStartDateTime"]),
                        FileUploadEndDateTime = Convert.ToString(ch["FileUploadEndDateTime"]),
                        FileUploadDateTime = Convert.ToString(ch["FileUploadDateTime"]),
                        FileUploadedBy = Convert.ToString(ch["FileUploadedBy"]),
                        HostName = Convert.ToString(ch["HostName"])
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
            return ch1;
        }

        public List<ServiceMaster> getServiceMaster()
        {
            var sm = new List<ServiceMaster>();
            try
            {
                return _SqlDbBridge.ExecuteDataSet("proc_FillDropDown", new List<SqlParameter>() { new SqlParameter("param0", "ServiceMaster") }).Tables[0].AsEnumerable().Select(t => new ServiceMaster { ServiceID = Convert.ToInt32(t["ServiceID"]), ServiceName = Convert.ToString(t["ServiceName"]) }).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

            return sm;
            
        }

        public List<DataSourceProfileMaster> getProfileMaster()
        {
            var lst = new List<DataSourceProfileMaster>();
            try
            {
                return _SqlDbBridge.ExecuteDataSet("proc_FillDropDown", new List<SqlParameter>() { new SqlParameter("param0", "ProfileMaster") }).Tables[0].AsEnumerable().Select(t => new DataSourceProfileMaster { ProfileID = Convert.ToInt32(t["ProfileID"]), ProfileName = Convert.ToString(t["ProfileName"]) }).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

            return lst;

        }

        public List<CampaignMapping> getCampaignMapping()
        {
            var lst = new List<CampaignMapping>();
            try
            {
                return _SqlDbBridge.ExecuteDataSet("proc_CampaignAllocation", new List<SqlParameter>() { new SqlParameter("param0", "SELECT") }).Tables[0].AsEnumerable().Select(t =>
                    new CampaignMapping
                    {
                        AllocationID=Convert.ToInt64(t["AllocationID"]),
                        user = new User { UserId = Convert.ToInt32(t["UserID"]), UserName = Convert.ToString(t["UserName"]), FullName = Convert.ToString(t["FullName"]) },
                        serviceMaster = new ServiceMaster { ServiceID = Convert.ToInt32(t["ServiceID"]), ServiceName = Convert.ToString(t["ServiceName"]) },
                        dataSourceProfileMaster = new DataSourceProfileMaster { ProfileID=Convert.ToInt32(t["ProfileID"]),ProfileName=Convert.ToString(t["ProfileName"])}
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

            return lst;

        }

        public DataSet UpdateCampaignMapping(string UserID, string ServiceID, string ProfileID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            _param.Add(new SqlParameter("@param0", "UPDATE"));
            _param.Add(new SqlParameter("@UserId", UserID));
            _param.Add(new SqlParameter("@ServiceID", ServiceID));
            _param.Add(new SqlParameter("@ProfileID", ProfileID));
            _param.Add(new SqlParameter("@CreatedBy", ""));
            _param.Add(new SqlParameter("@HostName", ""));
            try
            {
                return _SqlDbBridge.ExecuteDataSet("proc_CampaignAllocation", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

            return new DataSet();

        }
         
    }
}
