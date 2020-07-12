using CRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CRM.WEB
{
    public static class MaskMobileNo
    {
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        public static ActiveMenuCss MakeActiveClass(this UrlHelper urlHelper, string controller)
        {
            ActiveMenuCss objActiveMenuCss = new ActiveMenuCss();
            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();
            string ActionName = urlHelper.RequestContext.RouteData.Values["action"].ToString();

            if (!controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                objActiveMenuCss = new ActiveMenuCss { LiClass = "", UlClass = "", UlStyle = "" };
            }
            else
            {
                if (controllerName == "Employee" && ActionName == "DashBoard")
                {
                    return new ActiveMenuCss { LiClass = "", UlClass = "", UlStyle = "" };
                }
                else
                {
                    return new ActiveMenuCss { LiClass = "active", UlClass = "menu-open", UlStyle = "" };
                }
               
            }

            return objActiveMenuCss;
        }

        public static ActiveMenuCss LiMakeActiveClass(this UrlHelper urlHelper, string controller,string method)
        {
            ActiveMenuCss objActiveMenuCss = new ActiveMenuCss();
            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();
            string ActionName = urlHelper.RequestContext.RouteData.Values["action"].ToString();

            if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase) && ActionName.Equals(method, StringComparison.OrdinalIgnoreCase))
            {

                return new ActiveMenuCss { UlStyle = "color:black" };
            }
            else
            {
                objActiveMenuCss = new ActiveMenuCss { LiClass = "", UlClass = "", UlStyle = "" };
            }

            return objActiveMenuCss;
        }

        public static MvcHtmlString MaskEmailAdressEmail(this HtmlHelper helper, string email)
        {
            var displayCase = email;

            var partToBeObfuscated = Regex.Match(displayCase, @"[^@]*").Value;
            if (partToBeObfuscated.Length - 3 > 0)
            {
                var obfuscation = "";
                for (var i = 0; i < partToBeObfuscated.Length - 3; i++) obfuscation += "*";
                displayCase = String.Format("{0}{1}{2}{3}", displayCase[0], displayCase[1], obfuscation, displayCase.Substring(partToBeObfuscated.Length - 1));
            }
            else if (partToBeObfuscated.Length - 2 == 0)
            {
                displayCase = String.Format("{0}*{1}", displayCase[0], displayCase.Substring(2));
            }

            return new MvcHtmlString(displayCase);
        }

        public static MvcHtmlString MaskContactNo(string mobileno, int startIndex, string mask)
        {
            if (string.IsNullOrEmpty(mobileno))
                return new MvcHtmlString(string.Empty);

            string result = mobileno;
            int starLengh = mask.Length;


            if (mobileno.Length >= startIndex)
            {
                result = mobileno.Insert(startIndex, mask);
                if (result.Length >= (startIndex + starLengh * 2))
                    result = result.Remove((startIndex + starLengh), starLengh);
                else
                    result = result.Remove((startIndex + starLengh), result.Length - (startIndex + starLengh));

            }

            return new MvcHtmlString(result);
        }
    }
}