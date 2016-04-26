using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OnlineTestApp.Models;
using OnlineTestBL;
using OnlineTestEntities;


namespace OnlineTestApp.Controllers
{
    public class EmployeeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: Employee
        public ActionResult EmployeeRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegistration(Employee Employee)
        {
            var empManager = new EmployeeManager();
            empManager.CreateEmployee(Employee);
            return View("Index");

        }
        public ActionResult HrRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HrRegistration(Hr Hr)
        {
            var hrManager = new HrManager();
            hrManager.CreateHr(Hr);
            return View("Index");

        }
    }

}