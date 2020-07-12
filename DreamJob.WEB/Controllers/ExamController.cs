using CRM.WEB.Contexts;
using DJ_BAL;
using DJ_Entity;
using DJ_Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamJob.WEB.Controllers
{
    [Authorize(Roles = "Home")]
    public class ExamController : BaseController
    {
        //
        // GET: /Exam/

        public ActionResult Index()
        {
            return RedirectToAction("Instruction");
        }

        public ActionResult Instruction()
        {
            return View();
        }

        public ActionResult QuestionPaper()
        {
            Applicant user;
            using (var usersContext = new UsersContext())
            {
                user = usersContext.GetApplicant(User.Identity.Name);
            }
            DJ_Entity.ApplicantExamVM vm = new ApplicantExamVM { ApplicantAttempt = new ApplicantExamAttempt { Applicant = new Applicant { ApplicantID = user.ApplicantID } } };
            return View(vm);
        }

        public JsonResult GetQuestionSetForApplicant(int? ApplicantID)
        {
            ApplicantExamAttempt d;
            using (var usersContext = new UsersContext())
            {
                d = usersContext.GetApplicantExamAttempts(ApplicantID);
            }
            DreamJobsBAL DJBAL = new DreamJobsBAL();
            ApplicantExamVM _ApplicantExamVM = DJBAL.GetQuestionSetForApplicant(new ApplicantExamVM { ApplicantAttempt = new ApplicantExamAttempt { AttemptID = d == null ? 0 : d.AttemptID, Applicant = new Applicant { ApplicantID = ApplicantID } } });
            string strPartialView = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialQuestions", _ApplicantExamVM);
            if (_ApplicantExamVM.ApplicantAttempt == null)
            {
                return Json(new JsonReurnData { ReturnStatus = 0, ReturnMessage = "No question set available for you at this moment!", ReturnData = strPartialView }, JsonRequestBehavior.AllowGet);
            
            }
            return Json(new JsonReurnData { ReturnStatus = 1, ReturnMessage = "", ReturnData = strPartialView }, JsonRequestBehavior.AllowGet);
        }

        public class ReturnStatus
        {
            public int ErrorStatus { get; set; }
            public string ErrorMessage { get; set; }
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitPaper(FormCollection form, string IsSubmit="1")
        {
            DataTable dtSummary = new DataTable();
            dtSummary.TableName = "ExamSummary";
            dtSummary.Columns.Add("IsSubmit");
            dtSummary.Columns.Add("IpAddress");
            DataRow sDrow = dtSummary.NewRow();
            sDrow["IsSubmit"] = IsSubmit;
            sDrow["IpAddress"] = System.Web.HttpContext.Current.Request.UserHostAddress;
            dtSummary.Rows.Add(sDrow);


            //if (Request.IsAjaxRequest())
            //{
            //    return Json(new ReturnStatus { ErrorStatus = 0, ErrorMessage = "Success" }, JsonRequestBehavior.AllowGet);
            //}

            try
            {


                if (ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "You're already registered!";
                    //return View("RegistrationSuccess", ViewBag.ErrorMessage);

                    // TODO: Add insert logic here

                    int count = form.Keys.Count;
                    List<ApplicantAnswer> kv = new List<ApplicantAnswer>();
                    string[] strKv;
                    foreach (string key in form.Keys)
                    {
                        if (key.StartsWith("_")) continue;

                        strKv = form[key].Split(new char[] { '_' });
                        if (strKv.Length > 1)
                            kv.Add(new ApplicantAnswer {AttemptID=Convert.ToInt64(form["ApplicantAttempt.AttemptID"]),  QuestionID = Convert.ToInt64(strKv[1]) , AnswerByApplicant = strKv[2] });
                    }
                    DataTable dt = new DJ_Utilities.ListtoDataTableConverter().ToDataTable(kv);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "ExamAttempt";
                    ds.Tables.Add(dtSummary);
                    string strXml = ds.GetXml();

                    DreamJobsBAL DJBAL = new DreamJobsBAL();
                    DataSet dsResult = DJBAL.SubmitPaper(strXml);

                    if (Request.IsAjaxRequest())
                    {
                        return Json(new ReturnStatus { ErrorStatus = 0, ErrorMessage = "Answersheet saved successfully as draft" }, JsonRequestBehavior.AllowGet);
                    }
                    return View("Result");
                }

                return View();
            }
            catch
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new ReturnStatus { ErrorStatus = 1, ErrorMessage = "Error in saving draft" }, JsonRequestBehavior.AllowGet);
                }
                return View();
            }
        }

        public ActionResult Result()
        {
            return View();
        }
    }
}
