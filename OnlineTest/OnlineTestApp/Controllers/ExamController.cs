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
        public IEnumerable<object> QuestionSetId { get; private set; }

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
            var studentDTO = sManager.CreateStudent(student);
            Session["StudentId"] = studentDTO.StudentId;

            Session["examCode"] = studentModel.ExamCode; //TODO :  Get the code from the ui

            return RedirectToAction("ExamTaking");
        }

        public ActionResult Exam()
        {
            return View();
        }

        public ActionResult ExamTaking()
        {
            var studentID = (int)Session["studentID"];
            StudentManager sManager = new StudentManager();
            var studentDTO = sManager.GetStudent(studentID);

            //TODO : GEt the exam object from DB
            var examinationCode = (string)Session["ExamCode"];
            ExaminationManager exManager = new ExaminationManager();
            var ExaminationDTO = exManager.GetExaminationbyCode(examinationCode);
            //TODO :  Get the questions for the exams QuestionSet
               
            List<ExamTakingModel> examtakingModels = new List<ExamTakingModel>();

            foreach (var question in ExaminationDTO.Questionset.Questions)
            {
                var examTakingModel = new ExamTakingModel();
                examTakingModel.QuestionId = question.QuestionId;
                examtakingModels.Add(examTakingModel);
            }


            return View(examtakingModels);
        }

        [HttpPost]
        public ActionResult ExamTaking(List<ExamTakingModel> examTakingModels)
        {
            int marks = 0;
            foreach (var examModel in examTakingModels)
            {
                if (examModel.Choice == examModel.Question.AnswerKey) {
                    marks++;
                }
            }

            var marksobject = new Marks();
            marksobject.Markss = marks;

            var examManager = new ExaminationManager();
            int marksid=examManager.CreateAndGetMarksId(marksobject);


            var studentID = (int)Session["StudentId"];
            StudentManager sManager = new StudentManager();

            var examinationCode = (string)Session["ExamCode"];
            ExaminationManager exManager = new ExaminationManager();
            var ExaminationDTO = exManager.GetExaminationbyCode(examinationCode);
            
            sManager.UpdateStudentsMarks(studentID,marksid,ExaminationDTO.ExaminationId);

            //TODO ; create object of Marks and assign the marks

            //TODO: save the marks abd get the ID 

            //TODO : Assign the marks Id to the student Object .Update the Student Object.


            return View("Index");
        }
    }
}