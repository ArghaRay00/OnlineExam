using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestApp.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int AnswerKey { get; set; }
        public int? TechnicalQuestionSetID { get; set; }
        public int? QuantitativeQuestionSetID { get; set; }
        public int? QuestionSetId { get; set; }
    }
}