using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace DreamJob.WEB.Controllers
{
    public class BaseController : Controller
    {
        private CRM.Entity.User UserModel;

        public BaseController()
        {
            this.UserModel = new CRM.Entity.User();

            if (DJSession.UserName != null)
            {
                using (var usersContext = new CRM.WEB.Contexts.UsersContext())
                {
                    UserModel = usersContext.GetUser(DJSession.UserName);
                }
            }
            ViewBag.UserModel = UserModel;
        }

    }

    public class DJSession
    {
        private static HttpSessionState session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static string UserName
        {
            get
            {
                return session["UserName"] as string;
            }
            set
            {
                session["UserName"] = value;
            }
        }
    }
}
