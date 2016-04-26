using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineTestApp.Models;
using OnlineTestEntities;
using OnlineTestBL;

namespace OnlineTestApp.Controllers
{
    public class QuestionsController : Controller
    {
        #region Question

        /// <summary>
        /// Method to list all questions
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            QuestionSetManager List = new QuestionSetManager();
            var questions = List.GetAllQuestions();
            return View(questions.ToList());
        }

        /// <summary>
        /// Method to display the details of a paticular question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Question question = List.GetallQuestion(id);


            return View(question);
        }

        /// <summary>
        /// Method to add a Question
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            QuestionSetManager questions = new QuestionSetManager();
            var List = questions.GetAllQuestionSet();
            
            ViewBag.QuestionSetId = new SelectList(List, "QuestionSetId", "QuestionSetCode");
          
            return View();
        }

        /// <summary>
        /// Method to Save a Question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {

            QuestionSetManager questions = new QuestionSetManager();
            var List = questions.GetAllQuestionSet();

            
            ViewBag.QuestionSetId = new SelectList(List, "QuestionSetId", "QuestionSetCode", question.QuestionSetId);
            
            questions.CreateQuestion(question);
            return RedirectToAction("Index", "Questions");
        }


        /// <summary>
        /// Method to edit a particular question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {

            QuestionSetManager EditQuestion = new QuestionSetManager();
            Question question = EditQuestion.GetallQuestion(id);
            var List = EditQuestion.GetAllQuestionSet();
            
            ViewBag.QuestionSetId = new SelectList(List, "QuestionSetId", "QuestionSetCode");
           
            return View(question);
        }

        /// <summary>
        /// Method to Update a Edited question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            QuestionSetManager EditQuestion = new QuestionSetManager();
            var List = EditQuestion.GetAllQuestionSet();
            EditQuestion.ModifyQuestion(question);
           
            ViewBag.QuestionSetId = new SelectList(List, "QuestionSetId", "QuestionSetCode");
            
            return RedirectToAction("Index", "Questions");
        }

        /// <summary>
        /// Method to delete a particular question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Question question = List.GetallQuestion(id);
            return View(question);
        }


        /// <summary>
        /// Method to delete a question from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Question question = List.GetallQuestion(id);
            List.DeleteQuestion(question);
            return RedirectToAction("Index");
        }

        #endregion

        #region QuestionSet

        /// <summary>
        /// Method to get a Particular Questions of the QuestionSet
        /// </summary>
        /// <param name="questionid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetParticularQuestions(int questionid)
        {
            QuestionSetManager List = new QuestionSetManager();
            var ParticularQuestions = List.GetAllQuestions().Where(x => x.QuestionSetId == questionid).ToList();
            return View(ParticularQuestions);
        }

        /// <summary>
        /// Method to display a List of questionSet
        /// </summary>
        /// <returns></returns>
        public ActionResult ListOfQuestionSet() {
            QuestionSetManager List = new QuestionSetManager();
            var QuestionSetList = List.GetAllQuestionSet();
            return View(QuestionSetList.ToList());
        }

        /// <summary>
        /// Method to add a questionset
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddQuestionSet()
        {
            return View();
        }

        /// <summary>
        /// Method to Save a Questionset
        /// </summary>
        /// <param name="questionset"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddQuestionSet(Questionset questionset)
        {
            QuestionSetManager createquestion = new QuestionSetManager();
            createquestion.CreateQuestionSet(questionset);
            return RedirectToAction("ListOfQuestionSet");
        }

        /// <summary>
        /// method to edit a particular questionset
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditQuestionSet(int id)
        {
            QuestionSetManager EditQuestion = new QuestionSetManager();
            Questionset EditedQuestionSet = EditQuestion.GetparticularQuestionSet(id);
            return View(EditedQuestionSet);

        }

        /// <summary>
        /// method to Update a Particular QuestionSet
        /// </summary>
        /// <param name="EditedQuestion"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditQuestionSet(Questionset EditedQuestion)
        {
            QuestionSetManager EditQuestion = new QuestionSetManager();
            EditQuestion.ModifyQuestionSet(EditedQuestion);
            return RedirectToAction("ListOfQuestionSet");

        }

        /// <summary>
        /// method to display a particular questionset
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DetailQuestionSet(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Questionset question = List.GetparticularQuestionSet(id);


            return View(question);
        }

        /// <summary>
        /// method to delete a particular questionset
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteQuestionSet(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Questionset question = List.GetparticularQuestionSet(id);
            return View(question);
        }

        /// <summary>
        /// method to delete the questionset in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteQuestionSet")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedForQuestionSet(int id)
        {
            QuestionSetManager List = new QuestionSetManager();
            Questionset question = List.GetparticularQuestionSet(id);
            List.DeleteQuestionSet(question);
            return RedirectToAction("ListOfQuestionSet");
        }
        
        #endregion

    }
}

