using DJ_Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_BAL
{
    public partial class DreamJobsBAL
    {
        public List<Registration> GetRegistrations(string StartDate, string EndDate, string MobileNo="")
        {
            DataSet ds = _DreamJobsDAL.GetRegistrations(StartDate, EndDate, MobileNo);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].AsEnumerable().Select(d => new Registration
                {
                    RegistrationID = (long)d["RegistrationID"],
                    RegistrationDate = Convert.ToDateTime(d["RegistrationDate"]),
                    _Applicant = new Applicant { ApplicantID=(long)d["ApplicantID"], ApplicantName=d["ApplicantName"].ToString()
                    ,AltContactNo=d["AltContactNo"].ToString(),DOB=Convert.ToDateTime( d["DOB"]),EmailID=d["EmailID"].ToString()
                    ,FatherName=d["FatherName"].ToString(),MobileNo=d["MobileNo"].ToString()},
                    AppliedJob = new Jobs { JobID = (int)d["JobID"], JobTitle = d["JobTitle"].ToString(), JobDescription = d["JobDescription"].ToString() }
                }).ToList();
            }
            return new List<Registration>();
        }


        public DataSet uploadExccel(string SourceFileName, string xmlData, string UploadedBy, int JobID)
        {
            return _DreamJobsDAL.uploadExccel(SourceFileName, xmlData, UploadedBy, JobID);
        }

        public List<QuestionPaper> GetUploadedQuestionPapers(int JobID = 0)
        {
            DataSet ds = _DreamJobsDAL.getUploadHistory(0, JobID);
            List<QuestionPaper> lstQ = new List<QuestionPaper>();
            return ds.Tables[0].AsEnumerable().Select(d => new QuestionPaper
            {
                QuestionPaperID = Convert.ToInt32(d["QuestionPaperID"])
                ,
                AppliedJob=new DJ_Entity.Jobs { JobID=Convert.ToInt32(d["JobID"]),JobTitle=Convert.ToString(d["JobTitle"]) }
                ,
                CreateDateTime = Convert.ToDateTime(d["CreateDateTime"])
                ,
                CreatedBy = Convert.ToString(d["CreatedBy"])
                ,
                UploadedFilePath = Convert.ToString(d["UploadedFilePath"])
            }).ToList();
            return lstQ;
        }

        public DataSet GetExamSummary(string JobID, string DateFrom, string DateTo)
        {
            return _DreamJobsDAL.GetExamSummary(JobID,DateFrom,DateTo);
        }

        

        public ApplicantExamVM GetExamDetail(ApplicantExamVM ApplicantExamVM, out DataTable  dtResult)
        {
            DataSet ds = _DreamJobsDAL.GetExamDetail(ApplicantExamVM);
            if (ds.Tables.Count > 0)
            {
                ApplicantExamVM _ApplicantExamVM = new DJ_Entity.ApplicantExamVM();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    _ApplicantExamVM.ApplicantAttempt = new ApplicantExamAttempt
                    {
                        AttemptID = (long)ds.Tables[0].Rows[0]["AttemptID"]
                        ,
                        Applicant = new Applicant { ApplicantID = (long)ds.Tables[0].Rows[0]["ApplicantID"] }
                        ,
                        ExamStartDateTime = ds.Tables[0].Rows[0]["ExamStartDateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[0]["ExamStartDateTime"]),
                        ExamEndDateTime = ds.Tables[0].Rows[0]["ExamEndDateTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(ds.Tables[0].Rows[0]["ExamEndDateTime"]),
                        MarksObtained = ds.Tables[0].Rows[0]["MarksObtained"] == DBNull.Value ? 0 : Convert.ToDouble(ds.Tables[0].Rows[0]["MarksObtained"])
                    };

                    _ApplicantExamVM.ApplicantAnswers = ds.Tables[0].AsEnumerable().Select(d => new ApplicantAnswer
                    {
                        Question = new QuestionSet
                        {
                            QNO = Convert.ToInt32(d["QNO"])
                            ,
                            QuestionID = Convert.ToInt64(d["QuestionID"])
                            ,
                            Question = Convert.ToString(d["Question"])
                            ,
                            A = d["A"] == DBNull.Value ? "" : Convert.ToString(d["A"])
                            ,
                            B = d["B"] == DBNull.Value ? "" : Convert.ToString(d["B"])
                            ,
                            C = d["C"] == DBNull.Value ? "" : Convert.ToString(d["C"])
                            ,
                            D = d["D"] == DBNull.Value ? "" : Convert.ToString(d["D"])
                            ,
                            E = d["E"] == DBNull.Value ? "" : Convert.ToString(d["E"])
                                //,
                                //MarksForCorrect = Convert.ToDouble(d["MarksForCorrect"]),
                                //MarksForInCorrect = Convert.ToDouble(d["MarksForInCorrect"]),
                                //AttachmentUrl = d["AttachmentUrl"] == DBNull.Value ? "" : Convert.ToString(d["AttachmentUrl"])
                            ,
                            CorrectAnswer = d["CorrectAnswer"] == DBNull.Value ? "" : Convert.ToString(d["CorrectAnswer"])
                        }
                        ,
                        AnswerByApplicant = d["AnswerByApplicant"] == DBNull.Value ? "" : Convert.ToString(d["AnswerByApplicant"])
                    }).ToList();
                }

                if (ds.Tables.Count > 1)
                {
                    dtResult = ds.Tables[1];
                }
                else { dtResult = new DataTable(); }
                return _ApplicantExamVM;

            }
            else
            {
                dtResult = new DataTable();
                return new ApplicantExamVM { ApplicantAnswers = new List<ApplicantAnswer> { new ApplicantAnswer { Question = new QuestionSet { QuestionID = 0, Question = "No Question  available for you this time" } } } };
            }
            return new ApplicantExamVM();

        }

    }
}
