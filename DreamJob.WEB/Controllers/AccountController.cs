using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CRM.WEB.Contexts;
using CRM.Entity;
using CRM.WEB.Interfaces;
using CRM.WEB.Models;
using CRM.WEB.Services;
using System.Data;

namespace CRM.WEB.Controllers
{
    public class AccountController : DreamJob.WEB.Controllers.BaseController
    {
        //
        // GET: /Account/
        public IMembershipService MembershipService { get; set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/LogOn

        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            //return RedirectToAction("Dashboard", "Home");
            string[] message = new string[2];
            string pass = Infrastructure.CustomMembershipProvider.GetMd5Hash(model.Password);
            if (ModelState.IsValid)
            {
                //if (!model.UserName.Equals("Admin"))
                //{
                //    DataSet ds = new CRM.BLL.AccountBLL().getLoggedInUsers(model.UserName);
                //    if (ds.Tables[0].Select("LoginStatus='1'").Length > 0)
                //    {
                //        message[0] = "User already logged in, please contact your administrator!";
                //        if (Request.IsAjaxRequest())
                //        {
                //            return new JsonResult { Data = message[0], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                //        }
                //        else
                //        {
                //            ViewBag.Message = message;
                //            return View(model);
                //        }
                //    }
                //}
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    DreamJob.WEB.Controllers.DJSession.UserName = model.UserName;
                    message[0] = "Success";

                    new CRM.BLL.AccountBLL().setLoginLogout(model.UserName, "Login");

                    SetupFormsAuthTicket(model.UserName, model.RememberMe);
                    //if (!Roles.IsUserInRole(model.UserName, user.UserType))
                    //Roles.AddUserToRole(model.UserName, user.UserType);
                    string[] role = Roles.GetRolesForUser(model.UserName);
                    if (role.Length > 0)
                        message[1] = role[0];
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", role[0]);
                }
                else
                {
                    message[0] = "Invalid UserName or Password";
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            else
            {
                message[0] = "Invalid UserName or Password";
                ModelState.AddModelError("", "The user name and password are required");
            }



            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data =message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View(model);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Logout(string Username="")
        {
            ReturnStatus objReturnStatus= new CRM.BLL.AccountBLL().setLoginLogout(string.IsNullOrEmpty(Username) ? User.Identity.Name : Username, "Logout");

            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Session.RemoveAll();
            if (System.Web.HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(objReturnStatus, JsonRequestBehavior.AllowGet);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LogOn", "Account");
            }
        }


        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            
            return View();
        }

        // ChangePassword method not implemented in CustomMembershipProvider.cs
        // Feel free to update!

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private User SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            User user;
            using (var usersContext = new UsersContext())
            {
                user = usersContext.GetUser(userName);
            }
            var userId = user.UserId;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                                        userName, // user name
                                                        DateTime.Now,             //creation
                                                        DateTime.Now.AddMinutes(540), //Expiration
                                                        persistanceFlag, //Persistent
                                                        userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            

            return user;
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

    }
}
