using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DJ_Entity
{
    public class ApplicantExamVM
    {
        public ApplicantExamAttempt ApplicantAttempt { get; set; }
        public List<ApplicantAnswer> ApplicantAnswers { get; set; }
    }

    

    public class QuestionPaper
    {
        [Key]
        public int QuestionPaperID { get; set; }
        public DJ_Entity.Jobs AppliedJob { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UploadedFilePath { get; set; }

        public List<QuestionSet> Questions { get; set; }
    }

    public class QuestionSet
    {
        public int QNO { get; set; }
        [Key]
        public long QuestionID { get; set; }
        public string Question { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string CorrectAnswer { get; set; }
        public double MarksForCorrect { get; set; }
        public double MarksForInCorrect { get; set; }
        public string AttachmentUrl { get; set; }
        public string SetNumber { get; set; }
        public int QuestionPaperID { get; set; }
        public virtual QuestionPaper QuestionPaper { get; set; }

    }

    public class ApplicantExamAttempt
    {
        [Key]
        public Int64 AttemptID { get; set; }
        public Int64? ApplicantID { get; set; }
        public int? QuestionPaperID { get; set; }
        public virtual DJ_Entity.Applicant Applicant { get; set; }
        public virtual QuestionPaper QuestionPaper { get; set; }
        public DateTime? ExamStartDateTime { get; set; }
        public DateTime? ExamEndDateTime { get; set; }
        public double? MarksObtained { get; set; }
        public string EvaluatedBy { get; set; }
        public string IPAddress { get; set; }
    }

    public class ApplicantAnswer
    {
        [Key]
        public Int64 AnswerSheetID { get; set; }
        public Int64? AttemptID { get; set; }
        public Int64? QuestionID { get; set; }
        public virtual QuestionSet Question { get; set; }
        public string AnswerByApplicant { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }

    /*




      
     */
}