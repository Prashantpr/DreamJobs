using DJ_Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DJ_DAL
{
    public partial class DreamJobsDAL
    {
        //private SqlDbBridge _SqlDbBridge;
        //public DreamJobsDAL()
        //{
        //    _SqlDbBridge = new SqlDbBridge();
        //}

        public DataSet GetRegistrations(string StartDate, string EndDate,string MobileNo)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@StartDate", string.IsNullOrEmpty(StartDate) ? DBNull.Value : (object)StartDate));
                _param.Add(new SqlParameter("@EndDate", string.IsNullOrEmpty(EndDate) ? DBNull.Value : (object)EndDate));
                _param.Add(new SqlParameter("@MobileNo", string.IsNullOrEmpty(MobileNo) ? DBNull.Value : (object)MobileNo));
                return _SqlDbBridge.ExecuteDataSet("usp_GetRegistrations", _param);
            }
            catch (Exception ex)
            {
                return new DataSet();
                throw;
            }
        }

        public DataSet uploadExccel(string SourceFileName, string xmlData, string UploadedBy, int JobID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@SourceFileName", SourceFileName));
                _param.Add(new SqlParameter("@xmlData", xmlData));
                _param.Add(new SqlParameter("@UploadedBy", UploadedBy));
                _param.Add(new SqlParameter("@JobID", JobID));
                return _SqlDbBridge.ExecuteDataSet("proc_ImportExcel", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }


        }

        public DataSet getUploadHistory(int QuestionPaperID, int JobID)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@JobID", JobID==0?DBNull.Value:(object)JobID));
                _param.Add(new SqlParameter("@QuestionPaperID", QuestionPaperID == 0 ? DBNull.Value : (object)QuestionPaperID));
                return _SqlDbBridge.ExecuteDataSet("proc_GetUploadedQuestionSets", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }


        }

        public DataSet GetExamSummary(string JobID, string DateFrom, string DateTo)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@JobID", JobID == "0" ? DBNull.Value : (object)JobID));
                _param.Add(new SqlParameter("@DateFrom", DateFrom == "" ? DBNull.Value : (object)DateFrom));
                _param.Add(new SqlParameter("@DateTo", DateTo == "" ? DBNull.Value : (object)DateTo));
                return _SqlDbBridge.ExecuteDataSet("usp_GetExamSummary", _param);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }


        }


        public DataSet GetExamDetail(ApplicantExamVM ApplicantExamVM)
        {

            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@ApplicantID", ApplicantExamVM.ApplicantAttempt.Applicant.ApplicantID));
                _param.Add(new SqlParameter("@AttemptID", (object)ApplicantExamVM.ApplicantAttempt.AttemptID ?? DBNull.Value));
                return _SqlDbBridge.ExecuteDataSet("usp_GetExamDetail", _param);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }
    }
}
