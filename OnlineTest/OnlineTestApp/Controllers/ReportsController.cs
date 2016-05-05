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

            var students = exam.Students.ToList().OrderByDescending(x => x.Marks.Markss);

            return View("_ResultReportsView", students);
        }
        public ActionResult StudentsReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentsReport(StudentsReport model)
        {

            var sManager = new StudentManager();
            var student = sManager.GetAllStudentsByCollge(model.CollegeName);

            if (student == null)
            {
                return View("Error");
            }

            var ListofStudents = student.Students.ToList().OrderByDescending(z => z.StudentDob);

            return View("_StudentReportsView", ListofStudents);
        }

    }
}