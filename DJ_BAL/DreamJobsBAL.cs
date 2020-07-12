using DJ_DAL;
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

        private DreamJobsDAL _DreamJobsDAL;
        public DreamJobsBAL()
        {
            _DreamJobsDAL = new DreamJobsDAL();
        }
        public DataSet fillDropDown(string param0, string param1 = "", string param2 = "")
        {
            return _DreamJobsDAL.fillDropDown(param0, param1, param2);

        }

        public ApplicantExamVM GetQuestionSetForApplicant(ApplicantExamVM ApplicantExamVM)
        {
            DataSet ds = _DreamJobsDAL.GetQuestionSetForApplicant(ApplicantExamVM);
            if (ds.Tables.Count > 0)
            {
                ApplicantExamVM _ApplicantExamVM = new DJ_Entity.ApplicantExamVM();

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

                    }
                    ,
                    AnswerByApplicant = d["AnswerByApplicant"] == DBNull.Value ? "" : Convert.ToString(d["AnswerByApplicant"])
                }).ToList();



                return _ApplicantExamVM;

            }
            else
            {
                return new ApplicantExamVM { ApplicantAnswers = new List<ApplicantAnswer> { new ApplicantAnswer { Question = new QuestionSet { QuestionID = 0, Question = "No Question  available for you this time" } } } };
            }
            return new ApplicantExamVM();

        }

        public DataSet SaveRegistration(Registration reg)
        {
            return _DreamJobsDAL.SaveRegistration(reg);
        }

        public DataSet SubmitPaper(string strXml)
        {
            return _DreamJobsDAL.SubmitPaper(strXml);
        }
    }
}
