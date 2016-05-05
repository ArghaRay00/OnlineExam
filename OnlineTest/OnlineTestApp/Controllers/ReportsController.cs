using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTestApp.Models;
using OnlineTestBL;

namespace OnlineTestApp.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResultsReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResultsReport(ResultsReportModel model)
        {

            var eManager = new ExaminationManager();
            var exam = eManager.GetExaminationbyCode(model.ExamCode);

            if (exam == null)
            {
                return View("Error");
            }

            
            return View("_ResultReportsView",);
        }
    }
}