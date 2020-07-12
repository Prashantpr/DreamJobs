using CRM.Entity;
using DJ_Entity;
using DJ_Utilities;
using DJ_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ClosedXML.Excel;
using System.Drawing;
using CRM.WEB.Contexts;

namespace DreamJob.WEB.Controllers
{
    public class ReportsController : BaseController
    {
        //
        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report(string q)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DataUploadHistory()
        {
            ViewBag.Jobs = new SelectList(new List<DJ_Entity.Jobs> { new DJ_Entity.Jobs { JobID = 1, JobTitle = "Air Asia" }, new DJ_Entity.Jobs { JobID = 2, JobTitle = "Metro" }, new DJ_Entity.Jobs { JobID = 3, JobTitle = "Railway" }, new DJ_Entity.Jobs { JobID = 4, JobTitle = "Patanjali" } }, "JobID", "JobTitle");

            return PartialView("_PartialDataUploadHistoryReport");
        }

        [HttpPost]
        [ActionName("DataUploadHistory")]
        public JsonResult DataUploadHistory(string JobID)
        {
            PartialViewLoader objPartialViewLoader = new PartialViewLoader();
            List<QuestionPaper> lstCallHistory = new DJ_BAL.DreamJobsBAL().GetUploadedQuestionPapers(string.IsNullOrEmpty(JobID) ? 0 : Convert.ToInt32(JobID));
            objPartialViewLoader.strPartialView = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialDataUploadHistoryReportData", lstCallHistory);

            return Json(objPartialViewLoader, JsonRequestBehavior.AllowGet);
            //return PartialView("_PartialCallHistoryReport");
        }

        public ActionResult GetUploadedData(string SourceID)
        {
            ExcelPackage objExcelPackage = new ExcelPackage();

            DataSet p_dsSrc = new DataSet();


            //p_dsSrc = new CustomerBLL().getUploadedData(SourceID, User.Identity.Name);


            //DataTable dt = new DataTable("MyDATA");
            //dt.Columns.Add("ID");
            //dt.Columns.Add("Name");
            //DataRow drow = dt.NewRow();
            //drow["ID"] = 1;
            //drow["Name"] = "Prashant";
            //dt.Rows.Add(drow);
            //p_dsSrc.Tables.Add(dt);

            foreach (DataTable dtSrc in p_dsSrc.Tables)
            {
                //Create the worksheet    
                ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add(dtSrc.TableName);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                objWorksheet.Cells["A1"].LoadFromDataTable(dtSrc, true);
                objWorksheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 10));
                objWorksheet.Cells.AutoFitColumns();
                //Format the header    
                //using (ExcelRange objRange = objWorksheet.Cells["A1:XFD1"])
                //{
                //    objRange.Style.Font.Bold = true;
                //    objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //    objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //    objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //    objRange.Style.Fill.BackgroundColor.SetColor(Color.FromName("#eaeaea"));
                //}
            }


            string handle = Guid.NewGuid().ToString();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                objExcelPackage.SaveAs(memoryStream);
                memoryStream.Position = 0;
                TempData[handle] = memoryStream.ToArray();
            }

            // Note we are returning a filename as well as the handle
            //return new JsonResult()
            //{
            //    Data = new { FileGuid = handle, FileName = "TestReportOutput.xlsx" }
            //};
            FileResponse objFileResponse = new FileResponse() { FileGuid = handle, FileName = string.Format("UploadedData_SourceID_{0}.xlsx", SourceID) };
            return Json(objFileResponse, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ExamSummary()
        {
            ViewBag.Jobs = new SelectList(new List<DJ_Entity.Jobs> { new DJ_Entity.Jobs { JobID = 1, JobTitle = "Air Asia" }, new DJ_Entity.Jobs { JobID = 2, JobTitle = "Metro" }, new DJ_Entity.Jobs { JobID = 3, JobTitle = "Railway" }, new DJ_Entity.Jobs { JobID = 4, JobTitle = "Patanjali" } }, "JobID", "JobTitle");

            return PartialView("_PartialExamSummary");
        }

        [HttpPost]
        [ActionName("ExamSummary")]
        public JsonResult ExamSummary(string JobID,string DateFrom , string DateTo)
        {
            PartialViewLoader objPartialViewLoader = new PartialViewLoader();
            DataSet ds = new DJ_BAL.DreamJobsBAL().GetExamSummary(string.IsNullOrEmpty(JobID) ? "0" : JobID,DateFrom,DateTo );
            if (ds.Tables.Count > 0)
            {
                objPartialViewLoader.strPartialView = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialExamSummaryData", ds.Tables[0]);
            }
            return Json(objPartialViewLoader, JsonRequestBehavior.AllowGet);
            //return PartialView("_PartialCallHistoryReport");
        }

        //[HttpPost]
        //[ActionName("GetExamDetail")]
        //public JsonResult GetExamDetail(string ApplicantID,string AttemptID)
        //{
        //    PartialViewLoader objPartialViewLoader = new PartialViewLoader();
        //    DataSet ds = new DJ_BAL.DreamJobsBAL().GetExamDetail(ApplicantID, AttemptID);
        //    if (ds.Tables.Count > 0)
        //    {
        //        objPartialViewLoader.strPartialView = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialExamSummaryData", ds.Tables[0]);
        //    }
        //    return Json(objPartialViewLoader, JsonRequestBehavior.AllowGet);
        //    //return PartialView("_PartialCallHistoryReport");
        //}

        [HttpPost]
        [ActionName("GetExamDetail")]
        public JsonResult GetExamDetail(string ApplicantID, string AttemptID)
        {
            //System.Threading.Thread.Sleep(5000);
            DataTable dtResult;
            //DreamJobsBAL DJBAL = new DreamJobsBAL();
            ApplicantExamVM _ApplicantExamVM = new DJ_BAL.DreamJobsBAL().GetExamDetail(new ApplicantExamVM { ApplicantAttempt = new ApplicantExamAttempt { AttemptID = Convert.ToInt32(AttemptID), Applicant = new Applicant { ApplicantID = Convert.ToInt32(ApplicantID) } } },out dtResult );
            string strPartialView = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialExamDetail", _ApplicantExamVM);
            string strResultSummary = RenderPartialToStringExtensions.RenderPartialToString(this.ControllerContext, "_PartialExamResultSummary", dtResult );
            if (_ApplicantExamVM.ApplicantAttempt == null)
            {
                return Json(new JsonReurnData { ReturnStatus = 0, ReturnMessage = "Detail could not fetched!", ReturnData = strPartialView }, JsonRequestBehavior.AllowGet);

            }
            return Json(new JsonReurnData { ReturnStatus = 1, ReturnMessage = "", ReturnData = strPartialView, ReturnData2 = strResultSummary }, JsonRequestBehavior.AllowGet);
        }
        public class FileResponse
        {
            public string FileGuid { get; set; }
            public string FileName { get; set; }
        }
    }
}
