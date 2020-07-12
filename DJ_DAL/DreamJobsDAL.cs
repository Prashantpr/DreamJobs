using CRM.DbBridge;
using DJ_Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_DAL
{
    public partial class DreamJobsDAL
    {
        private SqlDbBridge _SqlDbBridge;
        public DreamJobsDAL()
        {
            _SqlDbBridge = new SqlDbBridge();
        }

        public DataSet fillDropDown(string param0, string param1 = "", string param2 = "")
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

        public DataSet GetQuestionSetForApplicant(ApplicantExamVM ApplicantExamVM)
        {
            
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@ApplicantID", ApplicantExamVM.ApplicantAttempt.Applicant.ApplicantID));
                _param.Add(new SqlParameter("@AttemptID", (object)ApplicantExamVM.ApplicantAttempt.AttemptID ?? DBNull.Value));
                return _SqlDbBridge.ExecuteDataSet("usp_GetQuestionSetForApplicant", _param);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }

        public DataSet SaveRegistration(Registration reg)
        {
            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@ApplicantName", (object)reg._Applicant.ApplicantName));
                _param.Add(new SqlParameter("@FatherName", (object)reg._Applicant.FatherName ?? DBNull.Value));
                _param.Add(new SqlParameter("@DOB", (object)reg._Applicant.DOB ?? DBNull.Value));
                _param.Add(new SqlParameter("@MobileNo", (object)reg._Applicant.MobileNo ?? DBNull.Value));
                _param.Add(new SqlParameter("@AltContactNo", (object)reg._Applicant.AltContactNo ?? DBNull.Value));
                _param.Add(new SqlParameter("@EmailID", (object)reg._Applicant.EmailID ?? DBNull.Value));
                _param.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                _param.Add(new SqlParameter("@CreatedBy", (object)reg._Applicant.MobileNo ?? DBNull.Value));
                _param.Add(new SqlParameter("@Password", (object)reg.Password ?? DBNull.Value));
                _param.Add(new SqlParameter("@IpAddress", DBNull.Value));
                _param.Add(new SqlParameter("@JobID", reg.AppliedJob.JobID));
                return _SqlDbBridge.ExecuteDataSet("usp_SaveRegistration", _param);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }


        public DataSet SubmitPaper(string strXml)
        {

            List<SqlParameter> _param = new List<SqlParameter>();
            try
            {
                _param.Add(new SqlParameter("@xmlData", strXml));
                return _SqlDbBridge.ExecuteDataSet("usp_SubmitPaper", _param);
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
        }
    }
}
