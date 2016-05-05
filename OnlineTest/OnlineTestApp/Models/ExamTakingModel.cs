using OnlineTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTestBL;

namespace OnlineTestApp.Models
{
    public class ExamTakingModel
    {
        public Question Question
        {
            get
            {
                if (QuestionId != 0)
                {
                    var eManager = new QuestionSetManager();

                    return eManager.GetQuestionByID(QuestionId);
                }
                return null;
            }
        }

        public int QuestionId { get; set; }
        public int Choice { get; set; }

        public ExamTakingModel()
        {
            
            var eManager = new QuestionSetManager();

            eManager.GetQuestionByID(QuestionId);
        }
    }
}