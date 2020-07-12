using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DreamJob.WEB.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Index()
        {

            ViewBag.Jobs = new SelectList(new List<DJ_Entity.Jobs> { new DJ_Entity.Jobs { JobID = 1, JobTitle = "Air Asia" }, new DJ_Entity.Jobs { JobID = 2, JobTitle = "Metro" }, new DJ_Entity.Jobs { JobID = 3, JobTitle = "Railway" }, new DJ_Entity.Jobs { JobID = 4, JobTitle = "Patanjali" } }, "JobID", "JobTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DJ_Entity.Registration reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DJ_BAL.DreamJobsBAL BAL = new DJ_BAL.DreamJobsBAL();
                    reg.Password = CRM.WEB.Infrastructure.CustomMembershipProvider.GetMd5Hash(reg.Password);
                    DataSet ds = BAL.SaveRegistration(reg);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["SaveStatus"].ToString() == "0")
                        {
                            ViewBag.ErrorMessage = ds.Tables[0].Rows[0]["SaveMessage"].ToString();// "You're already registered!";
                            ModelState.AddModelError("", ds.Tables[0].Rows[0]["SaveMessage"].ToString());
                        }
                        else
                        {
                            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SendSms"]))
                            {
                                string SmsBody = string.Empty;
                                SmsBody = "Hi " + reg._Applicant.ApplicantName.Trim() + ", you are registered successfully. Use your mobile number as Username to login http://dreamjobsnotification.com/";

                                string response = SendSMS(reg._Applicant.MobileNo, SmsBody);
                            }
                            return View("RegistrationSuccess", reg);
                        }
                    }
                    //return View("RegistrationSuccess", ViewBag.ErrorMessage);
                    // TODO: Add insert logic here
                }

                ViewBag.Jobs = new SelectList(new List<DJ_Entity.Jobs> { new DJ_Entity.Jobs { JobID = 1, JobTitle = "Air Asia" }, new DJ_Entity.Jobs { JobID = 2, JobTitle = "Metro" }, new DJ_Entity.Jobs { JobID = 3, JobTitle = "Railway" }, new DJ_Entity.Jobs { JobID = 4, JobTitle = "Patanjali" } }, "JobID", "JobTitle", reg.AppliedJob == null ? null : (object)reg.AppliedJob.JobID);
                return View(reg);
            }
            catch
            {
                return View(reg);
            }
        }

        public string SendSMS(string _mobile, string _message) //Prashant(2017-09-28)
        {
            //objActivityLogBal.LogSentEmailSmsLog(new Utility.EssActivityLog { ModuleName = "ESS Forget Password", SendingType = "SMS", ContactNo = _mobile, Response = "Calling Method", RemmainingSmsBal = "0", CreatedBy = EssSession.EmpId, SmsText = "Test Calling Method", TextCount = 0, ToalUnits = 1 });
            string strResponse = string.Empty;



            //Your user name
            string user = System.Configuration.ConfigurationManager.AppSettings["user"];
            //Your authentication key
            string key = System.Configuration.ConfigurationManager.AppSettings["key"];
            //Multiple mobiles numbers separated by comma
            //string mobile = "+91" + _mobile.Trim();
            string mobile =  _mobile.Trim();
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderid = System.Configuration.ConfigurationManager.AppSettings["senderid"];
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(_message.Trim());

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("username={0}", user);
            sbPostData.AppendFormat("&password={0}", key);
            sbPostData.AppendFormat("&number={0}", mobile);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&senderid={0}", senderid);
            sbPostData.AppendFormat("&route={0}", System.Configuration.ConfigurationManager.AppSettings["routeid"]);

            try
            {

                string proxyPath = System.Configuration.ConfigurationManager.AppSettings["proxyPath"];
                string proxy_userid = System.Configuration.ConfigurationManager.AppSettings["proxy_userid"];
                string proxy_password = System.Configuration.ConfigurationManager.AppSettings["proxy_password"];
                string proxy_domain = System.Configuration.ConfigurationManager.AppSettings["proxy_domain"];




                //Call Send SMS API
                string sendSMSUri = System.Configuration.ConfigurationManager.AppSettings["sendSMSUri"];
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);

                if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UseProxy"]))
                {
                    //pass proxy credential
                    WebProxy wp = new WebProxy(proxyPath, true);
                    wp.BypassProxyOnLocal = true;
                    wp.Credentials = new NetworkCredential(proxy_userid, proxy_password, proxy_domain);
                    httpWReq.Proxy = wp; //Prashant
                }

                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();
                //Response.Write(responseString);
                //Response.Write("  Status Code : ");
                //Response.Write(response.StatusCode.ToString());

                strResponse = responseString;// response.StatusCode.ToString()-> OK (for success);


                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                //Response.Write("<font color='red'>" + ex.Message.ToString() + "</font>");
                strResponse = ex.Message;
            }

            return strResponse;
        }


    }
}
