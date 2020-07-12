using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Entity;
using CRM.WEB.Contexts;
using DJ_BAL;
using DJ_Entity;

namespace DreamJob.WEB.Controllers
{
    [Authorize(Roles = "Home")]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppliedJob()
        {
            DreamJobsBAL DJBAL = new DreamJobsBAL();
            List<Registration> lstReg = DJBAL.GetRegistrations("","",User.Identity.Name);
            return View(lstReg);
        }

        

        
    }
}
