using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTestApp.Models;
using OnlineTestBL;

using System.Web.Script.Serialization;
using System.Web.Security;
using Newtonsoft.Json;

namespace OnlineTestApp.Controllers
{
    public class LoginBaseController : Controller
    {
        
        /// <summary>
        /// Login Get Method
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginView()
        {
            return View();
        }

        /// <summary>
        /// Login Post Method
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginView(LoginViewModel model, string returnUrl = "")
        {
            try
            {
                LoginManager loginDetails = new LoginManager();
                if (ModelState.IsValid)
                {
                    var Details = loginDetails.GetloginUsers();
                    var user = Details.Where(u => u.Username.ToLower() == model.Username.ToLower() && u.Password.ToLower() == model.Password.ToLower()).FirstOrDefault();
                    if (user != null)
                    {
                        var roles = user.Roles.Select(m => m.RoleName).ToArray();

                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                        serializeModel.UserId = user.UserId;
                        serializeModel.FirstName = user.FirstName;
                        serializeModel.LastName = user.LastName;
                        serializeModel.roles = roles;

                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                 1,
                                user.Email,
                                 DateTime.Now,
                                 DateTime.Now.AddMinutes(15),
                                 false,
                                 userData);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);

                        if (roles.Contains("HR"))
                        {
                            return RedirectToAction("RegisterExamination", "Admin");
                        }
                        else if (roles.Contains("Technical panelist"))
                        {
                            return RedirectToAction("AssignPanelToExam", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Registration", "Exam");
                        }
                    }

                    ViewBag.error = "Incorrect username or password";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginView", "Login", null);
        }
	}
}