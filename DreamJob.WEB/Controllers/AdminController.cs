using DJ_BAL;
using DJ_Entity;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamJob.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisteredApplicants()
        {
            DreamJobsBAL DJBAL = new DreamJobsBAL();
            List<Registration> lstReg = DJBAL.GetRegistrations("", "");
            return View(lstReg);
        }

        [HttpPost]
        public ActionResult RegisteredApplicants(string FromDate, string ToDate)
        {
            DreamJobsBAL DJBAL = new DreamJobsBAL();
            List<Registration> lstReg = DJBAL.GetRegistrations(FromDate, ToDate);
            return View(lstReg);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload, FormCollection frm)
        {
            string JobID;
            JobID = frm["CampaignType"];
            //ProfileID = frm["ProfileType"];
            //return View();
            //http://techbrij.com/read-excel-xls-xlsx-asp-net-mvc-upload
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(JobID) || JobID == "0")
                {
                    ModelState.AddModelError("File", "Please select Job Type");
                }
                else
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                        // to get started. This is how we avoid dependencies on ACE or Interop:
                        Stream stream = upload.InputStream;

                        // We return the interface, so that
                        IExcelDataReader reader = null;


                        if (upload.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            return View();
                        }

                        reader.IsFirstRowAsColumnNames = true;

                        DataSet result = reader.AsDataSet();
                        reader.Close();

                        DataSet ds = new DataSet();
                        ds.Tables.Add(result.Tables[0].Copy());
                        ds.Tables[0].TableName = "QSet";
                        string xmlCustomers = ds.GetXml();

                        DreamJobsBAL DJBAL = new DreamJobsBAL();
                        ViewBag.Data = result.Tables[0].Copy();
                        ds = new DataSet();
                        ds = DJBAL.uploadExccel(upload.FileName, xmlCustomers, User.Identity.Name, Convert.ToInt32(JobID));

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                ViewBag.Message = ds.Tables[0].Rows[0][1].ToString();
                            }
                        }

                        //return View(result.Tables[0]);
                        return View(ViewBag.Data);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                }
            }
            return View();
        }

        public ActionResult GetJobType()
        {
            DreamJobsBAL DJBAL = new DreamJobsBAL();
            List<Jobs> lstSM = DJBAL.fillDropDown("Jobs", "1", "").Tables[0].AsEnumerable().Select(d => new Jobs { JobID = (int)d["JobID"], JobTitle =Convert.ToString( d["JobTitle"]) }).ToList();
            string ddlHtml = "<option value='0'>-Select Job Type-</option>";
            ddlHtml += string.Join("", lstSM.Select(t => string.Format("<option value='{0}'>{1}</option>", t.JobID, t.JobTitle)));
            return Json(ddlHtml, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public FileResult DownloadFormat()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Templates/QuestionSet.xlsx"));
            string fileName = "QuestionSet.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
