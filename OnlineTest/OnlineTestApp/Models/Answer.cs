using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineTestApp.Models;
using OnlineTestEntities;

namespace OnlineTestApp.models
{

     public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}

    