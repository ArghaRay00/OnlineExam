using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTestEntities;
using OnlineTestBL;
using OnlineTestApp.models;
using OnlineTestApp.Models;

namespace OnlineTestApp.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Login Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoginView()
        {
            LoginDetails model = new LoginDetails();
            var pManager = new RolesManager();
            List<Roles> results = pManager.GetRoles().ToList();
            List<RolesList> RolesList = new List<RolesList>();
            foreach (Roles role in results)
            {
                RolesList Roles = new RolesList
                {
                    RoleID = role.RolesID,
                    Roles = role.RolesName,
                };
                RolesList.Add(Roles);

            }

            ViewBag.Roles = RolesList;
            return View("LoginView", model);
        }

        [HttpPost]
        public ActionResult LoginView(LoginDetails models)
        {

            LoginDetails logininfo = new LoginDetails();
            logininfo.Roles = new Roles();
            logininfo.UserName = models.UserName;
            logininfo.UserPassword = models.UserPassword;
            logininfo.Roles.RolesName = models.Roles.RolesName;
            logininfo.Roles.RolesID = models.Roles.RolesID;
            var pManager = new RolesManager();
            List<Roles> results = pManager.GetRoles().ToList();
            List<RolesList> RolesList = new List<RolesList>();
            foreach (Roles role in results)
            {
                RolesList Roles = new RolesList
                {
                    RoleID = role.RolesID,
                    Roles = role.RolesName,
                };
                RolesList.Add(Roles);

            }
            ViewBag.Roles = RolesList;
            if (logininfo.UserName != null && logininfo.UserPassword != null)
            {
                  var LoginParameters = new LoginManager();
                  var result = LoginParameters.Getlogindetails().ToList();

                if (logininfo.UserName != "")
                {
                    var usrcheck = result.Where(x => x.UserName == logininfo.UserName).FirstOrDefault().UserName;

                    if (usrcheck != logininfo.UserName)
                    {
                        ViewBag.Error = "Invalid UserName.";
                        return View("LoginView");
                    }
                }
                if (logininfo.UserPassword != "")
                {
                    var passcheck =  result.Where(x => x.UserPassword == logininfo.UserPassword).FirstOrDefault().UserPassword;
                    if (passcheck == null)
                    {
                        ViewBag.Error = "Invalid Password.";
                        return View("LoginView");
                    }
                }
                if (logininfo.Roles.RolesName == null)
                {
                    ViewBag.Error = "Please Select at least one Role.";
                    return View("LoginView");
                }
            }
            else {
                ViewBag.Error = "Invalid UserName or Password.";
                return View("LoginView");
            }
            if (logininfo.Roles.RolesName == "HR")
            {
                return RedirectToAction("", "");
            }

            if (logininfo.Roles.RolesName == "TechnicalPanelist")
            {
                return RedirectToAction("", "");
            }
            else {
                return RedirectToAction("", "");
            }

        }

	}
}