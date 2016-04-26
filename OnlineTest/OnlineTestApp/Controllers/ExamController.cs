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
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(StudentRegistration studentModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StudentRegistration, Student>());
            var mapper = config.CreateMapper();
            Student student = mapper.Map<Student>(studentModel); //automatically maps student to studentModel.

            StudentManager sManager= new StudentManager();
            sManager.CreateStudent(student);
            return View("Index");
        }
        public ActionResult Exam()
        {
            return View();
        }
    }
}